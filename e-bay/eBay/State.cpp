#include"State.h"
#include"Shop.h"
#include"ViewManager.h"

BaseState* MainMenuState::update()
{
	BaseState* newState = nullptr;
	if (success())
	{
		switch (strategy->result())
		{
		case '1': newState = new LogInState(Shop::getUManager());
		}
	}
	else
	{
		newState = new MainMenuState;
	}
	return newState;
}

BaseState* LogInState::update()
{
	BaseState* newState = nullptr;
	if (success())
	{
		newState = new UserLoggedState(_currUser);
	}
	else
	{
		newState = new LogInState(Shop::getUManager());
	}
	return newState;
}

BaseState* UserLoggedState::update()
{
	BaseState* newState = nullptr;
	if (success())
	{
		PAUSE;
	}
	else
	{
		newState = new UserLoggedState(_currUser);
	}
	return newState;
}