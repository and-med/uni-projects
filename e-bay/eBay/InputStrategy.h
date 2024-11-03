#pragma once

#include<iostream>
#include<string>
#include<conio.h>
#include"User.h"
#include"Additional.h"

using std::cin;
using std::cout;

class InputStrategy
{
public:
	virtual void get() = 0;
};

class MainMenuInput :public InputStrategy
{
	char option_ = '1';
public:
	MainMenuInput(){}
	void get()
	{
		unsigned x = 39;
		unsigned y = 13;
		bool canContinue = true;
		while (canContinue)
		{
			additional::setCursorPosition(x, y);
			cout << char(4);
			unsigned char c = _getch();
			if (c == 224)
			{
				c = _getch();
				switch (c)
				{
				case DOWN:
					y < 15 ? ++y, ++option_ : y;
					break;
				case UP:
					y > 13 ? --y, --option_ : y;
					break;
				}
				cout << "\b \b";
			}
			else if (c == ENTER)
			{
				canContinue = false;

			}
		}
	}
	char result() const
	{
		return option_;
	}
};

class LogInput : public InputStrategy
{
	string name;
	string password;
public:
	LogInput() {}
	string result() const
	{
		return name;
	}
	string getPass() const
	{
		return password;
	}
	void get()
	{
		int x = 50;
		int y = 9;
		additional::setCursorPosition(x, y);
		char gotten = _getch();
		while (gotten != ENTER)
		{
			if (gotten == '\b' && name.size() != 0)
			{
				cout << "\b \b";
				name.erase(name.size() - 1);
			}
			else if (gotten != '\b' && name.size() <= 30)
			{
				if (additional::isAllowed(gotten))
				{
					cout << gotten;
					name += gotten;
				}
			}
			gotten = _getch();
		}
		y += 1;
		additional::setCursorPosition(x, y);
		gotten = _getch();
		while (gotten != ENTER)
		{
			if (gotten == '\b' && password.size() != 0)
			{
				cout << "\b \b";
				password.erase(password.size() - 1);
			}
			else if(gotten != '\b' && password.size() <= 30)
			{
				if (additional::isAllowed(gotten))
				{
					cout << '*';
					password += gotten;
				}
			}
			gotten = _getch();
		}
	}
};

class UserLoggedInput : public InputStrategy
{
	char _option = '1';
public:
	UserLoggedInput() {}
	void get()
	{
		unsigned x = 39;
		unsigned y = 9;
		bool canContinue = true;
		while (canContinue)
		{
			additional::setCursorPosition(x, y);
			cout << char(4);
			unsigned char c = _getch();
			if (c == 224)
			{
				c = _getch();
				switch (c)
				{
				case DOWN:
					y < 11 ? ++y, ++_option : y;
					break;
				case UP:
					y > 9 ? --y, --_option : y;
					break;
				}
				cout << "\b \b";
			}
			else if (c == ENTER)
			{
				canContinue = false;
			}
		}
	}
};