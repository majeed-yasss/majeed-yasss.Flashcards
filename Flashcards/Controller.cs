using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using DataObjects;
namespace Flashcards;

public class Controller
{
    private static readonly Model _model = new();
    private static Stack? _currentStack = null;
    public static void Run()
    {
        ChangeCurrentStack();
        while (true) 
        {
            if (_currentStack is null) Excute(View.WelcomeMenu());
            else Excute(View
                .EnumMenu<Options.MainMenu>("Main Menu",_currentStack.Name));
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
        if (_currentStack is null) return;
        switch (option)
        {
            case Options.MainMenu.Stacks: StacksMgr(); break;
            case Options.MainMenu.Study: StartStudy(); break;
            case Options.MainMenu.Sessions: Sessions(); break;
            default: View.Exit(); break;
        };
    }
    private static void StacksMgr()
    {
        if (_currentStack is null) return;
        Options.WorkingStackMenu option = View
            .EnumMenu<Options.WorkingStackMenu>("Stack Menu",_currentStack.Name);
        switch (option)
        {
            case Options.WorkingStackMenu.Flashcards: FlashcardsMgr(); break;
            case Options.WorkingStackMenu.Change: ChangeCurrentStack(); break;
            case Options.WorkingStackMenu.Create: CreateStack(); break;
            case Options.WorkingStackMenu.Delete: DeleteStack(); break;
            
            default: break;
        };
    }
    private static void StartStudy()
    {
    }
    private static void Sessions()
    {
    }
    private static void CreateStack()
    {
        bool unique = false;
        do
        {
            string name = View.Read("Enter Stack Name: ", 50);
            try
            {
                _currentStack = _model.CreateStack(name);
                unique = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (!unique);
        
    }
    private static void FlashcardsMgr()
    {
        if (_currentStack is null) return;
        Options.FlashcardsMenu option = 
            View.EnumMenu<Options.FlashcardsMenu>("Flashcards",_currentStack.Name);
        switch (option)
        {
            case Options.FlashcardsMenu.Create: CreateFlashcard(); break;
            case Options.FlashcardsMenu.View: ViewFlashcards(); break;
            //case Options.FlashcardsMenu.Edit: EditFlashcard(); break;
            //case Options.FlashcardsMenu.Delete: DeleteFlashcard(); break;

            default: break;
        };
    }
    private static void ChangeCurrentStack()
    {
        var records = _model.RetriveRecords<Stack>();
        if (records.Count() == 0) _currentStack = null;
        else _currentStack = View.Select(records,
            "Select the [green]Stack[/] you want to work with:");
    }
    private static void DeleteStack()
    {
        _model.Delete<Stack>(_currentStack);
        ChangeCurrentStack();
    }
    private static void CreateFlashcard()
    {
        string front = View.Read("read front", 100);
        string back = View.Read("read back", 100);
        _model.CreateFlashcard(new Flashcard(
          _currentStack.Id, front, back));
        if (View.Confirm("Create another Flashcard?", false))
            CreateFlashcard();
    }
    private static void ViewFlashcards()
    {
        
    }

}
