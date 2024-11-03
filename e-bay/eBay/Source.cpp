#include<iostream>
#include<fstream>
#include<iomanip>
#include<time.h>
#include<string>
#include"Time.h"
#include"View.h"
#include"Shop.h"
#include<Windows.h>
#include"ViewManager.h"
#include<sstream>
using namespace std;

int main()
{
	ifstream in("History.txt");
	Shop::run();
	system("pause>>void");
}