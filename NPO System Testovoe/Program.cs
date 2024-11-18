using NPO_System_Testovoe;

internal class Program
{
    private static void Main(string[] args)
    {
        string? url = args.Length >= 1 ? args[0] : null;
        while (url == null)
        {
            Console.WriteLine("Введите путь/ссылку для получения информации о тендере:");
            url = Console.ReadLine();
        }

        Console.WriteLine("Поиск информации по пути/ссылке: " + url);

        if (url.StartsWith("http"))
        {
            var uri = new Uri(url);
            Console.WriteLine(uri.AbsolutePath);
        }

        // var test = new Tender(12, "privte", 12, "RUB", DateTime.Now, "HI", null);
        // Console.WriteLine(test.tenderName);
    }
}