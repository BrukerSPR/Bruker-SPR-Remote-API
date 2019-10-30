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
		static std::wstring ToWstring(std::string& s);
		static std::string ToString(std::wstring& s);
};

