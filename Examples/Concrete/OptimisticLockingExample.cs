namespace Locking;

public class OptimisticLockingExample : ILockingExample
{
    private class Item
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Version { get; set; }
    }

    private List<Item> Items { get; set; } =
    [
        new Item() { Id = 1, Content = "Hello World", Version = 0 },
    ];

    public void Execute()
    {
        //Using versions allow to not block users from save itself, although it handles exception and notifies user
        //that update was not successful
        for (int i = 0; i < 2; i++)
        {
            var userIndex = i;
            var t = new Thread(() =>
            {
                var itemId = 1;
                var originalVersion = GetItemById(itemId)?.Version;
                Thread.Sleep(1000);
                var item = GetItemById(itemId);
                Console.WriteLine($"User {userIndex} tries to update item.");
                if (item.Version == originalVersion)
                {
                    item.Content = "Updated Content";
                    item.Version++;
                    Console.WriteLine("Updated content successfully");
                }
                else
                {
                    Console.WriteLine("Version mismatch. Update failed.");
                }
            });
            t.Start();
        }
    }

    private Item? GetItemById(int itemId)
    {
        return Items.FirstOrDefault(i => i.Id == itemId);
    }
}