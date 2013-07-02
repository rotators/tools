#pragma warning (disable:4996)
#include <stdio.h>
#include <windows.h>
#include <tchar.h>
#include <string>
#include <map>
#include <vector>
#include <ctype.h>
#include <set>

using namespace std;

typedef map<string,string> MapStrStr;
typedef map<int,string> MapIntStr;
typedef vector<string> StrVec;
typedef vector<int> IntVec;
typedef set<string> StrSet;

const char* def_cfg = "ProjectCreator.cfg";

#define KEYS_LENGTH	(9)
char* Keys[] =
{
	"ServerProjectFile",
	"ClientProjectFile",
	"MapperProjectFile",
	"ScriptsDirectory",
	//"FilesRelativePath",
	"ServerModulesFilter",
	"ServerDisabledModulesFilter",
	"ServerHeadersFilter",
	"ClientModulesFilter",
	"ClientDisabledModulesFilter",
	"ClientHeadersFilter",
	"MapperModulesFilter",
	"MapperDisabledModulesFilter",
	"MapperHeadersFilter"
};


MapStrStr Map;
StrVec ProjLines;
MapIntStr ProjAssoc;
StrSet Modules;
StrSet DisabledModules;
StrSet Headers;

StrSet Defines;

int isSpace(int c)
{
	if(c<0) return 0;
	return isspace(c);
}

string stolower(string st)
{
	for(int i=0,j=st.length();i<j;i++) st[i]=tolower(st[i]);
	return st;
}

void ParseBuf(char* buf)
{
	string line(buf);
	if(!line.length() || !line[0]) return;
	int pos=0;
	while(isSpace(line[pos]) && pos<(int)line.length()) pos++;
	line.replace(0,pos,"");
	if(!line.length() || line[0]=='#') return;
	pos=line.find("=");
	string key=line.substr(0,pos);
	pos++;
	string val=line.substr(pos,line.length()-pos-1);
	Map[key]=val;
}

int BufState=0;
void ParseBufVC(char* buf, const string& filter_modules, const string& filter_disabled, const string& filter_headers)
{
	static bool ignore_files=false;
	string line(buf);
	switch(BufState)
	{
	case 0: // expect filter start (set state to 1) or just a line (state =0)
		{
			if(line.find("<Filter")!=-1) BufState++;
			ProjLines.push_back(line);
			break;
		}
	case 1: // found filter, read the name and maybe start ignoring files, state++
		{
			int from=line.find("\"")+1;
			int to=line.find("\"",from);
			string name=line.substr(from,to-from);
			if(name==filter_modules || name==filter_headers || name==filter_disabled)
			{
				ProjAssoc[ProjLines.size()+4]=name;
				ignore_files=true;
			}
			BufState++;
			ProjLines.push_back(line);
			break;
		}
	case 2: // state++
	case 3: // state++
	case 4: // state++
		{
			BufState++;
			ProjLines.push_back(line);
			break;
		}
	case 5: // expect file or end of filter (back to 0, stop ignoring files)
		{
			// end of filter?
			if(line.find("</Filter>")!=-1)
			{
				BufState=0;
				ignore_files=false;
				ProjLines.push_back(line);
				break;
			}
			// file, pass
		}
	case 6: // state++
	case 7: // state++
	case 8: // state++
		{
			if(!ignore_files) ProjLines.push_back(line);
			BufState++;
			if(BufState>8) BufState=5;
			break;
		}
	default: ;
	}
}

int strsplit(string& st, StrVec& vec)
{
	vec.clear();
	int last_pos=0;
	int pos=0;
	int max_pos=st.size();
	while(isSpace(st[pos]) && pos<max_pos) pos++;
	while(pos<max_pos)
	{
		last_pos=pos;
		while(!isSpace(st[pos]) && pos<max_pos) pos++;
		vec.push_back(st.substr(last_pos,pos-last_pos));
		while(isSpace(st[pos]) && pos<max_pos) pos++;
	}
	return vec.size();
}

int strsplit_slash(string& st, StrVec& vec)
{
	vec.clear();
	int last_pos=0;
	int pos=0;
	int max_pos=st.size();
	while(pos<max_pos)
	{
		last_pos=pos;
		while(st[pos]!='/' && pos<max_pos) pos++;
		vec.push_back(st.substr(last_pos,pos-last_pos));
		while(st[pos]=='/' && pos<max_pos) pos++;
	}
	return vec.size();
}


string trimmedName(string& s)
{
	//printf("splitting %s\n",s.c_str());
	StrVec vec;
	int n=strsplit_slash(s, vec);

	//for(int i=0;i<n;i++) printf("  '%s'\n",vec[i].c_str());

	unsigned int i=0;
	while(i<vec.size())
	{
		if(i>0)
		{
			if(vec[i]==".." && vec[i-1]!="..")
			{
				i--;
				vec.erase(vec.begin()+i,vec.begin()+i+2);
				continue;
			}
		}

		i++;
	}
	if(!vec.size()) return "";
	string ret="";
	for(unsigned int i=0;i<vec.size();i++)
	{
		ret+=vec[i];
		if(i+1!=vec.size()) ret+="/";
	}
	return ret;
}

