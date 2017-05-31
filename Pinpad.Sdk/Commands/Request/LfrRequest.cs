﻿using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Request to upload a file into pinpad memory.
    /// If the file exceeds the maximum size of the command, the file should be split and sent
    /// by multiple requests.
    /// </summary>
    internal sealed class LfrRequest : BaseCommand
    {
        /// <summary>
        /// Command name, LFR in this case.
        /// </summary>
        public override string CommandName { get { return "LFR"; } }
        /// <summary>
        /// Indicates if the pinpad needs user interaction before returning
        /// any response.
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// File data.
        /// </summary>
        public VariableLengthProperty<string> LFR_Data { get; private set; }

        /// <summary>
        /// Creates the command and all it's fields.
        /// </summary>
        public LfrRequest()
            : base (new IngenicoContext())
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFR_Data = new VariableLengthProperty<string>(
                "LFR_Data", 3, 999, 0.5f, false, false, StringFormatter.StringStringFormatter, 
                StringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFR_Data);
            }
            this.EndLastRegion();
        }
    }
}
