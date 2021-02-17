using Microsoft.EntityFrameworkCore;
using Quote.Database.Models;
using Quote.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote.Database.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<spRole>().HasData(
                    new spRole
                    {
                        Id = 1,
                        Name = "admin",
                        UserAccess = "1,2,3",
                        Status = 1,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = 1
                    },
                    new spRole
                    {
                        Id = 2,
                        Name = "user",
                        UserAccess = "4",
                        Status = 1,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = 1
                    });

            modelBuilder.Entity<tbUser>().HasData(
                    new tbUser
                    {
                        Id = 1,
                        LastName = "LastName",
                        Name = "Name",
                        Patronymic = "Patronymic",
                        Email = "admin@gmail.com",
                        Password = CHash.EncryptMD5("1"),
                        RoleId = 1,
                        Status = 1,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = 1
                    },
                    new tbUser
                    {
                        Id = 2,
                        LastName = "LastName",
                        Name = "Name",
                        Patronymic = "Patronymic",
                        Email = "user@gmail.com",
                        Password = CHash.EncryptMD5("1"),
                        RoleId = 2,
                        Status = 1,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = 1
                    });

            modelBuilder.Entity<spSender>().HasData(
                   new spSender
                   {
                       Id = 1,
                       Name = "email",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   },
                   new spSender
                   {
                       Id = 2,
                       Name = "sms",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   });

            modelBuilder.Entity<spCategory>().HasData(
                   new spCategory
                   {
                       Id = 1,
                       Name = "Category 1",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   },
                   new spCategory
                   {
                       Id = 2,
                       Name = "Category 2",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   });

            modelBuilder.Entity<spAccessList>().HasData(
                   new spAccessList
                   {
                       Id = 1,
                       Name = "Create",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   },
                   new spAccessList
                   {
                       Id = 2,
                       Name = "Delete",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   },
                   new spAccessList
                   {
                       Id = 3,
                       Name = "Update",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   },
                   new spAccessList
                   {
                       Id = 4,
                       Name = "View",
                       Status = 1,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = 1
                   });

            modelBuilder.Entity<tbAuthor>().HasData(
                  new tbAuthor
                  {
                      Id = 1,
                      LastName = "Alimov",
                      Name = "Rustam",
                      Patronymic = "Rustam",
                      Status = 1,
                      CreateDate = DateTime.UtcNow,
                      CreateUser = 1
                  },
                  new tbAuthor
                  {
                      Id = 2,
                      LastName = "Sherali",
                      Name = "Juray",
                      Patronymic = "Ozod",
                      Status = 1,
                      CreateDate = DateTime.UtcNow,
                      CreateUser = 1
                  });

            modelBuilder.Entity<tbQuote>().HasData(
                 new tbQuote
                 {
                     Id = 1,
                     Text = "Text 1",
                     AuthorId = 1,
                     CategoryId = 1,
                     Status = 1,
                     CreateDate = DateTime.UtcNow,
                     CreateUser = 1
                 },
                 new tbQuote
                 {
                     Id = 2,
                     Text = "Text 2",
                     AuthorId = 2,
                     CategoryId = 2,
                     Status = 1,
                     CreateDate = DateTime.UtcNow,
                     CreateUser = 1
                 },
                 new tbQuote
                 {
                     Id = 3,
                     Text = "Text 3",
                     AuthorId = 1,
                     CategoryId = 2,
                     Status = 1,
                     CreateDate = DateTime.UtcNow,
                     CreateUser = 1
                 },
                 new tbQuote
                 {
                     Id = 4,
                     Text = "Text 4",
                     AuthorId = 2,
                     CategoryId = 1,
                     Status = 1,
                     CreateDate = DateTime.UtcNow,
                     CreateUser = 1
                 });

            modelBuilder.Entity<tbSubscribe>().HasData(
                new tbSubscribe
                {
                    Id = 1,
                    SubscribeUserId = 1,
                    SenderId = 1,
                    Status = 1,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = 1
                },
                new tbSubscribe
                {
                    Id = 2,
                    SubscribeUserId = 2,
                    SenderId = 2,
                    Status = 1,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = 1
                });
        }
    }
}
