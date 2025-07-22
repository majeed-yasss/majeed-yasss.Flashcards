using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards;
internal static class Options
{
    public enum WelcomeMenu { Create, Exit }
    public static Dictionary<string, WelcomeMenu> WelcomeMenuLabels =
        new() {
            { "Create your first Stack" , WelcomeMenu.Create },
            { "Exit" , WelcomeMenu.Exit }
        };
    public enum MainMenu { Stacks, Study, Sessions, Exit }
    //public static Dictionary<string, MainMenu> MainMenuLabels = ...
    public enum WorkingStackMenu { Flashcards, Change, Create, Return }
    //public static Dictionary<string, WorkingStackMenu> WorkingStackMenuLabels = ...
    public enum FlashcardsMenu { Create, View, Edit, Delete, Return }
}
