using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class SaloonLaboratoryMapping : PressDotEntityTypeConfiguration<SaloonLaboratory>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SaloonLaboratory> builder)
        {
            builder.ToTable(nameof(SaloonLaboratory));
            builder
                .Property(b => b.Id)
                .HasColumnName("SaloonLaboratoryId");
            builder.HasOne(e => e.Saloon)
                .WithMany(e => e.SaloonLaboratories)
                .HasForeignKey(e => e.SaloonId);

            builder.HasOne(e => e.Laboratory)
                .WithMany(e => e.SaloonLaboratories)
                .HasForeignKey(e => e.LaboratoryId);

            base.Configure(builder);
        }
        #endregion
    }
}
