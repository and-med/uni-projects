#pragma once

#include<string>
#include<iostream>

using std::string;

class Country
{
	string name;
public:
	friend std::istream& operator>>(std::istream& is, Country& c)
	{
		std::getline(is >> std::ws, c.name);
		return is;
	}
};