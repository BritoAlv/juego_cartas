## Compilador y Lenguaje:

> Esta sección es dedicada a la implementación de un compilador para un nuevo lenguaje usando *C#*.

## Notas:

#### Lexer:

El lexer o tokenizer es la primera fase de un compilador, consistente en un programa que recibe como entrada el código fuente, de otro programa (secuencia de carácteres) y produce una salida compuesta de tokens o símbolos. Estos tokens sirven para una etapa posterior del proceso de traduccion, siendo la entrada para el analizador sintáctico (en ingles parser), este convierte un string en una secuencia de tokens. 

#### Parser:

Analiza una cadena de símbolos según las reglas de una gramática formal, el análisis sintáctico convierte el texto de entrada en otras estructuras (árboles) que capturan la jerarquía implícita de la entrada. Un analizador léxico crea tokens de una secuencia de carácteres de entrada y son estos tokens los que son procesados por el analizador sintáctico para construir la estructura de datos, por ejemplo un *abstract syntax tree*. 

#### Árbol de Syntaxis Abstracta:

Es una representación de árbol de la estructura sintáctica simplificada del código fuente escrito en cierto lenguaje de programación, cada nodo del árbol denota una construcción que ocurre en el código fuente. La sintaxis es abstracta en el sentido que no representa cada detalla que aparezca en la sintaxis verdadera. 

#### Recursive Descent Parser:

Tenemos operadores binarios, unarios,  paréntesis, ..., en fin. Los operadores tienen precedencia, asociatividad.  

Concepto De Grammar:  Para cada elemento del grammar hay que definir reglas, símbolos no-terminales (son los términos del grammar), 

El lenguaje tiene definido: expresiones, términos y factores:

- un factor es either un ID, un entero o una expresión menos un factor.

- un término es either un factor *(/) un término, o un factor. 

- una expresión es either un término +(-) expresión, o un término.

El input consiste en un grupo de tokens ( obtenido a partir del Lexer )

En el AST, los nodos de mayor importancia van siempre por arriba de los de menor importancia, de esta forma es implementado recursivamente el evaluador del AST, su funcionamiento es evaluar desde abajo hacia arriba.