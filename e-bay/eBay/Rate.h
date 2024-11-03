#pragma once

#include<string>

using std::string;

class Rate
{
	unsigned currRate;
public:
	Rate() : currRate(20) {}

	void increase()
	{
		currRate += 1;
	}

	void decrease()
	{
		currRate -= 1;
	}
};