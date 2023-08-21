public struct CharacterSelection
{
    public static string[] CharacterNames = new string[]
    {
        "Mario",
        "Donkey Kong",
        "Link",
        "Samus",
        "Zero Suit Samus",
        "Yoshi",
        "Kirby",
        "Fox",
        "Pikachu",
        "Luigi",
        "Captain Falcon",
        "Ness",
        "Bowser/Giga Bowser",
        "Peach",
        "Zelda",
        "Sheik",
        "Ice Climbers",
        "Marth",
        "Mr. Game & Watch",
        "Falco",
        "Ganondorf",
        "Wario/Wario-Man",
        "Meta Knight",
        "Pit",
        "Olimar & Pikmin",
        "Lucas",
        "Diddy Kong",
        "Pokémon Trainer",
        "Charizard",
        "Squirtle",
        "Ivysaur",
        "King Dedede",
        "Lucario",
        "Ike",
        "R.O.B.",
        "Jigglypuff",
        "Toon Link",
        "Wolf",
        "Snake",
        "Sonic",
        "None",
        "Random",
        "Knuckles (Project+)",
        "Roy (Project M)",
        "Mewtwo (Project M)",
        "Red Alloy",
        "Blue Alloy",
        "Yellow Alloy",
        "Green Alloy",
        // 0x3F to 0x7F are extra fighters
        "ExFighter0x3F", "ExFighter0x40", "ExFighter0x41", "ExFighter0x42",
        "ExFighter0x43", "ExFighter0x44", "ExFighter0x45", "ExFighter0x46",
        "ExFighter0x47", "ExFighter0x48", "ExFighter0x49", "ExFighter0x4A",
        "ExFighter0x4B", "ExFighter0x4C", "ExFighter0x4D", "ExFighter0x4E",
        "ExFighter0x4F", "ExFighter0x50", "ExFighter0x51", "ExFighter0x52",
        "ExFighter0x53", "ExFighter0x54", "ExFighter0x55", "ExFighter0x56",
        "ExFighter0x57", "ExFighter0x58", "ExFighter0x59", "ExFighter0x5A",
        "ExFighter0x5B", "ExFighter0x5C", "ExFighter0x5D", "ExFighter0x5E",
        "ExFighter0x5F", "ExFighter0x60", "ExFighter0x61", "ExFighter0x62",
        "ExFighter0x63", "ExFighter0x64", "ExFighter0x65", "ExFighter0x66",
        "ExFighter0x67", "ExFighter0x68", "ExFighter0x69", "ExFighter0x6A",
        "ExFighter0x6B", "ExFighter0x6C", "ExFighter0x6D", "ExFighter0x6E",
        "ExFighter0x6F", "ExFighter0x70", "ExFighter0x71", "ExFighter0x72",
        "ExFighter0x73", "ExFighter0x74", "ExFighter0x75", "ExFighter0x76",
        "ExFighter0x77", "ExFighter0x78", "ExFighter0x79", "ExFighter0x7A",
        "ExFighter0x7B", "ExFighter0x7C", "ExFighter0x7D", "ExFighter0x7E",
        "ExFighter0x7F",
    };

    public byte CharacterID;
    public float CursorPositionX;
    public float CursorPositionY;
    public float NamePositionX;
    public float NamePositionY;

    public CharacterSelection(byte[] characterSelectionBytes)
    {
        CharacterID = characterSelectionBytes[0];
        CursorPositionX = BitConverter.ToSingle(characterSelectionBytes, 4);
        CursorPositionY = BitConverter.ToSingle(characterSelectionBytes, 8);
        NamePositionX = BitConverter.ToSingle(characterSelectionBytes, 12);
        NamePositionY = BitConverter.ToSingle(characterSelectionBytes, 16);
        // Reverse endianness
        if (BitConverter.IsLittleEndian)
        {
            CursorPositionX = BitConverter.ToSingle(BitConverter.GetBytes(CursorPositionX).Reverse().ToArray(), 0);
            CursorPositionY = BitConverter.ToSingle(BitConverter.GetBytes(CursorPositionY).Reverse().ToArray(), 0);
            NamePositionX = BitConverter.ToSingle(BitConverter.GetBytes(NamePositionX).Reverse().ToArray(), 0);
            NamePositionY = BitConverter.ToSingle(BitConverter.GetBytes(NamePositionY).Reverse().ToArray(), 0);
        }
    }

    public string GetCharacterName()
    {
        if (CharacterID < CharacterNames.Length)
            return CharacterNames[CharacterID];
        else
            return "Unknown";
    }
}
