using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class StatesMapping : PressDotEntityTypeConfiguration<States>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<States> builder)
        {
            builder.ToTable(nameof(States));
            builder
                .Property(b => b.Id)
                .HasColumnName("StateId");
            base.Configure(builder);
        }
        #endregion
    }
}
