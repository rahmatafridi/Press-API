using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class AppointmentMapping : PressDotEntityTypeConfiguration<Appointment>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable(nameof(Appointment));
            builder
                .Property(b => b.Id)
                .HasColumnName("AppointmentId");

            builder.HasOne(customer => customer.Customer)
                .WithMany()
                .HasForeignKey(customer => customer.CustomerId);

            builder.HasOne(doctor => doctor.Doctor)
                .WithMany()
                .HasForeignKey(doctor => doctor.DoctorId);
            builder.HasOne(saloon => saloon.Saloon)
                .WithMany()
                .HasForeignKey(saloon => saloon.SaloonId);
            builder.HasOne(status => status.State)
                .WithMany()
                .HasForeignKey(status => status.StateId);
            base.Configure(builder);
        }
        #endregion
    }
}
