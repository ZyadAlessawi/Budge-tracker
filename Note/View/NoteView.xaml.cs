using Essential_Lib.API;
using Note.Model;
using Note.ViewModel;

namespace Note.View;

public partial class NoteView : ContentPage
{
   // private readonly NoteViewModel noteView;
    public NoteView()
	{
		InitializeComponent();



        BindingContext = new ViewModel.NoteViewModel();
    }



    //  public async void GetData()
    //  {
    //      // await LocalDatabaseAPIs.DeleteTableAsync<Keys>();
    //
    //      var sql = await LocalDatabaseAPIs.GetAllItemsAsync<Notes>();
    //
    //
    //      ListViewNote.ItemsSource = sql;
    //
    //  }
    //
    //  
    //  public void ListViewNote_ItemSelected()
    //  {
    //      // Set Data to Title and Description
    //      noteView.SetData();
    //  }
    //
    //
    //
    //  private async void InsertPage_ToolbarItem_Clicked(object sender, EventArgs e)
    //  {
    //      await Navigation.PushModalAsync(new InsertNote(noteView));
    //  }




    protected override void OnAppearing()
    {
        ((ViewModel.NoteViewModel)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(InsertNote));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var note = (Model.Notes)e.CurrentSelection[0];

        await Shell.Current.GoToAsync($"{nameof(InsertNote)}?{nameof(InsertNote.ItemId)}={note.Filename}");

        notesCollection.SelectedItem = null;
    }

    private async void NoteCard_Tapped(object sender, EventArgs e)
    {
        var note = ((VisualElement)sender).BindingContext as Model.Notes;

        if (note is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(InsertNote)}?{nameof(InsertNote.ItemId)}={note.Filename}");
    }

    private void RemoveNote_Invoked(object sender, EventArgs e)
    {
        var note = ((SwipeItem)sender).BindingContext as Model.Notes;

        if (File.Exists(note.Filename))
            File.Delete(note.Filename);
        BindingContext = new ViewModel.NoteViewModel();
    }

}