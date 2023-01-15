# Introducción:

Este proyecto se trata de implementar un juego de cartas, con un minilenguaje que permita realizar efectos durante el juego, al estilo *Yugioh*, el cual además, debe poseer un jugador virtual y una interfaz gráfica. Su objetivo es evaluar las buenas prácticas de Oriented Object Programming. Y constituye una introducción a la creación de proyectos a gran escala, donde es necesario saber separar las componentes en modulos, submódulos, y así continuamente.

En este proyecto, la temática es un juego de poker, que ha sido extendido para que los jugadores puedan realizar algunas acciones durante la partida, a través de él minilenguaje, usa como interfaz visual la consola prieta, y hasta hoy no posee correctamente separada la parte visual de el backend.

### Partes Por Separado:

- backend => toda la lógica de un juego de Texas Hold'em Poker siendo lo más extensible posible, que permita por ejemplo añadir un nuevo ranking, como por ejemplo dos dos.

- parte visual => se supone que la lógica del juego quede separada para que si alguien desea implementar el juego en web le sea posible.

- jugador virtual o computadora => se supone que el juego permita implementar un jugador de la computadora, que posea estrategias a seguir, iendo más profundo la implementación del juego debe ser capaz de permitir entrenar a un jugador a base de juegos.

- minilenguaje => el minilenguaje debe soportar while y ifs, más asignación de variables al menos de tipo int, bool al igual que operaciones con ellas, y más aún los efectos deben poder componerse entre si mismos.

# Instrucciones y dependencias para ejecutarlo.

Tener NET 7.0 instalado, posee como dependencias los paquete de Nuget: 

- FluentAssertions

- Xunit

- Unicode

Los dos primeros son debido a los Test, realizados y el último es para poner en la consola los símbolos de las cartas.

Ambas dependencias deben ser automáticamente añadidas por dotnet la primera vez que se ejecuta el proyecto, (asumiendo que donde se ejecuta posee Internet)

## Run

Está implementado en C# 11 (NET 7.0) para hacer uso de la nueva *feature* de que las interfaces pueden definir métodos estáticos abstractos, Para ejecutarlo realizar:

```bash
cd Game
dotnet run
```

# Based on:

 https://edcharbeneau.com/csharp-functional-workshop-instructions/
 https://github.com/terrajobst/minsk/
