using System;
using Intranet.Labor.Model;

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class represention an extension for the shift types
    /// </summary>
    public static class ShiftTypeExtension
    {
        /// <summary>
        ///     Converts the shift type into a friendly user string
        /// </summary>
        /// <param name="shiftType">the shift type</param>
        /// <returns>a user friendly string</returns>
        public static String ToFriendlyString( this ShiftType shiftType )
        {
            switch ( shiftType )
            {
                case ShiftType.Morning:
                    return "Morgen";
                case ShiftType.Night:
                    return "Nacht";
                case ShiftType.Late:
                    return "Spät";
                default:
                    return "Unbekannte Schicht";
            }
        }
    }
}