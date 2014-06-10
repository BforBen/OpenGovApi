using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Spatial;

namespace OpenGovApi.Models
{
    [ComplexType]
    [DisplayColumn("Display")]
    public class PersonName
    {
        [MaxLength(10)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Title + " " + FirstName + " " + LastName;
            }
        }
    }

    /// <summary>
    /// Location
    /// </summary>
    [ComplexType]
    public class Location
    {
        public Address Address { get; set; }
        
    }

    /// <summary>
    /// Address
    /// </summary>
    [ComplexType]
    public class Address
    {
        /// <summary>
        /// UPRN
        /// </summary>
        public Int64? Uprn { get; set; }
        /// <summary>
        /// USRN
        /// </summary>
        public Int64? Usrn { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Property { get; set; }
        [MaxLength(200)]
        public string Street { get; set; }
        [MaxLength(200)]
        public string Locality { get; set; }
        [MaxLength(200)]
        public string Town { get; set; }
        [MaxLength(200)]
        public string PostTown { get; set; }
        [MaxLength(200)]
        public string County { get; set; }
        [MaxLength(10)]
        public string PostCode { get; set; }
        [MaxLength(200)]
        public string Country { get; set; }
    }
}