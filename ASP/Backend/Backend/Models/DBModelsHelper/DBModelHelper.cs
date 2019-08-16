namespace Backend.Models.DBModelsHelper
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Caching;

    public partial class DBModelHelper : DbContext
    {
        public DBModelHelper()
            : base("name=DBModelHelper")
        {
        }

        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<ContactsEmails> ContactsEmails { get; set; }
        public virtual DbSet<ContactsPhones> ContactsPhones { get; set; }
        public virtual DbSet<Emails> Emails { get; set; }
        public virtual DbSet<Phones> Phones { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<Contacts>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.Contacts)
                .HasForeignKey(e => e.Contact);

            modelBuilder.Entity<Contacts>()
                .HasMany(e => e.ContactsEmails)
                .WithRequired(e => e.Contacts)
                .HasForeignKey(e => e.Contact);

            modelBuilder.Entity<Contacts>()
                .HasMany(e => e.ContactsPhones)
                .WithRequired(e => e.Contacts)
                .HasForeignKey(e => e.Contact);

            modelBuilder.Entity<Emails>()
                .HasMany(e => e.ContactsEmails)
                .WithRequired(e => e.Emails)
                .HasForeignKey(e => e.Email);

            modelBuilder.Entity<Phones>()
                .HasMany(e => e.ContactsPhones)
                .WithRequired(e => e.Phones)
                .HasForeignKey(e => e.Phone);

            modelBuilder.Entity<States>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.States)
                .HasForeignKey(e => e.State);

            modelBuilder.Entity<Statuses>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Statuses)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<Types>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.Types)
                .HasForeignKey(e => e.Type);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Contacts)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete();
        }
        public void AddCategory(string categoryName)
        {
            Categories.Add(new Categories() { Category = categoryName });
            SaveChanges();
        }
        public void AddType(string typeName)
        {
            Types.Add(new Types() {Type = typeName });
            SaveChanges();
        }
        public void AddState(string stateName)
        {
            States.Add(new States() { State = stateName });
            SaveChanges();
        }
        public void ChangeCategory(Parameter category)
        {
            var tmp = Categories.Where(c => c.Id == category.Id).First();
            tmp.Category = category.Name;
            SaveChanges();
        }
        public void ChangeType(Parameter type)
        {
            var tmp = Types.Where(c => c.Id == type.Id).First();
            tmp.Type = type.Name;
            SaveChanges();
        }
        public void ChangeState(Parameter state)
        {
            var tmp = States.Where(c => c.Id == state.Id).First();
            tmp.State = state.Name;
            SaveChanges();
        }
        public void DeleteCategory(int idCategory)
        {
            var category = Categories.Where(c => c.Id == idCategory).First();
            Categories.Remove(category);
            SaveChanges();
        }
        public void DeleteType(int idType)
        {
            var type = Types.Where(c => c.Id == idType).First();
            Types.Remove(type);
            SaveChanges();
        }
        public void DeleteState(int idState)
        {
            var state = States.Where(c => c.Id == idState).First();
            States.Remove(state);
            SaveChanges();
        }
        public void GetCategories()
        {
            object ob = new object();
            Cache cache = new Cache();
            cache.Insert("Categories", ob, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
           
        }
    }
}
