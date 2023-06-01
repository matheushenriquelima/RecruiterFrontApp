using RecruiterFrontApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RecruiterFrontApp;

public partial class MainPage : ContentPage
{

    ObservableCollection<Candidato> candidatos = new ObservableCollection<Candidato>();
    private readonly string uri = "https://localhost:7008/api/Candidates";
    private HttpClient client = new HttpClient();

    public MainPage()
    {
        InitializeComponent();
        list.ItemsSource = candidatos;
        GetCandidatos();
    }

    public async void GetCandidatos()
    {

        using var response = await client.GetAsync(uri);
        var json = await response.Content.ReadAsStringAsync();
        List<Candidato> candidatosResponse = JsonConvert.DeserializeObject<List<Candidato>>(json);
        candidatosResponse.ForEach(x => candidatos.Add(x));
    }

    private async void NewClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastrarPage());
    }
    private async void Remover(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var candidato = button.CommandParameter as Candidato;
        using var client = new HttpClient();
        HttpResponseMessage response = await client.DeleteAsync(uri + "/" + candidato.Id);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("sucesso", "Recurso excluído com sucesso!", "ok");
            GetCandidatos();
        }
        else
        {
            await DisplayAlert("erro", $"Ocorreu um erro ao excluir o recurso. Código de status: {response.StatusCode}", "ok");
        }
    }
    private async void Editar(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        var candidato = button.CommandParameter as Candidato;
        await Navigation.PushAsync(new EditarPage(candidato.Id));

    }
}

