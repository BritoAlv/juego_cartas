# Backend

## Estructura de la librería de clases :

```bash
├── ClassLibrary
│   ├── Cartas
│   ├── Coleccionable
│   ├── Contexto
│   ├── Decision
│   ├── EvalExpresiones
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
├── Effects
├── Examples
└── Game
```

### Player:

Players representa a lo jugadores, dado por una clase abstracta *Player* que contiene lo que debe cumplir un jugador que se desee implementar para funcionar en nuestro juego, teniendo en cuenta que sea lo suficientemente extensible para poder implementar un jugador virtual que pueda tomar decisiones más efectivas en base a el contexto que posee.

### Ranks:

Implementación de como dadas varias manos es decidido cual gana el juego, esto posee una lista por defecto de rankings para usar para determinar la mejor mano, pero también posee una interfaz a través de la cual es posible añadir nuevos rankings al juego, en caso de que se quieran usar rankings *raros* como el jugador que tenga más cartas *divisibles entre 3*.

### Ronda:

Define la lógica de lo que ocurre en una ronda de nuestro juego, además una ronda está compuesta por mini_rondas que también están implementadas aquí. 

### Cartas & Hand:

Contiene la definición de Carta, dada por el *Suit* y el *Value*. Análogamente *Hand* contiene lo que representa la Mano de lo jugadores. Tanto las cartas, como la mano, como los jugadores implementan la interfaz *IDescribable<T>* que define cómo pueden ser descritos estos objetos en el minilenguaje. (la responsabilidad de como debe ser descrito está en la estructura y no en quien utiliza la descripción). 

### Contexto:

Definí la interfaz IGlobalContexto para evitar propagación de errores al cambiar la estructura interna de la clase que implementa esa interfaz. (las clases no deben de depender de clases específicas sino de contratos.) Mientras que este sea cumplido, las clases que usan el contexto no tendrán problemas (acordarse propagación de errores).

Los contextos representan la información de la partida que se está ejecutando tanto como los jugadores activos como las apuestas realizadas, se supone que esta parte de la librería de clases, defina lo que un jugador pueda acceder y revisar el estado del juego, y a partir de esto determinar decisiones como *banear* a un jugador o regalar dinero, está dividido en el contexto de la partida, de las rondas, y el de las mini-rondas que quede explicito donde es útil cada estructura.

- Contexto de la Partida: Contiene un Manager de Jugadores a través del cual se pueden aplicar acciones como banear a un jugador de una ronda.

- Contexto de la Ronda: Contiene un Manager de Apuestas y un Manager de cartas, análogamente a través de estas estructuras el jugador puede *apostar* o *regalar dinero* o *quitar una carta al enemigo*

- Contexto de la MiniRonda: Este contiene como estructura un Repartidor que es el que se encarga de en dicha ronda repartir las cartas a los jugadores, como lo hace puede ser modificado, por ejemplo aleatoriamente, o las mismas cartas para todo el mundo como es en el caso del Poker después de haber repartido las dos primeras cartas.

### Decisión:

Esto representa acciones que puede realizar un jugador como *Apostar*, *Pasar*, *Banear*, estas son definidas a través de la interfaz *IDecision* y en caso de que la forma en que cada jugador tome la decisión sea modificable se puede definir una interfaz para esto como es en el caso de *Apostar* y *Banear* . La libertad de que decisión es implementable depende de que tan modificable sea la estructura de el contexto, ya que las modificaciones están limitadas por lo que ofrezca el Contexto.

### Manager:

La clase Manager se encarga de recibir toda la configuración que el usuario desea ponerle a la partida de *Poker* , pasándole los jugadores, la información a los *Contextos* como que clase de repartidor usar, o cuantas cartas repartir por ronda.

### Mejoras, Ideas, Bugs:

- el contexto está muy complicado lo que genera inconsistencia, además de que tiene problemas con las palabras *public, internal, private, sealed, protected, override, virtual, new*. 

- posee la parte visual que debe ser abstraida y separada del backend.