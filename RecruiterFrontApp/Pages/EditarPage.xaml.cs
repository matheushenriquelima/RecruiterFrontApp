using RecruiterFrontApp.Models;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

namespace RecruiterFrontApp;

public partial class EditarPage : ContentPage
{
    private readonly string uri = "https://localhost:7008/api/Candidates";
    private readonly string OK = "OK";
    private HttpClient client = new HttpClient();
    int candidatoId;
    public EditarPage(int id)
	{
		InitializeComponent();
        GetCandidato();
        candidatoId = id;
	}

	public async void GetCandidato()
    {
        using var response = await client.GetAsync(uri + '/' + candidatoId);
        var json = await response.Content.ReadAsStringAsync();
        Candidato candidatoEdit = JsonConvert.DeserializeObject<Candidato>(json);
        entNome.Text = candidatoEdit.Name;
        entHabilidades.Text = candidatoEdit.Skills;
        entContato.Text = candidatoEdit.Contact;
    }
    public async void EditarClicked(object sender, EventArgs args)
	{
        
        var httpClient = new HttpClient();
        var data = new
            {
                name = entNome.Text,
                skills = entHabilidades.Text,
                contact = entContato.Text,
                hiringDate = entDataContratacao.Date.ToUniversalTime(),
                isHired = entStatus.IsChecked,
        };
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PutAsync(uri + '/' + candidatoId, content);

            if (response.IsSuccessStatusCode){
                await DisplayAlert("Sucesso", "Edição Concluida", OK);
                await Navigation.PushAsync(new MainPage());
            }
            else {
                await DisplayAlert("Error",$"Ocorreu um erro na edição: {response.RequestMessage}", "VOLTAR");
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Error",$"Ocorreu um erro ao fazer a solicitação: {e.Message}", "VOLTAR");
        }
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}