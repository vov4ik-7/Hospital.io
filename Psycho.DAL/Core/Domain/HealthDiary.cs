using System;

namespace Psycho.DAL.Core.Domain
{
    public class HealthDiary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string BlockName { get; set; }
        public string BlockValue { get; set; }
    }
}
