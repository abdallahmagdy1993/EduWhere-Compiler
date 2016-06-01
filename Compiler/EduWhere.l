%{
	#include<stdio.h>
	#include <math.h>
	#include <string.h>
	int c;
	#include "NodeType.h"
	#include "y.tab.h"
	int yylineno=1;
	
%}

DIGIT    [1-9][0-9]*|"0"
FLOATDIGIT [1-9][0-9]*"."[0-9]+"f"?|"0"?"."[0-9]+"f"?|[1-9][0-9]*"f"|"0f"
OCTADIGIT "0"[0-7]+
HEXADIGIT "0x"[0-9A-F]+
CHARC    \'.\'
ID       [a-zA-Z][a-zA-Z0-9]*

%%

 /***************************************** DATATYPES *************************************************/

"int" {
	//printf( "integer datatype \n");
	return INTEGER;
}
"float" {
	//printf( "float datatype \n");
	return FLOAT;
}
"char" {
	//printf( "character datatype \n");
	return CHAR;
}
"const" {
	//printf( "const datatype \n");
	return CONST;
}
"bool" {
	//printf( "boolean datatype \n");
	return BOOLEAN;
}

 /***************************************** KeyWords *******************************************/

"if"|"then"|"else"|"while"|"whileuntil"|"for"|"repeat"|"do"|"until"|"switch"|"case"|"break"|"default" {
        //printf( "A keyword: %s\n", yytext );
	if (!strcmp(yytext,"if")){ 
	      //printf("IF\n");
		return IF;
		}
		else if(!strcmp(yytext,"then")){
		//printf("Then\n");
		return THEN;
		}
        else if(!strcmp(yytext,"else")){
		//printf("Else\n");
		return ELSE;
		}
        else if(!strcmp(yytext,"while")){
		//printf("While\n");
		return WHILE;
		}
        else if(!strcmp(yytext,"whileuntil")){
		//printf("WhileUntil\n");
		return WHILEUNTIL;
		}
        else if(!strcmp(yytext,"for")){
		//printf("For\n");
		return FOR;
		}
        else if(!strcmp(yytext,"switch")){
		//printf("Switch\n");
		return SWITCH;
		}
        else if(!strcmp(yytext,"case")){
		//printf("Case\n");
		return CASE;
		}
		else if(!strcmp(yytext,"break")){
		//printf("Sreak\n");
		return BREAK;
		}
		else if(!strcmp(yytext,"default")){
		//printf("Default\n");
		return DEFAULT;
		}
		else if(!strcmp(yytext,"repeat")){
		//printf("Default\n");
		return REPEAT;
		}
		else if(!strcmp(yytext,"do")){
		//printf("Default\n");
		return DO;
		}
		else if(!strcmp(yytext,"until")){
		//printf("Default\n");
		return UNTIL;
		}
	}

"false"|"true" {
	//printf( "BOOLEAN: %s\n", yytext );
	if (!strcmp(yytext,"false")){
                //printf("False\n");
		return FALSE;
		}
        else if(!strcmp(yytext,"true")){
		//printf("True\n");
		return TRUE;
		}
}

 /***************************************** Operators *******************************************/

">"|"<"|">="|"<="|"=="|"!=" {
	if (!strcmp(yytext,">")){
                //printf("GreaterThan\n");
		return GT;
		}
        else if(!strcmp(yytext,"<")){
		//printf("LessThan\n");
		return LT;
		}
        else if(!strcmp(yytext,">=")){
		//printf("GreaterThanOrEqual\n");
		return GE;
		}
        else if(!strcmp(yytext,"<=")){
		//printf("LessThanOrEqual\n");
		return LE;
		}
        else if(!strcmp(yytext,"==")){
		//printf("IsEqual\n");
		return ISEQUAL;
		}
        else if(!strcmp(yytext,"!=")){
		//printf("NotEqual\n");
		return NOTEQUAL;
		}
}

"&&" {
	//printf("LogicalAnd: %s\n",yytext);
	return LOGICALAND;        
}

"||" {
	//printf("LogicalOR: \n",yytext);
	return LOGICALOR;
}

 /***************************************** Special Characters *******************************************/

[ \t]+        /* ignore whitespace */
\n	{yylineno++;}

"/*".*"*/"|"//"[^\n]* {	
	//printf("Comment\n");
	return COMMENT;
}

 /***************************************** Variables *************************************************/


{DIGIT}    {       
		//printf( "An integer: %s (%s)\n", yytext,yytext );
		yylval.charpointval = strdup(yytext); 
		return INTEGERVALUE;
		}

{FLOATDIGIT}      {
	    if(yytext[strlen(yytext)-1] == 'f')
	    {
		yytext[strlen(yytext)-1]=0;
	    }
            //printf( "A float: %s (%s)\n", yytext,yytext );   
		    yylval.charpointval = strdup(yytext); 
		    return FLOATVALUE;
            }

{OCTADIGIT} {
		//printf( "An Octal Number: %s \n", yytext );
		yylval.charpointval= strdup(yytext); 
		return OCTAVALUE;
}

{HEXADIGIT} {
		//printf( "A Hex Number: %s \n", yytext );
		yylval.charpointval= strdup(yytext); 
		return HEXAVALUE;
}

{CHARC}     {//printf( "A Character: %s\n", yytext );
		yylval.charpointval=strdup(yytext);
		return CHARVALUE;
		}

{ID}        {//printf( "An identifier: %s\n", yytext );
		yylval.charpointval=strdup(yytext);
		return IDENTIFIER;
	}

  /**************** to return non tokens ********/

[^a-z0-9\b\n\t ]    {
                 c = yytext[0];
                 return(c);
               }
%%

 /*
main(){
yylex();
}

 */