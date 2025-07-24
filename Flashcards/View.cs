using Spectre.Console;
namespace Flashcards;
internal class View
{
    public static void Header(string MarkupMsg, string name, int quantity)
    {
        Console.Clear();
        Rule r1 = new(MarkupMsg);
        r1.LeftJustified();

        Rule r2 = new($"Current wroking stack: {name}");
        r2.Centered();

        Rule r3 = new($"Flashcards quantity: {quantity}");
        r3.RightJustified();

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
    public static Options.MainMenu MainMenu(string name)
    {
        Header("Main Menu", name, 0);
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames<Options.MainMenu>()));

        return Enum.Parse<Options.MainMenu>(option);
    }
    public static Options.WorkingStackMenu StackMenu(string name)
    {
        Header("Stack Menu", name, 0);
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames<Options.WorkingStackMenu>()));

        return Enum.Parse<Options.WorkingStackMenu>(option);
    }
    internal static void Exit()
    {
        //Console.Clear();
        Console.WriteLine("exiting..");
        Environment.Exit(0);
    }

    internal static void Massage(string MarkupMsg)
    {
        AnsiConsole.WriteLine(MarkupMsg);
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

    internal static T Select<T>(IEnumerable<T> items, string MarkupMassage="") where T : notnull
    {
        Console.Clear();
        AnsiConsole.Write(new Rule());

        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
            .Title(MarkupMassage)
            .AddChoices(items));
    }
}
