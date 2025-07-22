using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards;

public class Controller
{
    public static void Run()
    {
        while (true) 
        {
            MenuOption option = View.MainMenu();
            Excute(option);
        }
    }

    private static void Excute(MenuOption option)
    {
        throw new NotImplementedException();
    }
}
