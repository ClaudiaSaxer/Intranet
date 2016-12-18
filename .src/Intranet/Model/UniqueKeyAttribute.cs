#region Usings

using System;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class representing a unique attribute.
    /// </summary>
    [AttributeUsage( AttributeTargets.Property )]
    public class UniqueKeyAttribute : Attribute
    {
    }
}