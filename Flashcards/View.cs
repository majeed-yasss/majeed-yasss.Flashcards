using Spectre.Console;
namespace Flashcards;
internal class View
{
    public static Options.WelcomeMenu WelcomeMenu()
    {
        Rule rule = new("Welcome!");
        rule.LeftJustified();

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Options.WelcomeMenuLabels.Keys));

        return Options.WelcomeMenuLabels[option];
    }
    public static Options.MainMenu MainMenu()
    {
        Rule rule = new("Main Menu");
        rule.LeftJustified();

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(Enum.GetNames<Options.MainMenu>()));

        return Enum.Parse<Options.MainMenu>(option);
    }
    public static Options.WorkingStackMenu StackMenu(string name, int quantity)
    {
        Rule r1 = new("Stack Menu");
        r1.LeftJustified();

        Rule r2 = new($"Current wroking stack: {name}");
        r2.Centered();

        Rule r3 = new($"Flashcards quantity: {quantity}");
        r3.RightJustified();

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

    internal static string Read(string MarkupMsg, int limit)
    {
        string value;
        do value = AnsiConsole.Ask<string>(MarkupMsg);
        while (value is null || value.Length > limit);
        return value;
    }
}
