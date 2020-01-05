using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using EPiServer.PlugIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Website.Microsoft.Tests
{
    [TestClass]
    public class SchedulePluginsHygieneTests
    {
        ///// <summary>
        ///// The following properties describes which ScheduledPlugIn attributes that should be checked.
        ///// </summary>
        private readonly bool Check_ScheduledPlugInGuid = true;
        private readonly bool Check_ScheduledPlugInDisplayName = true;
        private readonly bool Check_ScheduledPlugInDescription = true;
        private readonly bool Check_ScheduledPlugInLanguagePath = false;
        private readonly bool Check_ScheduledPlugInSortIndex = false;
        private readonly bool Check_ScheduledPlugInIntervalLength = true;
        //private readonly bool Check_ScheduledPlugInInitialTime = true;
        private readonly bool Check_ScheduledPlugInHelpFile = false;

        private readonly Assembly _assembly;
        private readonly IEnumerable<Type> _classes;

        public SchedulePluginsHygieneTests()
        {
            _assembly = typeof(Website.Global).Assembly;
            _classes = GetSchedulePlugInClasses();
            Console.Out.WriteLine($"Scanning {_classes.Count()} plugins");
        }

        [TestMethod]
        public void CheckSchedulePlugInGuidAttributeTest()
        {
            if (Check_ScheduledPlugInGuid)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).GUID;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have a GUID attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the GUID attribute.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInForDoubleGuidTest()
        {
            if (Check_ScheduledPlugInGuid)
            {
                var failList = new List<string>();
                var workingList = new NameValueCollection();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).GUID;
                    if (workingList.Get(attributeValue) != null)
                    {
                        failList.Add($"{ctClass.FullName} and {workingList.Get(attributeValue)} using the same GUID ({attributeValue}).");
                    }
                    else
                    {
                        workingList.Add(attributeValue, ctClass.FullName);
                    }
                }

                Assert.IsFalse(failList.Any(), $"{MakeCsvNames(failList)}\nMake sure that all SchedulePlugIns use unique GUIDs.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInDisplayNameAttributeTest()
        {
            if (Check_ScheduledPlugInDisplayName)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).DisplayName;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have a DisplayName attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the DisplayName attribute.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInDescriptionAttributeTest()
        {
            if (Check_ScheduledPlugInDescription)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).Description;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have a Description attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the Description attribute.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInLanguagePathAttributeTest()
        {
            if (Check_ScheduledPlugInLanguagePath)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).LanguagePath;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have a LanguagePath attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the LanguagePath attribute.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInSortIndexAttributeTest()
        {
            if (Check_ScheduledPlugInSortIndex)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    int attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).SortIndex;
                    // Check that the attribute value is not 0 and higher then 0.
                    if (attributeValue <= 0)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have/have a negative SortIndex attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the SortIndex attribute.");
            }
        }

        [TestMethod]
        public void CheckSchedulePlugInIntervalLengthAttributeTest()
        {
            if (Check_ScheduledPlugInIntervalLength)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    int attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).IntervalLength;
                    // Check that the attribute value is not 0 and higher then 0.
                    if (attributeValue <= 0)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have/have a negative IntervalLength attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the IntervalLength attribute.");
            }
        }

        //[Test]
        //public void CheckSchedulePlugInInitialTimeAttributeTest()
        //{
        //    if (Check_ScheduledPlugInInitialTime)
        //    {
        //        var failList = new List<string>();

        //        foreach (Type ctClass in _classes)
        //        {
        //            string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).InitialTime;
        //            // Check that the attribute value is not 0 and higher then 0.
        //            if (attributeValue <= 0)
        //            {
        //                failList.Add($"\n{ctClass.FullName}");
        //            }
        //        }

        //        Assert.False(failList.Any(), $"The following SchedulePlugIns does not have/have a negative IntervalLength attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the IntervalLength attribute.");
        //    }
        //}

        [TestMethod]
        public void CheckSchedulePlugInHelpFileAttributeTest()
        {
            if (Check_ScheduledPlugInHelpFile)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _classes)
                {
                    string attributeValue = ((ScheduledPlugInAttribute)ctClass.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true)[0]).HelpFile;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.IsFalse(failList.Any(), $"The following SchedulePlugIns does not have HelpFile attribute.{MakeCsvNames(failList)}\nGo to the SchedulePlugIn and set a correct value in the HelpFile attribute.");
            }
        }

        /// <summary>
        /// Get all classes that use the attribute ContentTypeAttribute that exist in the specified namespace.
        /// </summary>
        private IEnumerable<Type> GetSchedulePlugInClasses()
        {
            var classes = from t in _assembly.GetTypes()
                          where t.IsClass && t.GetCustomAttributes(typeof(ScheduledPlugInAttribute), true).Any()
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
