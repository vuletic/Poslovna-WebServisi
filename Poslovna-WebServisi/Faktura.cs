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
    
    public partial class Faktura
    {
        public decimal Id_Faktura { get; set; }
        public decimal Id_Poslovna_godina { get; set; }
        public decimal Id_Partner { get; set; }
        public decimal Broj_fakture_Faktura { get; set; }
        public System.DateTime Datum_fakture_Faktura { get; set; }
        public System.DateTime Datum_valute_Faktura { get; set; }
        public Nullable<decimal> Ukupan_rabat_Faktura { get; set; }
        public decimal Ukupan_iznos_bez_PDV_a_Faktura { get; set; }
        public Nullable<decimal> Ukupan_PDV_Faktura { get; set; }
        public Nullable<decimal> Ukupno_za_placanje_Faktura { get; set; }
    
        public virtual Poslovna_godina Poslovna_godina { get; set; }
        public virtual Poslovni_partner Poslovni_partner { get; set; }
    }
}
