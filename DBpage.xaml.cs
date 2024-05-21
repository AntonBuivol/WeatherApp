namespace WeatherApp;

public partial class DBpage : ContentPage
{
	public DBpage()
	{
		InitializeComponent();
	}
	private void SaveCity(object sender, EventArgs e)
	{
		var city = (City)BindingContext;
		if(!String.IsNullOrEmpty(city.CityName))
		{
			App.Database.SaveItem(city);
		}
		this.Navigation.PopAsync();
	}
	private void DeleteCity(object sender, EventArgs e)
	{
        var city = (City)BindingContext;
        App.Database.DeleteItem(city.Id);
        this.Navigation.PopAsync();
    }
    private void Cancel(object sender, EventArgs e)
	{
		this.Navigation.PopAsync();
	}

    private async void SelectCity(object sender, EventArgs e)
    {
		var city = (City)BindingContext;
		MainPage mainPage = new MainPage();
    }
}