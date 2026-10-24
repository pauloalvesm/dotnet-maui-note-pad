using NotePad.App.Models;
using NotePad.App.Services;

namespace NotePad.App.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    private readonly NoteService _noteService;

    public string ItemId
    {
        set => LoadNote(value);
    }

    public NotePage(NoteService noteService)
    {
        InitializeComponent();
        _noteService = noteService;

        var randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
        var fullPath = Path.Combine(FileSystem.AppDataDirectory, randomFileName);

        BindingContext = new Note
        {
            Filename = fullPath,
            Date = DateTime.Now
        };
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!await ValidateAndAlertAsync())
        {
            return;
        }

        if (BindingContext is Note note)
        {
            note.Text = TextEditor.Text;
            note.Date = DateTime.Now;

            _noteService.SaveNote(note);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
        {
            _noteService.DeleteNote(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async Task<bool> ValidateAndAlertAsync()
    {
        if (string.IsNullOrWhiteSpace(TextEditor.Text))
        {
            await DisplayAlertAsync("Warning", "The note cannot be empty.", "OK");
            return false;
        }

        return true;
    }

    public void LoadNote(string fileName)
    {
        BindingContext = _noteService.LoadNote(fileName);
    }
}