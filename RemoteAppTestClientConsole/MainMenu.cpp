#include "MainMenu.h"

void MainMenu::ClearScreenAndShowHeader()
{
	system("CLS");
	std::cout << "********************************************************\n* Bruker Daltonics SPR Remote Connection Sample Client *\n********************************************************";
	std::cout << "\n";
}

MainMenu::ACTION MainMenu::ShowMainMenu()
{
	ClearScreenAndShowHeader();
	std::cout << "\ns\t- Status";
	std::cout << "\nm\t- Maintenance";
	std::cout << "\ni\t- Get Information About Methods";
	std::cout << "\nr\t- Get Information About Runsets";
	std::cout << "\np\t- Prepare And Run Assays";
	std::cout << "\nq\t- quit";

	std::cout << "\nChoice: ";
	char input;
	std::cin >> input;

	switch (input)
	{
	case 's':
		return status;
	case 'm':
		return maintenance;
	case 'i':
		return informationAboutMethods;
	case 'r':
		return informationAboutRunsets;
	case 'p':
		return prepareAndRunAssays;
	case 'q':
		return quit;
	default:
		return showMainMenu;
	}
}

void MainMenu::Return(bool quick)
{
	std::cout << "\n(Press any key to return.)" << std::endl;
	std::getchar();
	if(!quick)
		std::getchar();
}
