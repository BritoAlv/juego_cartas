# Minilenguaje

Contiene la implementación de los *efectos* en nuestro juego. El lenguaje se base en acciones que pueden retornar ya sea un *jugador*, una *carta*,  o *void* (no está limitado a estos tipos). Un ejemplo de efecto en nuestro lenguaje sería el siguiente:

```bash
(
$if ¿($holdhand { Cantidad %2  } {Jugador yo}) || ($holdcard {Valor mayor} {Dinero mayor})? =>
    ($finalround #efectonuevo 
        {
            (
            $if ¿($exist #dineromio)? => 
                (
                $if ¿($operationbool #stage)? =>
                    ($asignarint #actualdinero {($getdinero {Jugador yo})})
                        (
                        $if ¿($operationbool #actualdinero>dineromio)? =>
                            ($asignarint #dineromio {($operationint #actualdinero)})
                            !
                            ($aumentardinero #[dineromio-actualdinero]/2 {Jugador yo})
                            ($asignarbool #efectonuevo {($operationbool #false)})
                        )
                    !
                    ($asignarbool #stage { ($operationbool #true) } )
                    ($asignarint #dineromio { ($getdinero {Jugador yo}) })
                )
            !
            ($asignarint #dineromio {($getdinero {Jugador yo})})
            ($asignarbool #stage {($operationbool #false)})
            )
        }
    )
    ! 
    (
        $asignarint #valorcarta
        {
            (
                $getcardvalue 
                { Valor mayor } { Jugador yo  } 
            ) 
        }
    )
    (
        $while ¿($operationbool #valorcarta>0 )? =>
        (
            $asignarint #valorcarta 
            {
                (
                    $operationint #valorcarta/2
                )
            }
        )
        (
            $modificarvalorcarta #valorcarta 
                {Valor mayor} {Jugador random}    
        )    
    )
) 
```

###### Lo que nos permite realizar efectos complejos aparte de todas las posibles descripciones literales descritas a continuación es la posibilidad de componer acciones.

#### Syntaxis:

Cada acción es definida entre parentésis,  primero contiene su nombre , después se le pasan sus argumentos, una expresión dentro de {} representa un argumento, o sea, que al evaluarse esta expresión se devolverá un objeto. Por otro lado:

```bash
{Valor mayor && Suit corazonrojo}
```

Esto representa una descripción literal del objeto, en este caso una carta, que debe satisfacer las dos descripciones unarias anteriores, cada descripción unaria va a estar dada por un Objeto escrito con letra mayúscula y una palabra que describe a dicho objeto con letra minúscula. Cada estructura de descripción literal define los objetos que entiende y sus descripciones, en el caso de Carta posee definido los objetos *Valor* y *Suit*, cada uno, respectivamente posee en sus descripciones definido a *mayor* y *corazonrojo*.

Internamente ambos *lexer* y *parser* están definidos teniendo en cuenta la extensibilidad en el sentido de que si se desea añadir una  nueva acción predefinida a los efectos es posible realizarlo.

# Sobre los literalDescribe:

Esto se refiere a cuando queremos describir literalmente cierta estructura de nuestro juego, ya sea un jugador o una carta, esta debe implementar la clase abstracta *LiteralDescribe<T>* donde *T* es el objeto que deseamos describir , por lo que debe implementar la interfaz *IDescribable* .  O sea los literalDescribe poseen internamente un mini-parser aportado por la implementación del objeto *T*. Los literalDescribe (internamente) usan el source obtenido a través de la interfaz *IArgument<T>* para buscar el objeto descrito en cuestión. 

# IArgument<T>:

Cuando una action necesita como argumento un objeto *T*, ha de ser un objeto que implemente esta interfaz, esta posee un método que toma el *Contexto* de nuestro juego y un *IEnumerable* que va a ser un source para que en el caso de que el argumento sea de tipo *LiteralDescribe* este lo use para encontrar lo que busca, en caso de que sea una *Action* lo que usará será el contexto.

# Como añadir nuevas acciones a nuestro lenguaje:

Una acción predefinida ha de implementar la clase abstracta *Return<T>* donde T será el objeto específico que se va a devolver. Su constructor debe tomar en el mismo orden en que van a ser parseados los elementos que necesita para crearse, empezando por un paréntesis abierto y acabando en un paréntesis cerrado.  Ella misma define los argumentos que necesita mandando al parser a que los parsee, debe ser añadida a las acciones predefinidas de la Factory. 

Ejemplos: una vez creada la clase que implementa la interfaz *Return<T>* debe ser añadida de la siguiente forma:

```csharp
        factory.AddPredefined
        (
            (x, parser) => x == "$intercambiardoscartas" ? new IntercambiarDosCartas(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        factory.AddPredefined
        (
            (x, parser) => x == "$añadircarta" ? new AñadirCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$holdcard" ? new Card_Hold(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$getcardvalue" ? new Getcardvalue(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$exist" ? new Exist(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento) ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$finalround" ? new FinalRoundEffect(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.Match(Tipo.Argumento),  (IFirst)parser.ParseAction() ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$getdinero" ? new Getdinero(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>() ,  parser.Match(Tipo.ParéntesisCerrado)) : null!
        );
```

Esto hará que al leer el txt con el efecto el minilenguaje identifique la acción.

## Mejoras, Ideas, Bugs:

El minilenguaje posee una syntaxis horrible cuando necesita asignar variables y hacer operaciones con bools, e int, en particular necesita un símbolo como # para distinguir que uso debe darle a cada palabra, esto da más consistencia, pero no se ve bien.

Los efectos realizados con el minilenguaje son estáticos en sentido que no dependen de el jugador que los realiza, no poseen un identificador.
