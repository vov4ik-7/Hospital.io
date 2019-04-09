using System;
namespace Psycho.DAL.Core.Domain
{
    public class Report
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }

        public int AuthorizedUserId { get; set; }
        public virtual AuthorizedUser AuthorizedUser { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
