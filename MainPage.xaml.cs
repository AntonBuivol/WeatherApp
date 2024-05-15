using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WeatherApp.ViewModel;


namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetCurrentLocation();
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            string location = LocationEntry.Text; // Get location from entry field

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

                        string locationName = data["location"]["name"].ToString();
                        string temperature = data["current"]["temp_c"].ToString();
                        string condition = data["current"]["condition"]["text"].ToString();
                        string wind_dir = data["current"]["wind_dir"].ToString();

                        WeatherLabel.Text = $"Weather in {locationName} ({location}):\n" +
                                            $"Temperature: {temperature}°C\n" +
                                            $"Condition: {condition}\n" +
                                            $"Wind direction: {wind_dir}\n";
                    }
                    else
                    {
                        WeatherLabel.Text = $"Failed to retrieve weather data for {location}. Status code: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    WeatherLabel.Text = $"An error occurred: {ex.Message}";
                }
            }
        }


        private async void GetWeatherFromLocation(double Latitude, double Longitude)
        {
            string location = LocationEntry.Text; // Get location from entry field

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

                        string locationName = data["location"]["name"].ToString();
                        string temperature = data["current"]["temp_c"].ToString();
                        string condition = data["current"]["condition"]["text"].ToString();
                        string wind_dir = data["current"]["wind_dir"].ToString();

                        WeatherLabel.Text = $"Weather in {locationName}:\n" +
                                            $"Temperature: {temperature}°C\n" +
                                            $"Condition: {condition}\n" +
                                            $"Wind direction: {wind_dir}\n";
                    }
                    else
                    {
                        WeatherLabel.Text = $"Failed to retrieve weather data for {location}. Status code: {response.StatusCode}";
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
                    WeatherLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                    double Latitude = location.Latitude;
                    double Longitude = location.Longitude;

                    GetWeatherFromLocation(Latitude, Longitude);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                WeatherLabel.Text += $"FeatureNotSupportedException: {fnsEx}";
            }
            catch (FeatureNotEnabledException fneEx)
            {
                WeatherLabel.Text += $"FeatureNotEnabledException: {fneEx}";
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