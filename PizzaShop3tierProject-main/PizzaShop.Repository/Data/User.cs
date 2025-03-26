using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class User
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Contactnumber { get; set; }

    public int RoleId { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? Zipcode { get; set; }

    public string? Imageurl { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Category> CategoryCreatedbyNavigations { get; } = new List<Category>();

    public virtual ICollection<Category> CategoryUpdatedbyNavigations { get; } = new List<Category>();

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Customer> CustomerCreatedbyNavigations { get; } = new List<Customer>();

    public virtual ICollection<Customer> CustomerUpdatedbyNavigations { get; } = new List<Customer>();

    public virtual ICollection<User> InverseCreatedbyNavigation { get; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedbyNavigation { get; } = new List<User>();

    public virtual ICollection<Item> ItemCreatedbyNavigations { get; } = new List<Item>();

    public virtual ICollection<Item> ItemUpdatedbyNavigations { get; } = new List<Item>();

    public virtual ICollection<Modifier> ModifierCreatedbyNavigations { get; } = new List<Modifier>();

    public virtual ICollection<Modifier> ModifierUpdatedbyNavigations { get; } = new List<Modifier>();

    public virtual ICollection<Modifiergroup> ModifiergroupCreatedbyNavigations { get; } = new List<Modifiergroup>();

    public virtual ICollection<Modifiergroup> ModifiergroupUpdatedbyNavigations { get; } = new List<Modifiergroup>();

    public virtual ICollection<Order> OrderCreatedbyNavigations { get; } = new List<Order>();

    public virtual ICollection<Order> OrderUpdatedbyNavigations { get; } = new List<Order>();

    public virtual ICollection<Permission> PermissionCreatedbyNavigations { get; } = new List<Permission>();

    public virtual ICollection<Permission> PermissionUpdatedbyNavigations { get; } = new List<Permission>();

    public virtual ICollection<Permissionfield> PermissionfieldCreatedbyNavigations { get; } = new List<Permissionfield>();

    public virtual ICollection<Permissionfield> PermissionfieldUpdatedbyNavigations { get; } = new List<Permissionfield>();

    public virtual ICollection<Rating> RatingCreatedbyNavigations { get; } = new List<Rating>();

    public virtual ICollection<Rating> RatingUpdatedbyNavigations { get; } = new List<Rating>();

    public virtual ICollection<Resettoken> Resettokens { get; } = new List<Resettoken>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Section> SectionCreatedbyNavigations { get; } = new List<Section>();

    public virtual ICollection<Section> SectionUpdatedbyNavigations { get; } = new List<Section>();

    public virtual ICollection<Table> TableCreatedbyNavigations { get; } = new List<Table>();

    public virtual ICollection<Table> TableUpdatedbyNavigations { get; } = new List<Table>();

    public virtual ICollection<Tax> TaxCreatedbyNavigations { get; } = new List<Tax>();

    public virtual ICollection<Tax> TaxUpdatedbyNavigations { get; } = new List<Tax>();

    public virtual ICollection<Unit> UnitCreatedbyNavigations { get; } = new List<Unit>();

    public virtual ICollection<Unit> UnitUpdatedbyNavigations { get; } = new List<Unit>();

    public virtual User? UpdatedbyNavigation { get; set; }

    public virtual ICollection<Waitingtoken> WaitingtokenCreatedbyNavigations { get; } = new List<Waitingtoken>();

    public virtual ICollection<Waitingtoken> WaitingtokenUpdatedbyNavigations { get; } = new List<Waitingtoken>();
}
