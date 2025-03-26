using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Repository.Data;

public partial class PizzaShopDbContext : DbContext
{
    public PizzaShopDbContext(DbContextOptions<PizzaShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Favouriteitem> Favouriteitems { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Itemtomodifiergroup> Itemtomodifiergroups { get; set; }

    public virtual DbSet<Modifier> Modifiers { get; set; }

    public virtual DbSet<Modifiergroup> Modifiergroups { get; set; }

    public virtual DbSet<Modifiertomodifiergroup> Modifiertomodifiergroups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitemmodifier> Orderitemmodifiers { get; set; }

    public virtual DbSet<Ordertoitem> Ordertoitems { get; set; }

    public virtual DbSet<Ordertotable> Ordertotables { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Permissionfield> Permissionfields { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Resettoken> Resettokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitingtoken> Waitingtokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.CategoryCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("category_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.CategoryUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("category_updatedby_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "customer_email_key").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(15)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.CustomerCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.CustomerUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_updatedby_fkey");
        });

        modelBuilder.Entity<Favouriteitem>(entity =>
        {
            entity.HasKey(e => e.FavouriteitemId).HasName("favouriteitem_pkey");

            entity.ToTable("favouriteitem");

            entity.Property(e => e.FavouriteitemId).HasColumnName("favouriteitem_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Favouriteitems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("favouriteitem_item_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("item_pkey");

            entity.ToTable("item");

            entity.HasIndex(e => e.Shortcode, "item_shortcode_key").IsUnique();

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Defaulttax).HasColumnName("defaulttax");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(255)
                .HasColumnName("imageurl");
            entity.Property(e => e.Isavailable)
                .HasDefaultValueSql("true")
                .HasColumnName("isavailable");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Itemtype)
                .HasMaxLength(50)
                .HasColumnName("itemtype");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
            entity.Property(e => e.Shortcode)
                .HasMaxLength(20)
                .HasColumnName("shortcode");
            entity.Property(e => e.Taxpercentage)
                .HasPrecision(5, 2)
                .HasColumnName("taxpercentage");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("item_category_id_fkey");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.ItemCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("item_createdby_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Items)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("item_unit_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.ItemUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("item_updatedby_fkey");
        });

        modelBuilder.Entity<Itemtomodifiergroup>(entity =>
        {
            entity.HasKey(e => e.ItemtogroupId).HasName("itemtomodifiergroup_pkey");

            entity.ToTable("itemtomodifiergroup");

            entity.Property(e => e.ItemtogroupId).HasColumnName("itemtogroup_id");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Maxval).HasColumnName("maxval");
            entity.Property(e => e.Minval).HasColumnName("minval");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");

            entity.HasOne(d => d.Item).WithMany(p => p.Itemtomodifiergroups)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("itemtomodifiergroup_item_id_fkey");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Itemtomodifiergroups)
                .HasForeignKey(d => d.ModifiergroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("itemtomodifiergroup_modifiergroup_id_fkey");
        });

        modelBuilder.Entity<Modifier>(entity =>
        {
            entity.HasKey(e => e.ModifierId).HasName("modifier_pkey");

            entity.ToTable("modifier");

            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Modifiername)
                .HasMaxLength(50)
                .HasColumnName("modifiername");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(5, 2)
                .HasColumnName("rate");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.ModifierCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_createdby_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Modifiers)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_unit_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.ModifierUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_updatedby_fkey");
        });

        modelBuilder.Entity<Modifiergroup>(entity =>
        {
            entity.HasKey(e => e.ModifiergroupId).HasName("modifiergroup_pkey");

            entity.ToTable("modifiergroup");

            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Groupname)
                .HasMaxLength(50)
                .HasColumnName("groupname");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.ModifiergroupCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("modifiergroup_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.ModifiergroupUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("modifiergroup_updatedby_fkey");
        });

        modelBuilder.Entity<Modifiertomodifiergroup>(entity =>
        {
            entity.HasKey(e => e.ModifiertogroupId).HasName("modifiertomodifiergroup_pkey");

            entity.ToTable("modifiertomodifiergroup");

            entity.Property(e => e.ModifiertogroupId).HasColumnName("modifiertogroup_id");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Modifiertomodifiergroups)
                .HasForeignKey(d => d.ModifierId)
                .HasConstraintName("modifiertomodifiergroup_modifier_id_fkey");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Modifiertomodifiergroups)
                .HasForeignKey(d => d.ModifiergroupId)
                .HasConstraintName("modifiertomodifiergroup_modifiergroup_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("order_pkey");

            entity.ToTable("order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Noofperson).HasColumnName("noofperson");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Orderstatus)
                .HasMaxLength(50)
                .HasColumnName("orderstatus");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrderCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("order_createdby_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("order_customer_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrderUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("order_updatedby_fkey");
        });

        modelBuilder.Entity<Orderitemmodifier>(entity =>
        {
            entity.HasKey(e => e.OrderitemmodifierId).HasName("orderitemmodifier_pkey");

            entity.ToTable("orderitemmodifier");

            entity.Property(e => e.OrderitemmodifierId).HasColumnName("orderitemmodifier_id");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.OrdertoitemId).HasColumnName("ordertoitem_id");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Orderitemmodifiers)
                .HasForeignKey(d => d.ModifierId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderitemmodifier_modifier_id_fkey");

            entity.HasOne(d => d.Ordertoitem).WithMany(p => p.Orderitemmodifiers)
                .HasForeignKey(d => d.OrdertoitemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderitemmodifier_ordertoitem_id_fkey");
        });

        modelBuilder.Entity<Ordertoitem>(entity =>
        {
            entity.HasKey(e => e.OrdertoitemId).HasName("ordertoitem_pkey");

            entity.ToTable("ordertoitem");

            entity.Property(e => e.OrdertoitemId).HasColumnName("ordertoitem_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Instruction).HasColumnName("instruction");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Item).WithMany(p => p.Ordertoitems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ordertoitem_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertoitems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ordertoitem_order_id_fkey");
        });

        modelBuilder.Entity<Ordertotable>(entity =>
        {
            entity.HasKey(e => e.OrdertotableId).HasName("ordertotable_pkey");

            entity.ToTable("ordertotable");

            entity.Property(e => e.OrdertotableId).HasColumnName("ordertotable_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordertotables)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ordertotable_order_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Ordertotables)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("ordertotable_table_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(50)
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payment_order_id_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("Permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.PermissionId)
                .HasDefaultValueSql("nextval('\"Permission_permission_id_seq\"'::regclass)")
                .HasColumnName("permission_id");
            entity.Property(e => e.Canadd).HasColumnName("canadd");
            entity.Property(e => e.Candelete).HasColumnName("candelete");
            entity.Property(e => e.Canview).HasColumnName("canview");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.PermissionfieldId).HasColumnName("permissionfield_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.PermissionCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("Permission_createdby_fkey");

            entity.HasOne(d => d.Permissionfield).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionfieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Permission_permissionfield_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Permission_role_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.PermissionUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("Permission_updatedby_fkey");
        });

        modelBuilder.Entity<Permissionfield>(entity =>
        {
            entity.HasKey(e => e.PermissionfieldId).HasName("PermissionField_pkey");

            entity.ToTable("permissionfield");

            entity.Property(e => e.PermissionfieldId)
                .HasDefaultValueSql("nextval('\"PermissionField_permissionfield_id_seq\"'::regclass)")
                .HasColumnName("permissionfield_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Permissionfieldname)
                .HasMaxLength(50)
                .HasColumnName("permissionfieldname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.PermissionfieldCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PermissionField_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.PermissionfieldUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PermissionField_updatedby_fkey");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("rating_pkey");

            entity.ToTable("rating");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.Ambiencerate).HasColumnName("ambiencerate");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Foodrate).HasColumnName("foodrate");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Servicerate).HasColumnName("servicerate");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.RatingCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("rating_createdby_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("rating_order_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.RatingUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("rating_updatedby_fkey");
        });

        modelBuilder.Entity<Resettoken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("resettoken_pkey");

            entity.ToTable("resettoken");

            entity.HasIndex(e => e.Token, "resettoken_token_key").IsUnique();

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Expiredat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiredat");
            entity.Property(e => e.Isreseted)
                .HasDefaultValueSql("false")
                .HasColumnName("isreseted");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Resettokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("resettoken_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("nextval('roles_role_id_seq'::regclass)")
                .HasColumnName("role_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("section_pkey");

            entity.ToTable("section");

            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.SectionCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.SectionUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_updatedby_fkey");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("table_pkey");

            entity.ToTable("table");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("true")
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TableCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("table_createdby_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("table_section_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.TableUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("table_updatedby_fkey");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("tax_pkey");

            entity.ToTable("tax");

            entity.HasIndex(e => e.Name, "tax_name_key").IsUnique();

            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdefault)
                .HasDefaultValueSql("false")
                .HasColumnName("isdefault");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isenabled)
                .HasDefaultValueSql("true")
                .HasColumnName("isenabled");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Taxtype)
                .HasMaxLength(50)
                .HasColumnName("taxtype");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TaxCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("tax_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.TaxUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("tax_updatedby_fkey");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("unit_pkey");

            entity.ToTable("unit");

            entity.HasIndex(e => e.Shortname, "unit_shortname_key").IsUnique();

            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Shortname)
                .HasMaxLength(20)
                .HasColumnName("shortname");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.UnitCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("unit_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.UnitUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("unit_updatedby_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "user_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(20)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Imageurl).HasColumnName("imageurl");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("true")
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.InverseCreatedbyNavigation)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("user_createdby_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_role_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.InverseUpdatedbyNavigation)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("user_updatedby_fkey");
        });

        modelBuilder.Entity<Waitingtoken>(entity =>
        {
            entity.HasKey(e => e.WaitingtokenId).HasName("waitingtoken_pkey");

            entity.ToTable("waitingtoken");

            entity.HasIndex(e => e.Email, "waitingtoken_email_key").IsUnique();

            entity.Property(e => e.WaitingtokenId).HasColumnName("waitingtoken_id");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(15)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isassigned)
                .HasDefaultValueSql("false")
                .HasColumnName("isassigned");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Noofpersons).HasColumnName("noofpersons");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Waitingtime).HasColumnName("waitingtime");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.WaitingtokenCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitingtoken_createdby_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.Waitingtokens)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("waitingtoken_section_id_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.WaitingtokenUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitingtoken_updatedby_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
