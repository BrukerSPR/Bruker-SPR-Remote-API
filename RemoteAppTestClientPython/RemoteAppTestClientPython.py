import Mass1Api
import Tools

  
# create connection to API
api = Mass1Api.Mass1Api('http://10.56.0.42:9001/bruker-spr/json/')

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



