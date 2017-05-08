using System.Linq;
using System;

namespace Pinpad.Sdk.Commands.TypeCode
{
    //TODO : Docum
    /// <summary>
    /// 
    /// </summary>
    public enum BlockingCommandCode
    {
        /// <summary>
        /// invalid value
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///Get card
        /// </summary>
        GCR = 1,

        /// <summary>
        /// Go on chip
        /// </summary>
        GOC = 2,

        /// <summary>
        /// Get key
        /// </summary>
        GKY = 3,

        /// <summary>
        /// Get PIN
        /// </summary>
        GPN = 4,

        /// <summary>
        /// Remove card
        /// </summary>
        RMC = 5,
    }
    /// <summary>
    /// 
    /// </summary>
    public static class BlockingCommandCodeExtension
    {
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
