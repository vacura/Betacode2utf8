# Betacode2utf8
Convertor from betacode to UTF8 for LaTeX.
(If you dont understand it, then you dont need it :)

Usage: betacode2utf8 infile outfile [command]

Command is optional, it is useless now, but may be usefull in future.
Note: If infile already uses some special characters - like diacritical marks (žluťoučký kůň) - it has to be in UTF8 format, not ANSI etc.
What it does? Goes thru infile, finds all isntances of \betacode{something} or \bcode{something} and converts then to UTF-8 equivalent and save resulting outfile.

Example: betacode2utf8.exe myinfile.tex myoutfile.tex

Content of myinfile.tex
It is important to remember know all \bcode{ge/nh tou= o(/ntos}. Or maybe \bcode{ge/nh tw=n kategorion}? Or as Plato called them sometimes \bcode{me/gista ge/nh}). You should therefore read "\bcode{Peri\ tw=n genw=n tou= o)/ntos}"!!

Content of myoutfile.tex
It is important to remember know all γένη τοῦ ὅντοσ. Or maybe γένη τῶν κατεγοριον? Or as Plato called them sometimes μέγιστα γένη). You should therefore read "περὶ τῶν γενῶν τοῦ ὄντοσ"!!


