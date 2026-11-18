const http = require('http');
const fs = require('fs');
const path = require('path');

const PORT = process.env.PORT || 8080;
const TARGET_API_DEFAULT = 'http://localhost:9001';

const MIME_TYPES = {
    '.html': 'text/html; charset=utf-8',
    '.js': 'application/javascript; charset=utf-8',
    '.css': 'text/css; charset=utf-8',
    '.json': 'application/json; charset=utf-8',
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
    '.svg': 'image/svg+xml',
    '.ico': 'image/x-icon'
};

const server = http.createServer((req, res) => {
    // Add CORS headers to all responses
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
    res.setHeader('Access-Control-Allow-Headers', 'Content-Type, Accept');

    if (req.method === 'OPTIONS') {
        res.writeHead(204);
        res.end();
        return;
    }

    const reqUrl = new URL(req.url, `http://${req.headers.host}`);

    // Reverse proxy for Bruker SPR API calls (/bruker-spr/*)
    if (reqUrl.pathname.startsWith('/bruker-spr/')) {
        const targetUrl = new URL(reqUrl.pathname + reqUrl.search, TARGET_API_DEFAULT);
        const proxyReq = http.request(targetUrl, {
            method: req.method,
            headers: {
                ...req.headers,
                host: targetUrl.host
            }
        }, (proxyRes) => {
            res.writeHead(proxyRes.statusCode, {
                ...proxyRes.headers,
                'Access-Control-Allow-Origin': '*'
            });
            proxyRes.pipe(res);
        });

        proxyReq.on('error', (err) => {
            res.writeHead(502, { 'Content-Type': 'application/json' });
            res.end(JSON.stringify({ error: 'Proxy error', details: err.message }));
        });

        req.pipe(proxyReq);
        return;
    }

    // Static file serving
    let filePath;
    if (reqUrl.pathname.startsWith('/BrukerSprRemoteAPI.JavaScript/') || reqUrl.pathname.startsWith('/media/')) {
        filePath = path.join(__dirname, '..', reqUrl.pathname);
    } else {
        const relativePath = reqUrl.pathname === '/' ? 'index.html' : reqUrl.pathname.replace(/^\//, '');
        filePath = path.join(__dirname, relativePath);
    }

    fs.readFile(filePath, (err, content) => {
        if (err) {
            if (err.code === 'ENOENT') {
                res.writeHead(404, { 'Content-Type': 'text/plain' });
                res.end('404 Not Found');
            } else {
                res.writeHead(500, { 'Content-Type': 'text/plain' });
                res.end(`500 Internal Server Error: ${err.code}`);
            }
            return;
        }

        const ext = path.extname(filePath).toLowerCase();
        const contentType = MIME_TYPES[ext] || 'application/octet-stream';
        res.writeHead(200, { 'Content-Type': contentType });
        res.end(content);
    });
});

server.listen(PORT, () => {
    console.log(`Bruker SPR Remote HTML Test Client running at http://localhost:${PORT}/`);
    console.log(`Proxying /bruker-spr/* requests to ${TARGET_API_DEFAULT}`);
});
