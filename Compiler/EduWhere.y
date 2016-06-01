%{
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdarg.h>
#include "ParseTree.h"
//#include "graph.h"

nodeType *opr(int oper, int nops, ...);
nodeType *id(char *i);
nodeType *con(char * value);
void freeNode(nodeType *p);
int ex(nodeType *p);
extern int yylineno;

%}

%union {
    nodeType *nPtr; /* node pointer */  
    char* charpointval;
};

%start program
%token INTEGER FLOAT CHAR CONST BOOLEAN
%token IF THEN ELSE WHILE WHILEUNTIL FOR SWITCH CASE BREAK DEFAULT DO REPEAT UNTIL
%token FALSE TRUE
%token COMMENT
%token INTEGERVALUE OCTAVALUE HEXAVALUE 
%token FLOATVALUE
%token CHARVALUE 
%token IDENTIFIER
%nonassoc IFX
%right '='
%left LOGICALOR
%left LOGICALAND 
%left '|'
%left '^'
%left '&'
%nonassoc ISEQUAL NOTEQUAL
%left LT GE GT LE
%left '+' '-'
%left '*' '/' '%'
%nonassoc '!'
%left UMINUS  /*supplies precedence for unary minus */   //minus for unary 
//%type <nPtr> statement expr stmt_list 
%%                   /* beginning of rules section */

program:
        |                                
        program statement {ex($<nPtr>2); freeNode($<nPtr>2);}
        |
        program error ';'
        {
            yyerrok;
        }
        ;


statement:
        ';'                 {
                            $<nPtr>$ = opr(';',2,NULL,NULL);
                            }                    
        |
        declaration ';'     {$<nPtr>$ = $<nPtr>1;}           
        |
        assignment ';'      {$<nPtr>$ = $<nPtr>1;}                  
        |
        special             {$<nPtr>$ = $<nPtr>1;}
        |
        '{' stmt_list '}'   {$<nPtr>$ = opr('{',1,$<nPtr>2);}
        ; 

stmt_list:
                            {$<nPtr>$ = opr(' ',2,NULL,NULL);}         
        |                   
        statement           {$<nPtr>$ = $<nPtr>1;}
        | 
        stmt_list statement {$<nPtr>$ = opr(';', 2, $<nPtr>1, $<nPtr>2); }
        ;

declaration: 
        type IDENTIFIER                 {
                                        $<nPtr>$ = opr(IDENTIFIER,2,con($<charpointval>1),id($<charpointval>2));
                                        }
        | 
        type assignment        {
                                        $<nPtr>$ = opr(IDENTIFIER,3,con($<charpointval>1),$<nPtr>2,NULL);
                                        }
        |
        CONST type assignment  {
                                        $<nPtr>$ = opr(CONST,2,con($<charpointval>2),$<nPtr>3);
                                        }
        ;

type:
        INTEGER                         {
                                        $<charpointval>$ = "I";
                                        }
        |
        FLOAT                           {
                                        $<charpointval>$ = "F";
                                        }
        |
        CHAR                            {
                                        $<charpointval>$ = "C";
                                        }
        |
        BOOLEAN                         {
                                        $<charpointval>$ = "B";
                                        }
        ;

assignment:
        IDENTIFIER '=' assignment       {
                                        $<nPtr>$ = opr('=', 3, id($<charpointval>1), $<nPtr>3,NULL);
                                        }
        |
        IDENTIFIER '=' expr             {
                                        $<nPtr>$ = opr('=', 2, id($<charpointval>1), $<nPtr>3);
                                        }
        ;

