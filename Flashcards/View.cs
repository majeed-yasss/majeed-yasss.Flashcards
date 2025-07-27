using DataObjects;
using Spectre.Console;
namespace Flashcards;
internal class View
{
    public static void Header(string MarkupMsg, string name, int quantity)
    {
        Console.Clear();
        Rule r1 = new(MarkupMsg);
        r1.LeftJustified(); r1.RuleStyle("cyan");

        Rule r2 = new($"Current wroking stack: {name}");
        r2.Centered(); r2.RuleStyle("green");

        Rule r3 = new($"Flashcards quantity: {quantity}");
        r3.RightJustified(); r3.RuleStyle("yellow");

        AnsiConsole.Write(r1);
        AnsiConsole.Write(r2);
        AnsiConsole.Write(r3);
    }
    public static Options.WelcomeMenu WelcomeMenu()
    {
        Console.Clear();
        Rule rule = new("Welcome!");
        rule.LeftJustified();

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Options.WelcomeMenuLabels.Keys));

        return Options.WelcomeMenuLabels[option];
    }
    // Generic Enum Menu
    public static T EnumMenu<T>(string title,string name) where T : struct, Enum
    {
        Header(title, name, 0);
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames<T>()));

        return Enum.Parse<T>(option);
    }
    internal static void Exit()
    {
        //Console.Clear();
        Massage("exiting..");
        Environment.Exit(0);
    }
    internal static void Massage(string MarkupMsg)
    {
        AnsiConsole.MarkupLine(MarkupMsg);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    internal static string Read(string MarkupMsg, int limit)
    {
        string value;
        do value = AnsiConsole.Ask<string>(MarkupMsg);
        while (value is null || value.Length > limit);
        return value;
    }
    internal static bool Confirm(string MarkupMsg,bool Default)
    {
        return AnsiConsole.Confirm(MarkupMsg, Default);
    }
    internal static T Select<T>(IEnumerable<T> items, string MarkupMassage="") where T : notnull
    {
        Console.Clear();
        AnsiConsole.Write(new Rule());

        if (!items.Any()) return default;

        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
            .Title(MarkupMassage)
            .AddChoices(items));
    }

    internal static void Show(IEnumerable<Flashcard> records, string title)
    {
        if (!records.Any())
        {
            Massage("[red]Empty Stack![/]"); return;
        }
        Table table = new();
        table.AddColumn(new TableColumn("ID").Centered());
        table.AddColumn(new TableColumn("Front").Centered());
        table.AddColumn(new TableColumn("Back").Centered());
        int id = 0;
        foreach (var flashcard in records)
            table.AddRow(Convert.ToString(++id),flashcard.Front, flashcard.Back);

        table.Title(title)
        .ShowRowSeparators()
        .RoundedBorder();

        Console.Clear();
        AnsiConsole.Write(table);
        Massage("");
    }
}
