namespace Backend.Models.DBModelsHelper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ads
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ad { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime DataCreation { get; set; }

        [Required]
        public string Picture { get; set; }

        public int Category { get; set; }

        public int Contact { get; set; }

        [Required]
        public string Adress { get; set; }

        public int Type { get; set; }

        public int State { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual Contacts Contacts { get; set; }

        public virtual States States { get; set; }

        public virtual Types Types { get; set; }
    }
}
