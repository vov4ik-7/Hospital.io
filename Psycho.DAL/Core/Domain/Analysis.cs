
namespace Psycho.DAL.Core.Domain
{
    public class Analysis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppointmentId { get; set; }
        public string AnalysisResult { get; set; }
        public string DoctorConclusion { get; set; }
        public byte[] File { get; set; }
    }
}
