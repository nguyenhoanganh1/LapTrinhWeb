namespace Model.EL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public double Amount { get; set; }

        public int Status { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
