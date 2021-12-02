namespace Backend.AuthModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBUsers : DbContext
    {
        public DBUsers()
            : base("name=DBUsers")
        {
        }

        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Statuses>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Statuses)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Contacts)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete();
        }
        public ContactData FindUser(string login, string password)
        {
            var user = Users.FirstOrDefault(userDB => userDB.Login == login && userDB.Password == password);

            if (user != null)
            {
                return new ContactData()
                {
                    Name = user.Contacts.Name,
                    Login = login,
                    Password = password,
                    Role = user.Statuses.Status
                };
            }

            return null;
        }
    }
}
