using NotePad.App.Models;

namespace NotePad.App.Services;

public class NoteService
{
    public Note LoadNote(string filename)
    {
        var note = new Note
        {
            Filename = filename
        };

        if (File.Exists(filename))
        {
            note.Date = File.GetCreationTime(filename);
            note.Text = File.ReadAllText(filename);
        }

        return note;
    }

    public void SaveNote(Note note)
    {
        File.WriteAllText(note.Filename, note.Text);
    }

    public void DeleteNote(string filename)
    {
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
    }
}