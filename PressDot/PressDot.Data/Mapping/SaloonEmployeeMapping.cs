using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class SaloonEmployeeMapping : PressDotEntityTypeConfiguration<SaloonEmployee>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SaloonEmployee> builder)
        {
            builder.ToTable(nameof(SaloonEmployee));
            builder
                .Property(b => b.Id)
                .HasColumnName("SaloonEmployeeId");
            builder.HasOne(e => e.Saloon)
                .WithMany(e => e.SaloonEmployees)
                .HasForeignKey(e => e.SaloonId);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.SaloonEmployees)
                .HasForeignKey(e => e.EmployeeId);

            base.Configure(builder);
        }
        #endregion
    }
}
