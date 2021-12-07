using Entities.Models;
using Entities.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class UserConfiguration : DefaultGuids, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureModel(builder);
            AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(user => user.AccountInfo).HasIndex(info => info.Login).IsUnique(true);
        }

        private void AddInitialData(EntityTypeBuilder<User> builder)
        {
            if (Configuration.ShouldAddInitialData())
                BuildLogist(builder);
            BuildAdmin(builder);
        }

        private static void BuildLogist(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = LogistGuid,
                    Role = Enums.UserRole.Logist
                }
            );

            builder.OwnsOne
            (user => user.ContactPerson).HasData
            (
                new
                {
                    UserId = LogistGuid,
                    Name = "Sasha",
                    Surname = "Trikorochki",
                    Patronymic = "Vitaljevich",
                    PhoneNumber = "19(4235)386-91-39"
                }
            );

            builder.OwnsOne(user => user.AccountInfo).HasData
            (
                new
                {
                    UserId = LogistGuid,
                    Login = "login1",
                    Password = "password1",
                    PasswordHashString = AuthenticationUtility.CalculateStringHash("password1")
                }
            );
        }

        private static void BuildAdmin(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = AdminGuid,
                    Role = Enums.UserRole.Admin
                }
            );

            builder.OwnsOne(user => user.ContactPerson).HasData
            (
                new
                {
                    UserId = AdminGuid,
                    Name = "Pasha",
                    Surname = "Trichetire",
                    Patronymic = "Olegovich",
                    PhoneNumber = "19(4235)386-99-39"
                }
            );

            builder.OwnsOne(user => user.AccountInfo).HasData
            (
                new
                {
                    UserId = AdminGuid,
                    Login = "login2",
                    Password = "password1",
                    PasswordHashString = AuthenticationUtility.CalculateStringHash("password1")
                }
            );
        }
    }
}
