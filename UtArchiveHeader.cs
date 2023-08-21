public struct UtArchiveHeader
{
    public int FileSize;
    public int LookupOffset;
    public int LookupEntryCount;
    public int SectionCount;
    public int ExtSubroutineCount;
    public byte[] Padding;

    public UtArchiveHeader(byte[] headerBytes)
    {
        FileSize = BitConverter.ToInt32(headerBytes, 0);
        LookupOffset = BitConverter.ToInt32(headerBytes, 4);
        LookupEntryCount = BitConverter.ToInt32(headerBytes, 8);
        SectionCount = BitConverter.ToInt32(headerBytes, 12);
        ExtSubroutineCount = BitConverter.ToInt32(headerBytes, 16);
        Padding = new byte[12];
        // Reverse endianness
        if (BitConverter.IsLittleEndian)
        {
            FileSize = BitConverter.ToInt32(BitConverter.GetBytes(FileSize).Reverse().ToArray(), 0);
            LookupOffset = BitConverter.ToInt32(BitConverter.GetBytes(LookupOffset).Reverse().ToArray(), 0);
            LookupEntryCount = BitConverter.ToInt32(BitConverter.GetBytes(LookupEntryCount).Reverse().ToArray(), 0);
            SectionCount = BitConverter.ToInt32(BitConverter.GetBytes(SectionCount).Reverse().ToArray(), 0);
            ExtSubroutineCount = BitConverter.ToInt32(BitConverter.GetBytes(ExtSubroutineCount).Reverse().ToArray(), 0);
        }
    }
}
