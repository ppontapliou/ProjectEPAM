namespace Backend.Models.DBModelsHelper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContactsEmails
    {
        public int Id { get; set; }

        public int Contact { get; set; }

        public int Email { get; set; }

        public virtual Contacts Contacts { get; set; }

        public virtual Emails Emails { get; set; }
    }
}
