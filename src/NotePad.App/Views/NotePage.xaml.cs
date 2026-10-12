using NotePad.App.Models;

namespace NotePad.App.Views;

public partial class NotePage : ContentPage
{
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public string ItemId
    {
        set
        {
            LoadNote(value);
        }
    }

    public NotePage()
    {
        InitializeComponent();

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
        if (BindingContext is Note note)
        {
            note.Text = TextEditor.Text;
            note.Date = DateTime.Now;

            File.WriteAllText(note.Filename, note.Text);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        TextEditor.Text = string.Empty;
    }

    public void LoadNote(string fileName)
    {
        var note = new Note
        {
            Filename = fileName
        };

        if (File.Exists(fileName))
        {
            note.Date = File.GetCreationTime(fileName);
            note.Text = File.ReadAllText(fileName);
        }

        BindingContext = note;
    }
}