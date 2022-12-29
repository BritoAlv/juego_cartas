# Implementing Something That Looks Almost Like A Mix Between A Poker Game And The "UNO Game". Based on:

 https://edcharbeneau.com/csharp-functional-workshop-instructions/
 https://github.com/terrajobst/minsk/

## Run

This is implemented in NET 7.0 (no reason to use it by now ) . 

```bash
cd Game
dotnet run
```

## Carptetas dentro del Proyecto:

```bash
├── ClassLibrary
│   ├── Cartas
│   ├── Contexto
│   ├── Decision
│   ├── Hand
│   ├── Manager
│   ├── Minilenguaje
│   ├── Players
│   ├── Ranks
│   ├── Ronda
│   ├── Tests
│   └── Tools
├── Game
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

Contiene la implementación de los *efectos* en nuestro juego. El lenguaje se base en acciones que pueden retornar ya sea un *jugador*, una *carta* o *void*. Un efecto en nuestro lenguaje sería el siguiente:

```bash
( $void_añadircarta [ ( $carta_robar [Valor mayor && Suit corazonrojo ] {Jugador  PC}] {Bet mayorapostador}) 
```

###### Lo que nos permite realizar efectos complejos aparte de todas las posibles descripciones literales descritas a continuación es la posibilidad de componer acciones.

El ejemplo anterior ejecuta la acción de robarle la carta al jugador *PC* de mayor valor y a su vez de corazón rojo y después añadirsela a el jugador que más haya apostado. Notar que este efecto depende del estado actal del juego.

#### Syntaxis:

Cada acción es definida entre parentésis,  primero contiene su nombre definido como : $tiporetorno_nombre, después se le pasan los argumentos, una expresión dentro de [ ] al evaluarse devolverá una carta mientras que otra dentro de {} devolverá un jugador. Como se puede observar en el ejemplo dentro de [] hay una acción, esto es posble ya que esta devuelve una carta. Pero también existe syntaxis como:

```bash
Valor mayor && Suit corazonrojo 
```

Esto representa una descripción literal del objeto, en este caso una carta, que debe satisfacer las dos descripciones unarias anteriores, cada descripción unaria va a estar dada por un Objeto escrito con letra mayúscula y una palabra que describe a dicho objeto. Las posibles Objetos están predefinidos en nuestro lenguaje a igual que las palabras con los que los describimos.

Finalmente como dependemos de las acciones y sintaxis predefinidos he aquí un aŕbol de lo que es posible hacer con cada una.

```bash
├── Acciones
│   ├── Void
│   │    ├── $void_añadircarta
│   ├── Carta
│   │    ├── $carta_robar
│   ├── Jugador
│   │    
├── Descripciones
│   ├── Carta
│   │    ├── Valor
│   │    │   ├── >2, <3, mayor, menor, 1,2,3 ...
│   │    ├── Suit
│   │    │   ├── trebol, pica, ...
│   ├── Player
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