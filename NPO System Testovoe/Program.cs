using NPO_System_Testovoe;
using System.Text.Json;
using System.Xml.Serialization;

internal class Program
{
    private static Tender? GetTenderByURL(string url)
    {
        var uri = new Uri(url);
        if (uri.Host != "tenmon.ru")
        {
            Console.WriteLine("Поддерживается только tenmon.ru");
            return null;
        }

        string apiUrl = $"https://tenmon.ru/api/lot-cards{uri.AbsolutePath}";
        Console.WriteLine(uri.AbsolutePath);

        using HttpClient wc = new();
        Console.WriteLine("Получаем информацю...");
        var resp = wc.GetStringAsync(apiUrl);
        resp.Wait();

        if (!resp.IsCompletedSuccessfully)
        {
            Console.WriteLine("Не удалось получить информацию");
            return null;
        }

        var json = resp.Result;
        TenderJson? parsed =
            JsonSerializer.Deserialize<TenderJson>(json);

        if (parsed == null || (parsed.customers == null || parsed.customers.Count < 1) || (parsed.cost == null || !parsed.cost.ContainsKey("original")))
        {
            Console.WriteLine("Не удалось обработать информацию");
            return null;
        }

        return new Tender(
            parsed.govRuId ?? "0",
            parsed.tenderTitle ?? "unknown",
            parsed.cost["original"].cost ?? 0,
            parsed.cost["original"].currency?.alpha ?? "RUB",
            DateTime.Parse(parsed.datePublic ?? "0"),
            parsed.customers[0].title ?? "uknown",
            parsed.customers[0].inn
        );
    }

    private static Tender? GetTenderByPath(string path)
    {
        if (Path.GetExtension(path) != ".xml")
        {
            Console.WriteLine($"Поддерживаются только файлы с расширением XML.");
            return null;
        }

        TenderXml? obj;
        try
        {
            StreamReader xmlStream = new(path);
            XmlSerializer serializer = new(typeof(TenderXml));
            obj = serializer.Deserialize(xmlStream) as TenderXml;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Не удалось прочесть файл: " + ex.GetBaseException().Message);
            return null;
        }

        if (obj == null)
        {
            Console.WriteLine("Не удалось обработать файл");
            return null;
        }

        var parsed = obj.body;
        if (parsed.lot?.customers.list.Count == 0)
        {
            Console.WriteLine("Не удалось обработать файл");
            return null;
        }

        return new Tender(
            parsed.govRuId ?? "0",
            parsed.tenderTitle ?? "unknown",
            parsed.lot?.price ?? 0,
            parsed.lot?.currency.code ?? "RUB",
            DateTime.Parse(parsed.datePublic ?? "0"),
            parsed.lot?.customers.list[0].info.title ?? "unknown",
            null
        );
    }

    static async Task Main(string[] args)
    {
        string? userInput = args.Length >= 1 ? args[0] : null;
        while (userInput == null)
        {
            Console.WriteLine("Введите путь/ссылку для получения информации о тендере:");
            userInput = Console.ReadLine();
        }

        Console.WriteLine("Поиск информации по пути/ссылке: \"" + userInput + "\"");

        Tender? tender;
        if (userInput.StartsWith("http"))
            tender = GetTenderByURL(userInput);
        else
            tender = GetTenderByPath(userInput);

        if (tender == null)
        {
            Console.WriteLine("Нажмите любую клавишу, чтобы закрыть это окно:");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Информация обработана! Желаете ли сохранить этот тендер в БД? (y/n):");
        string yesno = Console.ReadLine() ?? "n";

        if (yesno.ToLower() != "y")
            return;

        Console.WriteLine("Подключаемся к БД...");
        var db = new DataBase();
        try
        {
            await db.SaveTender(tender);
        }
        catch(Exception e)
        {
            Console.WriteLine("Ошибка БД: " + e.GetBaseException().Message);
        }

        Console.WriteLine("Нажмите любую клавишу, чтобы закрыть это окно:");
        Console.ReadLine();
    }
}