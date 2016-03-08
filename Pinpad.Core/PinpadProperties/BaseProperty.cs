using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Properties 
{
	/// <summary>
	/// Base Controller for Read/Write property classes
	/// </summary>
	public abstract class BaseProperty 
	{
		// Members
		/// <summary>
		/// Property collection, contains all properties.
		/// </summary>
		private List<IProperty> propertyCollection { get; set; }
		/// <summary>
		/// Contains all property from the current active region.
		/// </summary>
		private List<RegionProperty> activeRegionCollection { get; set; }
		/// <summary>
		/// Command string of this command.
		/// </summary>
		public virtual string CommandString
		{
			get
			{
				StringBuilder commandStringBuilder = new StringBuilder();
				foreach (IProperty property in this.propertyCollection)
				{
					string propertyString = property.GetString();
					commandStringBuilder.Append(propertyString);

					if (this.IsPropertyFinal(property) == true) { break; }
				}

				return commandStringBuilder.ToString();
			}
			set 
			{
				StringReader stringReader = new StringReader(value);

				foreach (IProperty property in this.propertyCollection)
				{
					try
					{
						property.ParseString(stringReader);
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
		public BaseProperty() 
		{
			this.propertyCollection = new List<IProperty>();
			this.activeRegionCollection = new List<RegionProperty>();
		}

		// Methods
		/// <summary>
		/// Does the property makes the other properties be ignored?
		/// </summary>
		/// <param name="property">Property</param>
		/// <returns>boolean</returns>
		protected virtual bool IsPropertyFinal (IProperty property)
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
			this.propertyCollection.Add(property);
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
			this.activeRegionCollection.Add(regionProperty);

			// Add to property collection:
			this.propertyCollection.Add(regionProperty);
		}
		/// <summary>
		/// Closes the last region so it will only count the length until now.
		/// </summary>
		protected void EndLastRegion()
		{
			if (this.activeRegionCollection.Count == 0)
			{
				throw new InvalidOperationException("There are no regions currently active.");
			}

			this.activeRegionCollection.RemoveAt(activeRegionCollection.Count - 1);
		}
		/// <summary>
		/// Add a property to the current active region.
		/// </summary>
		/// <param name="property">Property to be added.</param>
		private void AddPropertyToActiveRegions(IProperty property)
		{
			foreach (RegionProperty activeRegion in this.activeRegionCollection)
			{
				activeRegion.AddProperty(property);
			}
		}
	}
}
