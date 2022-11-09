using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class SaloonTypeMapping : PressDotEntityTypeConfiguration<SaloonType>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SaloonType> builder)
        {
            builder.ToTable(nameof(SaloonType));
            builder
                .Property(b => b.Id)
                .HasColumnName("SaloonTypeId");

            base.Configure(builder);
        }
        #endregion
    }
}
