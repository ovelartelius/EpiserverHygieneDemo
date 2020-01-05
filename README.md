# EPiServer 
EPiServer Alloy project with unittests that test the hygiene of attributes set on content types and properties.  

## ContentTypes hygiene tests
[Content types documentation](ContentTypesHygieneTests.md)

## Schedule tasks hygiene tests
[Schedule tasks documentation](ScheduleTasksHygieneTests.md)

## Features
![Settings](/Documentation/images/UnittestSettings.PNG)
### Content types  
Check that developers have set a "Guid" attribute.  
Check that developers have not set the same Guid value on different content types.  
Check that developers have set a "DisplayName" attribute.  
Check that developers have set a "Description" attribute.  
Check that developers have set a "GroupName" attribute.  
Check that developers have set a "ImageUrl" attribute.  
Check that developers have set a "Order" attribute.  
Check that developers have not set the same Order value on different content types.  
  
### Content type properties
Check that developers have set a "Name" attribute.  
Check that developers have set a "Description" attribute.  
Check that developers have set a "ShortName" attribute.  
Check that developers have set a "GroupName" attribute.  
Check that developers have set a "Order" attribute.  
Check that developers have not set the same Order value on different properties in the same content type.  
Check that developers have set a "Prompt" attribute on string properties.  

## How to get started
1. Clone the project from GitHub.  
2. In your unittest project/structure. Create a new class with the name “ContentTypesHygieneTest.cs”.  
3. Copy the code (from your clone or directly from GitHub) and paste into the new created class.  
4. Change the namespace in the copied code to fit your location in your project.  
![Change namespace](/Documentation/images/UnittestNamespace.PNG)
5. Set the correct namespace to your website assembly. You simply check the website project settings and use “[Assembly name].Global”.  
![Change assembly](/Documentation/images/ConstructorAssemblySpecification.PNG)
![Change assembly](/Documentation/images/ConstructorAssemblyProjectAssemblyName.PNG)
6. Set which tests you want to run on your project. The properties that are set to true will be run. We strongly recommend that you run them all. Always!  
  
Now you should be ready to compile and run the tests.  

## How to set tests on/off  
Some projects use special custom functionality to generate translation for names, descriptions or other attributes. So, I added some logic that could turn on and off tests in the class. The test is implemented on a template project that will be used when new projects are created. Then it is nice that each project, can decide them self, what kind of tests that should want to run.  
In the beginning of the “ContentTypesHygieneTests” class there are a couple of properties that you can set to true/false. The list below describes in more detail what the test does.  
The following properties can be turned on/off:  
![Settings](/Documentation/images/UnittestSettings.PNG)

## What does the tests do?
### Check_ContentTypeGuid
If true: Checks that all content types has the “GUID” attribute set and that every ContentType GUID is unique.
![Check ContentType GUID attribute](/Documentation/images/ContentTypeGuidAttribute.PNG)  
![Check ContentType for duplicate GUIDs](/Documentation/images/ContentTypeDuplicateGuidAttribute.PNG)  
If any content types missing the attribute, the test will fail, and report which content types that does not contain the “GUID” attribute.  
![Missing GUID attribute - test failed](/Documentation/images/CheckContentTypesGuidTestFailed.PNG)
If there is more then one ContentType that use the same GUID, the test will fail.  
![Duplicate GUID attribute - test failed](/Documentation/images/CheckContentTypesDuplicateGuidTestFailed.PNG)
  
### Check_ContentTypeDisplayName
If true: Checks that all content types has the “DisplayName” attribute set.  
![Check ContentType GUID](/Documentation/images/ContentTypeDisplayNameAttribute.PNG)  
If any content types missing the attribute or are less then 3 characters, the test will fail, and report which content types that does not contain the “DisplayName” attribute.  
![Check ContentType GUID](/Documentation/images/CheckContentTypesDisplayNameTestFailed.PNG)
  
