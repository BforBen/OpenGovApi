using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OpenGovApi.Models
{
    public abstract class ObjectWithAttributes
    {
        /// <summary>
        /// A collection of key-value pairs to make the object extensible
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Attributes { get; set; }
    }

    public class Person : ObjectWithAttributes
    {
        public string Id { get; set; }
        public PersonName Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class Asset : ObjectWithAttributes
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Location Location { get; set; }
    }

    public class Account : ObjectWithAttributes
    {
        public string Id { get; set; }
        public string ServiceId { get; set; }
        public string Reference { get; set; }
        public List<string> Notes { get; set; }
        public DateTime Updated { get; set; }
    }

    public class Relationship : ObjectWithAttributes
    {
        public string Id { get; set; }
        public RelationshipType Type { get; set; }
        public string ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
    }

    public class Service
    {
        [Key]
        public string Id { get; set; }
        public int ServiceCategoryId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime Updated { get; set; }

        public virtual ServiceCategory Category { get; set; }
    }

    public class ServiceAttribute
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string HelpText { get; set; }
        public ServiceAttributeDataType Type { get; set; }
        public bool Required { get; set; }
        public int Order { get; set; }
    }

    public class ServiceAttributeAttribute
    {
        public int Id { get; set; }
        public int ServiceAttributeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ServiceAttributeValue
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public int ServiceAttributeId { get; set; }
    }

    public class ServiceCategory
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OpenGovContext : DbContext
    {
        public OpenGovContext()
            : base("name=OpenGovApiData")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAttribute> ServiceAttributes { get; set; }
        public DbSet<ServiceAttributeAttribute> ServiceAttributeAttributes { get; set; }
        public DbSet<ServiceAttributeValue> ServiceAttributeValues { get; set; }

        public DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}