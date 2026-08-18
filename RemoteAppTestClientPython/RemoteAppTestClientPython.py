import Mass1Api
import Tools

# replace localhost with the IP of the PC the control software is running on
address = 'http://localhost:9001/bruker-spr/json/'

# create connection to API 
api = Mass1Api.Mass1Api(address)

# create tools object to provide some helper methods
toolbox = Tools.Tools(api)
toolbox.StartLogging()

# enter your custom code here

api.LeaveStandby()

toolbox.WaitForReady()

api.RunMaintenanceProcedure('Wash Needle')

#toolbox.WaitForReady()

#api.RunMaintenanceProcedure('Prime')

#toolbox.WaitForReady()

#api.RunMaintenanceProcedure('Standby Mode')



