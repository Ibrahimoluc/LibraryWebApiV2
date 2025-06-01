using System;
using System.Drawing;
using LibraryWebApi.Models;
using LibraryWebApi.Models.Abstract;
using LibraryWebApi.Repo.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryWebApi.Repo.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        } 

        DbSet<Field> Fields { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<User> Users { get; set; }

        //createdDate ve updatedDate otomatik guncelleme icin
        public override int SaveChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if(e.Entity is BaseClass baseClass)
                {
                    if(e.State == EntityState.Added)
                    {
                        baseClass.CreatedDate = DateTime.Now;
                        baseClass.UpdatedDate = DateTime.Now;
                    }
                    if (e.State == EntityState.Modified)
                    {
                        baseClass.UpdatedDate = DateTime.Now;
                    }
                }
            });

            return base.SaveChanges();
        }
    }
}
