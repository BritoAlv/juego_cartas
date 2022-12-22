# Implementing Something That Look Almost Like A Poker Game. Based on:

 https://edcharbeneau.com/csharp-functional-workshop-instructions/
 https://github.com/terrajobst/minsk/

## Run

This is implemented in NET 7.0 (no reason to use it by now ) . 

```bash
cd Game
dotnet run
```

## Extensibility

- There is a class Scorer which contains default ranks for the poker game, you can remove them and implement the ranks you want as long as you hold the contract.
- You can change the number of cards distributed in each mini-round of the game.
- The interface IDecision allows you to define a new decision that players can take like *Apostar* or *Pasar*, to do so define a class that implement this interface, and in case that how the decision may be realized depends on the player define another interface which will represent the contract that players should have to put this decision in practice and use it to make the decision custom by player, or don't implement any additional interface like in the case of the *IPasar*  decision . Look at the examples in code.
- You can define your own Player.

## Bugs, Mejoras e Ideas Para Añadirle:

- Create some new decision that implements a mini-parser.
- A more sophisticated IA player.
- Extend more the Context.