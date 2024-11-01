
#include <windows.h>
#include <iostream>
#include <fstream>
#include <string>
#include <list>
#include <map>
#include "makelist.h"
#include "printlist.h"
#include "printmap.h"
#include "processlist.h"

using namespace std;

list<string> listStr;
map<int, string> mapWord;


int main(int argc, char* argv[])
{
    setlocale(LC_ALL, "Russian");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    if (argc == 2)
    {
        cout << "Имя входного файла: " << argv[1] << endl;
    }
    else
    {
        cout << "Ошибка: имя файла не задано" << endl;
        return 1;
    }

    ifstream f;
    f.open(argv[1]);
    if (!f)
    {
        cout << "Ошибка открытия файла" << endl;
        return 1;
    }

    makeList(listStr, f);
    cout << "Содержимое входного файла:" << endl;
    printList(listStr);
    cout << "--------------------" << endl;
    cout << "Обработанные данные:" << endl;
    processList(listStr, mapWord);
    printMap(mapWord);

    f.close();
    Sleep(1000);
    return 0;
}
