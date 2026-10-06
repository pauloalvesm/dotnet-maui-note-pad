namespace NotePad.App.Views;

public partial class NotePage : ContentPage
{
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

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
}