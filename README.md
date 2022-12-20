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
- You also can implement your own player with its own "how to bet logic".

