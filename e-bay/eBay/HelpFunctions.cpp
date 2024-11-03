#include"Additional.h"

void additional::setCursorPosition(int x, int y) {
	COORD p = { x, y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), p);
}

void additional::printEbay()
{
	std::cout << "\t\t\t\t\t       ___ " << std::endl;
	std::cout << "\t\t\t\t\t  ___ | _ ) __ _  _  _ " << std::endl;
	std::cout << "\t\t\t\t\t / -_)| _ \\/ _` || || |" << std::endl;
	std::cout << "\t\t\t\t\t \\___||___/\\__,_| \\_, |" << std::endl;
	std::cout << "\t\t\t\t\t                  |__/ " << std::endl;
}

bool additional::isAllowed(const char& a)
{
	return (a >= '0' && a <= '9') || (a >= 'A' && a <= 'z');
}