# Bruker SPR Remote API JavaScript / Node.js Client

A lightweight, promise-based JavaScript and TypeScript client for the **Bruker SPR Control Software Remote REST API**.

This package enables laboratory automation systems, web dashboards, Node.js scripts, and browser applications to control Bruker SPR instruments (such as SPR-32 and SPR-64) via REST JSON.

## Features

- **Full API Coverage**: Auto-generated from the official `IBrukerSprRemoteService` definition.
- **Universal Compatibility**: Works seamlessly in modern Node.js (v18+) and all modern web browsers.
- **TypeScript Support**: Full TypeScript type definitions (`.d.ts`) included out of the box.
- **Runtime Validation**: Validates parameter types and formats before sending network requests.
- **Tools Helper Class**: Background log streaming, error polling, and status synchronization (`WaitForReady`).

## Installation

```bash
npm install bruker-spr-remote-api
```

## Quick Start (Node.js)

```javascript
const { BrukerSprRemoteAPI, Tools } = require('bruker-spr-remote-api');

async function main() {
    // Connect to the REST API endpoint
    const api = new BrukerSprRemoteAPI('http://127.0.0.1:9001/bruker-spr/json/');
    const tools = new Tools(api);

    // Start background event logging
    tools.StartLogging({
        onMessage: (msg) => console.log(`[LOG] ${msg.join(' ')}`),
        onError: (err) => console.error(`[ERROR] ${err}`),
        onWarning: (warn) => console.warn(`[WARN] ${warn}`)
    });

    // Check current state
    const mode = await api.GetOperationMode();
    console.log(`Current operation mode: ${mode}`);

    // Leave standby if needed
    await api.LeaveStandby();
    await tools.WaitForReady();

    // Query available maintenance procedures
    const procedures = await api.GetNamesOfMaintenanceProcedures();
    console.log('Available maintenance procedures:', procedures);

    // Run a maintenance procedure (e.g., Wash Needle)
    if (procedures.includes('Wash Needle')) {
        await api.RunMaintenanceProcedure('Wash Needle');
        await tools.WaitForReady();
    }
}

main().catch(console.error);
```

## Quick Start (Browser / HTML)

Include `BrukerSprRemoteAPI.js` and `Tools.js` in your HTML:

```html
<script src="BrukerSprRemoteAPI.js"></script>
<script src="Tools.js"></script>
<script>
    const api = new BrukerSprRemoteAPI('http://localhost:9001/bruker-spr/json/');
    const tools = new Tools(api);

    async function checkStatus() {
        const mode = await api.GetOperationMode();
        console.log('Current operation mode:', mode);
    }
</script>
```

## License

MIT License. Copyright (c) Bruker Daltonics GmbH & Co KG.
