using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class UserDevicesMapping : PressDotEntityTypeConfiguration<UserDevices>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<UserDevices> builder)
        {
            builder.ToTable(nameof(UserDevices));
            builder
                .Property(b => b.Id)
                .HasColumnName("UserDeviceId");
            builder.HasOne(user => user.Users)
                .WithMany()
                .HasForeignKey(user => user.UserId);

            base.Configure(builder);
        }
        #endregion
    }
}
