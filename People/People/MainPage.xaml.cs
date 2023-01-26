using People.Models;
using System.Collections.Generic;

namespace People;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        await App.PersonRepo.AddNewPerson(newPerson.Text);
        statusMessage.Text = App.PersonRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Person> people = await App.PersonRepo.GetAllPeople();
        peopleList.ItemsSource = people;
    }

    public async void OnDeleteClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        Button b = (Button)sender;
        int id;
        int.TryParse(b.CommandParameter.ToString(), out id);

        await App.PersonRepo.DeletePerson(id);
        statusMessage.Text = App.PersonRepo.StatusMessage;
    }

}

