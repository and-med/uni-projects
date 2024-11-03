#include"Shop.h"

void GoodManager::download()
{
	createCategories();
	std::ifstream in(files::goods);
	string categ;
	string type;
	getline(in >> std::ws, categ);
	in >> type;
	while (!in.eof())
	{
		Auction* tAuc = nullptr;
		FixedPrice* tFP = nullptr;
		if (type == "Auction")
		{
			tAuc = new Auction();
			in >> *tAuc;
			User* user = Shop::getUManager()->belong(tAuc->getUserName());
			tAuc->setUser(user);
			catalog.addByCategory(tAuc, categ);
		}
		if (type == "FixedPrice")
		{
			tFP = new FixedPrice();
			in >> *tFP;
			User* user = Shop::getUManager()->belong(tFP->getUserName());
			tFP->setUser(user);
			catalog.addByCategory(tFP, categ);
		}
		getline(in >> std::ws, categ);
		in >> type;
	}
}