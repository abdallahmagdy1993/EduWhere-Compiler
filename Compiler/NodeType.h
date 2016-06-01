#include <stdbool.h>

typedef enum { typeCon, typeId, typeOpr } nodeEnum;
/* constants */
typedef struct {
	char * value; /* value of constant */
} conNodeType;
/* identifiers */
typedef struct {
	char *i; /* subscript to sym array */
} idNodeType;
/* operators */
typedef struct {
	int oper; /* operator */
	int nops; /* number of operands */
	struct nodeTypeTag *op[1]; /* operands (expandable) */
} oprNodeType;
typedef struct nodeTypeTag {
	nodeEnum type; /* type of node */
	/* union must be last entry in nodeType */
	/* because operNodeType may dynamically increase */
	union {
		conNodeType con; /* constants */
		idNodeType id; /* identifiers */
		oprNodeType opr; /* operators */
	};
} nodeType;

typedef enum {intType, floatType, charType, boolType} symEnum;

typedef struct symNode{
	char* name;
	char* assemblyName;
	symEnum type;
	int num;
	bool isConst;
}symNodeTag;
