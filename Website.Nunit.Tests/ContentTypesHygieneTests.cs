using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Core;
using NUnit.Framework;

namespace Website.Nunit.Tests
{
    /// <summary>
    /// Scan all content types in the web project and make sure that attributes is set correctly.
    /// There are private properties in the class that can be changed so that it tests the code in the way you decide in your project.
    /// </summary>
    [TestFixture]
    public class ContentTypesHygieneTests
    {
        /// <summary>
        /// The following properties describes which content types attributes that should be checked.
        /// </summary>
        private readonly bool Check_ContentTypeGuid = true;
        private readonly bool Check_ContentTypeDisplayName = true;
        private readonly bool Check_ContentTypeDescription = true;
        private readonly bool Check_ContentTypeGroupName = true;
        private readonly bool Check_ContentTypeUrlPath = true;

        /// <summary>
        /// The following properties describes which property attributes that should be checked in  content types.
        /// </summary>
        private readonly bool Check_PropertyDisplayName = true;
        private readonly bool Check_PropertyDescription = true;
        private readonly bool Check_PropertyOrder = true;
        private readonly bool Check_PropertyShortName = true;
        private readonly bool Check_PropertyDuplicateOrder = true;
        private readonly bool Check_PropertyGroupName = true;
        private readonly bool Check_PropertyStringPrompt = true;

        private readonly Assembly _assembly;
        private readonly IEnumerable<Type> _contentTypeClasses;

        public ContentTypesHygieneTests()
        {
            _assembly = typeof(Website.Global).Assembly;
            _contentTypeClasses = GetContentTypeClasses();
            Console.Out.WriteLine($"Scanning {_contentTypeClasses.Count()} content types");
        }

