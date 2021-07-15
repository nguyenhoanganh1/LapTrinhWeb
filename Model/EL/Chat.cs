namespace Model.EL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        public long Id { get; set; }

        public string Message { get; set; }

        public DateTime? DateTime { get; set; }

        [StringLength(20)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
