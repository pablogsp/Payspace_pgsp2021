//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Payspace_NextGen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Esocial_Layout
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Esocial_Layout()
        {
            this.PendenciaEsocials = new HashSet<PendenciaEsocial>();
        }
    
        public string Id { get; set; }
        public string Layout { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PendenciaEsocial> PendenciaEsocials { get; set; }
    }
}