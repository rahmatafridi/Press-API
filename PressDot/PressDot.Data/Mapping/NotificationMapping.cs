using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PressDot.Data.Mapping
{
    public class NotificationMapping : PressDotEntityTypeConfiguration<Notification>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {

            builder.ToTable(nameof(Notification));
            builder
                .Property(b => b.Id)
                .HasColumnName("Id");
            base.Configure(builder);
        
            builder.HasOne(role => role.Users)

                .WithMany(x => x.Notifications)
                .HasForeignKey(role => role.UserId);

            base.Configure(builder);


        }
        #endregion
    }
}
