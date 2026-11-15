using System.Text.Json;

namespace P8Maui;

public partial class MainPage : ContentPage
{
    // Outil pour faire des requêtes internet
    HttpClient client = new HttpClient();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnMenuClicked(object sender, EventArgs e)
{
    MenuBtn.Text = "Recherche des pizzas...";
    MenuBtn.IsEnabled = false;

    try
    {
        string url = "http://127.0.0.1:5068/pizzas";
        var response = await client.GetStringAsync(url);
        var pizzas = JsonSerializer.Deserialize<List<Pizza>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (pizzas != null && pizzas.Count > 0)
        {
            MenuBtn.Text = $"{pizzas.Count} pizza(s) au menu !";
            PizzasList.ItemsSource = pizzas;
        }
        else
        {
            MenuBtn.Text = "Le menu est vide";
            PizzasList.ItemsSource = null;
        }
    }
    catch (Exception ex)
    {
        MenuBtn.Text = "Erreur de connexion";
        await DisplayAlertAsync("Détail de l'erreur", ex.Message, "OK");
    }
    finally
    {
        MenuBtn.IsEnabled = true;
    }
}
}