﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class localdbEntities : DbContext
    {
        public localdbEntities()
            : base("name=localdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Calculation> Calculations { get; set; }
        public virtual DbSet<calculationDetail> calculationDetails { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<PostCode> PostCodes { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<RateValue> RateValues { get; set; }
        public virtual DbSet<PendenciaEsocial> PendenciaEsocials { get; set; }
        public virtual DbSet<Esocial_Layout> Esocial_Layout { get; set; }
    }
}