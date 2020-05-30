using System;

namespace Psycho.DAL.Core.Domain
{
    public class BodyIndicator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Indicator { get; set; }
        public decimal Value { get; set; }
    }
}
