using Pinpad.Sdk.Connection;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.EmvTable.Mapper;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.PinPad;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LegacyPinpadAid = PinPadSDK.Controllers.Tables.EmvAidTable;
using LegacyPinpadCapk = PinPadSDK.Controllers.Tables.CapkTable;
using System.Diagnostics;

namespace Pinpad.Sdk.EmvTable
{
    /// <summary>
    /// PinpadTable manager. PinpadTable is responsible for all operations related to table (CAPK and AID tables) controlling.
    /// </summary>
    public class PinpadTable : IPinpadTable
    {
		// Singleton
		private static PinpadTable _pinpadTable;

        // Members
        private PinPadTable legacyPinpadTable;
        private PinPadFacade legacyPinpadFacade;
        private BasePinpadConnection pinpadConnection;
        
        
        /// <summary>
        /// Capk collection, containing all capk added to the capk table.
        /// </summary>
        public ICollection<CapkEntry> CapkTable
        {
            get;
            private set;
        }
        /// <summary>
        /// Aid collection, containing all aid added to the aid table.
        /// </summary>
        public ICollection<AidEntry> AidTable
        {
            get;
            private set;
        }

        // Constructor
		public static PinpadTable GetInstance(BasePinpadConnection pinpadConnection = null)
		{
			if (pinpadConnection != null)
			{
				if (_pinpadTable == null)
				{
					_pinpadTable = new PinpadTable(pinpadConnection);
				}
			}
			else
			{
				if (_pinpadTable == null)
				{
					return null;
				}
			}

			return _pinpadTable;
		}
        /// <summary>
        /// Constructor, sets all mandatory data. 
        /// </summary>
        /// <param name="pinpadConnection">Physical environment in which the application will communicate with the pinpad (for example, serial port, bluetooth, usb, etc).</param>
        /// <exception cref="System.ArgumentNullException">Thrown if pinpadConnection is null.</exception>
        private PinpadTable(BasePinpadConnection pinpadConnection)
        {
            if (pinpadConnection == null)
            {
                throw new ArgumentNullException("pinpadConnection");
            }

            this.pinpadConnection = pinpadConnection;
            this.legacyPinpadFacade = new PinPadFacade(this.pinpadConnection.LegacyPinpadConnection);
            this.legacyPinpadTable = new PinPadTable(this.legacyPinpadFacade);

            this.AidTable = new Collection<AidEntry>();
            this.CapkTable = new Collection<CapkEntry>();
        }

