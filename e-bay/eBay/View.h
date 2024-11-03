#pragma once

#include<iostream>
#include<string>
#include<Windows.h>
#include"InputStrategy.h"

using std::cout;
using std::endl;
using std::string;

class View
{
public:
	virtual ~View() {}
	virtual void show()const = 0;
	virtual InputStrategy* input() const = 0;
};

class MainMenuView : public View
{
public:
	void show()const
	{
		CLS;
		additional::printEbay();
		cout << "\n\n\n\n\n\n\n\t\t\t" << endl;
		cout << "\t\t\t\t\t1.SignIn" << endl;
		cout << "\t\t\t\t\t2.HistoryLoad and Inform" << endl;
		cout << "\t\t\t\t\t3.MakingReports" << endl;
	}
	MainMenuInput* input() const
	{
		MainMenuInput* input = new MainMenuInput;
		input->get();
		return input;
	}
};

class LogInView : public View
{
public:
	void show()const
	{
		CLS;
		cout << "\n\n\n\n\n\n\n\n\n\t\t\t\t\tLogin:\n\t\t\t\t\tPassword:";
	}
	LogInput* input() const
	{
		LogInput* input = new LogInput;
		input->get();
		return input;
	}
};

class UserLoggedView : public View
{
public:
	void show()const
	{
		CLS;
		cout << "\n\n\n\n\n\n\n\t\t\t\t\tMY CABINET\n\n	"
			<< "\t\t\t\tCreate User\n\t\t\t\t\tSell Good\n"
			<< "\t\t\t\t\tBuy Good\n\n\n\n";
		cout << "";
	}
	UserLoggedInput* input() const
	{
		UserLoggedInput* input = new UserLoggedInput;
		input->get();
		return input;
	}
};