using CustomerChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerChat.Infrastructure.Persistence.Configurations;

public sealed class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Subject)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>(); // Store as string for readability in DB

        builder.Property(c => c.ClosingReason)
            .HasMaxLength(500);

        // Owned collection — messages belong to the conversation aggregate
        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.CustomerId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.AssignedAgentId);

        // Ignore domain events — they're transient, not persisted
        builder.Ignore(c => c.DomainEvents);
    }
}

public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(m => m.SenderType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(m => m.MessageType)
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(m => m.ConversationId);
        builder.HasIndex(m => m.CreatedAt);
        builder.HasIndex(m => new { m.ConversationId, m.IsRead });

        builder.Ignore(m => m.DomainEvents);

        // EntityConfiguration
        builder.Property(c => c.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}

public sealed class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(a => a.Email).IsUnique();
        builder.HasIndex(a => a.Status);

        builder.Ignore(a => a.DomainEvents);
    }
}
