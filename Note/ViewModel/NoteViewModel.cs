using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Essential_Lib.API;
using Note.Model;
using Note.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModel
{
    public partial class NoteViewModel : ObservableObject
    {

     //   // Fields
     //   [ObservableProperty]
     //   public string noteTitle = string.Empty;
     //
     //   [ObservableProperty]
     //   public string noteDescription = string.Empty;
     //
     //   [ObservableProperty]
     //   Notes selectedNote;
     //
     //   [ObservableProperty]
     //   ObservableCollection<Notes> noteCollection;
     //
     //
     //   [RelayCommand]
     //   public async Task<Task> HandleNote(string buttonText)
     //   {
     //       
     //       if (buttonText == "Add Note")
     //       {
     //           if (NoteTitle != null)
     //       
     //           {
     //               // Set New Note
     //               Notes note = new Notes(){Title = NoteTitle, Description = NoteDescription};
     //
     //               await LocalDatabaseAPIs.AddItemAsync(note);
     //               //await LoadDataAsync();
     //
     //               // Rest Values
     //               NoteTitle = string.Empty;
     //               NoteDescription = string.Empty;
     //
     //           }
     //       }
     //
     //      
     //
     //
     //       if (buttonText == "Edit Note")
     //       {
     //          
     //           if (SelectedNote.Id != null)
     //           {
     //               // Set New Note
     //               Notes newNote = new Notes(){Title = NoteTitle, Description = NoteDescription, Id = SelectedNote.Id };
     //
     //               // Remove Note
     //               await LocalDatabaseAPIs.UpdateItemAsync(newNote);
     //               //await LoadDataAsync();
     //
     //           }
     //
     //       }
     //
     //       if(buttonText == "Remove Note")
     //       {
     //           if (SelectedNote != null)
     //           {
     //               await LocalDatabaseAPIs.DeleteItemAsync(SelectedNote);
     //               //await LoadDataAsync();
     //
     //           // Rest Values
     //               NoteTitle = string.Empty;
     //               NoteDescription = string.Empty;
     //           }
     //       }
     //
     //
     //
     //       return Task.CompletedTask;
     //
     //
     //
     //   }
     //
     //  
     //
     //   public  void SetData()
     //   {
     //       NoteTitle = SelectedNote.Title;
     //       NoteDescription = SelectedNote.Description;
     //   }
     //
     //   //private async Task LoadDataAsync()
     //   //{
     //   //     
     //   //
     //   //     List<Notes>? list = await LocalDatabaseAPIs.GetAllItemsAsync<Notes>();
     //   //     for (int i = 0; i < list.Count; i++)
     //   //     {
     //   //         Notes? note = list[i];
     //   //         NoteCollection.Add(note);
     //   //     }
     //   //     NoteCollection.Clear();
     //   //
     //   // }





        public ObservableCollection<Notes> Notes { get; set; } = new ObservableCollection<Notes>();

        public NoteViewModel() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            string appDataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pencotes");
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            IEnumerable<Notes> notes = Directory
                                        .EnumerateFiles(appDataPath, "*.txt")
                                        .Select(filename => new Notes()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetCreationTime(filename)
                                        })
                                        .OrderByDescending(note => note.Date);
            foreach (Notes note in notes)
                Notes.Add(note);
        }

    }
}