//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Magacin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Magacin()
        {
            this.Prijemni_dokument = new HashSet<Prijemni_dokument>();
            this.Prijemni_dokument1 = new HashSet<Prijemni_dokument>();
            this.Robna_kartica = new HashSet<Robna_kartica>();
        }
    
        public decimal Id_Magacin { get; set; }
        public Nullable<decimal> Id_Preduzece { get; set; }
        public decimal Id { get; set; }
        public string Naziv_Magacin { get; set; }
        public string Adresa_Magacin { get; set; }
    
        public virtual Mesto Mesto { get; set; }
        public virtual Preduzece Preduzece { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prijemni_dokument> Prijemni_dokument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prijemni_dokument> Prijemni_dokument1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Robna_kartica> Robna_kartica { get; set; }
    }
}
