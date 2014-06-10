using System;
using System.ComponentModel.DataAnnotations;

namespace OpenGovApi.Models
{
    public enum RelationshipType
    {
        [Display(Name = "Parent of")]
        ParentOf
    }

    public enum ObjectType
    {
        Person,
        Asset,
        Account
    }

    public enum ServiceDataNecessity
    {
        [Display(Name = "Not required")]
        Not_required,
        Optional,
        Required
    }

    public enum ServiceLocationType
    {
        Any,
        [Display(Name = "Residential property")]
        Residential_property,
        [Display(Name = "Commercial property")]
        Commercial_property,
        Street,
        Point
    }

    public enum ServiceAttributeDataType
    {
        None,
        DataList,
        Date,
        DateTime,
        Email,
        MultiSelect,
        Number,
        Password,
        Range,
        Search,
        Select,
        Tel,
        Text,
        TextArea,
        Time,
        Url
    }
}