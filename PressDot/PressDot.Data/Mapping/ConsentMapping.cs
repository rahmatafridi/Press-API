using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Data.Mapping
{
    public class ConsentMapping : PressDotEntityTypeConfiguration<Consent>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Consent> builder)
        {

            builder.ToTable(nameof(Consent));
            builder
                .Property(b => b.Id)
                .HasColumnName("Id");
            base.Configure(builder);
        }
        #endregion

    }
}
