# Detalles Estudiante

Nombre: Keylor Chacon Salas
Carné: FH18005886

## Commandos .NET

```
dotnet tool list --global

dotnet tool install --global dotnet-ef --version 9

dotnet tool update --global dotnet-ef --version 9.0.10

dotnet ef

---

dotnet new sln -n PP4

dotnet new console -n MyPP4Console -f "net8.0"

dotnet sln add MyPP4Console

---

cd MyPP4Console

---

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package CsvHelper

---

dotnet build

dotnet ef migrations add InitialCreate

dotnet ef database update

```

## Prompts

Prompt utilizada para los metodos de lectura de CSV y escritura de TSV:

https://chatgpt.com/s/t_6910364f891881918cfe91fe9b052137

## Preguntas

- ¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?
    - Code First: No se crea el `schema` con `constraints`, sino que las colecciones aparecen al hacer `inserts`. Imagino que esto se debe a lo dinámico y variable de los datos NoSQL.
    - Database First: En realidad de la misma forma. Al no haber un `schema` como tal no hay que convertir a código.

- ¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.
    - El punto y coma `;` también es utilizado comúnmente y de hecho también utiliza `.csv` como separador.