using Pinpad.Sdk.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinpad.Sdk
{
    /// <summary>
    /// PinpadTable manager. PinpadTable is responsible for all operations related to table (CAPK and AID tables) controlling.
    /// </summary>
    public class PinpadTable
    {
        // Constants:
        /// <summary>
        /// Stone acquirer application index.
        /// </summary>
        public const short DOWNLOAD_TABLES_FOR_STONE = 8;
        /// <summary>
        /// Generic acquirer application index.
        /// </summary>
        public const short DOWNLOAD_TABLES_FOR_ALL = 0;
        /// <summary>
        /// EMV table version lewgth required by ABECS protocol.
        /// </summary>
        public const short EMV_TABLE_VERSION_LENGTH = 10;

        // Pinpad providers:
        /// <summary>
        /// Pinpad communication provider.
        /// </summary>
        public PinpadCommunication PinpadCommunication { get; private set; }

        // Table controlling members:
        /// <summary>
        /// Contains all CAPK and AID entry tables.
        /// </summary>
        private List<BaseTable> tableCollection { get; set; }
        /// <summary>
        /// If the tables were modified or not.
        /// </summary>
        private bool tableCollectionModified { get; set; }

        // Members
        /// <summary>
        /// AID entries.
        /// </summary>
        public ICollection<AidTable> AidTable
        {
            get
            {
                lock (this.tableCollection)
                {
                    // Get's all AID entries (as raw):
					return this.tableCollection.OfType<AidTable>().ToList();
                }
            }
        }
        /// <summary>
        /// CAPK entries.
        /// </summary>
        public ICollection<CapkTable> CapkTable
        {
            get
            {
                lock (this.tableCollection)
                {
					// Get's all CAPK entries (as raw):
					return this.tableCollection.OfType<CapkTable>().ToList();
                }
            }
        }
        /// <summary>
        /// Expected EMV table version.
        /// </summary>
        private string expectedTableVersion;
        /// <summary>
        /// Expected EMV table version.
        /// </summary>
        public string ExpectedTableVersion
        {
            get
            {
                return this.expectedTableVersion;
            }
            set
            {
                if (String.IsNullOrEmpty(value) == true || value.Length != 10)
                {
                    throw new InvalidOperationException("TableVersions must be a string 10 characters long.");
                }
                else
                {
                    this.expectedTableVersion = value;
                }
            }
        }

        // Constructor:
        public PinpadTable (PinpadCommunication pinpadCommunication)
        {
			if (pinpadCommunication == null) { throw new ArgumentNullException("pinpad communication cannot be null."); }
            this.PinpadCommunication = pinpadCommunication;
            this.tableCollection = new List<BaseTable>();
        }

        /// <summary>
        /// Adds a pinpad entry, converts it into raw pinpad entry and adds to the collection.
        /// </summary>
        /// <param name="entry">Pinpad AID or CAPK entry.</param>
        /// <returns>If the entry was added or not. In case of false return, verify if the entry is CAPK or AID.</returns>
        public bool AddEntry (BaseTable entry)
        {
			if (entry is AidTable == false && entry is CapkTable == false)
			{
				throw new NotImplementedException("This sort of entry was not implemented yet. The current supported entries are: CAPK and AID.");
			}

            lock (this.tableCollection)
            {
                // Check for a table entry collision:
                BaseTable collision = this.tableCollection.FirstOrDefault((tableEntry) =>
                {
                    return tableEntry.TAB_ID == entry.TAB_ID &&
                    tableEntry.TAB_ACQ.Value == entry.TAB_ACQ.Value &&
                    tableEntry.TAB_RECIDX.Value == entry.TAB_RECIDX.Value;
                });

                // If a collision was detected, remove the old entry:
                if (collision != null)
                {
                    this.RemoveTable(collision);
                }

                // Add raw entry here:
                this.tableCollection.Add(entry);

                // Indicates changes (pinpad is not updated):
                this.tableCollectionModified = true;
            }

            return true;
        }
        /// <summary>
        /// Clear all AID and CAPK tables.
        /// </summary>
        public void Clear ()
        {
            lock (this.tableCollection)
            {
                this.tableCollection.Clear();
                this.tableCollectionModified = true;
            }
        }
        /// <summary>
        /// Get's the current EMV table version. This is used to verify if the pinpad has the last version of tables.
        /// </summary>
        /// <returns>The curret EMV table version.</returns>
        public string GetEmvTableVersion ()
        {
            GtsRequest request = new GtsRequest();

			// Acquirer application ID:
			// flag de acquirer.
			request.GTS_ACQIDX.Value = 00;
			//request.GTS_ACQIDX.Value = (int) StoneIndexCode.Application;

			// Sends GTS request and gets it's response:
			GtsResponse response = this.PinpadCommunication.SendRequestAndReceiveResponse<GtsResponse>(request);

            // Treats response status:
            if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
            {
                return null;
            }

            return response.GTS_TABVER.Value;
        }
        /// <summary>
        /// Verifies if pinpad tables are up to dated.
        /// </summary>
        /// <returns>If the pinpad tables are up to dated or not.</returns>
        public bool IsPinpadUpdated ()
        {
            return (this.tableCollectionModified == false && this.ExpectedTableVersion == this.GetEmvTableVersion());
        }

        public void RefreshFromPinpad ()
        {
            
        }

        /// <summary>
        /// Performs all operations to download tables.
        /// </summary>
        /// <param name="forceUpdate">If should inject tables even with a updated EMV table version.</param>
        /// <returns>If the operation succeed.</returns>
        public bool UpdatePinpad (bool forceUpdate)
        {
            // Retrieves pinpad's EMV table version (on Stone index)
            string expectedTableVersion = this.GetEmvTableVersion();

            if (expectedTableVersion == null) { return false; }

            // Pinpad is already updated and it's not a forced update:
            if (expectedTableVersion == this.expectedTableVersion && forceUpdate == false)
            {
                return true;
            }

            // If pinpad is not updated or is a forced update...
            // Verifies if it has a valid EMV table version:
            if (forceUpdate == true || expectedTableVersion == "0000000000")
            {
                // If it has not a valid version, generetas a new one:
                Random randomGen = new Random();
                expectedTableVersion = randomGen.Next(1, Int32.MaxValue).ToString();

                // Get's a string with legth of exactly 10 characters (required by ABECS protocol):
                expectedTableVersion = expectedTableVersion.PadRight(EMV_TABLE_VERSION_LENGTH, '0');
            }

            // Load tables into the pinpad:
            lock (this.tableCollection)
            {
				this.expectedTableVersion = expectedTableVersion;

                // Sends a command to the pinpad, indicating that a table injection is about to happen:
                if (this.StartLoadingTables() == false) { return false; }

                foreach (BaseTable table in this.tableCollection)
                {
                    // Loads one entry into the pinpad:
                    if (this.LoadTableEntry(table) == false) { return false; }
                }

                // Sends a command to the pinpad, indicating that there's no more tables to be injected:
                if (this.FinishLoadingTables() == false) { return false; }

                // Indicates that all changes has been saved:
                this.tableCollectionModified = false;

                return true;
            }
        }

        // Internally used:
        /// <summary>
        /// Removes a CAPK or AID entry from the collection and sinalizes that change.
        /// </summary>
        /// <param name="table">Table entry to be removed.</param>
        private void RemoveTable (BaseTable table)
        {
            lock (this.tableCollection)
            {
                // Removes the entry:
                this.tableCollection.Remove(table);

                // Indicates change:
                this.tableCollectionModified = true;
            }
        }

        // Loading tables commands:
        /// <summary>
        /// Sends a command to the pinpad, indicating that a table injection is about to happen.
        /// </summary>
        /// <returns>If the operation succeed.</returns>
        private bool StartLoadingTables ()
        {
            TliRequest request = new TliRequest();

            // Specifying how tables will be stored in pinpad memmory:
			// flag de acquirer
            //request.TLI_ACQIDX.Value = (int)StoneIndexCode.Application;
			request.TLI_ACQIDX.Value = DOWNLOAD_TABLES_FOR_ALL;

            // Specifying the version of the tables to be injected:
            request.TLI_TABVER.Value = this.ExpectedTableVersion;

            // Sends the request and gets the response:
            GenericResponse response = this.PinpadCommunication.SendRequestAndReceiveResponse<GenericResponse>(request);

            // Verify the response:
            if (response == null ||
                (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK && response.RSP_STAT.Value != AbecsResponseStatus.ST_TABVERDIF))
            {
                return false;
            }
            else { return true; }
        }
        /// <summary>
        /// Loads one entry into the pinpad.
        /// </summary>
        /// <param name="table">Table (entry) to be loaded.</param>
        /// <returns>If the operation succeed.</returns>
        private bool LoadTableEntry (BaseTable table)
        {
            TlrRequest request = new TlrRequest();
            request.TLR_REC.Value.Add(table);

            return this.PinpadCommunication.SendRequestAndVerifyResponseCode(request);
        }
        /// <summary>
        /// Sends a command to the pinpad, indicating that there's no more tables to be injected.
        /// </summary>
        /// <returns>If the operation succeed.</returns>
        private bool FinishLoadingTables ()
        {
            TleRequest request = new TleRequest();

            return this.PinpadCommunication.SendRequestAndVerifyResponseCode(request);
        }
    }
}