### Check_ContentTypeDescription
If true: Check if all content types has the “Description” attribute set.  
![Check ContentType GUID](/Documentation/images/ContentTypeDescriptionAttribute.PNG)  
If any content types missing the attribute or are less then 3 characters, the test will fail, and report which content types that does not contain the “Description” attribute.  
![Check ContentType GUID](/Documentation/images/CheckContentTypesDisplayNameTestFailed.PNG)
 
### Check_ContentTypeGroupName
If true: Check if all content types has the “GroupName” attribute set.  
 ![Check ContentType GUID](/Documentation/images/ContentTypeGroupNameAttribute.PNG)  
If any content types missing the attribute, the test will fail, and report which content types that does not contain the “GroupName” attribute.  
![Check ContentType GUID](/Documentation/images/CheckContentTypesGroupNameTestFailed.PNG)  
 
### Check_ContentTypeImageUrl
If true: Check if all content types has the “ImageUrl” attribute set.  
![Check ContentType GUID](/Documentation/images/ContentTypeImageUrlAttribute.PNG)
If any content types missing the attribute, the test will fail, and report which content types that does not contain the “ImageUrl” attribute.  
![Check ContentType GUID](/Documentation/images/CheckContentTypesImageUrlTestFailed.PNG)  
   
### Check_ContentTypeOrder
If true: Check if all content types has the “Order” attribute set.  
![Check ContentType GUID](/Documentation/images/ContentTypeOrderAttribute.PNG)  
If any content types missing the attribute, the test will fail, and report which content types that does not contain the “Order” attribute.  
![Check ContentType GUID](/Documentation/images/CheckContentTypesOrderTestFailed.PNG)  
   
### Check_ContentTypeDuplicateOrder
If true: Check if all content types has unique order index.    
If any content types have the same order index, the test will fail, and report which content types that use the same order index number.  
 
### Check_PropertyName
If true: Check if all properties has the “Name” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyNameAttribute.PNG)  
If any properties missing the attribute or are less then 2 characters, the test will fail, and report which [ContentType].[Property] that does not contain the “Name” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyNameTestFailed.PNG)  
 
### Check_PropertiesDescription
If true: Check if all properties has the “Description” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyDescriptionAttribute.PNG)  
If any properties missing the attribute or are less then 2 characters, the test will fail, and report which [ContentType].[Property] that does not contain the “Description” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyDescriptionTestFailed.PNG)  
   
### Check_PropertiesOrder
If true: Check if all properties has the “Order” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyOrderAttribute.PNG)  
If any properties missing the attribute, the test will fail, and report which [ContentType].[Property] that does not contain the “Order” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyOrderTestFailed.PNG)  
 
### Check_PropertiesShortName
If true: Check if all properties has the “ShortName” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyShortNameAttribute.PNG)  
If any properties missing the attribute or are less then 2 characters, the test will fail, and report which [ContentType].[Property] that does not contain the “ShortName” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyShortNameTestFailed.PNG)  
   
### Check_PropertiesDuplicateOrder
If true: Check if all properties with in a content type has unique order index.  
![Check ContentType GUID](/Documentation/images/PropertyDuplicateOrderAttribute.PNG)  
If any properties has the same order index, the test will fail, and report which [ContentType].[Property] that use the same order index number.  
![Check ContentType GUID](/Documentation/images/CheckPropertyDuplicateOrderTestFailed.PNG)  
   
### Check_PropertiesGroupName
If true: Check if all properties has the “GroupName” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyGroupNameAttribute.PNG)   
If any properties missing the attribute or are less then 2 characters, the test will fail, and report which [ContentType].[Property] that does not contain the “GroupName” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyGroupNameTestFailed.PNG)  
  
### Check_PropertiesStringPrompt
If true: Check if all string properties has the “Prompt” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyPromptAttribute.PNG)   
If any string properties missing the attribute or are less then 2 characters, the test will fail, and report which [ContentType].[Property] that does not contain the “Prompt” attribute.  
![Check ContentType GUID](/Documentation/images/CheckPropertyStringPromptTestFailed.PNG)
 
