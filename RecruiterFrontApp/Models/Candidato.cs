
namespace RecruiterFrontApp.Models
{
    internal class Candidato
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Skills { get; set; }
       
        public bool IsHired { get; set; }
        public DateTime? HiringDate { get; set; }
    
    }
}
