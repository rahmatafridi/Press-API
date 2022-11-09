using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class LaboratoryMapping : PressDotEntityTypeConfiguration<Laboratory>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            builder.ToTable(nameof(Laboratory));
            builder
                .Property(b => b.Id)
                .HasColumnName("LaboratoryId");

            base.Configure(builder);
        }
        #endregion
    }
}