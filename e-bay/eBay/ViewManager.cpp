#include"ViewManager.h"
#include"View.h"
#include"Additional.h"
#include"Shop.h"

View* ViewManager::setView()
{
	auto stateType = Shop::getState()->getType();
	switch (stateType)
	{
	case MAIN_MENU_STATE: _currView = new MainMenuView; break;
	case LOG_IN_STATE: _currView = new LogInView; break;
	case USER_LOGGED_STATE: _currView = new UserLoggedView; break;
	}
	return _currView;
}