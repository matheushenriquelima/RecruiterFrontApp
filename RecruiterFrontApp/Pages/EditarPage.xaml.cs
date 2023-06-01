using RecruiterFrontApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace RecruiterFrontApp;

public partial class EditarPage : ContentPage
{
    Candidato candidatoEdit;
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
        List<Candidato> objs = JsonConvert.DeserializeObject<List<Candidato>>(json);
        candidatoEdit = objs.FirstOrDefault((a => a.Id == candidatoId));
        entNome.Text = candidatoEdit.Name;
    }
    public async void EditarClicked(object sender, EventArgs args)
	{
        
        var httpClient = new HttpClient();
        var data = new
            {
                name = entNome.Text,
                skills = entHabilidades.Text,
                contact = entContato.Text,
                hiringDate = entDataContratacao.Date,
                isHired = entStatus.IsChecked,
        };
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PutAsync(uri + '/' + candidatoId, content);

            if (response.IsSuccessStatusCode){
                await DisplayAlert("Sucesso", "Edição Concluida", OK);
                entNome.Text = "";
                await Navigation.PushAsync(new MainPage());
            }
            else {
                await DisplayAlert("Error",$"Ocorreu um erro na edição: {response.RequestMessage}", "VOLTAR");
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Error",$"Ocorreu um erro ao fazer a solicitação: {e.Message}", "VOLTAR"));
        }
    }
}