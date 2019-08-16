namespace Backend.AuthModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public int Id { get; set; }

        public int Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual Contacts Contacts { get; set; }

        public virtual Statuses Statuses { get; set; }
    }
}
