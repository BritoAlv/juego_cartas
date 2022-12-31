# Implementing Something That Looks Almost Like A Poker :

# Based on:

 https://edcharbeneau.com/csharp-functional-workshop-instructions/
 https://github.com/terrajobst/minsk/

## Como colección:

Cuando un jugador gana una partida recibe un efecto de cada uno de los demás jugadores, añadiendo a su colección de efectos disponibles estos para un próximo juego.

## Run

Está implementado en NET 7.0 para hacer uso de la nueva *feature* de que las clases abstractas genéricas pueden definir métodos estáticos virtuales, así es como cada objeto le transmite al parser como interpretarlo, de forma que al añadir una estructura nueva a nuestro juego, tengamos que implementar la clase abstracta requerida a nivel de estructura y no modificar nuestro parser. Para ejecutarlo realizar:

```bash
cd Game
dotnet run
```

## Instrucciones para jugarlo:

En tu turno debes escribir que Decisión decides realizar, las posibles decisiones serían:

- Apostar : escribe la cantidad de dinero que deseas apostar

- Pasar : no apostar en la mini-ronda.

- Abandondar : salir de la ronda, no de la partida.

- Efecto: puedes escribir que efecto deseas realizar basado en las reglas del lenguaje descrito a continuación.

## Estructura de la librería de clases :

```bash
├── ClassLibrary
│   ├── Cartas
│   ├── Colector
│   ├── Contexto
│   ├── Decision
│   ├── Hand
│   ├── Manager
│   ├── Minilenguaje
│   │   ├── Lexer
│   │   ├── Parser
│   │   └── PredefinedActions
│   ├── Players
│   ├── Ranks
│   ├── Ronda
│   ├── Tests
│   └── Tools
└── Game
```

## Game:

La carpeta *Game* representa una aplicación de consola que apoyada en la librería de clases ejecuta el juego. Además es posible definir nuevas clases y objetos para extender las reglas / jugabilidad del juego, según cada estructura del juego lo permita.

# Estructura de la librería de clases:

### Cartas & Hand:

Contiene la definición de Carta, dada por el *Suit* y el *Value*. Análogamente *Hand* contiene lo que representa la Mano de lo jugadores.

### Contexto:

Los contextos representan la información de la partida que se está ejecutando tanto como los jugadores activos como las apuestas realizadas, se supone que esta parte de la librería de clases, defina lo que un jugador pueda acceder y revisar el estado del juego, y a partir de esto determinar decisiones como *banear* a un jugador o regalar dinero, está dividido en el contexto de la partida, de las rondas, y el de las minirondas que quede explicito donde es útil cada estructura. Además lo que la lógica del juego necesita del *Contexto* es a través de interfaces para garantizar que cambios en la estructura interna de el Contexto no afecte a el código exterior a él que depende de él mientras que se cumpla el contrato.

- Contexto de la Partida: Contiene un Manager de Jugadores a través del cual se pueden aplicar acciones como banear a un jugador de una ronda.

- Contexto de la Ronda: Contiene un Manager de Apuestas y un Manager de cartas, análogamente a través de estas estructuras el jugador puede *apostar* o *regalar dinero* o *quitar una carta al enemigo*

- Contexto de la MiniRonda: Este contiene como estructura un Repartidor que es el que se encarga de en dicha ronda repartir las cartas a los jugadores, como lo hace puede ser modificado, por ejemplo aleatoriamente, o las mismas cartas para todo el mundo como es en el caso del Poker después de haber repartido las dos primeras cartas.

### Decisión:

Esto representa acciones que puede realizar un jugador como *Apostar*, *Pasar*, *Banear*, estas son definidas a través de la interfaz *IDecision* y en caso de que la forma en que cada jugador tome la decisión sea modificable se puede definir una interfaz para esto como es en el caso de *Apostar* y *Banear* . La libertad de que decisión es implementable depende de que tan modificable sea la estructura de el contexto, ya que las modificaciones están limitadas por lo que ofrezca el Contexto.

### Manager:

La clase Manager se encarga de recibir toda la configuración que el usuario desea ponerle a la partida de *Poker* , pasándole los jugadores, la información a los *Contextos* como que clase de repartidor usar, o cuantas cartas repartir por ronda.

### MiniLenguaje:

Contiene la implementación de los *efectos* en nuestro juego. El lenguaje se base en acciones que pueden retornar ya sea un *jugador*, una *carta*,  o *void* (no está limitado a estos tipos). Un ejemplo de efecto en nuestro lenguaje sería el siguiente:

