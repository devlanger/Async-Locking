namespace Locking;

public class PessimisticLockingProcessor : ILockingExample
{
    private readonly string _filePath;
    private readonly object lockObject = new object();

    public PessimisticLockingProcessor(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Execute()
    {
        //Locks resources and each new iteration waits for a lock to be released. This introduces waiting time.
        for (var i = 0; i < 10; i++)
        {
            var t = new Thread(() =>
            {
                lock (lockObject)
                {
                    File.WriteAllText(_filePath, "Text1");
                    Thread.Sleep(1000);
                    File.WriteAllText(_filePath, "Text2");
                }
            });
    
            t.Start();
        }
    }
}