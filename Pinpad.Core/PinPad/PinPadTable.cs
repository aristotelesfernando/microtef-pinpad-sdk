using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using PinPadSDK.Exceptions;

namespace PinPadSDK.PinPad 
{
    /// <summary>
    /// PinPad table adapter
    /// </summary>
    public class PinPadTable 
	{
        /// <summary>
        /// PinPad being used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }
        /// <summary>
        /// Expected TableVersion in the PinPad, used to load tables and verify tables at the PinPad
        /// </summary>
        public string ExpectedTableVersion {
            get {
                return this._expectedTableVersion;
            }
            set {
                if (String.IsNullOrEmpty(value) == true || value.Length != 10) {
                    throw new InvalidOperationException("TableVersions must be a string 10 characters long.");
                }
                else {
                    this._expectedTableVersion = value;
                }
            }
        }
		private string _expectedTableVersion { get; set; }
		/// <summary>
		/// Are the tables in the buffer currently loaded in the PinPad
		/// </summary>
		public bool AreTablesLoaded
		{
			get
			{
				return (this._tableCollectionModified == false &&
				this.ExpectedTableVersion == this.GetTableEmvVersion());
			}
		}
		
		private List<BaseTable> _tableCollection { get; set; }
		private bool _tableCollectionModified { get; set; }

		// Tables
		/// <summary>
        /// Gets the tables currently in the buffer
        /// </summary>
        public BaseTable [] TableCollection {
            get {
                lock (this._tableCollection) {
                    return _tableCollection.ToArray( );
                }
            }
        }
        /// <summary>
        /// Gets the Capk tables currently in the buffer
        /// </summary>
        public CapkTable [] CapkTableCollection {
            get {
                BaseTable[] baseTableCollection;
                lock (this._tableCollection) {
                    baseTableCollection= _tableCollection.Where((table) => {
                        return table is CapkTable;
                    }).ToArray();
                }
                CapkTable[] capkTableCollection = baseTableCollection.Cast<CapkTable>().ToArray();
                return capkTableCollection;
            }
        }
        /// <summary>
        /// Gets the Emv Aid tables currently in the buffer
        /// </summary>
        public EmvAidTable [] EmvAidTableCollection {
            get {
                BaseTable[] baseTableCollection;
                lock (this._tableCollection) {
                    baseTableCollection = _tableCollection.Where((table) => {
                        return table is EmvAidTable;
                    }).ToArray();
                }
                EmvAidTable[] emvAidTableCollection = baseTableCollection.Cast<EmvAidTable>().ToArray();
                return emvAidTableCollection;
            }
        }

		// Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad"></param>
        public PinPadTable(PinPadFacade pinPad) {
            this.PinPad = pinPad;
            this._tableCollection = new List<BaseTable>( );
        }

		// Methods
        /// <summary>
        /// Adds a table to the buffer
        /// </summary>
        /// <param name="table">BaseTable</param>
        /// <returns>true if successfully added</returns>
        public bool AddTable(BaseTable table) {
            try {
                string temp = table.CommandString;
            }
            catch (Exception) {
                return false;
            }

            lock (this._tableCollection) {
                //Check for a table entry collision
                BaseTable collision = this._tableCollection.FirstOrDefault((tableEntry) => {
                    return tableEntry.TAB_ID == table.TAB_ID &&
                    tableEntry.TAB_ACQ.Value == table.TAB_ACQ.Value &&
                    tableEntry.TAB_RECIDX.Value == table.TAB_RECIDX.Value;
                });

                //Remove the old entry
                if (collision != null) {
                    this.RemoveTable(collision);
                }

                this._tableCollection.Add(table);
                this._tableCollectionModified = true;
                return true;
            }
        }
        /// <summary>
        /// Clears all tables from the buffer
        /// </summary>
        public void ClearTables( ) {
            lock (this._tableCollection) {
                this._tableCollection.Clear( );
                this._tableCollectionModified = true;
            }
        }
        /// <summary>
        /// Loads the table buffer to the PinPad
        /// </summary>
        /// <returns>true if successfully loaded</returns>
        public bool LoadTables(string expectedTableVersion = null) 
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
		/// Gets the TableVersion currently in the Pinpad.
		/// </summary>
		public string GetTableEmvVersion()
		{
			GtsRequest request = new GtsRequest();

			// Acquirer application ID:
			request.GTS_ACQIDX.Value = 8;

			// Sends GTS request and gets it's response:
			GtsResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<GtsResponse>(request);
			
			// Treats response status:
			if (response == null || response.RSP_STAT.Value != ResponseStatus.ST_OK)
			{
				return null;
			}
			
			return response.GTS_TABVER.Value; 
		}

		// Private methods
		private void RemoveTable(BaseTable table)
		{
			lock (this._tableCollection)
			{
				this._tableCollection.Remove(table);
				this._tableCollectionModified = true;
			}
		}
        private bool StartLoadingTables() {
            TliRequest request = new TliRequest( );
            request.TLI_ACQIDX.Value = 8; //0 indica que todas os indices serão atualizados
            request.TLI_TABVER.Value = this.ExpectedTableVersion;

            GenericResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<GenericResponse>(request);
            if (response == null ||
                (response.RSP_STAT.Value != ResponseStatus.ST_OK && response.RSP_STAT.Value != ResponseStatus.ST_TABVERDIF)) 
			{
                return false;
            }
            else { return true; }
        }
        private bool LoadTableEntry(BaseTable table) {
            TlrRequest request = new TlrRequest( );
            request.TLR_REC.Value.Add(table);

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }
        private bool FinishLoadingTables() {
            TleRequest request = new TleRequest( );

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }
    }
}