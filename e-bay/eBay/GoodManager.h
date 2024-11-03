#pragma once
#include<iostream>
#include<fstream>
#include<string>
#include"Good.h"
#include"UserManager.h"
#include<vector>
#include"Additional.h"
using namespace std;

using std::string;

class BaseCategory
{
public:
	virtual string what()const = 0;
	virtual string getType()const = 0;
	virtual void addGood(Good* good) = 0;
};

class CompositeCatalog : public BaseCategory
{
	string type;
	std::vector<BaseCategory*> categories;
	friend class GoodManager;
public:
	CompositeCatalog() : type("All") {}
	CompositeCatalog(string tType) : type(tType) {}
	string what()const
	{
		return "Catalog";
	}
	string getType()const
	{
		return type;
	}
	void addGood(Good* good)
	{
		throw "Trying to add Good to category!!!";
	}
	void addCategory(BaseCategory* good)
	{
		categories.push_back(good);
	}
	void addByCategory(Good* good, string category)
	{
		for (auto it : categories)
		{
			if (it->what() == "Category")
			{
				if (it->getType() == category)
				{
					it->addGood(good);
					return;
				}
			}
			else
			{
				CompositeCatalog* receivedCat = dynamic_cast<CompositeCatalog*>(it);
				receivedCat->addByCategory(good, category);
			}
		}
	}
};

class CompositeCategory: public BaseCategory
{
	string type;
	vector<Good*> goods;
	friend class GoodManager;
public:
	CompositeCategory(string tType) : type(tType) {}
	string what()const
	{
		return "Category";
	}
	string getType()const
	{
		return type;
	}	
	void addGood(Good* good)
	{
		goods.push_back(good);
	}
};

class GoodManager
{
	CompositeCatalog catalog;
	UserManager* uManager;
public:
	GoodManager(UserManager* manager) : uManager(manager) {}

	void createCategories()
	{
		for (auto it : catalog::categories)
		{
			CompositeCategory* category = new CompositeCategory(it);
			catalog.addCategory(category);
		}
	}
	void download();
};