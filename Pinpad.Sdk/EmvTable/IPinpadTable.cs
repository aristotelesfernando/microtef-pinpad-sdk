using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PinPadSDK.Controllers.Tables;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.EmvTable
{
    /// <summary>
    /// Is responsible for all operations related to table (CAPK and AID tables) controlling.
    /// </summary>
    public interface IPinpadTable
    {
        /// <summary>
        /// Actual Capk collection, containing all capk added to the capk table.
        /// </summary>
        ICollection<CapkEntry> CapkTable { get; }
        /// <summary>
        /// Actual Aid collection, containing all aid added to the aid table.
        /// </summary>
        ICollection<AidEntry> AidTable { get; }
        /// <summary>
        /// Adds one entry, CAPK or AID, into it respective collection.
        /// </summary>
        /// <param name="entry">Entry: could be a <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CAPK</see> or <see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">AID</see> entry.</param>
        /// <exception cref="System.NotImplementedException">Thrown if the parameter is valid, but not recognized.</exception>
        void AddEntry(BaseTableEntry entry);
        /// <summary>
        /// Updates pinpad AID & CAPK tables.
        /// </summary>
        /// <returns>Whether tables were successfuly updated or not.</returns>
        bool UpdatePinpad(bool forceUpdate);
        /// <summary>
        /// Fulfill CapkTable and AidTable with retrieved tables from pinpad.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">If one table entry is either see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">AID</see> nor see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CAPK</see> entry.</exception>
        void RefreshFromPinpad();
        /// <summary>
        /// Cleans all stuff from CAPK & AID collections.
        /// </summary>
        void Clear();
        /// <summary>
        /// Verify if pinpad has tables.
        /// </summary>
        /// <returns>True if pinpad has any table, and false otherwise.</returns>
        bool IsPinpadUpdated();
		/// <summary>
		/// Actual version of the EMV table.
		/// </summary>
		string GetEmvTableVersion();
    }
}

