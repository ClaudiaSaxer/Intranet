using System;

namespace Intranet.Labor
{
    /// <summary>
    ///     Class representing a unique attribute.
    /// </summary>
    [AttributeUsage( AttributeTargets.Property )]
    public class UniqueKeyAttribute : Attribute
    {
    }
}