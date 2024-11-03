#pragma once

#include"User.h"
#include<iostream>
#include<fstream>
#include"Additional.h"
#include<vector>

using std::vector;

class UserManager
{
	vector<SimpleUser*> _users;
	vector<Admin*> _admins;
	void downloadUsers()
	{
		std::ifstream in(files::users);
		SimpleUser* user = new SimpleUser();
		while (in >> *user)
		{
			_users.push_back(user);
			user = new SimpleUser();
		}
	}
	void downloadAdmins()
	{
		std::ifstream in(files::admins);
		Admin* admin = new Admin();
		while (in >> *admin)
		{
			_admins.push_back(admin);
			admin = new Admin();
		}
	}
public:
	User* belong(string name, string pass)
	{
		for (auto it : _users)
		{
			if (name == it->getLogin())
			{
				if (it->checkPass(pass))
				{
					return it;
				}
			}
		}
		for (auto it : _admins)
		{
			if (name == it->getLogin())
			{
				if (it->checkPass(pass))
				{
					return it;
				}
			}
		}
		return nullptr;
	}
	User* belong(string name)
	{
		for (auto it : _users)
		{
			if (name == it->getLogin())
			{
				return it;
			}
		}
		for (auto it : _admins)
		{
			if (name == it->getLogin())
			{
				return it;
			}
		}
		return nullptr;
	}
	void download()
	{
		downloadUsers();
		downloadAdmins();
	}
};