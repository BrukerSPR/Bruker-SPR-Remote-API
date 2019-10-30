#pragma once

#include <iostream>

class MainMenu
{
	public:

		enum ACTION
		{
			quit,
			showMainMenu,
			status,
			maintenance,
			informationAboutMethods,
			informationAboutRunsets,
			prepareAndRunAssays
		};

		static void ClearScreenAndShowHeader();
		static ACTION ShowMainMenu();
		static void Return(bool quick = false);
};

