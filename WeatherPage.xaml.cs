using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    private double _latitude;
    private double _longitude;
    public List<Models.List> WeatherList { get; set; }
	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocationAsync();
        await GetWeatherDataByLocationAsync(_latitude, _longitude);
        
    }

    #region [Events]

    private async void TapLocation_Tapped(object sender, TappedEventArgs e)
    {
        await GetLocationAsync();
        await GetWeatherDataByLocationAsync(_latitude, _longitude);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
        if (!string.IsNullOrEmpty(response))
        {
            await GetWeatherDataByCityAsync(response);
        }
    }

    #endregion [Events]

    #region [Private Methods]
    private async Task GetWeatherDataByLocationAsync(double latitude, double longitude)
    {
        var weatherData = await ApiService.GetWeatherAsync(latitude, longitude);
        UpdateUI(weatherData);
    }

    private async Task GetWeatherDataByCityAsync(string city)
    {
        var weatherData = await ApiService.GetWeatherByCityAsync(city);
        UpdateUI(weatherData);

    }

    private void UpdateUI(dynamic weatherData)
    {
        LblCity.Text = weatherData.City.Name;
        FillCollectionView(weatherData);
        LblWeatherDescription.Text = weatherData.List[0].Weather[0].Description;
        LblTemperature.Text = weatherData.List[0].Main.Temprature + "°C";
        LblHumidity.Text = weatherData.List[0].Main.Humidity + "%";
        LblWind.Text = weatherData.List[0].Wind.Speed + "km/h";
        ImgWeatherIcon.Source = weatherData.List[0].Weather[0].CustomIconUrl;
    }

    private async Task GetLocationAsync()
    {
        var location = await Geolocation.GetLocationAsync();
        _longitude = location.Longitude;
        _latitude = location.Latitude;
    }

    private void FillCollectionView(Root weatherData)
    {
        WeatherList.Clear();
        foreach (var item in weatherData.List)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;
    }
    #endregion [Private Methods]
















}