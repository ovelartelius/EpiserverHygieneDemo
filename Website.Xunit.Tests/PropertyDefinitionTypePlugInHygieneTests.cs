using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Website.Xunit.Tests
{
	public class PropertyDefinitionTypePlugInHygieneTests
	{
		private readonly bool Check_PropertyDefinitionTypePlugInGuid = true;
		private readonly bool Check_PropertyDefinitionTypePlugInDescription = true;
		private readonly bool Check_PropertyDefinitionTypePlugInDisplayName = true;
		private readonly bool Check_PropertyDefinitionTypePlugInLanguagePath = true;
		private readonly bool Check_PropertyDefinitionTypePlugInSortIndex = false;
		private readonly Assembly _assembly;
		private readonly IEnumerable<Type> _classes;

		public PropertyDefinitionTypePlugInHygieneTests()
		{
			_assembly = typeof(Website.Global).Assembly;
			_classes = GetPropertyDefinitionTypePlugInClasses();
			Console.Out.WriteLine($"Scanning {_classes.Count()} plugins");
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInGuidAttributeTest()
		{
			if (Check_PropertyDefinitionTypePlugInGuid)
			{
				var failList = new List<string>();

				foreach (Type ctClass in _classes)
				{
					string attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).GUID;
					// Check that the attribute value is not empty and contains more than 2 chars.
					if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
					{
						failList.Add($"\n{ctClass.FullName}");
					}
				}

				Assert.False(failList.Any(),
					$"The following PropertyDefinitionTypePlugIns does not have a GUID attribute.{MakeCsvNames(failList)}\nGo to the PropertyDefinitionTypePlugIn and set a correct value in the GUID attribute.");
			}
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInForDoubleGuidTest()
		{
			if (Check_PropertyDefinitionTypePlugInGuid)
			{
				var failList = new List<string>();
				var workingList = new NameValueCollection();

				foreach (Type ctClass in _classes)
				{
					string attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).GUID;
					if (workingList.Get(attributeValue) != null)
					{
						failList.Add(
							$"{ctClass.FullName} and {workingList.Get(attributeValue)} using the same GUID ({attributeValue}).");
					}
					else
					{
						workingList.Add(attributeValue, ctClass.FullName);
					}
				}

				Assert.False(failList.Any(),
					$"{MakeCsvNames(failList)}\nMake sure that all PropertyDefinitionTypePlugIn use unique GUIDs.");
			}
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInDisplayNameAttributeTest()
		{
			if (Check_PropertyDefinitionTypePlugInDisplayName)
			{
				var failList = new List<string>();

				foreach (Type ctClass in _classes)
				{
					string attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).DisplayName;
					// Check that the attribute value is not empty and contains more than 2 chars.
					if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
					{
						failList.Add($"\n{ctClass.FullName}");
					}
				}

				Assert.False(failList.Any(),
					$"The following PropertyDefinitionTypePlugIn does not have a DisplayName attribute.{MakeCsvNames(failList)}\nGo to the PropertyDefinitionTypePlugIn and set a correct value in the DisplayName attribute.");
			}
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInDescriptionAttributeTest()
		{
			if (Check_PropertyDefinitionTypePlugInDescription)
			{
				var failList = new List<string>();

				foreach (Type ctClass in _classes)
				{
					string attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).Description;
					// Check that the attribute value is not empty and contains more than 2 chars.
					if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
					{
						failList.Add($"\n{ctClass.FullName}");
					}
				}

				Assert.False(failList.Any(),
					$"The following PropertyDefinitionTypePlugIns does not have a Description attribute.{MakeCsvNames(failList)}\nGo to the PropertyDefinitionTypePlugIn and set a correct value in the Description attribute.");
			}
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInLanguagePathAttributeTest()
		{
			if (Check_PropertyDefinitionTypePlugInLanguagePath)
			{
				var failList = new List<string>();

				foreach (Type ctClass in _classes)
				{
					string attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).LanguagePath;
					// Check that the attribute value is not empty and contains more than 2 chars.
					if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
					{
						failList.Add($"\n{ctClass.FullName}");
					}
				}

				Assert.False(failList.Any(),
					$"The following PropertyDefinitionTypePlugIns does not have a LanguagePath attribute.{MakeCsvNames(failList)}\nGo to the PropertyDefinitionTypePlugIn and set a correct value in the LanguagePath attribute.");
			}
		}

		[Fact]
		public void CheckPropertyDefinitionTypePlugInSortIndexAttributeTest()
		{
			if (Check_PropertyDefinitionTypePlugInSortIndex)
			{
				var failList = new List<string>();

				foreach (Type ctClass in _classes)
				{
					int attributeValue =
						((PropertyDefinitionTypePlugInAttribute)ctClass.GetCustomAttributes(
							typeof(PropertyDefinitionTypePlugInAttribute), true)[0]).SortIndex;
					// Check that the attribute value is not 0 and higher then 0.
					if (attributeValue <= 0)
					{
						failList.Add($"\n{ctClass.FullName}");
					}
				}

				Assert.False(failList.Any(),
					$"The following PropertyDefinitionTypePlugIns does not have/have a negative SortIndex attribute.{MakeCsvNames(failList)}\nGo to the PropertyDefinitionTypePlugIn and set a correct value in the SortIndex attribute.");
			}
		}

		/// <summary>
		/// Get all classes that use the attribute PropertyDefinitionTypePlugInAttribute that exist in the specified namespace.
		/// </summary>
		private IEnumerable<Type> GetPropertyDefinitionTypePlugInClasses()
		{
			var classes = from t in _assembly.GetTypes()
						  where t.IsClass && t.GetCustomAttributes(typeof(PropertyDefinitionTypePlugInAttribute), true).Any()
						  select t;
			return classes;
		}

		/// <summary>
		/// Takes a IEnumerable of string and make CSV string of all the values.
		/// </summary>
		private string MakeCsvNames(IEnumerable<string> listOfNames)
		{
			var stringBuilder = new StringBuilder();

			var iterator = 1;
			foreach (string name in listOfNames)
			{
				if (iterator > 1)
				{
					stringBuilder.Append(", ");
				}

				stringBuilder.Append(name);
				iterator++;
			}

			return stringBuilder.ToString();
		}
	}
}
