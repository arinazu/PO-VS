#include "processlist.h"
#include <regex>

void processList(list<string>& l, map<int, string>& a)
{
	regex reg1("[=].*");
	regex reg2("[\*].*");
	regex reg3("[=]*(.*)");
	regex reg4("[\*]*(.*)");
	//regex reg5("[[:alnum:]_�-��-�]{11,}");
	regex reg5("[0-9a-zA-Z_�-��-�]{11,}");
	int num_words = 0;

	list<string>::const_iterator it;
	it = l.cbegin();
	while (it != l.cend())
	{
		string s = *it;
		if (s.size() == 0)
		{
			cout << "�����";
		}
		else if (regex_match(s, reg1))
		{
			cout << "���������: ";
			s = regex_replace(s, reg3, "$1");
		}
		else if (regex_match(s, reg2))
		{
			cout << "������� ������: ";
			s = regex_replace(s, reg4, "$1");
		}
		cout << s << endl;
		it++;

		//������� ���������� ����
		auto pos = s.cbegin();
		auto end = s.cend();
		smatch m;

		while (1)
		{
			bool found = regex_search(pos, end, m, reg5);
			if (!found) break;
			num_words++;
			pos = m.suffix().first;

			//cout << "m.size() = " << m.size() << endl;
			//cout << "m.str() = " << m.str() << endl;
			//cout << "m.length() = " << m.length() << endl;
			//cout << "m.position() = " << m.position() << endl;

			a.insert({ num_words, m.str() });
		}
	}
	cout << "���������� ���� >10: " << a.size() << endl;
}
