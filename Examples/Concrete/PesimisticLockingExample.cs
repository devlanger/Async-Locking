namespace Locking;

public class PessimisticLockingExample(string filePath) : ILockingExample
{
    private readonly object lockObject = new object();

    public void Execute()
    {
        //Locks resources and each new iteration waits for a lock to be released. This introduces waiting time.
        for (var i = 0; i < 10; i++)
        {
            var t = new Thread(() =>
            {
                lock (lockObject)
                {
                    Write("Text1");
                    Thread.Sleep(1000);
                    Write("Text2");
                }
            });
    
            t.Start();
        }
    }

    private void Write(string content)
    {
        File.WriteAllText(filePath, content);
    }
}