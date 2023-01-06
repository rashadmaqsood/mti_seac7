#region Imports

using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


#endregion

namespace DatabaseConfiguration.CommonModels
{
    public class ServicesModel : DbContext
    {
        public ServicesModel(DbConnection conn) : base(conn, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);                        
        }

        #region Properties

        public virtual DbSet<all_quantities> AllQuantitiesData { get; set; }

        public virtual DbSet<authentication_type> Authentication_Type_Data { get; set; }

        public virtual DbSet<billingitem_group> BillingItem_Group_Data { get; set; }

        public virtual DbSet<billing_items> BillingItems_Data { get; set; }

        public virtual DbSet<bill_tariff_quantity> BillTariffQuantity_Data { get; set; }

        public virtual DbSet<capture_objects> Capture_Objects_Data { get; set; }
        
        public virtual DbSet<configuration_new> Configurations_Data { get; set; }

        public virtual DbSet<device> DevicesData { get; set; }

        public virtual DbSet<device_association> Device_Associations_Data { get; set; }

        public virtual DbSet<display_windows> Display_Windows_Data { get; set; }

        public virtual DbSet<displaywindows_group> DisplayWindows_Group_Data { get; set; }

        public virtual DbSet<event_info> Events_Info_Data { get; set; }

        public virtual DbSet<event_logs> Event_Logs_Data { get; set; }

        public virtual DbSet<events_group> Events_Group_Data { get; set; }

        public virtual DbSet<loadprofile_group> LoadProfile_Group_Data { get; set; }

        public virtual DbSet<load_profile_channels> LoadProfileChannels_Data { get; set; }

        public virtual DbSet<manufacturer> Manufacturer_Data { get; set; }

        public virtual DbSet<obis_rights> Obis_Rights_Data { get; set; }

        public virtual DbSet<obis_rights_group> Obis_Rights_Group_Data { get; set; }

        public virtual DbSet<obis_details> ObisDetails_Data { get; set; }

        public virtual DbSet<rights> Rights_Data { get; set; }

        public virtual DbSet<status_word> StatusWord_Data { get; set; }

        public virtual DbSet<users> users { get; set; }

        #endregion // Properties
    }
}
