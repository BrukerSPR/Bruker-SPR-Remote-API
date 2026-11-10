# Bruker SPR Remote API Documentation

Welcome to the documentation for the **Bruker SPR Remote API**.

The Bruker SPR Control Software's remote API allows client frameworks to connect to the Control Software remotely and execute essential operations such as starting batch jobs or running maintenance operations.

## Key Sections

* **[User Manual](docs/overview.md)**: Conceptual guide covering remote-control mode, simulation mode, endpoint configuration, client implementation (C#, C++, Python, JavaScript), interface details, and device geometry.
* **[API Reference](api/index.md)**: Generated API reference documentation for the service contract interfaces, proxy classes, and enums.

## Overview

The implementation is based on Windows Communication Foundation (WCF) technology, enabling multiple endpoints (TCP and HTTP/REST) for flexible client integration across languages and platforms.
