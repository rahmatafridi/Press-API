using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class LaboratoryUsersMapping : PressDotEntityTypeConfiguration<LaboratoryUsers>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<LaboratoryUsers> builder)
        {
            builder.ToTable(nameof(LaboratoryUsers));
            builder
                .Property(b => b.Id)
                .HasColumnName("LaboratoryUsersId");

            builder.HasOne(lab => lab.Laboratory)
                .WithMany()
                .HasForeignKey(lab => lab.LaboratoryId);

            builder.HasOne(user => user.User)
                .WithMany()
                .HasForeignKey(user => user.UserId);
            base.Configure(builder);
        }
        #endregion
    }
}