public class FileWriter : IWriter
{
    private readonly string _filePath;

    public FileWriter(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Write(string content)
    {
        File.WriteAllText(_filePath, content);
    }
}