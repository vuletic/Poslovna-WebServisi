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
    
    public partial class Preduzece
    {
        public Preduzece()
        {
            this.Grupa_roba = new HashSet<Grupa_roba>();
            this.Magacins = new HashSet<Magacin>();
            this.Poslovna_godina = new HashSet<Poslovna_godina>();
            this.Poslovni_partner = new HashSet<Poslovni_partner>();
            this.Robas = new HashSet<Roba>();
        }
    
        public decimal Id_Preduzece { get; set; }
        public decimal Id { get; set; }
        public string Naziv_Preduzece { get; set; }
        public decimal Maticni_broj_Preduzece { get; set; }
        public decimal PIB_Preduzece { get; set; }
        public string Adresa_Preduzece { get; set; }
    
        public virtual ICollection<Grupa_roba> Grupa_roba { get; set; }
        public virtual ICollection<Magacin> Magacins { get; set; }
        public virtual Mesto Mesto { get; set; }
        public virtual ICollection<Poslovna_godina> Poslovna_godina { get; set; }
        public virtual ICollection<Poslovni_partner> Poslovni_partner { get; set; }
        public virtual ICollection<Roba> Robas { get; set; }
    }
}