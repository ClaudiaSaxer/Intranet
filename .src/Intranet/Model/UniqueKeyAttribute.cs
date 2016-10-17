using System;

namespace Intranet.Model
{
    ///<summary> 
    ///Class representing a unique attribute.
    ///</summary> 
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueKeyAttribute : Attribute
    {
    }
}