using System.Data;
using System.Diagnostics;
using System.Net.Http.Json;

namespace RecruiterFrontApp;

public partial class CadastrarPage : ContentPage
{
    private readonly string uri = "https://localhost:7008/api/Candidates";
    private HttpClient httpClient = new HttpClient();
    public CadastrarPage()
	{
		InitializeComponent();
	}
    private async void SubmitClicked(object sender, EventArgs e)
	{
        try
        {
            var data = new
            {
                name = entNome.Text,
                skills = entHabilidades.Text,
                contact = entContato.Text,
                hiringDate = entDataContratacao.Date.ToUniversalTime(),
                isHired = entStatus.IsChecked,
            };
            Debug.WriteLine(data);

            var response = await httpClient.PostAsJsonAsync(uri, data);

            response.EnsureSuccessStatusCode();

            await DisplayAlert("Sucesso", "Salvo!!", "OK");          
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}