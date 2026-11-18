/**
 * Sample Application interacting with Bruker SPR Remote REST API.
 */

let api = null;
let tools = null;
let pollTimer = null;

const OperationModes = {
    0: "Ready / Idle",
    1: "Paused",
    2: "Completed",
    10: "Standby",
    11: "Reset Required",
    12: "Maintenance Required",
    13: "Door Open",
    20: "Running"
};

document.addEventListener("DOMContentLoaded", () => {
    initApp();
});

function initApp() {
    const apiInput = document.getElementById("apiUrl");
    if (window.location.protocol.startsWith('http') && window.location.port && window.location.port !== '9001') {
        // If served via local dev server (e.g. port 8080 with reverse proxy), default to local proxy endpoint
        apiInput.value = `${window.location.origin}/bruker-spr/json/`;
    }
    const defaultUrl = apiInput.value;
    updateClientInstance(defaultUrl);

    // Event listeners
    document.getElementById("btnConnect").addEventListener("click", testConnection);
    apiInput.addEventListener("keydown", (e) => {
        if (e.key === "Enter") {
            testConnection();
        }
    });

    document.getElementById("btnLeaveStandby").addEventListener("click", leaveStandby);
    document.getElementById("btnEnterStandby").addEventListener("click", enterStandby);
    document.getElementById("btnResetSystem").addEventListener("click", resetSystem);
    document.getElementById("btnAbortBatch").addEventListener("click", abortBatch);
    document.getElementById("btnPauseBatch").addEventListener("click", pauseBatch);
    document.getElementById("btnResumeBatch").addEventListener("click", resumeBatch);

    document.getElementById("btnLoadProcedures").addEventListener("click", loadMaintenanceProcedures);
    document.getElementById("btnRunProcedure").addEventListener("click", runSelectedProcedure);

    document.getElementById("btnLoadMethods").addEventListener("click", loadMethods);
    document.getElementById("btnExecuteApiCall").addEventListener("click", executeDirectCall);
    document.getElementById("btnClearLogs").addEventListener("click", clearLogs);

    document.getElementById("pollInterval").addEventListener("change", (e) => {
        setupPolling(parseInt(e.target.value, 10));
    });

    document.getElementById("methodSelect").addEventListener("change", onMethodSelectChanged);

    // Populate method dropdown
    populateMethodDropdown();

    // Initial greeting
    log("Web client initialized. Click 'Connect / Test' to start communication.", "info");

    // Start default polling
    setupPolling(2000);
}

function updateClientInstance(url) {
    api = new BrukerSprRemoteAPI(url);
    tools = new Tools(api);
}

function log(message, type = "info") {
    const container = document.getElementById("logContainer");
    if (!container) return;
    const timestamp = new Date().toLocaleTimeString();
    const entry = document.createElement("div");
    entry.className = `log-entry ${type}`;
    
    const timeSpan = document.createElement("span");
    timeSpan.className = "log-time";
    timeSpan.textContent = `[${timestamp}]`;
    
    const msgSpan = document.createElement("span");
    msgSpan.className = "log-msg";
    msgSpan.textContent = message;

    entry.appendChild(timeSpan);
    entry.appendChild(msgSpan);
    container.appendChild(entry);
    container.scrollTop = container.scrollHeight;
    updateLogCount();
}

function clearLogs() {
    const container = document.getElementById("logContainer");
    if (container) container.innerHTML = "";
    updateLogCount();
}

function updateLogCount() {
    const countBadge = document.getElementById("logCount");
    if (countBadge) {
        const container = document.getElementById("logContainer");
        const count = container ? container.children.length : 0;
        countBadge.textContent = `${count} ${count === 1 ? 'entry' : 'entries'}`;
    }
}

function updateConnectionBadge(status, text) {
    const badge = document.getElementById("connectionStatus");
    if (badge) {
        badge.className = `badge badge-${status}`;
        badge.textContent = text;
    }
    const dot = document.getElementById("statusDot");
    if (dot) {
        dot.className = `status-dot status-dot-${status}`;
    }
}

async function testConnection() {
    const url = document.getElementById("apiUrl").value;
    updateClientInstance(url);
    log(`Connecting to ${url}...`, "info");

    try {
        const mode = await api.GetOperationMode();
        updateConnectionBadge("connected", "Connected");
        log(`Connection successful. Operation Mode: ${mode} (${OperationModes[mode] || "Unknown"})`, "info");
        await refreshSystemStatus();
    } catch (err) {
        updateConnectionBadge("disconnected", "Offline / Error");
        log(`Connection failed: ${err.message}`, "error");
    }
}

