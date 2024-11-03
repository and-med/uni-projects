#pragma once

#include<iostream>
#include<string>
#include"InputStrategy.h"
#include"User.h"
#include"UserManager.h"

enum StateType{MAIN_MENU_STATE, LOG_IN_STATE, USER_LOGGED_STATE};

class BaseState
{
public:
	virtual void handleInput(class InputStrategy* i) = 0;
	virtual bool success()const = 0;
	virtual BaseState* update() = 0;
	virtual StateType getType() = 0;
};

class MainMenuState : public BaseState
{
	MainMenuInput* strategy;
public:
	MainMenuState() : strategy(nullptr) {}
	void handleInput(InputStrategy* i)
	{
		strategy = dynamic_cast<MainMenuInput*>(i);
	}
	MainMenuInput* getInput()
	{
		return strategy;
	}
	BaseState* update();
	StateType getType()
	{
		return MAIN_MENU_STATE;
	}
	bool success() const
	{
		return (strategy!=nullptr) && (strategy->result() <= '3' && strategy->result() >= '1');
	}
};

class LogInState: public BaseState
{
	User* _currUser;
	UserManager* _users;
public:
	LogInState() : _currUser(nullptr), _users(nullptr) {}
	LogInState(UserManager* manager) : _users(manager) {}
	void handleInput(InputStrategy* i)
	{
		LogInput* strategy = dynamic_cast<LogInput*>(i);
		_currUser = _users->belong(strategy->result(), strategy->getPass());		
	}
	BaseState* update();
	StateType getType()
	{
		return LOG_IN_STATE;
	}
	bool success() const
	{
		return (_currUser != nullptr);
	}
};

class UserLoggedState : public BaseState
{
	User* _currUser;
	InputStrategy* input;
public:
	UserLoggedState() : _currUser(nullptr), input(nullptr) {}
	UserLoggedState(User* currUser) : _currUser(currUser), input(nullptr) {}
	void handleInput(InputStrategy* i)
	{
		input = i;
	}
	BaseState* update();
	StateType getType()
	{
		return USER_LOGGED_STATE;
	}
	bool success() const
	{
		return false;
	}
};

class State
{
	BaseState* state;
public:
	State() : state(new MainMenuState) {}
	void handleInput(class InputStrategy* i)
	{
		state->handleInput(i);
	}
	StateType getType()
	{
		return state->getType();
	}
	bool success()
	{
		return state->success();
	}
	void update()
	{
		BaseState* newState = state->update();
		delete state;
		state = newState;
	}
	BaseState* getState()
	{
		return state;
	}
};