# IArgument<T>:

Cuando una action necesita como argumento un objeto *T*, ha de ser un objeto que implemente esta interfaz, esta posee un método que toma el *Contexto* de nuestro juego y un *IEnumerable<T>* que va a ser un source para que en el caso de que el argumento sea de tipo *LiteralDescribe* este lo use para encontrar lo que busca, en caso de que sea una *Action* lo que usará será el contexto.

# Sobre los literalDescribe:

Esto se refiere a cuando queremos describir literalmente cierta estructura de nuestro juego, ya sea un jugador o una carta, esta debe implementar la interfaz *IArgument<T>* porque a través de ella no realizamos una acción sino que devolvemos un objeto de tipo *T*, obtenido a través de la descripción literal, pero sucede que las descripciones literales pueden ser *predicados*, o sea que no tienen sentido hasta que le pasamos un *sujeto* por esto es que la interfaz toma un *IEnumerable<T> list* que representará la fuente de donde vamos a buscar el objeto *T* o podemos hacerlo a través de el contexto. Cada literalDescribe debe contener la implementación de como evalúa los objetos que lo describen, esto añade la flexibilidad de que cada literalDescribe puede interpretar como desee los operadores y sus propios descriptores. O sea los literalDescribe poseen internamente un mini-parser.

# Como añadir nuevas acciones a nuestro lenguaje:

Una acción predefina ha de implementar la clase abstracta *Return* donde T será el objeto específico que se va a devolver. Su constructor debe tomar en el mismo orden en que van a ser parseados los elementos que necesita para crearse, empezando por un paréntesis abierto y acabando en un paréntesis cerrado.  Los restantes objetos deben ser de tipo *IArgument* para tipos específicos. 
