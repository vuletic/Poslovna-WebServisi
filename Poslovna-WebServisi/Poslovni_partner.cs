//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Poslovni_partner
    {
        public Poslovni_partner()
        {
            this.Fakturas = new HashSet<Faktura>();
            this.Prijemni_dokument = new HashSet<Prijemni_dokument>();
        }
    
        public decimal Id_Partner { get; set; }
        public decimal Id_Preduzece { get; set; }
        public decimal Id { get; set; }
        public string Tip_Partner { get; set; }
        public string Naziv_Partner { get; set; }
        public decimal PIB_Partner { get; set; }
        public decimal Maticni_broj_Partner { get; set; }
        public string Adresa_Partner { get; set; }
    
        public virtual ICollection<Faktura> Fakturas { get; set; }
        public virtual Mesto Mesto { get; set; }
        public virtual Preduzece Preduzece { get; set; }
        public virtual ICollection<Prijemni_dokument> Prijemni_dokument { get; set; }
    }
}