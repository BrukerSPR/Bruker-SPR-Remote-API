import urllib.request
import json

class Mass1Api:

    def __init__(self, url) :
        self.__url = url


    #Get information about methods

    def GetNamesOfMethods(self) :
        return self.CallApi('GetNamesOfMethods')

    def GetNamesOfMethodsOfAssayType(self, assayType) :
        return self.CallApi('GetNamesOfMethodsOfAssayType?assayType=' + str(assayType).replace(" ", "%20"))

    def GetAssayTypesOfAllMethods(self) :
        return self.CallApi('GetAssayTypesOfAllMethods')

    def GetNameOfCurrentMethod(self) :
        return self.CallApi('GetNameOfCurrentMethod')

    def GetAssayTypeOfCurrentMethod(self) :
        return self.CallApi('GetAssayTypeOfCurrentMethod')

    def GetAssayTypeOfMethod(self, methodName) :
        return self.CallApi('GetAssayTypeOfMethod?methodName=' + str(methodName).replace(" ", "%20"))


    #Get information about runsets

    def GetNamesOfRunsets(self) :
        return self.CallApi('GetNamesOfRunsets')

    def GetNamesOfRunsetsOfAssayType(self, assayType) :
        return self.CallApi('GetNamesOfRunsetsOfAssayType?assayType=' + str(assayType).replace(" ", "%20"))

    def GetAssayTypesOfAllRunsets(self) :
        return self.CallApi('GetAssayTypesOfAllRunsets')

    def GetNameOfCurrentRunset(self) :
        return self.CallApi('GetNameOfCurrentRunset')

    def GetAssayTypeOfCurrentRunset(self) :
        return self.CallApi('GetAssayTypeOfCurrentRunset')

    def GetAssayTypeOfRunset(self, runsetName) :
        return self.CallApi('GetAssayTypeOfRunset?runsetName=' + str(runsetName).replace(" ", "%20"))

    def GetMethodNamesOfRunset(self, runsetName) :
        return self.CallApi('GetMethodNamesOfRunset?runsetName=' + str(runsetName).replace(" ", "%20"))


    #Prepare and run assays

    def SelectMethod(self, methodName) :
        return self.CallApi('SelectMethod?methodName=' + str(methodName).replace(" ", "%20"))

    def SelectRunset(self, runsetName) :
        return self.CallApi('SelectRunset?runsetName=' + str(runsetName).replace(" ", "%20"))

    def CreateRunset(self, methodNames) :
        methodNameStr = '|'.join(methodNames)
        return self.CallApi('CreateRunset?methodNames=' + str(methodNameStr).replace(" ", "%20"))

    def SetSamplePlateId(self, methodIndex, plateId) :
        return self.CallApi('SetSamplePlateId?methodIndex=' + str(methodIndex).replace(" ", "%20") + '&plateId=' + str(plateId))

    def GetSamplePlateId(self, methodIndex) :
        return self.CallApi('GetSamplePlateId?methodIndex=' + str(methodIndex))

    def GetCurrentSamplePlateId(self) :
        return self.CallApi('GetCurrentSamplePlateId')

    def MoveSamplePlateTrayOut(self) :
        return self.CallApi('MoveSamplePlateTrayOut')

    def MoveSamplePlateTrayIn(self) :
        return self.CallApi('MoveSamplePlateTrayIn')

    def StartSelectedRunset(self) :
        return self.CallApi('StartSelectedRunset')

    def StartSelectedRunsetFrom(self, methodIndex) :
        return self.CallApi('StartSelectedRunsetFrom?methodIndex=' + str(methodIndex))

    def PauseRunsetAfter(self, pauseMode) :
        return self.CallApi('PauseRunsetAfter?pauseMode=' + str(pauseMode))

    def ResumeRunset(self) :
        return self.CallApi('ResumeRunset')

    def ResetRunset(self) :
        return self.CallApi('ResetRunset')

    def AbortScript(self) :
        return self.CallApi('AbortScript')

    def LeaveStandby(self) :
        return self.CallApi('LeaveStandby')

    def SetStandbyAfterFinish(self, methodName) :
        return self.CallApi('SetStandbyAfterFinish?runsetName=' + str(methodName).replace(" ", "%20"))

    def GetStandbyAfterFinish(self) :
        return self.CallApi('GetStandbyAfterFinish')


    #Maintenance

    def GetNamesOfMaintenanceProcedures(self) :
        return self.CallApi('GetNamesOfMaintenanceProcedures')

    def RunMaintenanceProcedure(self, procedureName) :
        return self.CallApi('RunMaintenanceProcedure?procedureName=' + str(procedureName).replace(" ", "%20"))


    #Status

    def GetOperationMode(self) :
        return self.CallApi('GetOperationMode')

    def IsChipDocked(self) :
        return self.CallApi('IsChipDocked')

    def IsSamplePlateTrayIn(self) :
        return self.CallApi('IsSamplePlateTrayIn')

    def HasMessage(self) :
        return self.CallApi('HasMessage')

    def GetMessage(self) :
        return self.CallApi('GetMessage')

    def HasErrors(self) :
        return self.CallApi('HasErrors')

    def GetErrors(self) :
        return self.CallApi('GetErrors')

    def HasWarnings(self) :
        return self.CallApi('HasWarnings')

    def GetWarnings(self) :
        return self.CallApi('GetWarnings')


    # Other methods

    def CallApi(self, call) :
        url = self.__url + str(call)
        req = urllib.request.Request(url)
        r = urllib.request.urlopen(req).read()
        return json.loads(r.decode('utf-8'))