        // Methods
        /// <summary>
        /// Adds one entry, CAPK or AID, into it respective collection.
        /// </summary>
        /// <param name="entry">Entry: could be a <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CAPK</see> or <see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">AID</see> entry.</param>
        /// <exception cref="System.NotImplementedException">Thrown if the parameter is valid, but not recognized.</exception>
        public void AddEntry(BaseTableEntry entry)
        {
            if (entry is AidEntry)
            {
                this.AidTable.Add((AidEntry)entry);
            }
            else if (entry is CapkEntry)
            {
                this.CapkTable.Add((CapkEntry)entry);
            }
            else
            {
                throw new NotImplementedException("Type of entry was not implemented. Please create a new table entry type or change entry parameter.");
            }
        }
        /// <summary>
        /// Updates pinpad AID & CAPK tables.
        /// </summary>
        /// <returns>Whether tables were successfuly updated or not.</returns>
        public bool UpdatePinpad(bool forcePinpadUpdate = false)
        {
            // Adding this class aid table to legacy aid table.
            if (UpdateLegacyPinpadAidTable() == false) { return false; }

            // Adding this class capk table to legacy capk table.
            if (UpdateLegacyPinpadCapkTable() == false) { return false; }

			// Retrieves pinpad's EMV table version (on Stone index)
			string expectedTableVersion = this.legacyPinpadTable.GetTableEmvVersion();

			// Verifies if it has a valid EMV table version:
			if (forcePinpadUpdate == true || expectedTableVersion == "0000000000" || expectedTableVersion == null)
			{
				// If it has not a valid version, generetas a new one:
				Random randomGen = new Random();
				expectedTableVersion = randomGen.Next(1, Int32.MaxValue).ToString();
				expectedTableVersion = expectedTableVersion.PadRight(10, '0');
			}

			// Load new tables of the specific version on pinpad memmory:
			this.legacyPinpadTable.LoadTables(expectedTableVersion);

            return true;
        }
        /// <summary>
        /// Updates pinpad AID table.
        /// </summary>
        /// <returns>Whether AID table was successfuly updated or not.</returns>
        private bool UpdateLegacyPinpadAidTable()
        {
            // Iterating through this class AidTable.
            foreach (AidEntry aid in this.AidTable)
            {
                // Translating AidEntry into legacy AidEntry (Pinpad.Core).
                LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(aid);

                // Adding legacy aid stuff and verifying if it was successfuly updated.
                if (this.legacyPinpadTable.AddTable(mappedAid) == false) { return false; }
            }

            return true;
        }
        /// <summary>
        /// Updates pinpad CAPK table.
        /// </summary>
        /// <returns>Whether CAPK table was successfuly updated or not.</returns>
        private bool UpdateLegacyPinpadCapkTable()
        {
            // Iterating through this class CapkTable.
            foreach(CapkEntry capk in this.CapkTable)
            {
                // Translating CapkEntry into legacy CapkEntry (Pinpad.Core).
                LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(capk);

                // Adding legacy capk stuff and verifying if it was successfuly updated.
                if (this.legacyPinpadTable.AddTable(mappedCapk) == false) { return false; }
            }

            return true;
        }
        /// <summary>
        /// Fulfill CapkTable and AidTable with retrieved tables from pinpad.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">If one table entry is either see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">AID</see> nor see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CAPK</see> entry.</exception>
        public void RefreshFromPinpad()
        {
            // Creating a list with current pinpad table.
            List<BaseTable> tableList = new List<BaseTable>(this.legacyPinpadTable.TableCollection);

            // Clear all actual table content.
            this.AidTable.Clear();
            this.CapkTable.Clear();

            // Iterating through the list retrieved and setting each item into it respective collection.
            foreach (BaseTable currentTableEntry in tableList)
            {
                if (currentTableEntry is LegacyPinpadAid)
                {
                    // Maps legacy aid into AidEntry.
                    AidEntry mappedAid = AidMapper.MapToAidEntry(currentTableEntry as LegacyPinpadAid);
                    
                    // Adds to collection.
                    this.AidTable.Add(mappedAid);
                }
                else if (currentTableEntry is LegacyPinpadCapk)
                {
                    // Maps legacy capk into CapkEntry.
                    CapkEntry mappedCapk = CapkMapper.MapToPinpadCapk(currentTableEntry as LegacyPinpadCapk);

                    // Adds to collection.
                    this.CapkTable.Add(mappedCapk);
                }
                else
                {
                    // If one table entry is either aid nor capk, throw exception.
                    throw new InvalidOperationException("Could not retrieve pinpad table, because an unknown table entry was received.");
                }
            }

			Debug.WriteLine("Tables were refreshed. <AIDs {0}, CAPKs {1}>", this.AidTable.Count, this.CapkTable.Count);
        }
        /// <summary>
        /// Cleans all stuff from CAPK & AID collections.
        /// </summary>
        public void Clear()
        {
            this.CapkTable.Clear();
            this.AidTable.Clear();
            this.legacyPinpadTable.ClearTables();
        }
        /// <summary>
        /// Returns whether the pinpad has tables or not.
        /// </summary>
        /// <returns>False if there is not tables into the pinpad (that is, it cannot transact); and true otherwise. </returns>
        public bool IsPinpadUpdated()
        {
            if (this.legacyPinpadTable.TableCollection.Count() <= 0)
            {
                return false;
            }

            return true;
        }

		/// <summary>
		/// Actual version of the EMV table.
		/// </summary>
		public string GetEmvTableVersion()
		{
			return this.legacyPinpadTable.GetTableEmvVersion();
		}
    }
}
