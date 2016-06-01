/* A Bison parser, made by GNU Bison 2.7.  */

/* Bison interface for Yacc-like parsers in C
   
      Copyright (C) 1984, 1989-1990, 2000-2012 Free Software Foundation, Inc.
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.
   
   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */

#ifndef YY_YY_Y_TAB_H_INCLUDED
# define YY_YY_Y_TAB_H_INCLUDED
/* Enabling traces.  */
#ifndef YYDEBUG
# define YYDEBUG 0
#endif
#if YYDEBUG
extern int yydebug;
#endif

/* Tokens.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
   /* Put the tokens into the symbol table, so that GDB and other debuggers
      know about them.  */
   enum yytokentype {
     INTEGER = 258,
     FLOAT = 259,
     CHAR = 260,
     CONST = 261,
     BOOLEAN = 262,
     IF = 263,
     THEN = 264,
     ELSE = 265,
     WHILE = 266,
     WHILEUNTIL = 267,
     FOR = 268,
     SWITCH = 269,
     CASE = 270,
     BREAK = 271,
     DEFAULT = 272,
     DO = 273,
     REPEAT = 274,
     UNTIL = 275,
     FALSE = 276,
     TRUE = 277,
     COMMENT = 278,
     INTEGERVALUE = 279,
     OCTAVALUE = 280,
     HEXAVALUE = 281,
     FLOATVALUE = 282,
     CHARVALUE = 283,
     IDENTIFIER = 284,
     IFX = 285,
     LOGICALOR = 286,
     LOGICALAND = 287,
     NOTEQUAL = 288,
     ISEQUAL = 289,
     LE = 290,
     GT = 291,
     GE = 292,
     LT = 293,
     UMINUS = 294
   };
#endif
/* Tokens.  */
#define INTEGER 258
#define FLOAT 259
#define CHAR 260
#define CONST 261
#define BOOLEAN 262
#define IF 263
#define THEN 264
#define ELSE 265
#define WHILE 266
#define WHILEUNTIL 267
#define FOR 268
#define SWITCH 269
#define CASE 270
#define BREAK 271
#define DEFAULT 272
#define DO 273
#define REPEAT 274
#define UNTIL 275
#define FALSE 276
#define TRUE 277
#define COMMENT 278
#define INTEGERVALUE 279
#define OCTAVALUE 280
#define HEXAVALUE 281
#define FLOATVALUE 282
#define CHARVALUE 283
#define IDENTIFIER 284
#define IFX 285
#define LOGICALOR 286
#define LOGICALAND 287
#define NOTEQUAL 288
#define ISEQUAL 289
#define LE 290
#define GT 291
#define GE 292
#define LT 293
#define UMINUS 294



#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED
typedef union YYSTYPE
{
/* Line 2058 of yacc.c  */
#line 18 "EduWhere.y"

    nodeType *nPtr; /* node pointer */  
    char* charpointval;


/* Line 2058 of yacc.c  */
#line 141 "y.tab.h"
} YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define yystype YYSTYPE /* obsolescent; will be withdrawn */
# define YYSTYPE_IS_DECLARED 1
#endif

extern YYSTYPE yylval;

#ifdef YYPARSE_PARAM
#if defined __STDC__ || defined __cplusplus
int yyparse (void *YYPARSE_PARAM);
#else
int yyparse ();
#endif
#else /* ! YYPARSE_PARAM */
#if defined __STDC__ || defined __cplusplus
int yyparse (void);
#else
int yyparse ();
#endif
#endif /* ! YYPARSE_PARAM */

#endif /* !YY_YY_Y_TAB_H_INCLUDED  */
