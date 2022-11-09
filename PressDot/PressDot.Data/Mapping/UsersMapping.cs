﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressDot.Core.Domain;

namespace PressDot.Data.Mapping
{
    public class UsersMapping : PressDotEntityTypeConfiguration<Users>
    {
        #region Methods

        /// <summary>
        /// Configures the Expression entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable(nameof(Users));
            builder
                .Property(b => b.Id)
                .HasColumnName("UserId");
            builder.HasOne(role =>role.UsersRole)
                .WithMany(x => x.Users)
                .HasForeignKey(role=>role.UserRoleId);

            base.Configure(builder);
        }
        #endregion
    }
}
