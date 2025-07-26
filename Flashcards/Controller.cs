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
            case Options.MainMenu.Flashcards: FlashcardsMgr(); break;
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
            case Options.WorkingStackMenu.Change: ChangeCurrentStack(); break;
            case Options.WorkingStackMenu.Create: CreateStack(); break;
            case Options.WorkingStackMenu.Rename: RenameStack(); break;
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
        string name;
        do name = View.Read("Enter Stack Name: ", 50);
        while (_model.IsExistStack(name));
        _model.CreateStack(name);
    }
    private static void RenameStack()
    {
        string name;
        do name = View.Read("Enter [red](Unique)[/] Stack Name: ", 50);
        while (_model.IsExistStack(name));
        _model.RenameStack(_currentStack.Id,name);
        _currentStack.Name = name;
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
            case Options.FlashcardsMenu.Edit: EditFlashcard(); break;
            case Options.FlashcardsMenu.Delete: DeleteFlashcard(); break;

            default: break;
        };
    }
    private static void ChangeCurrentStack()
    {
        var records = _model.RetriveRecords<Stack>();
        if (!records.Any()) _currentStack = null;
        else _currentStack = View.Select(records,
            "Select the [green]Stack[/] you want to work with:");
    }
    private static void DeleteStack()
    {
        _model.Delete<Stack>(_currentStack);
        ChangeCurrentStack();
    }
    private static Flashcard MockFlashcard()
    {
        string front = View.Read("Enter the question (card's front):", 100);
        string back = View.Read("Enter the answer (card's back):", 100);
        return new Flashcard(_currentStack.Id, front, back);
    }
    private static void CreateFlashcard()
    {
        Flashcard card = MockFlashcard();
        
        if (View.Confirm($"  {card}\n[yellow]Create Card?[/]", true))
            _model.CreateFlashcard(card);
        if (View.Confirm("Create [yellow]another Flashcard?[/]", false))
            CreateFlashcard();
    }
    private static void ViewFlashcards()
    {
        var records = _model.RetriveRecords<Flashcard>(_currentStack.Id);
        View.Show(records,_currentStack.Name);
    }
    private static void EditFlashcard()
    {
        var records = _model.RetriveRecords<Flashcard>(_currentStack.Id);
        if (!records.Any())
        {
            View.Massage($"{_currentStack.Name} Stack has no Flashcards!");
            return;
        }

        Flashcard toEdit = View.Select<Flashcard>(records, _currentStack.Name);
        if (!View.Confirm($"  {toEdit}\n Proceed [yellow]Editing this card?[/]", true))
            return;

        Flashcard newCard = MockFlashcard();
        string msg = $"Before:\n  {toEdit}\n" +
                    $"After:\n  {newCard}\n" +
                    $"[yellow]Confirm Edit?[/]";

        if (View.Confirm(msg, true)) _model.EditFlashcard(toEdit.Id, newCard);

        if (View.Confirm("Edit [yellow]another Flashcard?[/]", false))
            EditFlashcard();
    }
    private static void DeleteFlashcard()
    {
        var records = _model.RetriveRecords<Flashcard>(_currentStack.Id);
        if (!records.Any())
        {
            View.Massage($"{_currentStack.Name} Stack has no Flashcards!");
            return;
        }

        Flashcard toDelete = View.Select<Flashcard>(records, _currentStack.Name);
        if (View.Confirm($"  {toDelete}\n [red]Delete this card?[/]", true))
            _model.Delete<Flashcard>(toDelete.Id);

        if (View.Confirm("Delete [red]another Flashcard?[/]", false))
            DeleteFlashcard();
    }
}
