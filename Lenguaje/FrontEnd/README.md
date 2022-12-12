## Estructura del MiniLenguaje:

- SyntaxToken: La secuencia de carácteres en la cadena *string* línea de código a procesar es particionada en objetos de este tipo, los cuales poseen las siguientes propiedades:
  
  - *SyntaxKind*, *position*, *text* *value*

- SyntaxKind: Esto es un Enum donde tenemos un *id* para cada tipo de token que reconocerá nuestro *Lexer* . 

- SyntaxNode: Esta es una clase abstracta, que contiene como propiedades abstractas: *SyntaxKind* y un método para devolver los hijos de este nodo. Debido a que después de el *Lexer* realizar su trabajo el parser construirá un árbol binario es necesario tener una clase abstracta que represente cada nodo de este árbol.

- SyntaxTree: Contiene la raíz del árbol que genera el parser y la lista final de Diagnósticos (errores) del compilador. El parser devuelve un objeto de este tipo.

- El *Abstract Syntax Tree* que el Parser va a formar esta formado por objetos de tipo *ExpressionSyntax*  que es una clase abstracta, estos  son por ahora de tipo *Binary, Number, Parenthesized*. Cada una de estas expresiones son construidas por el *Parser*.

### Lexer:

- Posee como campos el texto que va a *lexear*, un entero que representa la posición dentro de ese texto por donde va el proceso, una propiedad *char* *Current* que no es más que el carácter que se encuentra en la posición por donde va el lexer y además para tomar ventaja de que su *get* es definido como una propiedad, devuelve un carácter especial en caso de que la posición se sobrepase al tamaño del texto. 

- Posee un método *NextToken* que se encarga de realizar dos cosas una vez que es llamado:
  
  - mover hacia donde comenzaría el siguiente token.
  
  - devolver el token obtenido a partir de la lectura en el carácter actual. 

Notar que el *Lexer* funciona como un *Enumerador* tiene un método para moverse hacia adelante y devolver lo que hay en la posición actual, y posee una condición de parada que es cuando el token que sea devuelto sea *EndofFileToken*, o sea el carácter especial que mencioné más arriba cuando me refería a la propiedad *Current*.

- Posee un *IEnumerable<string>* que guarda cada error comportamiento inusual en la línea de código encontrado.

Entonces el método *NextToken* debe ser capaz de procesar una secuencia de números que serán convertidos a un *Token* de tipo número, etc. 

### Parser:

El parser primero usa a el *Lexer* para obtener un array de tokens a partir de la línea de código a parsear donde son ignorados los *tokens* que representan espacios en blanco. Además posee un *IEnumerable* que guarda cada error comportamiento inusual en la línea de código encontrado y además incorpora los obtenidos del *Lexer*. 

El Parser posee una forma muy peculiar de como leer una expresión, se definen:

- expresión primaria: números o un término entre paréntesis.

- factor: el producto o división de expresiones primarias.

- término: la suma o resta de factores.

Esta terminología nos provee de una idea sobre como parsear un array de *tokens*, análogamente al *Lexer* el *Parser* posee *Current* y *_position_* , dado que el array de *tokens* constituye un término que es una suma / resta de factores, definimos el siguiente algoritmo para parsear un término:

Parseamos el factor que se encuentra a partir de la posición actual y lo llamamos *left*, esto nos deja el *Current* en un operador que puede ser suma o resta o el final del término, si no es un operador válido (suma o resta) devolvemos lo que acabamos de parsear, que es un objeto de tipo *Expression Syntax* que hereda de *Syntax Node*, por otro lado si es un operador valido, lo guardamos para con el resultado de el factor que se espera a continuación generar una expresión de tipo *Binaria* con el *left*, *operand*, *right*, si después de este operador hay otro, lo que hacemos es llamar *left* a la expresión binaria obtenida, y repetir el proceso (un *while*), hasta que se complete el proceso. La idea para parsear un *factor* es análoga y en el caso de la Expresión Primaria en el caso de que sea un número es sencillo y si es una expresión con parentésis parsear como término lo que está dentro de esa expresión, encontrar el parentésis cerrado que le corresponde y generar una expresión de tipo *Paréntesis*. 

#### Forma más general de parsear una expresión:

Debido a que queremos introducir más operadores debemos generalizar

el algoritmo anterior, por lo que se introduce el concepto de *precedencia / prioridad* mientras más abajo esté en el árbol el operador más prioridad tiene, por lo que el operador de multiplicación tiene prioridad $2$, mientras que el de suma $1$, nuestro algoritmo ahora es recursivo tomando como argumento la prioridad del nodo padre de la parte del árbol donde estamos, inicialmente es $0$, lo que significa que recorreremos cada operador de la expresión, la cosa es que si llegamos a un operador con más prioridad que el padre actual en ese mismo nodo del árbol podemos parsearlo o en caso contrario dejarlo para el final.

### Evaluador:

Este solamente tiene que descender en el árbol y en dependencia de que tipo *Expression Syntax* sea la clase *nodo* ejecutar la evaluación.

### Uso de Yield Return:

You use the `yield` statement in an iterator in two following forms:

- `yield return`: to provide the next value in iteration, as the following example shows:
  
  ```csharp
  foreach (int i in ProduceEvenNumbers(9))
  {    Console.Write(i);    Console.Write(" ");}
  // Output: 0 2 4 6 8
  
  IEnumerable<int> ProduceEvenNumbers(int upto)
  {    for (int i = 0; i <= upto; i += 2)    {        yield return i;    }}
  ```

- `yield break`: to explicitly signal the end of iteration, as the following example shows:
  
  ```csharp
  Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 2, 3, 4, 5, -1, 3, 4})));
  // Output: 2 3 4 5
  
  Console.WriteLine(string.Join(" ", TakeWhilePositive(new[] { 9, 8, 7 })));
  // Output: 9 8 7
  
  IEnumerable<int> TakeWhilePositive(IEnumerable<int> numbers)
  {    foreach (int n in numbers)    {        if (n > 0)        {            yield return n;        }        else
          {            yield break;        }    }}
  ```
  
  Iteration also finishes when control reaches the end of an iterator.

In the preceding examples, the return type of iterators is *IEnumerable<T>*.As the preceding example shows, when you start to iterate over an iterator's result, an iterator is executed until the first `yield return` statement is reached. Then, the execution of an iterator is suspended and the caller gets the first iteration value and processes it. On each subsequent iteration, the execution of an iterator resumes after the `yield return` statement that caused the previous suspension and continues until the next `yield return` statement is reached. The iteration completes when control reaches the end of an iterator or a `yield break` statement.

### Diagnósticos:

Tenemos una *List<string>* donde añadiremos las cosas inesperadas en las líneas de código que serán los errores que el compilador nos diŕa, los hay en dos partes en el *Match* del *Parser* para garantizar que después de encontrar un paréntesis abierto se encuentre uno cerrado y así. Y el otro se encuentra en el *Lexer* para cosas como un entero que no es válido *Int32*.  O un carácter que es inválido