int IgnoreDepth;
int Depth;
#define IGNORING	(Depth>IgnoreDepth)
bool RecursiveParse(string& file_name, string& dir)
{
	static char buf[4096];

	file_name=Map["ScriptsDirectory"]+dir+file_name;
	FILE* f=fopen(file_name.c_str(),"r");
	if(!f)
	{
		printf("Couldn't open %s for reading.\n",file_name.c_str());
		return false;
	}
	while(fgets(buf,4096,f))
	{
		int pre=0;
		while(buf[pre] && isSpace(buf[pre])) pre++;
		if(!buf[pre] || buf[pre]!='#') continue;

		// buf[pre] == '#'
		int post = pre + 1;
		while(buf[post] && isSpace(buf[post])) post++;
		if(!buf[post]) continue;

		string line(buf);
		line.erase(pre, post - pre);

		StrVec vec;
		strsplit(line,vec);
		if(!vec[0].compare("define") && !IGNORING)
		{
			if(vec.size()<1) continue; // err
			string def=vec[1];
			int pos=def.find("@");
			if(pos>=0) def=def.substr(0,pos);
			Defines.insert(def);
			continue;
		}
		else if(!vec[0].compare("include") && !IGNORING)
		{
			if(vec.size()<1) continue; // err
			string incname=vec[1];
			if(incname[0]!='\"' || incname[incname.size()-1]!='\"') continue;
			incname=stolower(incname.substr(1,incname.size()-2));
			int idx=incname.find_last_of("/");
			string sdir = dir;
			if(idx!=-1)
			{
				sdir = dir + incname.substr(0,idx+1);
				incname = incname.substr(idx+1,incname.length()-idx-1);
			}

			if(!Headers.count(trimmedName(sdir+incname)))
			{
				Headers.insert(trimmedName(sdir+incname));
				printf("\tFound new header: '%s'\n",incname.c_str());
			}

			if(!RecursiveParse(incname, sdir))
				printf("Ignoring the include file %s.\n", incname.c_str());

			continue;
		}
		else if(!vec[0].compare("ifdef"))
		{
			if(vec.size()<1) continue; // err
			string def=vec[1];
			int pos=def.find("@");
			if(pos>=0) def=def.substr(0,pos);
			if(Defines.count(def)) IgnoreDepth++;
			Depth++;
			continue;
		}
		else if(!vec[0].compare("ifndef"))
		{
			if(vec.size()<1) continue; // err
			string def=vec[1];
			int pos=def.find("@");
			if(pos>=0) def=def.substr(0,pos);
			if(!Defines.count(def)) IgnoreDepth++;
			Depth++;
			continue;
		}
		else if(!vec[0].compare("endif"))
		{
			Depth--;
			if(Depth<0) Depth=0; // err
			if(Depth<IgnoreDepth) IgnoreDepth--;
		}
		else if(!vec[0].compare("undef") && !IGNORING)
		{
			if(vec.size()<1) continue; // err
			string def=vec[1];
			int pos=def.find("@");
			if(pos>=0) def=def.substr(0,pos);
			Defines.erase(def);
			continue;
		}
	}
	fclose(f);
	return true;
}

