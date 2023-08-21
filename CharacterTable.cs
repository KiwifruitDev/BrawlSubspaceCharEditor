
public struct CharacterTable
{
    public int Offset;
    public CharacterSelection[] CharacterSelection;
    public byte[] Unknown;
    public CharacterTable(int offset, byte[] CharacterTableBytes)
    {
        Offset = offset;
        // Parse character selection 20 bytes at a time and stop at 20 bytes before the end
        List<CharacterSelection> characterSelectionList = new List<CharacterSelection>();
        for (int i = 0; i < CharacterTableBytes.Length - 20; i += 20)
        {
            byte[] characterSelectionBytes = new byte[20];
            Array.Copy(CharacterTableBytes, i, characterSelectionBytes, 0, 20);
            CharacterSelection characterSelection = new CharacterSelection(characterSelectionBytes);
            if(characterSelection.CharacterID != 255)
                characterSelectionList.Add(characterSelection);
            else
                break;
        }
        CharacterSelection = characterSelectionList.ToArray();
        Unknown = new byte[20];
        // Populate unknown bytes
        Array.Copy(CharacterTableBytes, CharacterTableBytes.Length - 20, Unknown, 0, 20);
    }
}
