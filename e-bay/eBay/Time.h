#pragma once

#include<string>
#include<time.h>
#include<iomanip>

using std::string;

class Time
{
	time_t curr;
	tm description;
	string properString;
	string parse() const
	{
		char buffer[20];
		std::strftime(buffer, 20, "%d.%m.%Y %H:%M:%S", &description);
		string parsed(buffer);
		return parsed;
	}
public:
	Time() : curr(time(NULL))
	{
		localtime_s(&description, &curr);
		properString = parse();
	}
	Time(const Time& t) : curr(t.curr), description(t.description), properString(t.properString) {}
	void outputDetails(std::ostream& out)
	{
		out << properString;
	}
	~Time() {}
	friend std::ostream& operator<<(std::ostream& os, const Time& t)
	{
		os << t.curr;
		return os;
	}
	friend std::istream& operator>>(std::istream& is, Time& t)
	{
		is >> t.curr;
		localtime_s(&t.description, &t.curr);
		t.properString = t.parse();
		return is;
	}
};