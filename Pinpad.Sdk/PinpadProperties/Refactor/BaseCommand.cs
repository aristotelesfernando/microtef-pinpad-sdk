using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

using DeprecatedProperties = Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.PinpadProperties.Refactor
{
    public abstract class BaseCommand : ICommand
    {
        // Members
        /// <summary>
		/// Name of this command
		/// </summary>
		public abstract string CommandName { get; }
        /// <summary>
        /// Command Id
        /// </summary>
        protected Refactor.FixedLengthProperty<string> CMD_ID { get; set; }
        /// <summary>
        /// Context of the command.
        /// </summary>
        public IContext Context { get; set; }

        /// <summary>
        /// Property collection, contains all properties.
        /// </summary>
        private List<Refactor.IProperty> PropertyCollection { get; set; }
        /// <summary>
        /// Contains all property from the current active region.
        /// </summary>
        private List<Refactor.RegionProperty> ActiveRegionCollection { get; set; }
        /// <summary>
        /// Command string of this command.
        /// </summary>
        public virtual string CommandString
        {
            get
            {
                StringBuilder commandStringBuilder = new StringBuilder();

                foreach (IProperty property in this.PropertyCollection)
                {
                    string propertyString;
                    
                    if (property is ITextProperty)
                    {
                        // Read as text:
                        propertyString = (property as ITextProperty).GetString();
                    }
                    else
                    {
                        // Read as binary:
                        byte[] propertyBytes = (property as IBinaryProperty).GetBytes();
                        propertyString = Encoding.UTF8.GetString(propertyBytes, 0, propertyBytes.Length);
                    }

                    commandStringBuilder.Append(propertyString);

                    if (this.IsPropertyFinal(property) == true) { break; }
                }

                return commandStringBuilder.ToString();
            }
        }
        // TODO: Doc
        public virtual byte[] CommandTrack
        {
            get
            {
                List<byte> commandTrack = new List<byte>();

                foreach (IProperty property in this.PropertyCollection)
                {
                    byte[] propertyBytes;

                    if (property is ITextProperty)
                    {
                        // Read as text:
                        propertyBytes = Encoding.UTF8.GetBytes((property as ITextProperty).GetString());
                    }
                    else
                    {
                        // Read as binary:
                        propertyBytes = (property as IBinaryProperty).GetBytes();
                    }

                    commandTrack.AddRange(propertyBytes);

                    if (this.IsPropertyFinal(property) == true) { break; }
                }

                return commandTrack.ToArray();
            }
            set
            {
                DeprecatedProperties.StringReader stringReader = new DeprecatedProperties
                    .StringReader(Encoding.UTF8.GetString(value, 0, value.Length));

                foreach (IProperty property in this.PropertyCollection)
                {
                    try
                    {
                        if (property is ITextProperty)
                        {
                            (property as ITextProperty).ParseString(stringReader);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new PropertyParseException(stringReader.Value, property.Name, stringReader.LastReadString, "Failed to parse \"" + stringReader.LastReadString + "\"", ex);
                    }

                    if (this.IsPropertyFinal(property) == true) { break; }
                }
            }
        }

        // Constructor
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public BaseCommand(IContext context = default(IContext))
        {
            if (context == null)
            {
                context = new AbecsContext();
            }

            this.Context = context;

            this.CMD_ID = new FixedLengthProperty<string>("CMD_ID", this.Context.CommandNameLength, false,
                this.CommandNameStringFormatter, this.CommandNameStringParser, null, this.CommandName);

            this.PropertyCollection = new List<IProperty>();
            this.ActiveRegionCollection = new List<RegionProperty>();

            this.AddProperty(this.CMD_ID);
        }

        // Methods
        /// <summary>
		/// StringFormatter to limit the response to the expected command
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string, ignored</param>
		/// <returns>Value of the property as string</returns>
		protected virtual string CommandNameStringFormatter(string obj, int length)
        {
            string value = DeprecatedProperties.DefaultStringFormatter.StringStringFormatter(obj, length);

            if (CommandName.Equals(value) == false)
            {
                throw new CommandNameMismatchException("\"" + value + "\" not supported for " + CommandName);
            }
            return value;
        }
        /// <summary>
        /// StringParser to limit the response to the expected command
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>string</returns>
        protected virtual string CommandNameStringParser(DeprecatedProperties.StringReader reader, int length)
        {
            string value = DeprecatedProperties.DefaultStringParser.StringStringParser(reader, length);

            if (CommandName.Equals(value) == false)
            {
                throw new CommandNameMismatchException("\"" + value + "\" not supported for " + CommandName);
            }
            return value;
        }

        /// <summary>
        /// Does the property makes the other properties be ignored?
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>boolean</returns>
        protected virtual bool IsPropertyFinal(IProperty property)
        {
            return false;
        }
        /// <summary>
        /// Adds a property to the collection.
        /// </summary>
        /// <param name="property">Property to be added.</param>
        protected void AddProperty(IProperty property)
        {
            this.AddPropertyToActiveRegions(property);
            this.PropertyCollection.Add(property);
        }
        /// <summary>
        /// Adds a region property to the collection and activates it to register next properties.
        /// </summary>
        /// <param name="regionProperty">Region property to be added.</param>
        protected void StartRegion(RegionProperty regionProperty)
        {
            // Add the property to the current active region:
            this.AddPropertyToActiveRegions(regionProperty);

            // Add to the current region collection:
            this.ActiveRegionCollection.Add(regionProperty);

            // Add to property collection:
            this.PropertyCollection.Add(regionProperty);
        }
        /// <summary>
        /// Closes the last region so it will only count the length until now.
        /// </summary>
        protected void EndLastRegion()
        {
            if (this.ActiveRegionCollection.Count == 0)
            {
                throw new InvalidOperationException("There are no regions currently active.");
            }

            this.ActiveRegionCollection.RemoveAt(ActiveRegionCollection.Count - 1);
        }
        /// <summary>
        /// Add a property to the current active region.
        /// </summary>
        /// <param name="property">Property to be added.</param>
        private void AddPropertyToActiveRegions(IProperty property)
        {
            if (this.ActiveRegionCollection == null) { return; }

            foreach (RegionProperty activeRegion in this.ActiveRegionCollection)
            {
                activeRegion.AddProperty(property);
            }
        }
    }
}
