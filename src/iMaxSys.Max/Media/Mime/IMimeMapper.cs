namespace iMaxSys.Max.Media.Mime
{
    public interface IMimeMapper
    {
        IMimeMapper Extend(params MimeMappingItem[] extensions);
        string GetMimeFromExtension(string fileExtension);
        string GetMimeFromPath(string filePath);
    }
}