expr:
        '(' expr ')'                    {
                                        $<nPtr>$ = $<nPtr>2;
                                        }
        |
        expr '+' expr                   {
                                        $<nPtr>$ = opr('+', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '-' expr                   {
                                        $<nPtr>$ = opr('-', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '*' expr                   {
                                        $<nPtr>$ = opr('*', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '/' expr                   {
                                        $<nPtr>$ = opr('/', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '%' expr                   {
                                        $<nPtr>$ = opr('%', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr LOGICALAND expr            {
                                        $<nPtr>$ = opr(LOGICALAND, 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr LOGICALOR expr             {
                                        $<nPtr>$ = opr(LOGICALOR, 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '^' expr                   {
                                        $<nPtr>$ = opr('^', 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '&' expr                   {
                                        $<nPtr>$ = opr(LOGICALAND, 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        expr '|' expr                   {
                                        $<nPtr>$ = opr(LOGICALOR, 2, $<nPtr>1, $<nPtr>3);
                                        }
        |
        '!' expr                        {
                                        $<nPtr>$ = opr('!', 1, $<nPtr>2);    
                                        }
        |
        '-' expr %prec UMINUS           {
                                        $<nPtr>$ = opr(UMINUS, 1, $<nPtr>2);
                                        }
        |
        expr ISEQUAL expr               {
                                        $<nPtr>$ = opr(ISEQUAL, 2, $<nPtr>1, $<nPtr>3);   
                                        }
        |
        expr NOTEQUAL expr              {
                                        $<nPtr>$ = opr(NOTEQUAL, 2, $<nPtr>1, $<nPtr>3);   
                                        }       
        |
        expr GT expr                    {
                                        $<nPtr>$ = opr(GT, 2, $<nPtr>1, $<nPtr>3);   
                                        }    
        |
        expr GE expr                    {
                                        $<nPtr>$ = opr(GE, 2, $<nPtr>1, $<nPtr>3);   
                                        }
        |
        expr LT expr                    {
                                        $<nPtr>$ = opr(LT, 2, $<nPtr>1, $<nPtr>3);   
                                        }
        |
        expr LE expr                    {
                                        $<nPtr>$ = opr(LE, 2, $<nPtr>1, $<nPtr>3);   
                                        }
        |
        IDENTIFIER                      {
                                        $<nPtr>$ = id($<charpointval>1);
                                        }
        |
        value                           {
	                                    $<nPtr>$ = $<nPtr>1;                  
                                        }
        ;

value:  
        INTEGERVALUE                    {
                                        $<nPtr>$= opr('v',2,con($<charpointval>1),con("int"));
                                     
                                        }
        |
        OCTAVALUE                       {
                                       $<nPtr>$= opr('v',2,con($<charpointval>1),con("int"));
                                        }
        |
        HEXAVALUE                       {
                                        $<nPtr>$= opr('v',2,con($<charpointval>1),con("int"));
                                        }
        |
        FLOATVALUE                      {
                                        $<nPtr>$= opr('v',2,con($<charpointval>1),con("float"));
                                        }
        |
        CHARVALUE                       {
                                        $<nPtr>$= opr('v',2,con($<charpointval>1),con("char"));
                                        }
        |
        TRUE                            {
                                       $<nPtr>$= opr('v',2,con("1"),con("bool"));
                                        }
        |
        FALSE                           {
                                        $<nPtr>$= opr('v',2,con("0"),con("bool"));
                                        }
        ;   

special:
        ifstatement             { $<nPtr>$ = $<nPtr>1;}
        |
        loopstatement          { $<nPtr>$ = $<nPtr>1;}   
        |  
        switchstatement        { $<nPtr>$ = $<nPtr>1;}
       
        ;

ifstatement:
        IF '(' expr ')' THEN statement%prec IFX 
                                        { 
                                        $<nPtr>$ = opr(IF, 2, $<nPtr>3, $<nPtr>6); 
                                        }
        |
        IF '(' expr ')' THEN statement ELSE statement
                                        {
                                        $<nPtr>$ = opr(IF, 3, $<nPtr>3, $<nPtr>6, $<nPtr>8);
                                        }               
        ;

loopstatement:
        WHILE '(' expr ')' statement
                                       {
                                        $<nPtr>$ = opr(WHILE, 2, $<nPtr>3, $<nPtr>5);   
                                        }
        |                               
        FOR '(' assignment';'expr';' assignment')' statement
                                       {
                                        $<nPtr>$ = opr(FOR, 4, $<nPtr>3, $<nPtr>5, $<nPtr>7, $<nPtr>9);   
                                        }
        |
        WHILEUNTIL '(' expr ')' statement
                                       {
                                        $<nPtr>$ = opr(WHILEUNTIL, 2, $<nPtr>3, $<nPtr>5);   
                                        }
        |
        DO '{' statement '}' WHILE'(' expr ')' ';' 
                                        {
                                         $<nPtr>$ = opr(DO, 2, $<nPtr>3, $<nPtr>7);
                                             
                                        }                                                                  
        |
        REPEAT '{' statement '}' UNTIL'(' expr ')' ';' 
                                        {
                                         $<nPtr>$ = opr(REPEAT, 2, $<nPtr>3, $<nPtr>7);    
                                        }  
        ;

switchstatement:
        SWITCH '(' IDENTIFIER ')' '{' casestatements defaultcase '}'
                                        {
                                         $<nPtr>$ = opr(SWITCH, 3, id($<charpointval>3), $<nPtr>6,$<nPtr>7);    
                                        }
        ;                               

casestatements:
                                        {
                                        $<nPtr>$ = opr(' ',2,NULL,NULL);
                                        } 
        |
        CASE value ':' statement breakstatement casestatements
                                        {
                                         $<nPtr>$ = opr(CASE, 4,$<nPtr>2, $<nPtr>4,$<nPtr>5,$<nPtr>6);    
                                        } 
        |
        CASE value ':' breakstatement casestatements
                                        {
                                         $<nPtr>$ = opr(CASE, 4,$<nPtr>2, NULL,$<nPtr>4,$<nPtr>5);    
                                        }                                 
        ;

breakstatement:
                                        {
                                        $<nPtr>$ = opr(' ',2,NULL,NULL);
                                        } 
        |                               
        BREAK ';'                       {
                                         $<nPtr>$ = opr(BREAK, 0);    
                                        } 
        ;

defaultcase:
                                        {
                                        $<nPtr>$ = opr(' ',2,NULL,NULL);
                                        } 
        |
        DEFAULT ':' statement breakstatement 
                                        {
                                         $<nPtr>$ = opr(DEFAULT, 2, $<nPtr>3, $<nPtr>4);    
                                        } 
        |
        DEFAULT ':' breakstatement 
                                        {
                                         $<nPtr>$ = opr(DEFAULT, 2, NULL, $<nPtr>3);    
                                        }                                 
        ;
        


%%
#define SIZEOF_NODETYPE ((char *)&p->con - (char *)p)
nodeType *con(char * value) {
    nodeType *p;
    size_t nodeSize;
    /* allocate node */
    nodeSize = SIZEOF_NODETYPE + sizeof(conNodeType);
    if ((p = malloc(nodeSize)) == NULL)
    yyerror("out of memory");
    /* copy information */
    p->type = typeCon;
    p->con.value = strdup(value);
    return p;
}
nodeType *id(char *i) {

    nodeType *p;
    size_t nodeSize;
    /* allocate node */
    nodeSize = SIZEOF_NODETYPE + sizeof(idNodeType);
    if ((p = malloc(nodeSize)) == NULL)
	yyerror("out of memory");
    /* copy information */
    p->type = typeId;
    p->id.i = strdup(i);
    return p;
}
nodeType *opr(int oper, int nops, ...) {
    va_list ap;
    nodeType *p;
    size_t nodeSize;
    int i;
    /* allocate node */
    nodeSize = SIZEOF_NODETYPE + sizeof(oprNodeType) +
    (nops - 1) * sizeof(nodeType*);
    if ((p = malloc(nodeSize)) == NULL)
        yyerror("out of memory");
    /* copy information */
    p->type = typeOpr;
    p->opr.oper = oper;
    p->opr.nops = nops;
    va_start(ap, nops);
    for (i = 0; i < nops; i++){
        p->opr.op[i] = va_arg(ap, nodeType*);
        
    }
    
    va_end(ap);
    return p;
}	
void freeNode(nodeType *p) {
    int i;
    if (!p) return;
    if (p->type == typeOpr) {
        for (i = 0; i < p->opr.nops; i++)
        freeNode(p->opr.op[i]);
    }
    free (p);
}

main()
{
 yyparse();
 printTable();
 return 0;
}
yyerror(s)
char *s;
{
  printf("%s at line %d\n",s,yylineno);
}
yywrap()
{
  return(1);
}
