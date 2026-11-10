# Endpoint Configuration

The endpoints to which clients can connect are configured in the Control Software's `app.config` file. Endpoints can be configured for different transfer protocols (TCP or HTTP). Clients can either run locally on the same PC like the Control Software or access the API via a network connection. The base addresses for the endpoints can also be configured. By default, the base address for HTTP endpoints is 9001, resp. 9002 for TCP connections.

Once the remote-control server of the Control Software has been started you can check that it is running by entering the URI `http://localhost:9001/bruker-spr/` into a web browser's address field on the computer on which the Control Software is running. You can also do this from a different computer if "localhost" is replaced with the IP address of the Control Software's PC. Of course, the port 9001 must be open to the outside to do this.

You can also use the JSON endpoint to transfer commands directly from the browser to the control software. Some examples:

* `http://localhost:9001/bruker-spr/GetOperationMode` gets the current operation mode as an integer. The meaning of the value can be picked from the table in the Interface Description section.
* `http://localhost:9001/bruker-spr/GetNamesOfMaintenanceProcedures` will return a list of names of all maintenance commands that can be run remotely in JSON format.
* `http://localhost:9001/bruker-spr/json/RunMaintenanceProcedure?procedureName=Reset%20System` starts the maintenance command to reset the device's stepper motors and pumps. This only works if the Control Software is either idle (resp. paused or completed) or in "Reset Required" state (usually after an error).
