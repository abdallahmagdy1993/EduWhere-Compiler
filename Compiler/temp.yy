%{
#include <stdio.h>
#include <string.h>

int r=1;
int lbl =1;
%}
%start program

%union    {
    float floatval;
        char* charpointval;
        }

%token INTEGER FLOAT CHAR CONST BOOLEAN
%token IF THEN ELSE WHILE WHILEUNTIL FOR SWITCH CASE BREAK DEFAULT
%token FALSE TRUE
%token COMMENT
%token INTEGERVALUE OCTAVALUE HEXAVALUE 
%token FLOATVALUE
%token CHARVALUE 
%token IDENTIFIER
%token LT GE GT LE ISEQUAL NOTEQUAL LOGICALAND LOGICALOR

%right '='
%left LOGICALOR
%left LOGICALAND 
%left '|'
%left '^'
%left '&'
%nonassoc ISEQUAL NOTEQUAL
%left '+' '-'
%left '*' '/' '%'
%nonassoc '!'
%left UMINUS  /*supplies precedence for unary minus */   //minus for unary 
%%                   /* beginning of rules section */

program:                                
        |            
        program scope
        |
        program error
         {
           yyerrok;
           return 0;
         }
         ;


scope:  
        scope '{'scope '}' scope
        |
        scopexpr
        ;

scopexpr:
        |
        scopexpr statement
        |
        scopexpr COMMENT
        ;



statement:
        ';'
        |
        declaration ';'                 
        |
        assignment ';'                  
        |
        special ';'
        ;

declaration: 
        type IDENTIFIER                 {
                                        printf("; declaration\n");
                                        printf("%s %s ?\n",$<charpointval>1,$<charpointval>2);
                                        }
        | 
        type IDENTIFIER '=' expr        {
                                        printf("; declaration\n");
                                        printf("%s %s ?\n",$<charpointval>1,$<charpointval>2);
                                        printf("MOV %s %s\n",$<charpointval>2,$<charpointval>4);
                                        }
        |
        CONST type IDENTIFIER '=' expr  {
                                        printf("; const declaration\n");
                                        printf("%s EQU %s\n",$<charpointval>3,$<charpointval>5);
                                        }       //m3tbrenha wan bayte
        ;

type:
        INTEGER                         {
                                        $<charpointval>$="DD";
                                        }
        |
        FLOAT                           {
                                        $<charpointval>$="DD";
                                        }
        |
        CHAR                            {
                                        $<charpointval>$="DB";
                                        }
        |
        BOOLEAN                         {
                                        $<charpointval>$="DB";
                                        }
        ;

assignment:
        IDENTIFIER '=' assignment       {
                                        printf("; assignment\n");
                                        printf("MOV %s %s\n",$<charpointval>1,$<charpointval>3);
                                        }
        |
        IDENTIFIER '=' expr             {
                                        printf("; assignment\n");
                                        printf("MOV %s %s\n",$<charpointval>1,$<charpointval>3);
                                        }
        ;

expr:
        '(' expr ')'                    {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>2);
                                        }
        |
        expr '+' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("ADD %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '-' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("SUB %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '*' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("MUL %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '/' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("DIV %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '%' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("MOD %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr LOGICALAND expr            {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("AND %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr LOGICALOR expr             {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("OR %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '^' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("XOR %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '&' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("AND %s %s\n",str,$<charpointval>3);
                                        }
        |
        expr '|' expr                   {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        printf("OR %s %s\n",str,$<charpointval>3);
                                        }
        |
        '!' expr                        {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup($<charpointval>2);
                                        printf("MOV %s %s\n",str,$<charpointval>2);
                                        printf("NOT %s\n",str);
                                        printf("MOV %s %s\n",$<charpointval>$,str);
                                        }
        |
        '-' expr %prec UMINUS           {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup($<charpointval>2);
                                        printf("MOV %s %s\n",str,$<charpointval>2);
                                        printf("NEG %s\n",str);
                                        printf("MOV %s %s\n",$<charpointval>$,str);
                                        }
        |
        IDENTIFIER                      {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        value                           {
                                        printf("; expression\n");
                                        char str[100];
                                        sprintf(str,"R%d",r);
                                        r=r+1;
                                        $<charpointval>$=strdup(str);
                                        printf("MOV %s %s\n",str,$<charpointval>1);
                                        }
        ;

value:  
        INTEGERVALUE                    {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        OCTAVALUE                       {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        HEXAVALUE                       {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        FLOATVALUE                      {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        CHARVALUE                       {
                                        $<charpointval>$ = strdup($<charpointval>1);
                                        }
        |
        TRUE                            {
                                        $<charpointval>$ = "1";
                                        }
        |
        FALSE                           {
                                        $<charpointval>$ = "0";
                                        }
        ;   

special:
        ifstatement
        |
        whilestatement
        ;

ifstatement:
        IF '(' condition ')' THEN scope {
                                        int lbl1;
                                        printf("\tjz\tLabel%03d\n", lbl1 = lbl++);
                                        printf("Label%03d:\n", lbl1);
                                        }
        |
        IF '(' condition ')' THEN scope ELSE scope
                                        {
                                        int lbl1,lbl2;
                                        printf("\tjz\tLabel%03d\n", lbl1 = lbl++);
                                        printf("\tjmp\tLabel%03d\n", lbl2 = lbl++);
                                        printf("Label%03d:\n", lbl1); 
                                        printf("Label%03d:\n", lbl2);
                                        }               
        ;

whilestatement:
        WHILE '(' condition ')' scope   {
                                        int lbl1,lbl2;
                                        printf("Label%03d:\n", lbl1 = lbl++);
                                        printf("\tjz\tLabel%03d\n", lbl2 = lbl++);
                                        printf("\tjmp\tLabel%03d\n", lbl1);
                                        printf("Label%03d:\n", lbl2);    
                                        }
        ;

condition:
        '(' condition ')'
        |
        '!' condition
        |
        condition LOGICALAND operand
        |
        condition LOGICALOR operand
        |
        operand
        ;
operand:
        expr ISEQUAL expr
        |
        expr NOTEQUAL expr
        |
        expr GT expr 
        |
        expr GE expr
        |
        expr LT expr
        |
        expr LE expr
        ;




%%
main()
{
 return(yyparse());
}
yyerror(s)
char *s;
{
  printf("%s\n",s);
}
yywrap()
{
  return(1);
}