bool ProcessComponent(const char* comp_name, const string& vc_name, const string& filter_modules, const string& filter_disabled, const string& filter_headers, const char* define)
{
	ProjLines.clear();
	ProjAssoc.clear();
	int comp_len=strlen(comp_name);
	char buf[1024];
	printf("Processing %s...\n",comp_name);
	FILE* vc_file=fopen(vc_name.c_str(),"r");
	if(!vc_file)
	{
		printf("Couldn't open %s for reading.\n",vc_name.c_str());
		return false;
	}

	BufState=0;
	while(fgets(buf,1024,vc_file))
	{
		ParseBufVC(buf,filter_modules,filter_disabled,filter_headers);
	}
	fclose(vc_file);

	string script_name=Map["ScriptsDirectory"];
	script_name+="scripts.cfg";

	FILE* script_file=fopen(script_name.c_str(),"r");
	if(!script_file)
	{
		printf("Couldn't open %s for reading.\n",script_name.c_str());
		return false;
	}

	Modules.clear();
	DisabledModules.clear();
	Headers.clear();

	while(fgets(buf,1024,vc_file))
	{
		int pos=0;
		bool enabled=false;
		string line(buf);
		StrVec vec;
		int tok=strsplit(line,vec);
		if(!tok) continue;
		if(!vec[0].compare("@")) { pos++; enabled=true; };
		if(tok<pos+3) continue;
		if(!vec[pos].compare(comp_name) && !vec[pos+1].compare("module"))
		{
			if(enabled) Modules.insert(stolower(vec[pos+2]+".fos"));
			else DisabledModules.insert(stolower(vec[pos+2]+".fos"));
		}
	}
	fclose(script_file);

	for(StrSet::iterator it=Modules.begin(),end=Modules.end();it!=end;++it)
	{
		Defines.clear();
		Defines.insert(string(define));
		IgnoreDepth=0;
		Depth=0;
		string name=*it;
		printf("Preprocessing %s...\n",name.c_str());
		int idx=name.find_last_of("/");
		string dir="";
		if(idx!=-1)
		{
			dir=name.substr(0,idx+1);
			name=name.substr(idx+1,name.length()-idx-1);
		}
		if(!RecursiveParse(name,dir))
		{
			printf("Failed to preprocess %s.\n",name.c_str());
			return false;
		}
	}

	// write the project file
	vc_file=fopen(vc_name.c_str(),"w");

	if(!vc_file)
	{
		printf("Couldn't open %s for writing.\n","test.log");
		return false;
	}

	for(int i=0,j=ProjLines.size();i<j;i++)
	{
		if(ProjAssoc.count(i))
		{
			string name=ProjAssoc[i];
			StrSet* the_set=NULL;
			if(!name.compare(filter_modules)) the_set=&Modules;
			else if(!name.compare(filter_disabled)) the_set=&DisabledModules;
			else if(!name.compare(filter_headers))
			{
				the_set=&Headers;
				fprintf(vc_file,
				"\t\t\t<File\n"
				"\t\t\t\tRelativePath=\".\\intellisense");
				if(!strcmp(comp_name,"client")) fprintf(vc_file,"_client");
				else if(!strcmp(comp_name,"mapper")) fprintf(vc_file,"_mapper");
				
				fprintf(vc_file,".h\"\n"
				"\t\t\t\t>\n"
				"\t\t\t</File>\n");
			}
			
			for(StrSet::iterator it=the_set->begin(),end=the_set->end();it!=end;++it)
			{
				fprintf(vc_file,
				"\t\t\t<File\n"
				"\t\t\t\tRelativePath=\"%s%s\"\n"
				"\t\t\t\t>\n"
				"\t\t\t</File>\n",
				Map["FilesRelativePath"].c_str(),it->c_str());
			}
		}

		fprintf(vc_file,"%s",ProjLines[i].c_str());
	}

	fclose(vc_file);
	printf("Processing %s complete.\n",comp_name);
	
	return true;
}

bool ConfirmSettings()
{
	bool ret=true;
	if(!Map.count("FilesRelativePath") || !Map["FilesRelativePath"].compare(""))
	{
		printf("Warning: FilesRelativePath key is empty or absent, ScriptsDirectory will be used instead.\n");
		if(Map.count("ScriptsDirectory")) Map["FilesRelativePath"]=Map["ScriptsDirectory"];
	}
	for(int i=0;i<KEYS_LENGTH;i++)
	{
		if(!Map.count(Keys[i]) || Map[Keys[i]]=="")
		{
			printf("Error: %s key is empty or absent.\n",Keys[i]);
			ret=false;
		}
	}
	string path=Map["ScriptsDirectory"];
	if(path[path.length()-1]!='\\')
	{
		path+="\\";
		Map["ScriptsDirectory"]=path;
	}
	path=Map["FilesRelativePath"];
	if(path[path.length()-1]!='\\')
	{
		path+="\\";
		Map["FilesRelativePath"]=path;
	}
	return ret;
}

int _tmain(int argc, _TCHAR* argv[])
{
	const char* cfg_name;
	if(argc<2) cfg_name=def_cfg;
	else cfg_name=argv[1];

	FILE* cfg_file=fopen(cfg_name,"r");
	if(!cfg_file)
	{
		printf("Couldn't open %s for reading.\n",cfg_name);
		return 1;
	}

	char buf[1024];

	while(fgets(buf,1024,cfg_file))
	{
		ParseBuf(buf);
	}
	fclose(cfg_file);

	if(!ConfirmSettings())
	{
		printf("One on more errors found.\n");
		return 1;
	}

	if(!ProcessComponent("server",Map["ServerProjectFile"],Map["ServerModulesFilter"],Map["ServerDisabledModulesFilter"],Map["ServerHeadersFilter"],"__SERVER"))
		printf("One on more errors while creating server project.\n");
	if(!ProcessComponent("client",Map["ClientProjectFile"],Map["ClientModulesFilter"],Map["ClientDisabledModulesFilter"],Map["ClientHeadersFilter"],"__CLIENT"))
		printf("One on more errors while creating client project.\n");
	if(!ProcessComponent("mapper",Map["MapperProjectFile"],Map["MapperModulesFilter"],Map["MapperDisabledModulesFilter"],Map["MapperHeadersFilter"],"__MAPPER"))
		printf("One on more errors while creating mapper project.\n");
}