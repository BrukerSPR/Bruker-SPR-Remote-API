import time
from threading import Thread

class Tools(object):
    
    def __init__(self, api) :
        self.__api = api

    def WaitForReady(self) :
        while True:
            time.sleep(5)

            if (self.__api.GetOperationMode() == 0) :
                print("Ready")
                break

    def StartLogging(self) :
        t = Thread(target=self._Log, args=())
        t.start()

    def _Log(self) :
        self._LogMessages()
        self._LogErrorsAndWarnings()

    def _LogMessages(self) :
        while (True) :
            while (self.__api.HasMessage()) :
                message = self.__api.GetMessage()
                if(len(message)) == 3 :
                    print(message[0] + "    " + message[1] + "    " + message[2])
                else :
                    print("??? wrong message format ???")

            time.sleep(0.1)

    def _LogErrorsAndWarnings(self) :
        while (True) :
            if (self.__api.HasErrors()) :
                errors = self.__api.GetErrors()
                for error in errors :
                    print("Error: " + error)

            if (self.__api.HasWarnings()) :
                warnings = self.__api.GetWarnings()
                for warning in warnings :
                    print("Warning: " + warning)

            time.sleep(10)