async function refreshSystemStatus() {
    if (!api) return;

    try {
        const mode = await api.GetOperationMode();
        document.getElementById("valOperationMode").textContent = `${mode} - ${OperationModes[mode] || "Unknown"}`;

        // Get additional status info
        try {
            const isStandby = mode === 10;
            document.getElementById("valStandby").textContent = isStandby ? "Yes" : "No";
        } catch {}

        try {
            const hasErr = await api.HasErrors();
            const hasWarn = await api.HasWarnings();
            document.getElementById("valErrors").textContent = hasErr ? "ERRORS PRESENT" : (hasWarn ? "Warnings present" : "None");
            document.getElementById("valErrors").style.color = hasErr ? "#dc3545" : (hasWarn ? "#ffc107" : "#28a745");
        } catch {}

        // Poll messages
        while (await api.HasMessage()) {
            const msg = await api.GetMessage();
            if (Array.isArray(msg) && msg.length >= 3) {
                log(`${msg[0]} [${msg[1]}] ${msg[2]}`, msg[1].toLowerCase() === "error" ? "error" : (msg[1].toLowerCase() === "warning" ? "warn" : "info"));
            } else if (Array.isArray(msg)) {
                log(msg.join(' '), "info");
            }
        }
    } catch (err) {
        // Silent failure during background poll
    }
}

function setupPolling(intervalMs) {
    if (pollTimer) {
        clearInterval(pollTimer);
        pollTimer = null;
    }

    if (intervalMs > 0) {
        pollTimer = setInterval(refreshSystemStatus, intervalMs);
    }
}

async function leaveStandby() {
    try {
        log("Sending LeaveStandby command...", "info");
        const res = await api.LeaveStandby();
        log(`LeaveStandby result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`LeaveStandby error: ${err.message}`, "error");
    }
}

async function enterStandby() {
    try {
        log("Running 'Standby Mode' maintenance procedure...", "info");
        const res = await api.RunMaintenanceProcedure("Standby Mode");
        log(`Standby Mode procedure result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`EnterStandby error: ${err.message}`, "error");
    }
}

async function resetSystem() {
    try {
        log("Running 'Reset System' maintenance procedure...", "info");
        const res = await api.RunMaintenanceProcedure("Reset System");
        log(`Reset System result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`ResetSystem error: ${err.message}`, "error");
    }
}

async function abortBatch() {
    try {
        log("Aborting current script...", "warn");
        const res = await api.AbortScript();
        log(`AbortScript result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`AbortScript error: ${err.message}`, "error");
    }
}

async function pauseBatch() {
    try {
        log("Pausing current runset...", "info");
        const res = await api.PauseRunsetAfter(1);
        log(`PauseRunsetAfter result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`PauseRunsetAfter error: ${err.message}`, "error");
    }
}

async function resumeBatch() {
    try {
        log("Resuming runset...", "info");
        const res = await api.ResumeRunset();
        log(`ResumeRunset result: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`ResumeRunset error: ${err.message}`, "error");
    }
}

async function loadMaintenanceProcedures() {
    try {
        log("Querying maintenance procedures...", "info");
        const procedures = await api.GetNamesOfMaintenanceProcedures();
        const select = document.getElementById("selectProcedures");
        select.innerHTML = "";

        if (Array.isArray(procedures) && procedures.length > 0) {
            procedures.forEach(proc => {
                const opt = document.createElement("option");
                opt.value = proc;
                opt.textContent = proc;
                select.appendChild(opt);
            });
            log(`Loaded ${procedures.length} maintenance procedures.`, "info");
        } else {
            const opt = document.createElement("option");
            opt.textContent = "No procedures available";
            select.appendChild(opt);
        }
    } catch (err) {
        log(`Failed to load maintenance procedures: ${err.message}`, "error");
    }
}

async function runSelectedProcedure() {
    const select = document.getElementById("selectProcedures");
    const procName = select.value;
    if (!procName) {
        log("Please select a procedure to run.", "warn");
        return;
    }

    try {
        log(`Executing maintenance procedure '${procName}'...`, "info");
        const res = await api.RunMaintenanceProcedure(procName);
        log(`Maintenance procedure '${procName}' returned: ${res}`, "info");
        await refreshSystemStatus();
    } catch (err) {
        log(`Error executing '${procName}': ${err.message}`, "error");
    }
}

async function loadMethods() {
    try {
        log("Querying loaded methods...", "info");
        const methods = await api.GetNamesOfMethods();
        const resultDisplay = document.getElementById("methodsResult");
        resultDisplay.textContent = JSON.stringify(methods, null, 2);
        log(`Found ${methods.length} methods.`, "info");
    } catch (err) {
        log(`Failed to load methods: ${err.message}`, "error");
    }
}

