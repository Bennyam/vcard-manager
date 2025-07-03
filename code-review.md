## Code Review

Solide implementatie ;-).

### Beetje stof :
- UnitTest1.cs => remove

### Classes

#### Menu
- dubbele code (... smeekt om een kleine functie):
```csharp
console.Write("Voornaam: ");
var first = console.ReadLine();
console.Write("Achternaam: ");
var last = console.ReadLine();
```
Klasse kan nog meer delegeren, nieuwe klasse(s) i.p.v. private methods bvb.
User interactie liefst apart.

Ook nog wel wat testen nodig hier.



