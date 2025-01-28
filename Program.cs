using Locking;

var filePath = Path.Combine(Directory.GetCurrentDirectory(), "LockTest.txt");

if (!File.Exists(filePath))
{
    File.Create(filePath);
}

IList<ILockingExample> examples = [new PessimisticLockingProcessor(filePath), new OptimisticLockingExample()];

Console.WriteLine("Please select a locking example:");
Console.WriteLine("0 - Pessimistic example");
Console.WriteLine("1 - Optimistic example");
Console.WriteLine("2 - Both examples");

var choice = 0;
while (true)
{
    var input = Console.ReadLine();
    var isValidInput = choice >= 0 && choice < examples.Count;
    if (int.TryParse(input, out choice) && isValidInput)
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid input. Try again.");
    }
}

switch (choice)
{
    case 0:
        Console.WriteLine("Executing pessimistic example...");
        examples[0].Execute();
        Console.WriteLine("Finished pessimistic example...");
        break;
    case 1:
        Console.WriteLine("Executing optimistic example...");
        examples[1].Execute();
        Console.WriteLine("Finished optimistic example...");
        break;
    case 2:
        Console.WriteLine("Executing both examples...");
        foreach (var example in examples)
        {
            example.Execute();
        }
        Console.WriteLine("Finished both examples...");
        break;
}