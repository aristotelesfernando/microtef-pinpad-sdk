using System;
using System.Collections.Generic;
using System.Text;

namespace Pinpad.Sdk.Pinpad_commands
{
	internal class BaseCommand
	{
		// Members
		/// <summary>
		/// Command name according to ABECS.
		/// </summary>
		internal string Name { get; private set; }
		/// <summary>
		/// Whether the comand is blocking or not.
		/// </summary>
		internal bool IsBlocking { get; private set; }
		/// <summary>
		/// If the command has length.
		/// </summary>
		internal bool HasLength { get; private set; }
		/// <summary>
		/// If the command has length, then returns it's length. Otherwise, returns 0.
		/// Corresponds to the sum of it's properties length.
		/// </summary>
		internal int Length { get; private set; }
		/// <summary>
		/// String corresponding to the r
		/// </summary>
		internal string CommandString { get; private set; }
		/// <summary>
		/// All properties embedded in this command.
		/// </summary>
		private ICollection<IProperty> Properties;

		// Proxy
		/// <summary>
		/// Sets command's properties.
		/// </summary>
		/// <param name="name">Command name.</param>
		/// <param name="isBlocking">Whether the command is blocking or not.</param>
		/// <param name="hasLength">If the command has length or not.</param>
		/// <param name="length">Command length.</param>
		private BaseCommand (string name, bool isBlocking, bool hasLength) 
		{
			this.Name = name;
			this.IsBlocking = isBlocking;
			this.HasLength = hasLength;
		}

		// Methods
		/// <summary>
		/// Sends the command already assembled to the pinpad.
		/// </summary>
		/// <returns>Pinpad response.</returns>
		internal CommandResponseStatus Send()
		{
			string commandString = this.AssemblyCommand();

			// TODO: Implementar o envio do comando via porta serial.

			return CommandResponseStatus.ST_OK;
		}
		/// <summary>
		/// Creates the command string based on all properties.
		/// </summary>
		/// <returns>Command string ready to be sent to the pinpad.</returns>
		private string AssemblyCommand()
		{
			StringBuilder builder = new StringBuilder();

			// Iterates through all properties and creates the command string:
			foreach (IProperty currProperty in this.Properties)
			{
				builder.Append(currProperty.Value);
			}

			return builder.ToString();
		}

		/// <summary>
		/// Add a new property to the command.
		/// The order in which the properties are added interfeers in the command string.
		/// The property MUST be added in the same order of the ABECS protocol description.
		/// </summary>
		/// <param name="newProperty">A property to be added to the command.</param>
		internal void Add(IProperty newProperty)
		{
			this.Properties.Add(newProperty);
		}
		/// <summary>
		/// Returns all properties already added to the command.
		/// </summary>
		/// <returns>Command properties.</returns>
		internal ICollection<IProperty> GetAll()
		{
			return this.Properties;
		}

		// Validations
	}
}
