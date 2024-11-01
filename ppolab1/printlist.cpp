#include "printlist.h"

void printList(list<string>& l)
{
	list<string>::const_iterator it;
	it = l.cbegin();
	while (it != l.cend())
	{
		cout << *it << endl;
		it++;
	}
}

