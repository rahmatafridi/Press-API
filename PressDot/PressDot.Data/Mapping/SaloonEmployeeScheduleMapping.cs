using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    class SaloonEmployeeScheduleMapping : PressDotEntityTypeConfiguration<SaloonEmployeeSchedule>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<SaloonEmployeeSchedule> builder)
        {
            builder.ToTable(nameof(SaloonEmployeeSchedule));


            base.Configure(builder);
        }
        #endregion
    }
}
