using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class SaloonMapping : PressDotEntityTypeConfiguration<Saloon>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Saloon> builder)
        {
            builder.ToTable(nameof(Saloon));
            builder
                .Property(b => b.Id)
                .HasColumnName("SaloonId");

            builder.HasOne(country => country.Country)
                .WithMany()
                .HasForeignKey(country => country.CountryId);
            builder.HasOne(city => city.City)
                .WithMany()
                .HasForeignKey(city => city.CityId);
            builder.HasOne(type => type.SaloonType)
                .WithMany()
                .HasForeignKey(type => type.SaloonTypeId);
            base.Configure(builder);
        }
        #endregion
    }
}
