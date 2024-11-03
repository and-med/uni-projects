#pragma once

#include<iostream>
#include<string>
#include<vector>
#include<time.h>
#include"Country.h"
#include"User.h"
#include"Bid.h"
#include"Time.h"
using namespace std;

using std::string;
using std::vector;

class Good
{
	string name;
	string description;
	unsigned amount;
	unsigned price;
	vector<Country> deliverCountries;
	unsigned term;
	User* holder;
	Time startTime;
public:
	virtual string getUserName()
	{
		return holder->getLogin();
	}
	virtual void setUser(User* u)
	{
		delete holder;
		holder = u;
	}
	friend std::istream& operator>>(istream& is, Good& g)
	{
		getline(is >> ws, g.name);
		string description;
		getline(is >> ws, description);
		size_t position = 0;
		string temp;
		if ((position = description.find("\"")) != string::npos)
		{
			while (description.find("\"", position + 1) == string::npos)
			{
				getline(is >> ws, temp);
				description += temp;
			}
			position = description.find("\"", position + 1);
			description.erase(position + 1);
		}
		else
		{
			throw "Incorrect Input";
		}
		g.description = description;
		is >> g.amount >> g.price;
		int numOfCountries = 0;
		is >> numOfCountries;
		Country temporary;
		for (int i = 0; i < numOfCountries; ++i)
		{
			is >> temporary;
			g.deliverCountries.push_back(temporary);
		}
		is >> g.term;
		is >> g.startTime;
		string holderName;
		is >> holderName;
		g.holder = new SimpleUser(holderName);
		return is;
	}
};

class Auction : public Good
{
	unsigned minGrowth;
	vector<Bid> currBids;
public:
	friend std::istream& operator>>(std::istream& is, Auction& a)
	{
		is >> (Good&)a >> a.minGrowth;
		return is;
	}
};

class FixedPrice : public Good
{
	Bid currBid;
public:
	friend std::istream& operator>>(std::istream& is, FixedPrice& f)
	{
		is >> (Good&)f;
		return is;
	}
};