using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using AssetsDAL.Model;

namespace AssetsDAL
{
    public class AssetsContext : DbContext
    {
        public AssetsContext(DbContextOptions<AssetsContext> options) : base(options)
        {
            //Database.SetInitializer<AdminContext>(new CreateDatabaseIfNotExists<AdminContext>());
        }

        public DbSet<ASMasterErrorLog> ASMasterErrorLogs { get; set; }
        public DbSet<ASMasterBrand> ASMasterBrands { get; set; }
        public DbSet<ASMasterCategory> ASMasterCategories { get; set; }
        public virtual DbSet<ASMasterSubCategory> ASMasterSubCategories { get; set; }
        public virtual DbSet<ASMasterProductType> ASMasterProductTypes { get; set; }
        public virtual DbSet<ASMasterProductSize> ASMasterProductSizes { get; set; }
        public virtual DbSet<ASMasterProduct> ASMasterProducts { get; set; }
        public virtual DbSet<ASMasterProductChild> ASMasterProductChilds { get; set; }
        public virtual DbSet<ASTransactionProductHistory> ASTransactionProductHistorys { get; set; }
        public virtual DbSet<ASMasterAssetsAssignment> ASMasterAssetsAssignments { get; set; }

        public virtual DbSet<SPDropDownFillResult> DropDownFillResults { get; set; }
        public virtual List<SPDropDownFillResult> SP_ASDropDownFillResult(string TableName, long MasterId, string Type)
        {
            try
            {
                var _DropDownFillResult = this.DropDownFillResults.FromSqlRaw<SPDropDownFillResult>("Exec SP_ASDropDownFill {0},{1},{2}", TableName, MasterId, Type).ToList();
                return _DropDownFillResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPCheckAvailableResult> CheckAvailableResults { get; set; }
        public virtual List<SPCheckAvailableResult> SP_ASCheckAvailableResult(string TableName, string NameAvailable, long NameId)
        {
            try
            {
                var _CheckAvailableResult = this.CheckAvailableResults.FromSqlRaw<SPCheckAvailableResult>("Exec SP_ASCheckAvailable {0},{1},{2}", TableName, NameAvailable, NameId).ToList();
                return _CheckAvailableResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPNextMasterIdResult> NextMasterIdResults { get; set; }

        public virtual List<SPNextMasterIdResult> SP_ASNextMasterIdResult(string TableName)
        {
            try
            {
                var _NextMasterIdResult = this.NextMasterIdResults.FromSqlRaw<SPNextMasterIdResult>("Exec SP_ASNextMasterId {0}", TableName).ToList();
                return _NextMasterIdResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPMaxTableMasterIdResult> MaxTableMasterIdResults { get; set; }

        public virtual List<SPMaxTableMasterIdResult> SP_ASMaxTableMasterIdResult(string TableName)
        {
            try
            {
                var _MaxMasterIdResult = this.MaxTableMasterIdResults.FromSqlRaw<SPMaxTableMasterIdResult>("Exec SP_ASMaxTableMasterId {0}", TableName).ToList();
                return _MaxMasterIdResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
            
            //ADGenCodeMaster
            //modelBuilder.Entity<ADGenCodeMaster>().HasData(new ADGenCodeMaster { GenCodeMasterId=1, GenCodeMasterTitle= "Propritor", PrintDesc="", ADGenCodeType = 1, Sequence = 1, IsActive = true, EnterById = 1, EnterByDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

        }

        

        
    }
}