```bash
( $añadircarta [ ( $robarcarta [Valor mayor && Suit corazonrojo ] {Jugador  PC}] {Apuesta mayorapostador}) 
```

###### Lo que nos permite realizar efectos complejos aparte de todas las posibles descripciones literales descritas a continuación es la posibilidad de componer acciones.

El ejemplo anterior ejecuta la acción de robarle la carta al jugador *PC* de mayor valor y a su vez de corazón rojo y después añadírsela a el jugador que más haya apostado. Notar que este efecto depende del estado actual del juego.

#### Syntaxis:

Cada acción es definida entre parentésis,  primero contiene su nombre , después se le pasan los argumentos, una expresión dentro de [ ] al evaluarse devolverá una carta mientras que otra dentro de {} devolverá un jugador. Como se puede observar en el ejemplo dentro de [] hay una acción, esto es posible ya que esta devuelve una carta. Pero también existe syntax como:

```bash
[Valor mayor && Suit corazonrojo] 
```

Esto representa una descripción literal del objeto, en este caso una carta, que debe satisfacer las dos descripciones unarias anteriores, cada descripción unaria va a estar dada por un Objeto escrito con letra mayúscula y una palabra que describe a dicho objeto con letra minúscula. Cada estructura de descripción literal define los objetos que entiende y sus descripciones, en el caso de Carta posee definido los objetos *Valor* y *Suit*, cada uno, respectivamente posee en sus descripciones definido a *mayor* y *corazonrojo*.

Finalmente como dependemos de las acciones y sintaxis predefinidos he aquí un árbol de lo que es posible hacer con cada una. Internamente ambos *lexer* y *parser* están definidos teniendo en cuenta la extensibilidad en el sentido de que si se desea añadir una  nueva acción predefinida a los efectos es posible realizarlo, sobre esto leer [Action.md](./Action.md) . Aclaro que esto no se refiere a las acciones que puede realizar el usuario, estas son las que pueda realizar a través de las acciones predefinidas, anteriormente usando descripciones literales o composición. 

```bash
├── Acciones ()
│   ├── Void
│   │    ├── $añadircarta
│   │    ├── $banearjugador
│   ├── Carta
│   │    ├── $robarcarta
│   ├── Jugador
│   │    ├── 
├── Descripciones
│   ├── Carta []
│   │    ├── Valor
│   │    │   ├── >2, <3, mayor, menor, 1,2,3 ...
│   │    ├── Suit
│   │    │   ├── trebol, pica, ...
│   ├── Hand ¿?
│   │    ├── Valor
│   │    │   ├── >2, <3, mayor, menor, 
│   │    ├── Rank
│   │    │   ├── trio, color, ...
│   ├── Player {}
│   │    ├── Jugador
│   │    │   ├── nombre del player
│   │    ├── Dinero
│   │    │   ├── mayor, menor, >100, < 20
│   │    ├── Apuesta
│   │    │   ├── mayor, mayorapostador, menorapostador, menor
```

### Player:

Players representa a lo jugadores, dado por una clase abstracta *Player* que contiene lo que debe cumplir un jugador que se desee implementar para funcionar en nuestro juego, teniendo en cuenta que sea lo suficientemente extensible para poder implementar un jugador virtual que pueda tomar decisiones más efectivas en base a el contexto que posee.

### Ranks:

Implementación de como dadas varias manos es decidido cual gana el juego, esto posee una lista por defecto de rankings para usar para determinar la mejor mano, pero también posee una interfaz a través de la cual es posible añadir nuevos rankings al juego, en caso de que se quieran usar rankings *raros* como el jugador que tenga más cartas *divisibles entre 3*.

### Ronda:

Define la lógica de lo que ocurre en una ronda de nuestro juego, además una ronda está compuesta por mini_rondas que también están implementadas aquí. 

## Mejoras, Ideas, Bugs :

- El principal problema actual es que el parser solamente soporta devolver acciones de tipo *bool* dado que tengo que castear el objeto que devuelve el parser a tipo *bool*, esto no es lo correcto, además se supone que el parser sea lo menos dependiente posible de las acciones predefinidas, esto está casi hecho excepto por el problema descrito anteriormente que lo impide.

- Más capturas de pantalla mostrando la realización de los efectos a través del lenguaje finalmente.

- Acabar de implementar que los efectos que pueda usar un jugador sean los que el ha coleccionado hasta el momento, y no algunos random que el pueda escribir como sucede ahora.