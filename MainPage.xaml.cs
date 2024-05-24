using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetCurrentLocation();

            //    bool isConditionMet = true; // Example condition

            //    if (isConditionMet)
            //    {
            //        // Set the source to the animated image
            //        BackgroundImage.Source = "rain_gif.gif";
            //        BackgroundImage.IsAnimationPlaying = true;
            //    }
            //    else
            //    {
            //        // Set the source to the static image
            //        BackgroundImage.Source = "static_background.png";
            //        BackgroundImage.IsAnimationPlaying = false;
            //    }
        }

        private async void OnFavoriteClicked(object sender, EventArgs e)
        {
            DBListPage listPage = new DBListPage();
            await Navigation.PushAsync(listPage);
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            // Toggle visibility of the overlay menu
            MenuOverlay.IsVisible = !MenuOverlay.IsVisible;
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            string location = OverlayLocationEntry.Text;
            await RetrieveWeatherData(location);
        }

        private async void OnOverlayGetWeatherClicked(object sender, EventArgs e)
        {
            string location = OverlayLocationEntry.Text;
            MenuOverlay.IsVisible = false;
            await RetrieveWeatherData(location);
        }

        public async Task RetrieveWeatherData(string location)
        {
            string url = $"https://api.weatherapi.com/v1/current.json?key=aa7758b35f384a5eb62102337241405&q={location}&aqi=no";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);

                        string curlocation = data["location"]["name"].ToString();
                        string temperature = data["current"]["temp_c"].ToString();
                        string condition = data["current"]["condition"]["text"].ToString();

                        WeatherCard.BackgroundColor = GetBackgroundColor(condition);
                        UpdateWeatherLabels(curlocation, temperature, condition);
                        UpdateWeatherIcon(condition);
                    }
                    else
                    {
                        await DisplayAlert("Error", "City not found. Check the spelling of the city", "OK");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = $"An error occurred: {ex.Message}";
                }
            }
        }

        private Color GetBackgroundColor(string condition)
        {
            if (condition.ToLower().Contains("rain"))
                return Color.FromRgb(135, 206, 250); // LightSkyBlue
            else if (condition.ToLower().Contains("snow"))
                return Color.FromRgb(255, 250, 250); // Snow
            else if (condition.ToLower().Contains("sunny"))
                return Color.FromRgb(255, 223, 0); // Gold
            else if (condition.ToLower().Contains("cloud"))
                return Color.FromRgb(192, 192, 192); // Silver
            else
                return Colors.Transparent;
        }


        private void UpdateWeatherLabels(string curlocation, string temperature, string condition)
        {
            LocationLabel.Text = curlocation;
            TemperatureLabel.Text = $"Temperature: {temperature}°C";
            ConditionLabel.Text = condition;
        }

        private void UpdateWeatherIcon(string condition)
        {
            if (condition.ToLower().Contains("rain"))
                WeatherIcon.Source = "rain_icon.png"; // Ensure you have this image
            else if (condition.ToLower().Contains("snow"))
                WeatherIcon.Source = "snow_icon.png"; // Ensure you have this image
            else if (condition.ToLower().Contains("sunny"))
                WeatherIcon.Source = "sunny_icon.png"; // Ensure you have this image
            else if (condition.ToLower().Contains("cloud"))
                WeatherIcon.Source = "cloud_icon.png"; // Ensure you have this image
            else
                WeatherIcon.Source = "default_icon.png"; // Fallback image
        }



        // anton

        private async void GetWeatherFromLocation(double Latitude, double Longitude)
        {
            string location = OverlayLocationEntry.Text; // Get location from entry field

            string url = $"https://api.weatherapi.com/v1/current.json?key=aa7758b35f384a5eb62102337241405&q={Latitude},{Longitude}&aqi=no";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);

                        string curlocation = data["location"]["name"].ToString();
                        string temperature = data["current"]["temp_c"].ToString();
                        string condition = data["current"]["condition"]["text"].ToString();

                        WeatherCard.BackgroundColor = GetBackgroundColor(condition);
                        UpdateWeatherLabels(curlocation, temperature, condition);
                        UpdateWeatherIcon(condition);
                    }
                    else
                    {
                        ErrorLabel.Text = $"Failed to retrieve weather data for {location}. Status code: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                }
            }
        }

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;


        public async Task GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    double Latitude = location.Latitude;
                    double Longitude = location.Longitude;

                    GetWeatherFromLocation(Latitude, Longitude);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                ErrorLabel.Text += $"FeatureNotSupportedException: {fnsEx}";
            }
            catch (FeatureNotEnabledException fneEx)
            {
                ErrorLabel.Text += $"FeatureNotEnabledException: {fneEx}";
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You need to give permission to receive the location. Go to the application settings and give permission", "Ok"); ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            {
                _cancelTokenSource.Cancel();
            }
        }
    }
}