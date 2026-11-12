import os
import sys

# Ensure BrukerSprRemoteAPI package is accessible when running the script directly
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'BrukerSprRemoteAPI.Python')))

from BrukerSprRemoteAPI import BrukerSprRemoteAPI, Tools

# create connection to API
api = BrukerSprRemoteAPI('http://localhost:9001/bruker-spr/json/')

# create tools object to provide some helper methods
toolbox = Tools(api)
toolbox.StartLogging()

# enter your custom code here

toolbox.WaitForReady()
api.RunMaintenanceProcedure('Wash Needle')
toolbox.WaitForReady()
print("All done.")



