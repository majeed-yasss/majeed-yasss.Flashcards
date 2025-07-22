using DataObjects;
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
}
