LogParser
======

A small utility for parsing saved server log files and extracting errors and exceptions, grouping them by count, and associating with the corresponding (preprocessed) source file line.

Usage:
LogParser.exe [filename scripts_dir]

If no argument is given, default values are used:
filename=FOnlineServer.log
scripts_dir=.\scripts

scripts_dir is used for fetching the line from the preprocessed file.