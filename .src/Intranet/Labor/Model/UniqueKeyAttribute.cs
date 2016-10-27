using System;

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing a unique attribute.
    /// </summary>
    [AttributeUsage( AttributeTargets.Property )]
    public class UniqueKeyAttribute : Attribute
    {
    }
}