        /// <summary>
        /// Check all ContentType classes, that attribute GUID is set.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForGuidTest()
        {
            if (Check_ContentTypeGuid)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _contentTypeClasses)
                {
                    string attributeValue = ((ContentTypeAttribute)ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true)[0]).GUID;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.False(failList.Any(), $"The following content types does not have a GUID attribute.{MakeCsvNames(failList)}\nGo to the ContentTypes and set a correct value in the GUID attribute.");
            }
        }

        /// <summary>
        /// Check all ContentType classes that attribute GUID is set.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForDisplayNameTest()
        {
            if (Check_ContentTypeDisplayName)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _contentTypeClasses)
                {
                    string attributeValue = ((ContentTypeAttribute)ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true)[0]).DisplayName;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.False(failList.Any(), $"The following content types does not contain a correct DisplayName.{MakeCsvNames(failList)}\nGo to the ContentTypes and set a correct value in the DisplayName attribute.");
            }
        }

        /// <summary>
        /// Test that all ContentType classes has specified Description.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForDescriptionTest()
        {
            if (Check_ContentTypeDescription)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _contentTypeClasses)
                {
                    string attributeValue = ((ContentTypeAttribute)ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true)[0]).Description;
                    // Check that the attribute value is not empty and contains more than 2 chars.
                    if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                    {
                        failList.Add($"\n{ctClass.FullName}");
                    }
                }

                Assert.False(failList.Any(), $"The following content types does not contain a correct Description.{MakeCsvNames(failList)}\nGo to the ContentTypes and set a correct value in the Description attribute.");
            }
        }

        /// <summary>
        /// Test that all ContentType classes has specified GroupName.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForGroupNameTest()
        {
            if (Check_ContentTypeGroupName)
            {
                var failList = new List<string>();

                foreach (Type ctClass in _contentTypeClasses)
                {
                    if (ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true).Length > 0)
                    {
                        bool isMediaData = (ctClass.BaseType == typeof(MediaData));
                        bool mediaDescriptorAttributeExist = ctClass.GetCustomAttributes(typeof(MediaDescriptorAttribute), true).Any();
                        // If the class contains MediaDescriptorAttribute it is a Media type so we don't need to check UrlPath.
                        if (!mediaDescriptorAttributeExist && !isMediaData)
                        {
                            string attributeValue = ((ContentTypeAttribute)ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true)[0]).GroupName;
                            // Check that the attribute value is not empty and contains more than 2 chars.
                            if (string.IsNullOrWhiteSpace(attributeValue) || attributeValue.Length < 3)
                            {
                                failList.Add($"\n{ctClass.FullName}");
                            }
                        }
                    }


                }

                Assert.False(failList.Any(), $"The following content types does not contain a correct GroupName.{MakeCsvNames(failList)}\nGo to the ContentTypes and set a correct value in the GroupName attribute.");
            }
        }

        /// <summary>
        /// Test that all ContentType classes has a UrlPath reference.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForUrlPathTest()
        {
            if (Check_ContentTypeUrlPath)
            {
                var failList = new List<string>();

                foreach (Type type in _assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(ContentTypeAttribute), true).Length > 0)
                    {
                        bool isMediaData = (type.BaseType == typeof(MediaData));
                        bool mediaDescriptorAttributeExist = type.GetCustomAttributes(typeof(MediaDescriptorAttribute), true).Any();
                        // If the class contains MediaDescriptorAttribute it is a Media type so we don't need to check UrlPath.
                        if (!mediaDescriptorAttributeExist && !isMediaData)
                        {
                            bool imageUrlAttributeExist = type.GetCustomAttributes(typeof(ImageUrlAttribute), true).Any();

                            if (imageUrlAttributeExist)
                            {
                                string imageUrlPath = ((ImageUrlAttribute)type.GetCustomAttributes(typeof(ImageUrlAttribute), true)[0]).Path;
                                if (string.IsNullOrWhiteSpace(imageUrlPath))
                                {
                                    failList.Add($"\n{type.FullName}");
                                }
                            }
                            else
                            {
                                failList.Add($"\n{type.FullName}");
                            }
                        }
                    }
                }

                Assert.False(failList.Any(), $"The following content types does not contain a correct UrlPath.{MakeCsvNames(failList)}\nGo to the ContentTypes and set a correct value in the ImageUrlAttribute(UrlPath) attribute.");
            }
        }

        /// <summary>
        /// Go through all ContentType classes and check the properties after attributes that SHOULD be set/specified.
        /// </summary>
        [Test]
        public void CheckAllContentTypePropertiesAttributesTest()
        {
            var failList = new List<string>();

            // Get all class in the namespace that use the attribute ContentTypeAttribute.
            foreach (Type ctClass in _assembly.GetTypes())
            {
                var typeProp = GetEPiServerPropertiesFromType(ctClass);
                var contentTypeName = ctClass.Name;
                var properties = typeProp.Value;
                foreach (var ePiPropertyInfo in properties)
                {
                    // Check that all properties have a DisplayName
                    if (Check_PropertyDisplayName && (string.IsNullOrWhiteSpace(ePiPropertyInfo.UiName) || ePiPropertyInfo.UiName.Length < 2))
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a Name");
                    }

                    // Check that all properties have a Description
                    if (Check_PropertyDescription && (string.IsNullOrWhiteSpace(ePiPropertyInfo.Description) || ePiPropertyInfo.Description.Length < 5))
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a Description");
                    }

                    // Check that all properties have a Order
                    if (Check_PropertyOrder && ePiPropertyInfo.Order == 0)
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a Order");
                    }

                    // Check that all properties have a ShortName
                    if (Check_PropertyShortName && (string.IsNullOrWhiteSpace(ePiPropertyInfo.ShortName) || ePiPropertyInfo.ShortName.Length < 2))
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a ShortName");
                    }

                    // Check that all properties have a GroupName
                    if (Check_PropertyGroupName && (string.IsNullOrWhiteSpace(ePiPropertyInfo.GroupName) || ePiPropertyInfo.GroupName.Length < 2))
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a GroupName");
                    }

                    if (Check_PropertyStringPrompt && ePiPropertyInfo.Type.ToLower() == "string" && (string.IsNullOrWhiteSpace(ePiPropertyInfo.Prompt) || ePiPropertyInfo.Prompt.Length < 2))
                    {
                        failList.Add($"\n{contentTypeName}.{ePiPropertyInfo.PropertyName} property does not have a Prompt");
                    }
                }
            }

            if (failList.Any())
            {
                Assert.False(failList.Any(), $"Found properties without recommended attributes.{MakeCsvNames(failList)}. \nGo to the ContentTypes/Properties and set a correct value in the attribute(s).");
            }
        }

        /// <summary>
        /// Check all ContentType classes that GUIDs are not used twice.
        /// </summary>
        [Test]
        public void CheckAllContentTypesForDoubleGuidTest()
        {
            if (Check_ContentTypeGuid)
            {
                var failList = new List<string>();
                var workingList = new NameValueCollection();

                foreach (Type ctClass in _contentTypeClasses)
                {
                    string attributeValue = ((ContentTypeAttribute)ctClass.GetCustomAttributes(typeof(ContentTypeAttribute), true)[0]).GUID;
                    if (workingList.Get(attributeValue) != null)
                    {
                        failList.Add($"{ctClass.FullName} and {workingList.Get(attributeValue)} using the same GUID ({attributeValue}).");
                    }
                    else
                    {
                        workingList.Add(attributeValue, ctClass.FullName);
                    }
                }

                Assert.False(failList.Any(), $"{MakeCsvNames(failList)}\nMake sure that all content types use unique GUIDs.");
            }
        }

        /// <summary>
        /// Checks the properties Display=>order attributes.
        /// </summary>
        [Test]
        public void CheckPropertyOrder()
        {
            if (Check_PropertyDuplicateOrder)
            {
                var failList = new List<string>();

                foreach (Type type in _contentTypeClasses)
                {
                    var usedOrders = new Dictionary<int, string>();

                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        var displayAttribute = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                        if (displayAttribute == null)
                        {
                            continue;
                        }

                        int order = displayAttribute.GetOrder() ?? 0;
                        if (order <= 0)
                        {
                            failList.Add($" \n{type.FullName}.{property.Name}: Property don't have a order index set.");
                        }
                        else if (usedOrders.ContainsKey(order))
                        {
                            //{contentTypeName}.{ePiPropertyInfo.PropertyName}
                            failList.Add($"\n{type.FullName}.{usedOrders[order]}|{property.Name}: Properties use the same order index ({order}).");
                        }
                        else
                        {
                            usedOrders.Add(displayAttribute.Order, property.Name);
                        }
                    }
                }

                if (failList.Any())
                {
                    failList.Insert(0, $"{failList.Count} issues found");
                }
                Assert.False(failList.Any(), MakeCsvNames(failList));
            }

        }

        /// <summary>
        /// Get all EPi properties from a ContentType class and return information about attribute values etc.
        /// </summary>
        private KeyValuePair<string, IEnumerable<EPiPropertyInfo>> GetEPiServerPropertiesFromType(Type t)
        {
            KeyValuePair<string, IEnumerable<EPiPropertyInfo>> pairs = default(KeyValuePair<string, IEnumerable<EPiPropertyInfo>>);

            try
            {
                var properties = from p in t.GetProperties()
                                 where p.GetCustomAttributes(typeof(DisplayAttribute), true).Any()
                                 select new EPiPropertyInfo
                                 {
                                     PropertyName = p.Name,
                                     Type = p.PropertyType.Name,
                                     UiName = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).Name,
                                     Description = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).Description ?? string.Empty,
                                     GroupName = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).GroupName ?? string.Empty,
                                     Order = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).GetOrder() ?? 0,
                                     ShortName = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).ShortName ?? string.Empty,
                                     Prompt = ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute))).Prompt ?? string.Empty,
                                 };
                pairs = new KeyValuePair<string, IEnumerable<EPiPropertyInfo>>(t.Name, properties);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return pairs;
        }

        /// <summary>
        /// Get all classes that use the attribute ContentTypeAttribute that exist in the specified namespace.
        /// </summary>
        private IEnumerable<Type> GetContentTypeClasses()
        {
            var classes = from t in _assembly.GetTypes()
                          where t.IsClass && t.GetCustomAttributes(typeof(ContentTypeAttribute), true).Any()
                          select t;
            return classes;
        }

        public class EPiPropertyInfo
        {
            public string PropertyName { get; set; }
            public string Type { get; set; }
            public string UiName { get; set; }
            public string Description { get; set; }
            public string GroupName { get; set; }
            public int Order { get; set; }
            public string ShortName { get; set; }
            public string Prompt { get; set; }
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
