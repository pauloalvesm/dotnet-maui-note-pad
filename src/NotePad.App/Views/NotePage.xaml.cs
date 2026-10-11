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
        if (File.Exists(fileName)) 
        {
            TextEditor.Text = File.ReadAllText(fileName);
        }
	}

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        File.WriteAllText(fileName, TextEditor.Text);
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
        var note = new Note();
        note.Filename = fileName;

        if (File.Exists(fileName))
        {
            note.Date = File.GetCreationTime(fileName);
            note.Text = File.ReadAllText(fileName);
        }

        BindingContext = note;
    }
}