using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards;

public class Controller
{
    private readonly Model _model = new();
    private static bool _currentStack = false;
    public static void Run()
    {
        while (true) 
        {
            if (_currentStack) Excute(View.MainMenu());
            else Excute(View.WelcomeMenu());

        }
    }
    private static void Excute(Options.WelcomeMenu option)
    {
        switch (option)
        {
            case Options.WelcomeMenu.Create: CreateStack(); break;
            default: View.Exit(); break; 
        }
    }
    private static void Excute(Options.MainMenu option)
    {
         switch (option)
        {
            case Options.MainMenu.Stacks: Stacks(); break;
            case Options.MainMenu.Study: Study(); break;
            case Options.MainMenu.Sessions: Sessions(); break;
            default: View.Exit(); break;
        };
    }
    private static void CreateStack()
    {
        throw new NotImplementedException();
    }
}
