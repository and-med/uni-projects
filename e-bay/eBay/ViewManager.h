#pragma once

#include"View.h"
#include"State.h"

class ViewManager
{
	class View* _currView;
public:
	ViewManager() : _currView(nullptr) {}
	View* setView();
	void show() const
	{
		_currView->show();
	}
	InputStrategy* getInput()
	{
		return _currView->input();
	}
};