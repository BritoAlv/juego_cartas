

# Sobre los literalDescribe:

Esto se refiere a cuando queremos describir literalmente cierta estructura de nuestro juego, ya sea un jugador o una carta, esta debe implementar la clase abstracta *LiteralDescribe<T>* donde *T* es el objeto que deseamos describir , por lo que debe implementar la interfaz *IDescribable* .  O sea los literalDescribe poseen internamente un mini-parser aportado por la implementación del objeto *T*. Los literalDescribe (internamente) usan el source obtenido a través de la interfaz *IArgument<T>* para buscar el objeto descrito en cuestión. 

# IArgument<T>:

Cuando una action necesita como argumento un objeto *T*, ha de ser un objeto que implemente esta interfaz, esta posee un método que toma el *Contexto* de nuestro juego y un *IEnumerable* que va a ser un source para que en el caso de que el argumento sea de tipo *LiteralDescribe* este lo use para encontrar lo que busca, en caso de que sea una *Action* lo que usará será el contexto.

# Como añadir nuevas acciones a nuestro lenguaje:

Una acción predefinida ha de implementar la clase abstracta *Return<T>* donde T será el objeto específico que se va a devolver. Su constructor debe tomar en el mismo orden en que van a ser parseados los elementos que necesita para crearse, empezando por un paréntesis abierto y acabando en un paréntesis cerrado.  Los restantes objetos deben ser de tipo *IArgument* para tipos específicos. 
