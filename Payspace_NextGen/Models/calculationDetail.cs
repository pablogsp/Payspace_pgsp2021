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
    
    public partial class calculationDetail
    {
        public int Id { get; set; }
        public string EmployeID { get; set; }
        public string DetailName { get; set; }
        public decimal DetailValue { get; set; }
        public int IdCalculation { get; set; }
    
        public virtual Calculation Calculation { get; set; }
        public virtual Employe Employe { get; set; }
    }
}
