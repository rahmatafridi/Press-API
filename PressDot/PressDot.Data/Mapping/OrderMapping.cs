using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class OrderMapping : PressDotEntityTypeConfiguration<Order>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));
            builder
                .Property(b => b.Id)
                .HasColumnName("OrderId");

            builder.HasOne(laboratory => laboratory.Laboratory)
                .WithMany()
                .HasForeignKey(laboratory => laboratory.LaboratoryId);

            builder.HasOne(appointment => appointment.Appointment)
                .WithMany()
                .HasForeignKey(appointment => appointment.AppointmentId);

            builder.HasOne(status => status.State)
                .WithMany()
                .HasForeignKey(status => status.StateId);
            base.Configure(builder);
        }
        #endregion
    }
}
