

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace StokTakip_Deneme_.Models.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Markalar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Markalar()
        {
            this.Urunler = new HashSet<Urunler>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = " Kategori Alan� bo� ge�ilemez")]
        public int KategoriID { get; set; }

        [Required(ErrorMessage = " Marka Alan� bo� ge�ilemez")]
        public string Marka { get; set; }

        [Required(ErrorMessage = " A��klama Alan� bo� ge�ilemez")]
        public string Aciklama { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urunler> Urunler { get; set; }
    }
}
