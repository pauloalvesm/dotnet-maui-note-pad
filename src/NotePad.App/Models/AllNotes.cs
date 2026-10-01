using System.Collections.ObjectModel;

namespace NotePad.App.Models;

public class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes()
    {
        LoadNotes();
    }

    public void LoadNotes()
    {
        Notes.Clear();
        var appDataPath = FileSystem.AppDataDirectory;

        IEnumerable<Note> notes = Directory.EnumerateFiles(appDataPath, "*.notes.txt")
                                           .Select(filename => new Note()
                                           {
                                               Filename = filename,
                                               Text = File.ReadAllText(filename),
                                               Date = File.GetCreationTime(filename)
                                           })
                                           .OrderBy(note => note.Date);

        foreach (Note note in notes)
        {
            Notes.Add(note);
        }
    }
}
