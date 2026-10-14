using NotePad.App.Models;

namespace NotePad.App.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
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

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
        {
            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }
        }

        await Shell.Current.GoToAsync("..");
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