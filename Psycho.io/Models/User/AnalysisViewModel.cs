
using Microsoft.AspNetCore.Http;

namespace Psycho.io.Models.User
{
    public class AnalysisViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppointmentId { get; set; }
        public string AnalysisResult { get; set; }
        public string DoctorConclusion { get; set; }
        public IFormFile File { get; set; }
    }
}
