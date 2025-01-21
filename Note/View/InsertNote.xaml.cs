using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Essential_Lib.API;
using Note.Model;
using Note.ViewModel;

namespace Note.View;



[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class InsertNote : ContentPage
{
    //private readonly NoteViewModel noteView;
    
   
        public string ItemId
        {
            set { LoadNote(value); }
        }
        public InsertNote()//NoteViewModel noteView
        {
		InitializeComponent();

            // BindingContext = noteView;
            //this.noteView = noteView;
            string appDataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pencotes");
            string randomFileName = $"{Path.GetRandomFileName()}.txt";

            LoadNote(Path.Combine(appDataPath, randomFileName));
        }


        private void LoadNote(string filename)
        {
            Model.Notes noteModel = new Model.Notes();
            noteModel.Filename = filename;

            if (File.Exists(filename))
            {
                noteModel.Date = File.GetCreationTime(filename);
                noteModel.Text = File.ReadAllText(filename);
            }

            BindingContext = noteModel;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Model.Notes note && File.Exists(note.Filename))
                File.Delete(note.Filename);

            await Shell.Current.GoToAsync("..");
        }

        private void TextEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is Model.Notes note)
                File.WriteAllText(note.Filename, TextEditor.Text);
        }




        // private async void Back_ImageButton_Clicked(object sender, EventArgs e)
        // {
        //     await Navigation.PopModalAsync();
        // }


    }