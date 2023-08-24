public struct SelectCharacterFile
{
    public UtArchiveHeader Header;
    public CharacterTable[] CharacterTables;
    public FileStream fileStream;
    public SelectCharacterFile(FileStream rootFile)
    {
        fileStream = rootFile;
        // Deserialize first 20 bytes into header
        byte[] headerBytes = new byte[20];
        fileStream.Read(headerBytes, 0, 20);
        Header = new UtArchiveHeader(headerBytes);

        // Deserialize character tables
        // Use Header.LookupEntryCount to determine how many tables to deserialize
        CharacterTables = new CharacterTable[Header.LookupEntryCount];
        int offset = 0x20;
        for (int i = 0; i < Header.LookupEntryCount; i++)
        {
            CharacterTables[i] = NextCharacterTable(ref offset);
        }
    }

    private CharacterTable NextCharacterTable(ref int offset)
    {
        // Start from offset until FF is reached, then add the FF and 19 other remaining bytes to finish the table
        // Repeat until Header.LookupEntryCount is reached
        List<byte> characterTableBytes = new List<byte>();
        int startOffset = offset;
        // There are some inconsistencies in the character table separators
        // So we need to make sure we stay in the right range
        // Is first byte in range?
        fileStream.Seek(offset, SeekOrigin.Begin);
        // Look ahead 0x14 bytes from offset
        fileStream.Seek(offset + 0x14, SeekOrigin.Begin);
        byte lookAheadByte = (byte)fileStream.ReadByte();
        if (lookAheadByte > 0x7F)
        {
            // Offset by 4 bytes and restart
            offset += 4;
            return NextCharacterTable(ref offset);
        }
        byte currentByte = 0x00;
        while (currentByte != 0xFF)
        {
            fileStream.Seek(offset, SeekOrigin.Begin);
            byte newByte = (byte)fileStream.ReadByte();
            currentByte = newByte;
            characterTableBytes.Add(currentByte);
            offset++;
        }
        // Add the FF and 19 other remaining bytes to finish the table
        fileStream.Seek(offset, SeekOrigin.Begin);
        characterTableBytes.Add(currentByte);
        byte[] remainingBytes = new byte[19];
        fileStream.Read(remainingBytes, 0, 19);
        characterTableBytes.AddRange(remainingBytes);
        offset += 19;
        return new CharacterTable(startOffset, characterTableBytes.ToArray());
    }

    // Saving is performed by writing the header, then each character table using their offsets
    // The footer is kept intact for now, but if values are added or removed, it will need to be updated
    public void Save(string filePath)
    {
        // Save original file as .bak
        string backupPath = filePath + ".bak";
        fileStream.Seek(0, SeekOrigin.Begin);
        using (FileStream backupStream = new FileStream(backupPath, FileMode.Create))
        {
            fileStream.CopyTo(backupStream);
        }
        // Close fileStream
        fileStream.Close();
        // Copy bak file to filePath
        using (FileStream newFileStream = new FileStream(filePath, FileMode.Create))
        {
            using (FileStream backupStream = new FileStream(backupPath, FileMode.Open))
            {
                backupStream.CopyTo(newFileStream);
            }
        }
        // Open fileStream for writing
        fileStream = new FileStream(filePath, FileMode.Open);

        // Use big endian for saving
        if (BitConverter.IsLittleEndian)
        {
            Header.FileSize = BitConverter.ToInt32(BitConverter.GetBytes(Header.FileSize).Reverse().ToArray(), 0);
            Header.LookupOffset = BitConverter.ToInt32(BitConverter.GetBytes(Header.LookupOffset).Reverse().ToArray(), 0);
            Header.LookupEntryCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.LookupEntryCount).Reverse().ToArray(), 0);
            Header.SectionCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.SectionCount).Reverse().ToArray(), 0);
            Header.ExtSubroutineCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.ExtSubroutineCount).Reverse().ToArray(), 0);
        }

        // Write header
        fileStream.Seek(0, SeekOrigin.Begin);
        fileStream.Write(BitConverter.GetBytes(Header.FileSize), 0, 4);
        fileStream.Write(BitConverter.GetBytes(Header.LookupOffset), 0, 4);
        fileStream.Write(BitConverter.GetBytes(Header.LookupEntryCount), 0, 4);
        fileStream.Write(BitConverter.GetBytes(Header.SectionCount), 0, 4);
        fileStream.Write(BitConverter.GetBytes(Header.ExtSubroutineCount), 0, 4);
        fileStream.Write(Header.Padding, 0, 12);

        // Undo endianness
        if (BitConverter.IsLittleEndian)
        {
            Header.FileSize = BitConverter.ToInt32(BitConverter.GetBytes(Header.FileSize).Reverse().ToArray(), 0);
            Header.LookupOffset = BitConverter.ToInt32(BitConverter.GetBytes(Header.LookupOffset).Reverse().ToArray(), 0);
            Header.LookupEntryCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.LookupEntryCount).Reverse().ToArray(), 0);
            Header.SectionCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.SectionCount).Reverse().ToArray(), 0);
            Header.ExtSubroutineCount = BitConverter.ToInt32(BitConverter.GetBytes(Header.ExtSubroutineCount).Reverse().ToArray(), 0);
        }

        // Write character tables
        for (int i = 0; i < CharacterTables.Length; i++)
        {
            CharacterTable characterTable = CharacterTables[i];
            // Write character table
            fileStream.Seek(characterTable.Offset, SeekOrigin.Begin);
            for (int j = 0; j < characterTable.CharacterSelection.Length; j++)
            {
                CharacterSelection characterSelection = characterTable.CharacterSelection[j];
                // Use big endian for saving
                if (BitConverter.IsLittleEndian)
                {
                    characterSelection.CursorPositionX = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.CursorPositionX).Reverse().ToArray(), 0);
                    characterSelection.CursorPositionY = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.CursorPositionY).Reverse().ToArray(), 0);
                    characterSelection.NamePositionX = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.NamePositionX).Reverse().ToArray(), 0);
                    characterSelection.NamePositionY = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.NamePositionY).Reverse().ToArray(), 0);
                }
                fileStream.Write(new byte[] {characterSelection.CharacterID, 0x00, 0x00, 0x00}, 0, 4);
                fileStream.Write(BitConverter.GetBytes(characterSelection.CursorPositionX), 0, 4);
                fileStream.Write(BitConverter.GetBytes(characterSelection.CursorPositionY), 0, 4);
                fileStream.Write(BitConverter.GetBytes(characterSelection.NamePositionX), 0, 4);
                fileStream.Write(BitConverter.GetBytes(characterSelection.NamePositionY), 0, 4);
                // Undo endianness
                if (BitConverter.IsLittleEndian)
                {
                    characterSelection.CursorPositionX = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.CursorPositionX).Reverse().ToArray(), 0);
                    characterSelection.CursorPositionY = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.CursorPositionY).Reverse().ToArray(), 0);
                    characterSelection.NamePositionX = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.NamePositionX).Reverse().ToArray(), 0);
                    characterSelection.NamePositionY = BitConverter.ToSingle(BitConverter.GetBytes(characterSelection.NamePositionY).Reverse().ToArray(), 0);
                }
            }
            // Write separator
            fileStream.Write(characterTable.Unknown, 0, 20);
        }
    }
}