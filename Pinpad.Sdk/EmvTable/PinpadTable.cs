using Pinpad.Core.Commands;
using Pinpad.Core.Pinpad;
using Pinpad.Core.Tables;
using Pinpad.Core.TypeCode;
using Pinpad.Sdk.Connection;
using Pinpad.Sdk.EmvTable.Mapper;
using Pinpad.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

/* WARNING!
 * 
 * MUST BE REFACTORED.
 * 
 */

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
		/// <summary>
		/// Pinpad facade with all pinpad functionalities.
		/// </summary>
		private PinpadCommunication communication;
		/// <summary>
		/// Pinpad facade with all pinpad functionalities.
		/// </summary>
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
		/// <summary>
		/// Expected TableVersion in the Pinpad, used to load tables and verify tables at the Pinpad
		/// </summary>
		public string ExpectedTableVersion
		{
			get
			{
				return this._expectedTableVersion;
			}
			set
			{
				if (String.IsNullOrEmpty(value) == true || value.Length != 10)
				{
					throw new InvalidOperationException("TableVersions must be a string 10 characters long.");
				}
				else
				{
					this._expectedTableVersion = value;
				}
			}
		}
		/// <summary>
		/// Expected TableVersion in the Pinpad, used to load tables and verify tables at the Pinpad
		/// </summary>
		private string _expectedTableVersion { get; set; }
		/// <summary>
		/// Table collection containing pinpad AID and CAPK entries.
		/// </summary>
		private List<BaseTable> _tableCollection { get; set; }
		/// <summary>
		/// MEDIFIED table collection containing pinpad AID and CAPK entries.
		/// </summary>
		private bool _tableCollectionModified { get; set; }
		/// <summary>
		/// Gets the tables currently in the buffer
		/// </summary>
		public BaseTable[] TableCollection
		{
			get
			{
				lock (this._tableCollection)
				{
					return _tableCollection.ToArray();
				}
			}
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
					throw new ArgumentNullException("PinpadTable was not intantiated yet - in this case, the parameter PinpadConnection should not be null.");
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
			this.communication = new PinpadCommunication(pinpadConnection.PlatformPinpadConnection);
			this.AidTable = new Collection<AidEntry>();
			this.CapkTable = new Collection<CapkEntry>();
			this._tableCollection = new List<BaseTable>();
		}

		// Public methods
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
			string expectedTableVersion = this.GetEmvTableVersion();

			// Verifies if it has a valid EMV table version:
			if (forcePinpadUpdate == true || expectedTableVersion == "0000000000" || expectedTableVersion == null)
			{
				// If it has not a valid version, generetas a new one:
				Random randomGen = new Random();
				expectedTableVersion = randomGen.Next(1, Int32.MaxValue).ToString();
				expectedTableVersion = expectedTableVersion.PadRight(10, '0');
			}

			// Load new tables of the specific version on pinpad memmory:
			this.LoadTables(expectedTableVersion);

			return true;
		}
		/// <summary>
		/// Cleans all stuff from CAPK & AID collections.
		/// </summary>
		public void Clear()
		{
			this.CapkTable.Clear();
			this.AidTable.Clear();

			lock (this._tableCollection)
			{
				this._tableCollection.Clear();
				this._tableCollectionModified = true;
			}
		}
		/// <summary>
		/// Actual version of the EMV table.
		/// </summary>
		public string GetEmvTableVersion()
		{
			GtsRequest request = new GtsRequest();

			// Acquirer application ID:
			request.GTS_ACQIDX.Value = 8;

			// Sends GTS request and gets it's response:
			GtsResponse response = this.communication.SendRequestAndReceiveResponse<GtsResponse>(request);

			// Treats response status:
			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				return null;
			}

			return response.GTS_TABVER.Value; 
		}
		/// <summary>
		/// Fulfill CapkTable and AidTable with retrieved tables from pinpad.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">If one table entry is either see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">AID</see> nor see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CAPK</see> entry.</exception>
		public void RefreshFromPinpad()
		{
			// Creating a list with current pinpad table.
			List<BaseTable> tableList = new List<BaseTable>(this.TableCollection);

			// Clear all actual table content.
			this.AidTable.Clear();
			this.CapkTable.Clear();

			// Iterating through the list retrieved and setting each item into it respective collection.
			foreach (BaseTable currentTableEntry in tableList)
			{
				if (currentTableEntry is EmvAidTable)
				{
					// Maps legacy aid into AidEntry.
					AidEntry mappedAid = AidMapper.MapToAidEntry(currentTableEntry as EmvAidTable);
					
					// Adds to collection.
					this.AidTable.Add(mappedAid);
				}
				else if (currentTableEntry is CapkTable)
				{
					// Maps legacy capk into CapkEntry.
					CapkEntry mappedCapk = CapkMapper.MapToPinpadCapk(currentTableEntry as CapkTable);

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
		/// Returns whether the pinpad has tables or not.
		/// </summary>
		/// <returns>False if there is not tables into the pinpad (that is, it cannot transact); and true otherwise. </returns>
		public bool IsPinpadUpdated()
		{
			if (this.TableCollection.Count() <= 0)
			{
				return false;
			}

			return true;
		}

		// Private methods
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
				EmvAidTable mappedAid = AidMapper.MapToLegacyAid(aid);

				// Adding legacy aid stuff and verifying if it was successfuly updated.
				if (this.AddTable(mappedAid) == false) { return false; }
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
				CapkTable mappedCapk = CapkMapper.MapToLegacyCapk(capk);

				// Adding legacy capk stuff and verifying if it was successfuly updated.
				if (this.AddTable(mappedCapk) == false) { return false; }
			}

			return true;
		}
		/// <summary>
		/// Loads the table buffer to the Pinpad
		/// </summary>
		/// <returns>true if successfully loaded</returns>
		private bool LoadTables(string expectedTableVersion)
		{
			if (expectedTableVersion != null)
			{
				this.ExpectedTableVersion = expectedTableVersion;
			}

			if (this.ExpectedTableVersion == null) 
			{
				throw new InvalidOperationException("ExpectedTableVersion was not set.");
			}

			lock (this._tableCollection)
			{
				if (this.StartLoadingTables() == false)
				{
					return false;
				}

				foreach (BaseTable table in this._tableCollection)
				{
					if (this.LoadTableEntry(table) == false)
					{
						return false;
					}
				}

				if (this.FinishLoadingTables() == false)
				{
					return false;
				}

				this._tableCollectionModified = false;
				return true;
			}
		}
		/// <summary>
		/// Noticing Pinpad that table download has began
		/// </summary>
		/// <returns></returns>
		private bool StartLoadingTables()
		{
			TliRequest request = new TliRequest();
			request.TLI_ACQIDX.Value = 8; //0 indica que todas os indices serão atualizados
			request.TLI_TABVER.Value = this.ExpectedTableVersion;

			GenericResponse response = this.communication.SendRequestAndReceiveResponse<GenericResponse>(request);
			if (response == null ||
				(response.RSP_STAT.Value != AbecsResponseStatus.ST_OK && response.RSP_STAT.Value != AbecsResponseStatus.ST_TABVERDIF))
			{
				return false;
			}
			else { return true; }
		}
		/// <summary>
		/// Loads one CAPK or AID entry into the Pinpad
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		private bool LoadTableEntry(BaseTable table)
		{
			TlrRequest request = new TlrRequest();
			request.TLR_REC.Value.Add(table);

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Noticing Pinpad that table download has finished
		/// </summary>
		/// <returns></returns>
		private bool FinishLoadingTables()
		{
			TleRequest request = new TleRequest();

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Adds a table to the buffer
		/// </summary>
		/// <param name="table">BaseTable</param>
		/// <returns>true if successfully added</returns>
		private bool AddTable(BaseTable table)
		{
			try
			{
				string temp = table.CommandString;
			}
			catch (Exception)
			{
				return false;
			}

			lock (this._tableCollection)
			{
				//Check for a table entry collision
				BaseTable collision = this._tableCollection.FirstOrDefault((tableEntry) =>
				{
					return tableEntry.TAB_ID == table.TAB_ID &&
					tableEntry.TAB_ACQ.Value == table.TAB_ACQ.Value &&
					tableEntry.TAB_RECIDX.Value == table.TAB_RECIDX.Value;
				});

				//Remove the old entry
				if (collision != null)
				{
					this.RemoveTable(collision);
				}

				this._tableCollection.Add(table);
				this._tableCollectionModified = true;
				return true;
			}
		}
		/// <summary>
		/// Remove a table from Pinpad
		/// </summary>
		/// <param name="table">The table to be removed.</param>
		private void RemoveTable(BaseTable table)
		{
			lock (this._tableCollection)
			{
				this._tableCollection.Remove(table);
				this._tableCollectionModified = true;
			}
		}
	}
}
