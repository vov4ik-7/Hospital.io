
namespace Psycho.DAL.Core.Domain
{
    public class OrderProcessingRecord
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int AppointmentResultId { get; set; }
    }
}
