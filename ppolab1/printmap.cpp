#include "printmap.h"

void printMap(map<int, string>& m)
{
	map<int, string>::const_iterator it;
	it = m.cbegin();
	while (it != m.cend())
	{
		cout << it->first << '\t' << it->second << endl;
		it++;
	}
}

