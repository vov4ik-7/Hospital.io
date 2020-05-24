using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Psycho.DAL.Core.Domain;

namespace Psycho.DAL.Persistence.EntityConfigurations
{
    public class AnalysisConfiguration : IEntityTypeConfiguration<Analysis>
    {
        public void Configure(EntityTypeBuilder<Analysis> builder)
        {
            builder.HasKey(analysis => analysis.Id);
        }
    }
}
