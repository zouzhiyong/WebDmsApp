﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFine.MySQLModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class nfinebaseEntities : DbContext
    {
        public nfinebaseEntities()
            : base("name=nfinebaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bas_corporation> bas_corporation { get; set; }
        public virtual DbSet<bas_customer> bas_customer { get; set; }
        public virtual DbSet<bas_material> bas_material { get; set; }
        public virtual DbSet<bas_materialgroup> bas_materialgroup { get; set; }
        public virtual DbSet<bas_materialpicture> bas_materialpicture { get; set; }
        public virtual DbSet<bas_materialuom> bas_materialuom { get; set; }
        public virtual DbSet<bas_region> bas_region { get; set; }
        public virtual DbSet<bas_supplier> bas_supplier { get; set; }
        public virtual DbSet<bas_truck> bas_truck { get; set; }
        public virtual DbSet<bas_unitofmeasure> bas_unitofmeasure { get; set; }
        public virtual DbSet<bas_warehouse> bas_warehouse { get; set; }
        public virtual DbSet<bas_warehouseuser> bas_warehouseuser { get; set; }
        public virtual DbSet<pur_order> pur_order { get; set; }
        public virtual DbSet<pur_orderdetail> pur_orderdetail { get; set; }
        public virtual DbSet<sys_area> sys_area { get; set; }
        public virtual DbSet<sys_company> sys_company { get; set; }
        public virtual DbSet<sys_companyauthorize> sys_companyauthorize { get; set; }
        public virtual DbSet<sys_dbbackup> sys_dbbackup { get; set; }
        public virtual DbSet<sys_department> sys_department { get; set; }
        public virtual DbSet<sys_filterip> sys_filterip { get; set; }
        public virtual DbSet<sys_items> sys_items { get; set; }
        public virtual DbSet<sys_itemscustdetail> sys_itemscustdetail { get; set; }
        public virtual DbSet<sys_itemsdetail> sys_itemsdetail { get; set; }
        public virtual DbSet<sys_log> sys_log { get; set; }
        public virtual DbSet<sys_module> sys_module { get; set; }
        public virtual DbSet<sys_modulebutton> sys_modulebutton { get; set; }
        public virtual DbSet<sys_moduleform> sys_moduleform { get; set; }
        public virtual DbSet<sys_moduleforminstance> sys_moduleforminstance { get; set; }
        public virtual DbSet<sys_organize1> sys_organize1 { get; set; }
        public virtual DbSet<sys_role> sys_role { get; set; }
        public virtual DbSet<sys_roleauthorize> sys_roleauthorize { get; set; }
        public virtual DbSet<sys_user> sys_user { get; set; }
        public virtual DbSet<sys_userlogon> sys_userlogon { get; set; }
    }
}
