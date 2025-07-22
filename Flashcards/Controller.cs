using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards;

public class Controller
{
    private static readonly Model _model = new();
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
            case Options.MainMenu.Stacks: StacksMgr(); break;
            case Options.MainMenu.Study: StudyMgr(); break;
            case Options.MainMenu.Sessions: Sessions(); break;
            default: View.Exit(); break;
        };
    }
    private static void StacksMgr()
    {
        Options.WorkingStackMenu option = View.StackMenu();
        switch (option)
        {
            case Options.WorkingStackMenu.Flashcards: StacksMgr(); break;
            case Options.WorkingStackMenu.Change: StacksMgr(); break;
            case Options.WorkingStackMenu.Create: StacksMgr(); break;
            case Options.WorkingStackMenu.Delete: StacksMgr(); break;
            
            default: Excute(View.MainMenu()); break;
        };
    }
    private static void StudyMgr()
    {
        throw new NotImplementedException();
    }
    private static void Sessions()
    {
        throw new NotImplementedException();
    }
    private static void CreateStack()
    {
        string name = View.Read("Enter Stack Name: ", 50);
        _model.CreateStack(name);
    }
}
