# ConcenReact
Softwareprojekt 2019, IT17a

Gruppe:
Dennis Ebel
Kevin Groß
Oliver Hoffmann
Richard McDonald
Richard Sodke


# Doku:

## Charakter-Klasse
Die abstrakte Charakter-Klasse beinhaltet die Bitmaps und den Namen des Charakters

### Player-Klasse
Die Player-Klasse ist abgeleitet von der Charakter-Klasse und beinhaltet Hauptsächlich Daten wie

#### Attribute
- int xPos, yPos
- List<int> icores
- List<Item> items
- int hp, maxHp
- int Inventarplätze
- int MovePower (= Wie viele Felder in einer Runde gelaufen werden können)
#### Methoden
- void ChangeColorToEnemy() - (Erhöht den Rotanteil und verringert den Blauanteil)
- void AddEquipWeapon(DebugForm debug, Weapon w) - (Versucht die hinzugefügte Waffe direkt auszurüsten -> ansonsten ans Inventar anhängen)
- void AddEquipArmor(Armor a) - (Versucht das gleiche wie AddEquipArmor(...), jedoch mit Rüstung)
- void AddItem(Item item) - Fügt ein Item dem Inventar hinzu


## Item-Klasse
Die abstrakte Item-Klasse beinhaltet die Bitmap, Namen und 2 Status-Indikatoren
- bool isUsable (Ob ein Item benutzbar ist -> Heilung)
- bool isEquipable (Ob ein Item ausrüstbar ist -> Waffe, Rüstung, etc.)
- Bitmap itemBitmap (Bitmap des Items, sichtbar im Inventar)

### Armor-Klasse
Die Armor-Klasse ist abgeleitet von der Item-Basisklasse und beinhaltet zusätzlich einen sog. defenseModifier (in %)
#### Attribute
- double defenseModifier

### Weapon-Klasse
Die Weapon-Klasse ist abgeleitet von der Item-Basisklasse und beinhaltet zusätzlich einen sog. attackModifier und scoreModifier (in %)
- double attackModifier
- double scoreModifier
