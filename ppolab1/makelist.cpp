#include "makelist.h"

char bufStr[1024];

void makeList(list<string>& l, ifstream& f)
{
	l.clear();
	while (1)
	{
		f.getline(bufStr, sizeof(bufStr));
		if (f.eof()) break;
		string s = bufStr;
		l.push_back(s);
	}
}
