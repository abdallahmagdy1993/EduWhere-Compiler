set mypath=%cd%
chdir /d %mypath%
::win_flex phase1.l
::win_bison -dy phase1.y
::gcc lex.yy.c y.tab.c -o phase1.exe
EduWhere.exe < input.txt > output.txt