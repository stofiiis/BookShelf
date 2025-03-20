using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = "data.csv";
        Dictionary<string, string[]> data = new Dictionary<string, string[]>(); // Klíč = ISBN

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4)
                    {
                        string isbn = parts[1].Trim(); // ISBN jako klíč
                        string[] values = { parts[0].Trim(), parts[2].Trim(), parts[3].Trim() }; // ID, Autor, Název
                        data[isbn] = values;
                    }
                }
            }

            Console.WriteLine("Data byla načtena. Zvolte způsob vyhledávání:");
            Console.WriteLine("1 - Podle ISBN");
            Console.WriteLine("2 - Podle jména autora");
            Console.WriteLine("3 - Podle názvu knihy");
            Console.WriteLine("Napište 'exit' pro ukončení.");

            while (true)
            {
                Console.Write("\nZadejte volbu: ");
                string choice = Console.ReadLine()?.Trim().ToLower();

                if (choice == "exit")
                    break;

                switch (choice)
                {
                    case "1":
                        SearchByISBN(data);
                        break;
                    case "2":
                        SearchByAuthor(data);
                        break;
                    case "3":
                        SearchByTitle(data);
                        break;
                    default:
                        Console.WriteLine("❌ Neplatná volba. Zkuste znovu.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Chyba při čtení souboru: {ex.Message}");
        }
    }

    static void SearchByISBN(Dictionary<string, string[]> data)
    {
        Console.Write("Zadejte ISBN: ");
        string isbn = Console.ReadLine()?.Trim();

        if (data.TryGetValue(isbn, out string[] bookInfo))
        {
            Console.WriteLine($"\nISBN: {isbn} | ID: {bookInfo[0]} | Autor: {bookInfo[1]} | Název: {bookInfo[2]}\n");
        }
        else
        {
            Console.WriteLine("❌ ISBN nebylo nalezeno.");
        }
    }

    static void SearchByAuthor(Dictionary<string, string[]> data)
    {
        Console.Write("Zadejte jméno autora: ");
        string author = Console.ReadLine()?.Trim().ToLower();

        var results = data.Where(kv => kv.Value[1].ToLower().Contains(author)).ToList();

        if (results.Any())
        {
            Console.WriteLine("\nVýsledky hledání:");
            foreach (var item in results)
            {
                Console.WriteLine($"ISBN: {item.Key} | ID: {item.Value[0]} | Autor: {item.Value[1]} | Název: {item.Value[2]}");
            }
        }
        else
        {
            Console.WriteLine("❌ Autor nebyl nalezen.");
        }
    }

    static void SearchByTitle(Dictionary<string, string[]> data)
    {
        Console.Write("Zadejte název knihy: ");
        string title = Console.ReadLine()?.Trim().ToLower();

        var results = data.Where(kv => kv.Value[2].ToLower().Contains(title)).ToList();

        if (results.Any())
        {
            Console.WriteLine("\nVýsledky hledání:");
            foreach (var item in results)
            {
                Console.WriteLine($"ISBN: {item.Key} | ID: {item.Value[0]} | Autor: {item.Value[1]} | Název: {item.Value[2]}");
            }
        }
        else
        {
            Console.WriteLine("❌ Název knihy nebyl nalezen.");
        }
    }
}
