#pragma once

#include<iostream>
#pragma once

#include<string>
#include"Country.h"
#include"Rate.h"

using std::string;

class User
{
	string login;
	string password;
	string name;
	Country country;

protected:
	User() {}
	User(string tname) : login(tname) {}
	User(string tl, string tp, string tn, Country tCountry) : login(tl), password(tp), name(tn), country(tCountry) {}
public:
	string getLogin()
	{
		return login;
	}
	bool checkPass(string pass)
	{
		if (pass == password)
		{
			return true;
		}
		return false;
	}
	friend std::istream& operator>>(std::istream& is, User& u)
	{
		is >> u.login >> u.password;
		std::getline(is >> std::ws, u.name);
		is >> u.country;
		return is;
	}
};

class SimpleUser: public User
{
	string creditCard;
	Rate BuyerRate;
	Rate SellerRate;
	bool checkedSeller;
public:
	SimpleUser() : checkedSeller(false) {}
	SimpleUser(string name) : User(name), checkedSeller(false) {}
	SimpleUser(string tl, string tp, string tn, Country tCountry, string tc) : User(tl, tp, tn, tCountry), creditCard(tc) {}
	friend std::istream& operator>>(std::istream& is, SimpleUser& s)
	{
		is >> (User&)s >> s.creditCard;
		return is;
	}
};

class Admin : public User
{
public:
};