const ApiMethodCatalog = [
    { name: "GetOperationMode", params: [] },
    { name: "LeaveStandby", params: [] },
    { name: "GetNamesOfMethods", params: [] },
    { name: "GetNamesOfMethodsOfAssayType", params: [{ name: "assayType", type: "string" }] },
    { name: "GetAssayTypesOfAllMethods", params: [] },
    { name: "GetNameOfCurrentMethod", params: [] },
    { name: "GetAssayTypeOfCurrentMethod", params: [] },
    { name: "GetAssayTypeOfMethod", params: [{ name: "methodName", type: "string" }] },
    { name: "GetNamesOfRunsets", params: [] },
    { name: "GetNamesOfRunsetsOfAssayType", params: [{ name: "assayType", type: "string" }] },
    { name: "GetAssayTypesOfAllRunsets", params: [] },
    { name: "GetNameOfCurrentRunset", params: [] },
    { name: "GetAssayTypeOfCurrentRunset", params: [] },
    { name: "GetAssayTypeOfRunset", params: [{ name: "runsetName", type: "string" }] },
    { name: "GetMethodNamesOfRunset", params: [{ name: "runsetName", type: "string" }] },
    { name: "SelectMethod", params: [{ name: "methodName", type: "string" }] },
    { name: "SelectRunset", params: [{ name: "runsetName", type: "string" }] },
    { name: "GetNamesOfMaintenanceProcedures", params: [] },
    { name: "RunMaintenanceProcedure", params: [{ name: "procedureName", type: "string" }] },
    { name: "StartRunset", params: [] },
    { name: "PauseRunsetAfter", params: [{ name: "pauseMode", type: "number" }] },
    { name: "ResumeRunset", params: [] },
    { name: "ResetRunset", params: [] },
    { name: "AbortScript", params: [] },
    { name: "SetStandbyAfterFinish", params: [{ name: "state", type: "boolean" }] },
    { name: "GetStandbyAfterFinish", params: [] },
    { name: "HasErrors", params: [] },
    { name: "GetErrors", params: [] },
    { name: "HasWarnings", params: [] },
    { name: "GetWarnings", params: [] },
    { name: "HasMessage", params: [] },
    { name: "GetMessage", params: [] }
];

function populateMethodDropdown() {
    const select = document.getElementById("methodSelect");
    select.innerHTML = "";

    ApiMethodCatalog.forEach(m => {
        const opt = document.createElement("option");
        opt.value = m.name;
        opt.textContent = m.name;
        select.appendChild(opt);
    });

    onMethodSelectChanged();
}

function onMethodSelectChanged() {
    const select = document.getElementById("methodSelect");
    const methodName = select.value;
    const methodMeta = ApiMethodCatalog.find(m => m.name === methodName);
    const paramContainer = document.getElementById("methodParamsContainer");
    paramContainer.innerHTML = "";

    if (methodMeta && methodMeta.params.length > 0) {
        methodMeta.params.forEach(p => {
            const group = document.createElement("div");
            group.className = "form-group";
            group.innerHTML = `
                <label for="param_${p.name}">${p.name} (${p.type}):</label>
                <input type="${p.type === 'number' ? 'number' : 'text'}" id="param_${p.name}" placeholder="Enter ${p.name}">
            `;
            paramContainer.appendChild(group);

            const inputEl = group.querySelector("input");
            if (inputEl) {
                inputEl.addEventListener("keydown", (e) => {
                    if (e.key === "Enter") {
                        executeDirectCall();
                    }
                });
            }
        });
    }
}

async function executeDirectCall() {
    const select = document.getElementById("methodSelect");
    const methodName = select.value;
    const methodMeta = ApiMethodCatalog.find(m => m.name === methodName);
    const responseBox = document.getElementById("apiCallResult");

    try {
        log(`Calling API method '${methodName}'...`, "info");
        const args = [];

        if (methodMeta && methodMeta.params.length > 0) {
            methodMeta.params.forEach(p => {
                const input = document.getElementById(`param_${p.name}`);
                let val = input.value;
                if (p.type === "boolean") {
                    val = val.toLowerCase() === "true" || val === "1";
                } else if (p.type === "number") {
                    val = parseFloat(val);
                }
                args.push(val);
            });
        }

        const result = await api[methodName](...args);
        responseBox.textContent = JSON.stringify(result, null, 2);
        log(`Method '${methodName}' returned successfully.`, "info");
    } catch (err) {
        responseBox.textContent = `Error: ${err.message}`;
        log(`Error calling '${methodName}': ${err.message}`, "error");
    }
}
