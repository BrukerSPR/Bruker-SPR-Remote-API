/**
 * Helper toolbox for Bruker SPR Remote API operations.
 */
class Tools {
    /**
     * Initializes a new Tools instance.
     * @param {Object} api - The BrukerSprRemoteAPI client instance.
     */
    constructor(api) {
        if (!api) {
            throw new TypeError("Parameter 'api' must be an instance of BrukerSprRemoteAPI.");
        }
        this.api = api;
        this._isLogging = false;
        this._loggingTimeout = null;
    }

    /**
     * Polls the remote API until the operation mode is Ready (0).
     * @param {number} [pollIntervalMs=5000] - Polling interval in milliseconds.
     * @param {function} [onProgress] - Optional callback receiving the current operation mode.
     * @returns {Promise<void>}
     */
    async WaitForReady(pollIntervalMs = 5000, onProgress = null) {
        while (true) {
            await new Promise(resolve => setTimeout(resolve, pollIntervalMs));
            const mode = await this.api.GetOperationMode();
            if (typeof onProgress === 'function') {
                onProgress(mode);
            }
            if (mode === 0) {
                console.log("Ready");
                return;
            }
        }
    }

    /**
     * Alias for WaitForReady.
     * @param {number} [pollIntervalMs=5000]
     * @param {function} [onProgress]
     * @returns {Promise<void>}
     */
    waitForReady(pollIntervalMs = 5000, onProgress = null) {
        return this.WaitForReady(pollIntervalMs, onProgress);
    }

    /**
     * Starts continuous background polling for messages, errors, and warnings.
     * @param {Object} [handlers] - Event callbacks.
     * @param {function(string[])} [handlers.onMessage] - Callback for incoming log messages [date, time, text].
     * @param {function(string)} [handlers.onError] - Callback for error messages.
     * @param {function(string)} [handlers.onWarning] - Callback for warning messages.
     * @param {number} [pollIntervalMs=10000] - Polling interval in milliseconds.
     */
    StartLogging(handlers = {}, pollIntervalMs = 10000) {
        if (this._isLogging) {
            return;
        }

        const {
            onMessage = (msg) => {
                if (Array.isArray(msg) && msg.length === 3) {
                    console.log(msg[0] + "    " + msg[1] + "    " + msg[2]);
                } else if (Array.isArray(msg)) {
                    console.log(msg.join("    "));
                } else {
                    console.log("??? wrong message format ???");
                }
            },
            onError = (err) => console.log("Error: " + err),
            onWarning = (warn) => console.log("Warning: " + warn)
        } = handlers;

        this._isLogging = true;

        const poll = async () => {
            if (!this._isLogging) {
                return;
            }

            try {
                // Poll queued messages
                while (this._isLogging && (await this.api.HasMessage())) {
                    const message = await this.api.GetMessage();
                    if (Array.isArray(message)) {
                        onMessage(message);
                    }
                    // Brief pause to avoid saturating HTTP sockets
                    await new Promise(resolve => setTimeout(resolve, 50));
                }

                // Poll errors
                if (await this.api.HasErrors()) {
                    const errors = await this.api.GetErrors();
                    if (Array.isArray(errors)) {
                        for (const err of errors) {
                            onError(err);
                        }
                    }
                }

                // Poll warnings
                if (await this.api.HasWarnings()) {
                    const warnings = await this.api.GetWarnings();
                    if (Array.isArray(warnings)) {
                        for (const warn of warnings) {
                            onWarning(warn);
                        }
                    }
                }
            } catch {
                // Silently keep polling on transient errors
            } finally {
                if (this._isLogging) {
                    this._loggingTimeout = setTimeout(poll, pollIntervalMs);
                }
            }
        };

        poll();
    }

    /**
     * Alias for StartLogging.
     * @param {Object} [handlers]
     * @param {number} [pollIntervalMs=10000]
     */
    startLogging(handlers = {}, pollIntervalMs = 10000) {
        return this.StartLogging(handlers, pollIntervalMs);
    }

    /**
     * Stops continuous logging.
     */
    StopLogging() {
        this._isLogging = false;
        if (this._loggingTimeout) {
            clearTimeout(this._loggingTimeout);
            this._loggingTimeout = null;
        }
    }

    /**
     * Alias for StopLogging.
     */
    stopLogging() {
        return this.StopLogging();
    }
}

// Universal Module Definition / CommonJS / Browser global export
if (typeof module !== 'undefined' && module.exports) {
    module.exports = { Tools };
}
if (typeof window !== 'undefined') {
    window.BrukerSprTools = Tools;
    window.Tools = Tools;
}
if (typeof globalThis !== 'undefined') {
    globalThis.BrukerSprTools = Tools;
    globalThis.Tools = Tools;
}
