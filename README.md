# Implementing Something That Look Almost Like A Poker Game. Based on:

 https://edcharbeneau.com/csharp-functional-workshop-instructions/
 https://github.com/terrajobst/minsk/

## Run

This is implemented in NET 7.0 (no reason to use it by now ) . 

```bash
cd Game
dotnet run
```

## Carptetas dentro del Proyecto:

├── ClassLibrary
│   ├── Cartas
│   ├── Contexto
│   ├── Decision
│   ├── Hand
│   ├── Manager
│   ├── Players
│   ├── Ranks
│   ├── Ronda
│   ├── Tests
│   └── Tools
├── Game
└── Lenguaje

## Lenguaje:

Contiene *con el propósito de aprender* una implementación de un minilenguaje, con un *Lexer*, *Parser* que son implementados a través de C#, para ver que es posibe lograr con el se puede ejecutar:

```bash
cd Lenguaje
cd FrontEnd
dotnet run
```

## Game:

La carpeta *Game* representa una aplicación de consola que apoyada en la librería de clases ejecuta el juego. Además es posible definir nuevas clases y objetos para extender las reglas / jugabilidad del juego, según cada estructura del juego lo permita.

# Estructura de la librería de clases:

### Cartas & Hand:

Contiene la definición de Carta, dada por el *Suit* y el *Value*. Análogamente *Hand* contiene lo que representa la Mano de lo jugadores.

### Contexto:

Los contextos representam la información de la partida que se está ejecutando tanto como los jugadores activos como las apuestas realizadas, se supone que esta parte de la librería de clases, defina lo que un jugador pueda acceder y revisar el estado del juego, y a partir de esto determinar decisiones como *banear* a un jugador o regalar dinero, está dividido en el contexto de la partida, de las rondas, y el de las minirondas que quede explicito donde es útil cada estructura. Además lo que la lógica del juego necesita del *Contexto* es a través de interfaces para garantizar que cambios en la estructura interna de el Contexto no afecte a el código exterior a él que depende de él mientras que se cumpla el contrato.

- Contexto de la Partida: Contiene un Manager de Jugadores a través del cual se pueden aplicar acciones como banear a un jugador de una ronda.

- Contexto de la Ronda: Contiene un Manager de Apuestas y un Manager de cartas, análogamente a través de estas estructuras el jugador puede *apostar* o *regalar dinero* o *quitar una carta al enemigo*

- Contexto de la MiniRonda: Este contiene como estructura un Repartidor que es el que se encarga de en dicha ronda repartir las cartas a los jugadores, como lo hace puede ser modificado, por ejemplo aleatoriamente, o las mismas cartas para todo el mundo como es en el caso del Poker después de haber repartido las dos primeras cartas.

### Decisión:

Esto representa acciones que puede realizar un jugador como *Apostar*, *Pasar*, *Banear*, estas son definidas a través de la interfaz *IDecision* y en caso de que la forma en que cada jugador tome la decisión sea modificable se puede definir una interfaz para esto como es en el caso de *Apostar* y *Banear* . La libertad de que decisión es implementable depende de que tan modificable sea la estructura de el contexto, ya que las modificaciones están limitadas por lo que ofrezca el Contexto.

### Manager:

La clase Manager se encarga de recibir toda la configuración que el usuario desea ponerle a la partida de *Poker* , pasándole los jugadores, la información a los *Contextos* como que clase de repartidor usar, o cuantas cartas repartir por ronda.

### Player:

Players representa a lo jugadores, dado por una clase abstracta *Player* que contiene lo que debe cumplir un jugador que se desee implementar para funcionar en nuestro juego, teniendo en cuenta que sea lo suficientemente extensible para poder implementar un jugador virtual que pueda tomar decisiones más efectivas en base a el contexto que posee.

### Ranks:

Implementación de como dadas varias manos es decidido cual gana el juego, esto posee una lista por defecto de rankings para usar para determinar la mejor mano, pero también posee una interfaz a través de la cual es posible añadir nuevos rankings al juego, en caso de que se quieran usar rankings *raros* como el jugador que tenga más cartas *divisibles entre 3*.

### Ronda:

Define la lógica de lo que ocurre en una ronda de nuestro juego, además una ronda está compuesta por mini_rondas que también están implementadas aquí. 