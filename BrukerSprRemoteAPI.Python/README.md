# Bruker SPR Remote API Python Client

Python client library for connecting to and controlling Bruker Surface Plasmon Resonance (SPR) instruments via the Bruker SPR Remote API.

## Installation

Install using pip:

```bash
pip install .
```

Or install in editable / development mode:

```bash
pip install -e .
```

## Quick Start

```python
from BrukerSprRemoteAPI import BrukerSprRemoteAPI, Tools

# Create API client instance with the endpoint URL of the instrument
api = BrukerSprRemoteAPI('http://localhost:9001/bruker-spr/json/')

# Initialize helper tools and start background logging
toolbox = Tools(api)
toolbox.StartLogging()

# Example usage:
api.LeaveStandby()
toolbox.WaitForReady()

# Get available methods
methods = api.GetNamesOfMethods()
print("Available methods:", methods)
```

## Build
```
python -m pip install --upgrade build
python -m build
```
