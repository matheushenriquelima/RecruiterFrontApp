
namespace RecruiterFrontApp.Models
{
    internal class Candidato
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Skills { get; set; }
       
        public bool IsHired;
        public DateTime? HiringDate { get; set; }


        public string getIsHired()
        {
            return this.IsHired ? "Contratado" : "Reprovado";
        }

        public void setMyProperty(bool value)
        {
            this.IsHired = value;
        }
    }
}
