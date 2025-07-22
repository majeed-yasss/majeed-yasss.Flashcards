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
    private bool _currentStack = false;
    public static void Run()
    {
        while (true) 
        {
            Options.MainMenu option = View.MainMenu();
            Excute(option);
        }
    }
    private static void Excute(Options.WelcomeMenu option)
    {
        //option switch
        //{
        //Options.WelcomeMenu.Create ?  => CreateStack(),
        // _ => View.Exit()
        //};
    }
    private static void Excute(Options.MainMenu option)
    {
        //option switch
        //{
            //Options.MainMenu.Stacks => Stacks(),
            //Options.MainMenu.Flashcards => Flashcards(),
            //Options.MainMenu.Sessions => Sessions(),
            // _ => View.Exit()
        //};
    }


}
