using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(chat => chat.Id);

            builder.HasOne(chat => chat.Sender)
                .WithMany(sender => sender.ChatSender)
                .HasForeignKey(chat => chat.SenderId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(chat => chat.Receiver)
                .WithMany(receiver => receiver.ChatReceiver)
                .HasForeignKey(chat => chat.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
