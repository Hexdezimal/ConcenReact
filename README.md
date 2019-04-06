# ConcenReact
Softwareprojekt 2019, IT17a

Gruppe C

# Doku:

## Steuerung (TODO: Anpassbare Steuerung)
- WASD/Richtungstaste - Bewegen im Menü/ im Spiel
- ESC - Zurück zum Hauptmenü
- I - Inventar
- B - Debug-Battlescreen starten

## Charakter-Klasse
Die abstrakte Charakter-Klasse beinhaltet die Bitmaps und den Namen des Charakters

### Player-Klasse
Die Player-Klasse ist abgeleitet von der Charakter-Klasse und beinhaltet Hauptsächlich Daten wie

#### Attribute
- int xPos, yPos - logische Position auf dem Spielfeld
- List<int> scores - generische Liste von scores (zZ. nicht Verwendet)
- GUID saveGUID - Globally Unique Identifier - einzigartige ID für Savegame (zZ. nicht Verwendet)
- List<Item> items - generische Liste von items, beinhaltet den Itemanteil des Inventars (Nicht ausgerüstete Gegenstände)
- int hp, maxHp - aktuelle hp-Wert und der maximale des Charakters (Später hin durch level-ups und Items erhöhbar
- int Inventarplätze - Anzahl der max. Inventar-Plätze
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
Die Armor-Klasse ist abgeleitet von der ItemWithIcon-Basisklasse und beinhaltet zusätzlich einen sog. defenseModifier (in %)
#### Attribute
- double defenseModifier - Verteidigung um bestimmten %-Satz erhöhen

### Weapon-Klasse
Die Weapon-Klasse ist abgeleitet von der ItemWithIcon-Basisklasse und beinhaltet zusätzlich einen sog. attackModifier und scoreModifier (in %)
- double attackModifier - Angriff um bestimmten %-Satz erhöhen
- double scoreModifier - Score nach Kampf um bestimmten %-Satz erhöhen
