// ca-console.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <map>
#include <vector>
#include <string>
#include <iostream>
#include <fstream>

using namespace std;

struct Func
{
	string Module;
	string Name;
	Func(char* mod, char* name) : Module(mod), Name(name) {}
};

struct CallPath
{
	int Id;
	int Incl;
	int Excl;
	map<int,CallPath*> Children;
	CallPath(int id) : Id(id), Incl(1), Excl(0) {}
	void StackEnd() { Excl++; }
	CallPath* AddChild(int id);

};

typedef map<int,CallPath*> PathMap;
typedef map<int,Func*> FuncMap;
typedef vector<int> Callstack;

PathMap CallPaths;
FuncMap Functions;
int TotalCallPaths = 0;

CallPath* CallPath::AddChild(int id)
{
	CallPath* child=NULL;
	PathMap::iterator it=Children.find(id);
	if(it==Children.end())
	{
		child = new CallPath(id);
		Children[id]=child;
		return child;
	}
	it->second->Incl++;
	return it->second;
}

void ProcessCallStack(Callstack& stack)
{
	int top = stack.back();
	PathMap::iterator it = CallPaths.find(top);
	CallPath* path = NULL;
	if(it==CallPaths.end())
	{
		path = new CallPath(top);
		CallPaths[top] = path;
	}
	else
	{
		path = it->second;
		path->Incl++;
	}

	for (int i = stack.size() - 2; i >= 0; i--)
        path = path->AddChild(stack[i]);

    path->StackEnd();
}

// source c&p
typedef unsigned int uint;
struct OutputLine
{
    string FuncName;
    uint   Depth;
    float  Incl;
    float  Excl;
    OutputLine( char* text, uint depth, float incl, float excl ): FuncName( text ), Depth( depth ), Incl( incl ), Excl( excl ) {}
};

void TraverseCallPaths(CallPath* path, vector< OutputLine >& lines, uint depth, uint& max_depth, uint& max_len )
{
    Func* func = Functions[path->Id];
    char               name[ 2048 ] = { 0 };
    if( func )
		sprintf( name, "%s@%s", func->Module.c_str(), func->Name.c_str() );
    else
        strncpy( name, "???\0", 4);

    lines.push_back( OutputLine( name, depth,
                                 100.0f * (float) ( path->Incl ) / float(TotalCallPaths),
                                 100.0f * (float) ( path->Excl ) / float(TotalCallPaths) ) );

    uint len = strlen( name ) + depth;
    if( len > max_len )
        max_len = len;
    depth += 2;
    if( depth > max_depth )
        max_depth = depth;

    for( auto it = path->Children.begin(), end = path->Children.end(); it != end; ++it )
        TraverseCallPaths( it->second, lines, depth, max_depth, max_len );
}

string GetStatistics()
{
    string               result;

    vector< OutputLine > lines;
    uint                 max_depth = 0;
    uint                 max_len = 0;
    for( auto it = CallPaths.begin(), end = CallPaths.end(); it != end; ++it )
        TraverseCallPaths( it->second, lines, 0, max_depth, max_len );

    char buf[ 2048 ] = { 0 };
    sprintf( buf, "%-*s Inclusive %%  Exclusive %%\n\n", max_len, "" );
    result += buf;

    for( uint i = 0; i < lines.size(); i++ )
    {
        sprintf( buf, "%*s%-*s   %6.2f       %6.2f\n", lines[ i ].Depth, "",
                     max_len - lines[ i ].Depth, lines[ i ].FuncName.c_str(),
                     lines[ i ].Incl, lines[ i ].Excl );
        result += buf;
    }
    return result;
}

int _tmain(int argc, _TCHAR* argv[])
{
	if(argc<2)
	{
		cout << "Usage: ca-console intput_file" << endl;
		return 1;
	}

	ifstream file;
	file.open(argv[1], ios::in|ios::binary);
	if(!file.is_open())
	{
		cout << "File: " << argv[1] << " not found." << endl;
		return 1;
	}

	int magic;
	file.read((char*)&magic,4);
	if(magic!=0x10ADB10B)
	{
		cout << "File: " << argv[1] << " is not a correct profiler save." << endl;
		return 1;
	}

	file.read((char*)&magic,4); // version

	// skip modules
	char module[2048];
	while(true)
	{
		file.getline(module, 0xFFFFFFFF, '\0');
		if(!strlen(module)) break;
		file.ignore(0xFFFFFFFF, '\0');
	}

	// load functions
	char name[2048];
	int id;
	while(true)
	{
		file.read((char*)&id,4);
		if(id==-1) break;
		file.getline(module, 0xFFFFFFFF, '\0');
		if(!strlen(module)) break;
		file.getline(name, 0xFFFFFFFF, '\0');
		Functions[id]=new Func(module,name);
	};

	// load calls
	int line;
    Callstack stack;
	while(!file.eof())
    {
        file.read((char*)&id,4);
        if (id != -1)
        {
			file.read((char*)&line,4);
			stack.push_back(id);
            continue;
        }

        ProcessCallStack(stack);
		TotalCallPaths++;
		stack.clear();
    }

	if(!TotalCallPaths)
	{
		cout << "No calls were recorded." << endl;
		return 0;
	}
	cout << ::GetStatistics() << endl;

	return 0;
}

