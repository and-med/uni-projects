#pragma once

#include<iostream>
#include<string>
#include<Windows.h>

namespace additional
{
#define UP 72
#define DOWN 80
#define RIGHT 77
#define LEFT 75
#define ENTER 13
#define PAUSE system("pause>>void")
#define CLS system("cls")

	void setCursorPosition(int x, int y);
	void printEbay();
	bool isAllowed(const char& a);
}

namespace files
{
	const std::string users = "Users.txt";
	const std::string admins = "Admins.txt";
	const std::string history = "History.txt";
	const std::string goods = "Goods.txt";
}

namespace catalog
{
	const std::string categories[] = { 
		"Fashion", 
		"Sport goods", 
		"Electronics", 
		"Home & garden", 
		"Toys & hobbies", 
		"Entertainment", 
		"Motors", 
		"Other categories"};
}