using System.Linq;
using System;

namespace Pinpad.Sdk.Commands.TypeCode
{
    /// <summary>
    /// Contains the blocking commands.
    /// </summary>
    public enum BlockingCommandCode
    {
        /// <summary>
        /// Invalid value.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Get card from ABECS specification.
        /// </summary>
        GCR = 1,
        /// <summary>
        /// Go on chip from ABECS specification.
        /// </summary>
        GOC = 2,
        /// <summary>
        /// Get key from ABECS specification.
        /// </summary>
        GKY = 3,
        /// <summary>
        /// Get PIN from ABECS specification.
        /// </summary>
        GPN = 4,
        /// <summary>
        /// Remove card from ABECS specification.
        /// </summary>
        RMC = 5,
    }

    /// <summary>
    /// String extension class.
    /// </summary>
    public static class BlockingCommandCodeExtension
    {
        /// <summary>
        /// Checks if the command received is a blocking command.
        /// </summary>
        public static bool IsBlockingCommand(this string command)
        {
            return Enum.GetValues(typeof(BlockingCommandCode))
                                              .Cast<BlockingCommandCode>()
                                              .ToArray()
                                              .Where(x => command.Contains(x.ToString()))
                                              .FirstOrDefault() != BlockingCommandCode.Undefined;
        }
    }
}
