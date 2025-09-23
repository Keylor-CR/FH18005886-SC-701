# Detalles Estudiante

Nombre: Keylor Chacon Salas
Carné: FH18005886

## Commandos .NET

```

dotnet new sln -n PP1

dotnet new console -n MyPP1Console

donet sln add MyPP1Console

dotnet run

```

## Prompts

Prompt utilizada al final debido a que la implementacion #4 falla debido a ser computacionalmente muy grande:
https://chatgpt.com/share/68d06f3b-6284-8007-8596-0c1f8447a505


##Preguntas

¿Por qué todos los valores resultantes tanto de n como de sum difieren entre métodos (fórmula e implementación iterativa) y estrategias (ascendente y descendente)?

Debido a que la version Iterativa de la funcion va recorriendo de 1 a N (y de vuelta) de uno en uno. Sin embargo, SumFor es una operacion que combina multiplicacion y divicion por lo cual el resultado en cada valor de n es diferente.
 
¿Qué cree que sucedería si se utilizan las mismas estrategias (ascendente y descendente) pero con el método recursivo de suma (SumRec)? [si desea puede implementarlo y observar qué sucede en ambos escenarios]

De la misma forma, si se empieza con SumRec de forma descendente, el SumRex(Max) es computacionalmente inviable de calcular.
