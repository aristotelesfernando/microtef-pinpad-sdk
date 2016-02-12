using Pinpad.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Core.Commands {
    /// <summary>
    /// LFI request
    /// </summary>
    public class LfiRequest : BaseStoneRequest {
       /// <summary>
       /// Constructor
       /// </summary>
       public LfiRequest() {
           this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
           this.LFI_FILENAME = new VariableLengthProperty<string>("LFI_FILENAME", 3, 15, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

           this.StartRegion(this.CMD_LEN1);
           {
               this.AddProperty(this.LFI_FILENAME);
           }
           this.EndLastRegion();
       }

       /// <summary>
       /// Minimum stone version required for the request
       /// </summary>
       public override int MinimumStoneVersion { get { return 1; } }

       /// <summary>
       /// Name of the command
       /// </summary>
       public override string CommandName { get { return "LFI"; } }

       /// <summary>
       /// Length of the first region of the command
       /// </summary>
       public RegionProperty CMD_LEN1 { get; private set; }

       /// <summary>
       /// Name of the file
       /// </summary>
       public VariableLengthProperty<string> LFI_FILENAME { get; private set; }
    }
}
