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
    
    public partial class Poslovna_godina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poslovna_godina()
        {
            this.Fakturas = new HashSet<Faktura>();
            this.Prijemni_dokument = new HashSet<Prijemni_dokument>();
            this.Robna_kartica = new HashSet<Robna_kartica>();
        }
    
        public decimal Id_Poslovna_godina { get; set; }
        public decimal Id_Preduzece { get; set; }
        public decimal Godina_Poslovna_godina { get; set; }
        public bool Zakljucena_Poslovna_godina { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faktura> Fakturas { get; set; }
        public virtual Preduzece Preduzece { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prijemni_dokument> Prijemni_dokument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Robna_kartica> Robna_kartica { get; set; }
    }
}
