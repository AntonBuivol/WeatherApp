namespace WeatherApp;

public partial class DBListPage : ContentPage
{
    public DBListPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        cityList.ItemsSource = App.Database.GetItems();
        base.OnAppearing();
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        City selectedCity = (City)e.SelectedItem;
        DBpage dBpage = new DBpage();
        dBpage.BindingContext = selectedCity;
        await Navigation.PushAsync(dBpage);
    }

    public async void AddFAvorite(object sender, EventArgs e)
    {
        City city = new City();
        DBpage dBpage = new DBpage();
        dBpage.BindingContext = city;
        await Navigation.PushAsync(dBpage);
    }
}