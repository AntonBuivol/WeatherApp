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
        string location = city.CityName;

        City selectedCity = App.Database.SelectCityByName(location);

        if (selectedCity != null)
        {
            // Создаем экземпляр MainPage
            MainPage mainPage = new MainPage();

            // Вызываем асинхронный метод для получения данных о погоде
            await Navigation.PushAsync(mainPage);
            await mainPage.RetrieveWeatherData(selectedCity.CityName);

        }
        else
        {
            // Обработка ситуации, когда город не найден
            await DisplayAlert("Error", "City not found", "OK");
        }
    }
}