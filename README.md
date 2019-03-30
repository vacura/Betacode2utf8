# Betacode2utf8
Convertor from betacode to UTF8 for LaTeX.
(If you dont understand it, then you dont need it :)

#Usage 
betacode2utf8 infile outfile [command]

Command is optional, it is useless now, but may be usefull in future.
**Note:** If infile already uses some special characters - like diacritical marks (žluťoučký kůň) - it has to be in UTF8 format, not ANSI etc.

**What it does?** Goes thru infile, finds all instances of \betacode{something} or \bcode{something} and converts them to UTF-8 equivalent. Then it saves result to outfile.

#Example 
betacode2utf8.exe myinfile.tex myoutfile.tex

Content of myinfile.tex
It is important to remember know all \bcode{ge/nh tou= o(/ntos}. Or maybe \bcode{ge/nh tw=n kategorion}? Or as Plato called them sometimes \bcode{me/gista ge/nh}). You should therefore read "\bcode{Peri\ tw=n genw=n tou= o)/ntos}"!!

Content of myoutfile.tex
It is important to remember know all γένη τοῦ ὅντοσ. Or maybe γένη τῶν κατεγοριον? Or as Plato called them sometimes μέγιστα γένη). You should therefore read "περὶ τῶν γενῶν τοῦ ὄντοσ"!!

#Platform
Code uses .NET Core 2.1.
Resulting binaries can be used on Windows 10, Linux and Mac.

#Binaries
##Windows 10: 
* Dowload binary: http://www.vacura.cz/Downloads/Betacode2utf8-Win10-x64.zip
* Run application using "betacode2utf8.exe myinfile.tex myoutfile.tex"

##Linux and Mac:
* Dowload binary: http://www.vacura.cz/Downloads/Betacode2utf8-net.core.2.1.zip
* Download .NET Core Framework RUNTIME of your OS: https://dotnet.microsoft.com/download
* Run application using "dotnet betacode2utf8.DLL myinfile.tex myoutfile.tex"

#References
I used this old PHP project as a starting point:
https://sourceforge.net/projects/betacodeconvert/
