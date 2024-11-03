#pragma once

#include<iostream>
#include"UserManager.h"
#include"GoodManager.h"
#include"ViewManager.h"
#include"State.h"
#include"Additional.h"

class Shop
{
	UserManager uManager;
	ViewManager vManager;
	GoodManager gManager;
	State state;
	static Shop shop;
	Shop() : uManager(), vManager(), gManager(&uManager) {}
	Shop(const Shop&);
	Shop& operator=(const Shop&);

	static void downloadUsers()
	{
		shop.uManager.download();
		shop.gManager.download();
	}
public:
	static UserManager* getUManager()
	{
		return &shop.uManager;
	}
	static State* getState()
	{
		return &shop.state;
	}
	static bool isEnd()
	{
		return false;
	}

	static void run()
	{
		downloadUsers();
		while (!isEnd())
		{
			shop.vManager.setView();
			shop.vManager.show();
			InputStrategy* input = shop.vManager.getInput();
			shop.state.handleInput(input);
			shop.state.update();
		}
	}
};