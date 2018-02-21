# EduWhere Compiler
The project implements a compiler that supports a programming language that contains:

- Variables and Constants
- Mathematical and logical expressions
- Assignment statement
- If-then-else, Switch, While, Whileuntil, Do-While, Repeat-Until and For statements
- Block structure (nested scopes).

## Tools and Technologies
- Flex Windows (Lex and Yacc)
- Microsoft Visual C# (GUI)

## Language Specifications
- The language is syntax like C
- The language has six types of errors:
 - Syntax error: written code doesn’t match language rules
 - Redeclaration of variable error: declaring a variable that was declared at the same scope
 - Undefined variable: using of undeclared variable
 - Const variable can’t be assigned: trying to change value of const variable
 - Invalid operand type for operation: using float variables or values with integer operations
 - Type mismatch: using any two different data types in operation (no casting)
- Differences between our language and C
 - The language has four data types: int, float, char and bool
 - The language has some statements that are not exist in C
     - whileuntil statement: same like while statement but it loops when the condition is false
     - repeat-until statement: same like do-while but it loops when the condition is false
 - if statement requires (then) to be written after (if) condition
 - the language has no comments
 - the language has no functions
