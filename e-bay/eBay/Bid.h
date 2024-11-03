#pragma once

#include<string>
#include<time.h>
#include"User.h"

using std::string;

class Bid
{
	User* creator;
	unsigned monAmount;
	time_t creationTime;
};