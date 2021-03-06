#include <stdio.h>
#include "SymTable.h"
#include "y.tab.h"
static int lbl,reg,freg,caselbl;
char errorText[200];
int ex(nodeType *p) {
	int lbl1, lbl2;
	int exitlbl;
	int reg1,reg2;
	char* Name[200];
	int type1,type2,t,returnVal;
	bool isConst;
	if (!p) return 0;
	switch(p->type) {

	case typeCon:
		printf("\tpush %s\n", p->con.value);
		break;
	case typeId:
		t=getVar(p->id.i,Name,&isConst);
		if(*Name==NULL){
			sprintf(errorText,"error:udefined variable %s",p->id.i);
			yyerror(errorText);
			return t;
		}
		printf("\tpush %s\n", *Name);
		return t;
	case typeOpr:
		switch(p->opr.oper) {
		case 'v':
			ex(p->opr.op[0]);
			if(strcmp(p->opr.op[1]->con.value,"int")==0)return 1;
			if(strcmp(p->opr.op[1]->con.value,"float")==0)return 2;
			if(strcmp(p->opr.op[1]->con.value,"char")==0)return 3;
			if(strcmp(p->opr.op[1]->con.value,"bool")==0)return 4;
			break;
		case WHILE:
			printf("\t;;;;;;; While ;;;;;;;\n");
			printf("L%03d:\n", lbl1 = lbl++);	
			type1=ex(p->opr.op[0]);
			if(type1==2)
				printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
			else
				printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
			printf("\tJZ\tL%03d\n", lbl2 = lbl++); 
			ex(p->opr.op[1]);
			printf("\tJMP\tL%03d\n", lbl1);
			printf("L%03d:\n", lbl2);
			break;
		case WHILEUNTIL:
			printf("\t;;;;;;; While-until ;;;;;;;\n");
			printf("L%03d:\n", lbl1 = lbl++);	
			type1=ex(p->opr.op[0]);
			if(type1==2)
				printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
			else	
				printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
			printf("\tJNZ\tL%03d\n", lbl2 = lbl++); 
			ex(p->opr.op[1]);
			printf("\tJMP\tL%03d\n", lbl1);
			printf("L%03d:\n", lbl2);
			break;
		case DO:
			printf("\t;;;;;;; Do-While ;;;;;;;\n");
			addScope();
			ex(p->opr.op[0]);
			removeScope();
			printf("L%03d:\n", lbl1 = lbl++);	
			type1=ex(p->opr.op[1]);
			if(type1==2)
				printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
			else	
				printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
			printf("\tJZ\tL%03d\n", lbl2 = lbl++); 
			ex(p->opr.op[0]);
			printf("\tJMP\tL%03d\n", lbl1);
			printf("L%03d:\n", lbl2);
			break;
		case REPEAT:
			printf("\t;;;;;;; Repeat-Until ;;;;;;;\n");
			addScope();
			ex(p->opr.op[0]);
			removeScope();
			printf("L%03d:\n", lbl1 = lbl++);	
			type1=ex(p->opr.op[1]);
			if(type1==2)
				printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
			else
				printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
			printf("\tJNZ\tL%03d\n", lbl2 = lbl++); 
			ex(p->opr.op[0]);
			printf("\tJMP\tL%03d\n", lbl1);
			printf("L%03d:\n", lbl2);
			break;
		case FOR:
			printf("\t;;;;;;; For ;;;;;;;\n");
			ex(p->opr.op[0]);
			printf("L%03d:\n", lbl1 = lbl++);	
			type1=ex(p->opr.op[1]);
			if(type1==2)
				printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
			else
				printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
			printf("\tJZ\tL%03d\n", lbl2 = lbl++); 
			ex(p->opr.op[3]);
			ex(p->opr.op[2]);
			printf("\tJMP\tL%03d\n", lbl1);
			printf("L%03d:\n", lbl2);
			break;
		case IF:
			printf("\t;;;;;;; If ;;;;;;;\n");
			type1=ex(p->opr.op[0]);
			if (p->opr.nops > 2) {
				/* if else */
				if(type1==2)
					printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
				else
					printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
				printf("\tJZ\tL%03d\n", lbl1 = lbl++); 
				ex(p->opr.op[1]);
				printf("\tjmp\tL%03d\n", lbl2 = lbl++);
				printf("L%03d:\n", lbl1);
				ex(p->opr.op[2]);
				printf("L%03d:\n", lbl2);
			} else {
				/* if */
				if(type1==2)
					printf("\tpop F%d\n\tcmp F%d 0\n",reg1=freg++,reg1);
				else
					printf("\tpop R%d\n\tcmp R%d 0\n",reg1=reg++,reg1);
				printf("\tJZ\tL%03d\n", lbl1 = lbl++); 
				ex(p->opr.op[1]);
				printf("L%03d:\n", lbl1);

			}
			break;
		case SWITCH:
			exitlbl=caselbl++;
			printf("\t;;;;;;; Switch ;;;;;;;\n");
			addScope();
			type1=getVar(p->opr.op[0]->id.i,Name,&isConst);
			if(*Name==NULL){
				sprintf(errorText,"error:udefined variable %s",p->opr.op[0]->id.i);
				yyerror(errorText);
				removeScope();
				break;
			}
			switchex(p->opr.op[1],exitlbl,*Name,type1);
			ex(p->opr.op[2]);
			printf("Exit%03d:\n",exitlbl);
			removeScope();
			break;

		case DEFAULT:
			printf("\t;;;;;;; Default ;;;;;;;\n");
			type2=ex(p->opr.op[0]);
			switchex(p->opr.op[1],exitlbl,NULL,type2);
			break;
		case '=':

			if (p->opr.nops == 2) {
				printf("\t;;;;;;; Assignment ;;;;;;;\n");
				type2=ex(p->opr.op[1]);
				type1=getVar(p->opr.op[0]->id.i,Name,&isConst);
				if(*Name==NULL){
					sprintf(errorText,"error:udefined variable %s",p->opr.op[0]->id.i);
					yyerror(errorText);
					return type2;
				}
				if(isConst==true){
					sprintf(errorText,"error:const variable %s can't be assigned",p->opr.op[0]->id.i);
					yyerror(errorText);
					return type2;
				}
				t=applicapleTypes(type1,type2);
				if(t!=0){
					printf("\tpop %s\n",*Name);
				}
				else yyerror("error:type missmatch");
				return t;
			}
			else{
				printf("\t;;;;;;; ID=Assignment ;;;;;;;\n");
				ex(p->opr.op[1]);
				type2=getVar(p->opr.op[1]->opr.op[0]->id.i,Name,&isConst);
				if(*Name==NULL){
					return type2;
				}
				printf("\tpush %s\n",*Name);

				type1=getVar(p->opr.op[0]->id.i,Name,&isConst);
				if(*Name==NULL){
					sprintf(errorText,"error:udefined variable %s",p->opr.op[0]->id.i);
					yyerror(errorText);
					return type2;
				}
				if(isConst==true){
					sprintf(errorText,"error:const variable %s can't be assigned",p->opr.op[0]->id.i);
					yyerror(errorText);
					return type2;
				}
				t=applicapleTypes(type1,type2);
				if(t!=0){
					printf("\tpop %s\n",*Name);
				}
				else yyerror("error:type missmatch");
				return t;

			}
		case UMINUS:
			printf("\t;;;;;;; UMINUS ;;;;;;;\n");
			type1=ex(p->opr.op[0]);
			if(type1==2){
				printf("\tpop F%d\n",reg1 = freg++);
				printf("\tneg F%d\n",reg1);
				printf("\tpush F%d\n",reg1);
			}
			else{
				printf("\tpop R%d\n",reg1 = reg++);
				printf("\tneg R%d\n",reg1);
				printf("\tpush R%d\n",reg1);
			}
			return type1;
		case IDENTIFIER:
			if (p->opr.nops == 2) {
				printf("\t;;;;;;; Declaration ;;;;;;;\n");
				char* ptype;
				symEnum etype;
				if(!strcmp(p->opr.op[0]->con.value,"I")){
					ptype="DD";
					etype=intType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"F")){
					ptype="DD";
					etype=floatType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"C")){
					ptype="DB";
					etype=charType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"B")){
					ptype="DB";
					etype=boolType;
				}
				addVar(p->opr.op[1]->id.i,etype,false,Name);
				if(*Name==NULL){
					sprintf(errorText,"error:redeclaration of variable %s",p->opr.op[1]->id.i);
					yyerror(errorText);
					break;
				}
				printf("\t%s %s\n",ptype,*Name);
				break;
			}

			else
			{
				printf("\t;;;;;;; Declaration_Assignment ;;;;;;;\n");
				char* ptype;
				symEnum etype;
				if(!strcmp(p->opr.op[0]->con.value,"I")){
					ptype="DD";
					etype=intType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"F")){
					ptype="DD";
					etype=floatType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"C")){
					ptype="DB";
					etype=charType;
				}
				else if(!strcmp(p->opr.op[0]->con.value,"B")){
					ptype="DB";
					etype=boolType;
				}
				addVar(p->opr.op[1]->opr.op[0]->id.i,etype,false,Name);
				if(*Name==NULL){
					sprintf(errorText,"error:redeclaration of variable %s",p->opr.op[1]->opr.op[0]->id.i);
					yyerror(errorText);
					break;
				}
				printf("\t%s %s\n",ptype,*Name);
				ex(p->opr.op[1]);
				break;		
			}			
		case CONST:
			printf("\t;;;;;;; CONST-Declaration ;;;;;;;\n");
			char* ptype;
			symEnum etype;
			if(!strcmp(p->opr.op[0]->con.value,"I")){
				ptype="DD";
				etype=intType;
			}
			else if(!strcmp(p->opr.op[0]->con.value,"F")){
				ptype="DD";
				etype=floatType;
			}
			else if(!strcmp(p->opr.op[0]->con.value,"C")){
				ptype="DB";
				etype=charType;
			}
			else if(!strcmp(p->opr.op[0]->con.value,"B")){
				ptype="DB";
				etype=boolType;
			}
			constex(p->opr.op[1],p->opr.op[1]->opr.op[0]->id.i,ptype,etype);
			break;		
		case ' ':
			ex(p->opr.op[0]);			
			ex(p->opr.op[1]);
			break;
		case ';':
			ex(p->opr.op[0]);			
			ex(p->opr.op[1]);
			break;
		case '{':
			addScope();
			ex(p->opr.op[0]);			
			removeScope();
			break;
		default:

			type1=ex(p->opr.op[0]);			
			if(p->opr.nops>1)
			{
				type2=ex(p->opr.op[1]);
				if(type2==2)
					printf("\tpop F%d\n",reg2 = freg++);
				else	
					printf("\tpop R%d\n",reg2 = reg++);
			}
			if(type1==2)
				printf("\tpop F%d\n",reg1 = freg++);
			else
				printf("\tpop R%d\n",reg1 = reg++);
			switch(p->opr.oper) {
			case '+': printf("\t;;;;;;; Add ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tfadd F%d F%d\n",reg1,reg2);
					else
						printf("\tadd R%d R%d\n",reg1,reg2);
				}
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case '-': printf("\t;;;;;;; Sub ;;;;;;;\n"); 
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tfsub F%d F%d\n",reg1,reg2);
					else
						printf("\tsub R%d R%d\n",reg1,reg2);
				}
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case '*': printf("\t;;;;;;; Mul ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tfmul F%d F%d\n",reg1,reg2);
					else
						printf("\tmul R%d R%d\n",reg1,reg2);
				}
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case '/':  printf("\t;;;;;;; Div ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tfdiv F%d F%d\n",reg1,reg2);
					else
						printf("\tdiv R%d R%d\n",reg1,reg2);
				}
				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case '%': printf("\t;;;;;;; Mod ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t==2){
					yyerror("error:invalid operands to binary %");
					return 2;
				}
				if(t!=0)
					printf("\tmod R%d R%d\n",reg1,reg2);
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case LOGICALAND: printf("\t;;;;;;; And ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t==2){
					yyerror("error:invalid operands to binary &");
					return 2;
				}
				if(t!=0)
					printf("\tand R%d R%d\n",reg1,reg2);
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case LOGICALOR:  printf("\t;;;;;;; Or ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t==2){
					yyerror("error:invalid operands to binary |");
					return 2;
				}
				if(t!=0)
					printf("\tor R%d R%d\n",reg1,reg2);
				else {
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;
			case '!':  printf("\t;;;;;;; Not ;;;;;;;\n");
				if(type1==2){
					yyerror("error:invalid operands to binary !");
					return 2;
				}
				printf("\tnot R%d\n",reg1); 
				returnVal= 1;
				break;
			case LT:  printf("\t;;;;;;; LT ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJL\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJL\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			case GT: printf("\t;;;;;;; GT ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJG\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJG\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			case GE: printf("\t;;;;;;; GE ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJGE\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJGE\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			case LE:  printf("\t;;;;;;; LE ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJLE\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJLE\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			case NOTEQUAL: printf("\t;;;;;;; NQ ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJNE\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJNE\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			case ISEQUAL:  printf("\t;;;;;;; EQ ;;;;;;;\n");
				t=applicapleTypes(type1,type2);
				if(t!=0){
					if(t==2)
						printf("\tcmp F%d F%d\n\tmov F%d 1\n\tJE\tL%03d\n\tmov F%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					else	
						printf("\tcmp R%d R%d\n\tmov R%d 1\n\tJE\tL%03d\n\tmov R%d 0\n",reg1,reg2,reg1, lbl1=lbl++,reg1);
					printf("L%03d:\n", lbl1);
				}

				else{ 
					yyerror("error:type missmatch");
					return 0;
				}
				returnVal= t;
				break;

			default: break;

			}
			if(type1==2)
				printf("\tpush F%d\n",reg1);
			else	
				printf("\tpush R%d\n",reg1);
			return returnVal;
		}
	}
	return 0;
}
int switchex(nodeType *p,int exitlbl,char*var,int typee){
	int reg1,reg2,lbl1;
	int type1,type2,t;
	switch(p->opr.oper) {
	case CASE:
		printf("\t;;;;;;; Case %s: ;;;;;;;\n", p->opr.op[0]->opr.op[0]->con.value);
		printf("\tpush %s\n",var);
		type1=ex(p->opr.op[0]);
		if(type1==2)
			printf("\tpop F%d\n",reg1 = freg++);
		else	
			printf("\tpop R%d\n",reg1 = reg++);

		type2=typee;
		if(type2==2)
			printf("\tpop F%d\n",reg2 = freg++);
		else	
			printf("\tpop R%d\n",reg2 = reg++);

		t=applicapleTypes(type1,type2);
		if(t!=0){
			if(t==2)
				printf("\tcmp F%d F%d\n",reg2,reg1);
			else	
				printf("\tcmp R%d R%d\n",reg2,reg1);
		}
		else{ 
			yyerror("error:type missmatch");
			return 0;
		}

		printf("\tJNQ\tL%03d\n", lbl1 = lbl++); 
		ex(p->opr.op[1]);
		switchex(p->opr.op[2],exitlbl,NULL,typee);
		printf("L%03d:\n", lbl1);
		switchex(p->opr.op[3],exitlbl,var,typee);
		break;
	case BREAK:
		printf("\t;;;;;;; BREAK ;;;;;;;\n");
		printf("\tjmp Exit%03d:\n",exitlbl);
		break;
	}

}

int constex(nodeType *p,char *var,char *varType,symEnum vareType){
	char* Name[200];
	*Name=NULL;
	bool isConst;
	int t,type1,type2;
	type2=ex(p->opr.op[1]);
	if (p->opr.nops == 3) {
		printf("\t;;;;;;; ID=Assignment ;;;;;;;\n");
		type2=getVar(p->opr.op[1]->opr.op[0]->id.i,Name,&isConst);
		if(*Name==NULL){
			return 0;
		}
		printf("\tpush %s\n",*Name);
	}
	addVar(var,vareType,true,Name);
	if(*Name==NULL){
		sprintf(errorText,"error:redeclaration of variable %s",var);
		yyerror(errorText);
		return 0;
	}
	switch(vareType){
	case intType:
		type1= 1;
		break;
	case floatType:
		type1= 2;
		break;
	case charType:
		type1= 3;
		break;
	case boolType:
		type1= 4;
		break;
	}
	t=applicapleTypes(type1,type2);
	if(t!=0){
		printf("\t%s %s\n",varType,*Name);
		printf("\tpop %s\n",*Name);
	}

	else{ 
		yyerror("error:type missmatch");
		return 0;
	}
}
int applicapleTypes(int type1,int type2){

	if(type1==type2&&type1!=0)
		return type1;
	else return 0;				
}