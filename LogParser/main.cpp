#include <fstream>
#include <iostream>
#include <string>
#include <map>
#include <vector>
#include <algorithm>
#include <sstream>


using namespace std;

class Pair
{
public:
	Pair(int,string);
	int key;
	string val;
};

Pair::Pair(int key, const string val)
{
	this->key=key;
	this->val=val;
}

bool operator < (const Pair& p1, const Pair& p2) // here is some rediscovery of wheel
{
	return p1.key>p2.key;
}

map<string,int> Errors;
map<string,int> Exceptions;

vector<Pair> ErrorsVec;
vector<Pair> ExceptionsVec;

string ScriptsPath;

int split(string& line, vector<string>& vec, string sepchar) // more wheels
{
	int lpos=0;
	int len=1;
	int pos=line.find(sepchar,0);
	for(;;pos=line.find(sepchar,lpos))
	{
		if(pos<0) break;
		len++;
		vec.push_back(line.substr(lpos,pos-lpos));
		lpos=pos+1;
	}

	vec.push_back(line.substr(lpos,pos-lpos));
	return len;
}

void ParseLine(string& line)
{
	int type=0;
	int pos=line.find("Script error: ",0);
	if(pos>=0) type=1;
	else
	{
		pos=line.find("Script exception: ",0);
		if(pos>=0) type=2;
	}
	if(type==0) return;
	pos=line.find(": ",pos)+2;
	int lpos=pos;
	for(int i=0;i<4;i++) lpos=line.find(": ",lpos)+1;
	string nline=line.substr(pos,lpos-pos-2);
	//cout << nline << "|" << endl;
	if(type==1)
	{
		if(Errors.count(nline)) Errors[nline]++;
		else Errors[nline]=1;
	}
	else
	{
		if(Exceptions.count(nline)) Exceptions[nline]++;
		else Exceptions[nline]=1;
	}
}

void AddLine(ofstream& ofs, string& line)
{
	int pos=0;
	pos=line.find(": ",0)+2;
	int npos=0;
	npos=line.find(": ",pos);
	string modulename=line.substr(pos,npos-pos-1);
	npos=line.find(": ",npos+1)+2;
	pos=line.find(",",npos);
	string linenum=line.substr(npos,pos-npos);
	stringstream strStream(linenum);
	int targetline=0;
	strStream >> targetline;
	string fullname=ScriptsPath+"\\"+modulename+".fosp";
	ifstream ifs(fullname.c_str());
	if(!ifs.good())
	{
		ifs.close();
		return;
	}

	string fline;
	int curline=1;
	while(getline(ifs,fline))
	{
		vector<string> vec;
		split(fline,vec,"\r");
		for(vector<string>::iterator it=vec.begin(), end=vec.end();it!=end;it++)
		{
			if(curline==targetline)
			{
				ofs << "\t" << *it << endl;
				ifs.close();
				return;
			}
			curline++;
		}
	}

	ifs.close();
}

void WriteReport()
{
	ofstream ofs("LogParser.log");
	if(!ofs.good())
	{
		cout << "Failed to write to LogParser.log." << endl;
		return;
	}

	int errors=0;
	int exceptions=0;

	for(map<string,int>::iterator it=Errors.begin(),end=Errors.end();it!=end;it++)
	{
		ErrorsVec.push_back(Pair(it->second,it->first));
		errors+=it->second;
	}

	for(map<string,int>::iterator it=Exceptions.begin(),end=Exceptions.end();it!=end;it++)
	{
		ExceptionsVec.push_back(Pair(it->second,it->first));
		exceptions+=it->second;
	}

	sort(ErrorsVec.begin(),ErrorsVec.end());
	sort(ExceptionsVec.begin(),ExceptionsVec.end());

	ofs << "Errors (" << errors << " total in " << Errors.size() << " groups):" << endl;
	for(vector<Pair>::iterator it=ErrorsVec.begin(),end=ErrorsVec.end();it!=end;it++)
	{
		ofs << it->key << " : " << it->val << endl;
		AddLine(ofs, it->val);
	}

	ofs << endl << "Exceptions (" << exceptions << " total in "<< Exceptions.size() << " groups):" << endl;
	for(vector<Pair>::iterator it=ExceptionsVec.begin(),end=ExceptionsVec.end();it!=end;it++)
	{
		ofs << it->key << " : " << it->val << endl;
		AddLine(ofs, it->val);
	}

	ofs.close();
	cout << "Log saved to LogParser.log" << endl;
}

int main(int argc, char** argv)
{
	if(argc>1)
	{
		if(argc==2)
		{
			cout << "Too few arguments" << endl;
			return 1;
		}
		ScriptsPath.assign(argv[2]);
	}
	else ScriptsPath.assign(".\\scripts");
	ifstream ifs(argc<2?"FOnlineServer.log":argv[1]);
	if(!ifs.good())
	{
		ifs.close();
		cout << "Failed to open the file." << endl;
		return 1;
	}
	string line;
	while(getline(ifs,line))
	{
		vector<string> vec;
		split(line,vec,"\r");
		for(vector<string>::iterator it=vec.begin(), end=vec.end();it!=end;it++) ParseLine(*it);
	}
	ifs.close();
	WriteReport();
	cout << "Errors: " << Errors.size() << ", exceptions: " << Exceptions.size() << "." << endl;
	return 0;
}