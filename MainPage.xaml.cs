using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //GetCurrentLocation();
            InitializeComponent();
        }

        //    private async void OnMenuClicked(object sender, EventArgs e)
        //    {
        //        // Toggle visibility of the overlay menu
        //        MenuOverlay.IsVisible = !MenuOverlay.IsVisible;
        //        if (MenuOverlay.IsVisible)
        //            await MenuOverlay.TranslateTo(50, 0, 250, Easing.Linear);
        //        else
        //            await MenuOverlay.TranslateTo(1000, 0, 250, Easing.Linear);
        //    }

        //    private async void OnCloseMenuClicked(object sender, EventArgs e)
        //    {
        //        // Animate the translation of the MenuOverlay to the right
        //        await MenuOverlay.TranslateTo(50, 0, 250, Easing.Linear);

        //        // Hide the MenuOverlay after the animation completes
        //        MenuOverlay.IsVisible = false;
        //    }

        //    //private void UpdateWeatherIcon(string condition)
        //    //{
        //    //    if (condition.ToLower().Contains("rain"))
        //    //        WeatherIcon.Source = "rain_icon.png"; // Ensure you have this image
        //    //    else if (condition.ToLower().Contains("snow"))
        //    //        WeatherIcon.Source = "snow_icon.png"; // Ensure you have this image
        //    //    else if (condition.ToLower().Contains("sunny"))
        //    //        WeatherIcon.Source = "sunny_icon.png"; // Ensure you have this image
        //    //    else if (condition.ToLower().Contains("cloud"))
        //    //        WeatherIcon.Source = "cloud_icon.png"; // Ensure you have this image
        //    //    else
        //    //        WeatherIcon.Source = "default_icon.png"; // Fallback image
        //    //}


        //    protected override async void OnAppearing()
        //    {
        //        base.OnAppearing();
        //        await gifView.StartAnimation();
        //    }

        //    private async void OnGetWeatherClicked(object sender, EventArgs e)
        //    {
        //        string location = OverlayLocationEntry.Text;
        //        await RetrieveWeatherData(location);
        //    }

        //    private async void OnOverlayGetWeatherClicked(object sender, EventArgs e)
        //    {
        //        string location = OverlayLocationEntry.Text;
        //        MenuOverlay.IsVisible = false;
        //        await RetrieveWeatherData(location);
        //    }

        //    private async Task RetrieveWeatherData(string location)
        //    {
        //        string url = $"https://api.weatherapi.com/v1/current.json?key=aa7758b35f384a5eb62102337241405&q={location}&aqi=no";

        //        using (HttpClient client = new HttpClient())
        //        {
        //            try
        //            {
        //                HttpResponseMessage response = await client.GetAsync(url);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    string jsonResponse = await response.Content.ReadAsStringAsync();
        //                    JObject data = JObject.Parse(jsonResponse);

        //                    string curlocation = data["location"]["name"].ToString();
        //                    string temperature = data["current"]["temp_c"].ToString();
        //                    string condition = data["current"]["condition"]["text"].ToString();

        //                    WeatherCard.BackgroundColor = GetBackgroundColor(condition);
        //                    UpdateWeatherLabels(curlocation, temperature, condition);

        //                }
        //                else
        //                {
        //                    ErrorLabel.Text = $"Failed to retrieve weather data for {location}. Status code: {response.StatusCode}";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorLabel.Text = $"An error occurred: {ex.Message}";
        //            }
        //        }
        //    }

        //    private Color GetBackgroundColor(string condition)
        //    {
        //        if (condition.ToLower().Contains("rain"))
        //            return Color.FromRgb(135, 206, 250); // LightSkyBlue
        //        else if (condition.ToLower().Contains("snow"))
        //            return Color.FromRgb(255, 250, 250); // Snow
        //        else if (condition.ToLower().Contains("sunny"))
        //            return Color.FromRgb(255, 223, 0); // Gold
        //        else if (condition.ToLower().Contains("cloud"))
        //            return Color.FromRgb(192, 192, 192); // Silver
        //        else
        //            return Colors.Transparent;
        //    }

        //    private void UpdateWeatherLabels(string curlocation, string temperature, string condition)
        //    {
        //        LocationLabel.Text = curlocation;
        //        TemperatureLabel.Text = $"Temperature: {temperature}°C";
        //        ConditionLabel.Text = condition;
        //    }

        //    private async void GetCurrentLocation()
        //    {
        //        try
        //        {
        //            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        //            Location location = await Geolocation.GetLocationAsync(request);

        //            if (location != null)
        //            {
        //                double Latitude = location.Latitude;
        //                double Longitude = location.Longitude;

        //                GetWeatherFromLocation(Latitude, Longitude);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine($"Exception: {ex.Message}");
        //            ErrorLabel.Text = $"An error occurred: {ex.Message}";
        //        }
        //    }

        //    private async void GetWeatherFromLocation(double Latitude, double Longitude)
        //    {
        //        string url = $"https://api.weatherapi.com/v1/current.json?key=aa7758b35f384a5eb62102337241405&q={Latitude},{Longitude}&aqi=no";

        //        using (HttpClient client = new HttpClient())
        //        {
        //            try
        //            {
        //                HttpResponseMessage response = await client.GetAsync(url);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    string jsonResponse = await response.Content.ReadAsStringAsync();
        //                    JObject data = JObject.Parse(jsonResponse);

        //                    string curlocation = data["location"]["name"].ToString();
        //                    string temperature = data["current"]["temp_c"].ToString();
        //                    string condition = data["current"]["condition"]["text"].ToString();

        //                    WeatherCard.BackgroundColor = GetBackgroundColor(condition);
        //                    UpdateWeatherLabels(curlocation, temperature, condition);
        //                }
        //                else
        //                {
        //                    ErrorLabel.Text = $"Failed to retrieve weather data. Status code: {response.StatusCode}";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorLabel.Text = $"An error occurred: {ex.Message}";
        //            }
        //        }
        //    }
    }
}
