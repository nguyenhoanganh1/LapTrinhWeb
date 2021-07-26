namespace Model.EL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [DisplayName("Giá")]
        public double UnitPrice { get; set; }

        [DisplayName("Giảm Giá")]
        public double Discount { get; set; }

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày nhập")]
        public DateTime ProductDate { get; set; }

        [DisplayName("Đặc biệt")]
        public bool Special { get; set; }

        [DisplayName("Mới")]
        public bool Latest { get; set; }

        [DisplayName("Lượt xem")]
        public int ClickCount { get; set; }

        [DisplayName("Loại hàng")]
        public int CategoryId { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }


        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
