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
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = UserGuid,
                    Role = Enums.EUserRole.LOGIST
                }
            );

            builder.OwnsOne(user => user.ContactPerson).HasData
            (
                new
                {
                    UserId = UserGuid,
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
                    UserId = UserGuid,
                    Login = "login1",
                    Password = "password1",
                    PasswordHashString = AuthenticationUtility.CalculateStringHash("password1")
                }
            );
        }
    }
}
