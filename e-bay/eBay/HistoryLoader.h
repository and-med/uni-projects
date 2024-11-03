#pragma once

#include<iostream>
#include<string>
#include"Time.h"

using std::string;

class Command
{
	Time madeTime;
public:
	Command() {}
};

class UserCommand : public Command
{

};

class AdminCommand : public Command
{

};

class CreateUser : public UserCommand
{

};

class ChangeDescription : public UserCommand
{ 
	
};