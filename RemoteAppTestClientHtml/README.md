# Bruker SPR Remote API - HTML & JavaScript Sample Application

A web-based sample application and interactive explorer for controlling Bruker SPR instruments via the REST API.

## Features

- **Dashboard**: Real-time display of instrument Operation Mode, Standby state, and errors/warnings.
- **Instrument Actions**: Quick-action buttons to Leave Standby, Enter Standby, Reset System, Pause, Resume, and Abort batch jobs.
- **Maintenance Procedures**: Dynamically queries and executes maintenance procedures (Wash Needle, Prime, Clean, etc.).
- **Methods Explorer**: Lists available assay methods in the loaded runset library.
- **Interactive API Method Caller**: Test and execute any API method with dynamic parameter inputs and formatted JSON response viewer.
- **Live Event Log**: Real-time polling and streaming of system messages, warnings, and errors using `Tools.js`.

## Running the Sample Application

### Option 1: Using the Included Node.js Dev Server & Proxy (Recommended)

Because the instrument's Remote API server (port 9001) does not include Cross-Origin Resource Sharing (CORS) headers, modern browsers will block direct cross-origin requests made from `file://` or a separate port.

The included `server.js` serves the application and provides a built-in reverse proxy with CORS headers:

```bash
node RemoteAppTestClientHtml/server.js
```

Then open `http://localhost:8080` in your browser.

- You can connect to `http://localhost:8080/bruker-spr/json/` (proxied to port 9001 with CORS enabled) or directly to `http://localhost:9001/bruker-spr/json/`.

### Option 2: Direct Browser Launch / Other Static Servers

If serving `index.html` via a static file server or opening directly:
- **Browser Extension**: Use a CORS unblocker extension (e.g. "Allow CORS") for development.
- **Launch with Disabled Web Security** (Chrome/Edge):
  ```bash
  chrome.exe --user-data-dir="C:/temp/chrome-dev" --disable-web-security "file:///D:/Bruker-SPR-Remote-API/RemoteAppTestClientHtml/index.html"
  ```
- **Node.js / Desktop apps (Electron/Tauri)**: CORS restrictions only apply to browser web contexts. Scripts executing in Node.js, Python, or native applications are not subject to CORS.
