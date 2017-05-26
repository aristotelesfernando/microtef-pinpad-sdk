using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    // TODO: Doc
    public abstract class BaseProperty
    {
        /// <summary>
        /// Property collection, contains all properties.
        /// </summary>
        private List<IProperty> PropertyCollection { get; set; }
        /// <summary>
        /// Contains all property from the current active region.
        /// </summary>
        private List<RegionProperty> ActiveRegionCollection { get; set; }
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
            set
            {
                this.CommandTrack = Encoding.UTF8.GetBytes(value);
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
                StringReader stringReader = new StringReader(Encoding.UTF8.GetString(value, 0, value.Length));

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

        // TODO: Doc
        public BaseProperty()
        {
            this.PropertyCollection = new List<IProperty>();
            this.ActiveRegionCollection = new List<RegionProperty>();
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
