[<< Back](README.md)

# PropertyDefinitionTypePlugInHygiene tests

## Features
![Settings](/Documentation/images/PropertyDefinitionTypePlugIn_settings.jpg)
### Types  
Check that developers have set a "Guid" attribute.  
Check that developers have not set the same Guid value on different content types.  
Check that developers have set a "Description" attribute.  
Check that developers have set a "DisplayName" attribute.  
Check that developers have set a "LanguagePath" attribute.  
Check that developers have set a "SortIndex" attribute.  
  
## How to get started
1. Clone the project from GitHub.  
2. In your unittest project/structure. Create a new class with the name PropertyDefinitionTypePlugInHygieneTests.cs”.  
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
In the beginning of the “PropertyDefinitionTypePlugInHygieneTests” class there are a couple of properties that you can set to true/false. The list below describes in more detail what the test does.  
The following properties can be turned on/off:  
![Settings](/Documentation/images/PropertyDefinitionTypePlugIn_settings.jpg)

## What does the tests do?
### Check_PropertyDefinitionTypePlugInGuid
If true: Checks that all property definition types has the “GUID” attribute set and that every GUID is unique.
![Check ContentType GUID attribute](/Documentation/images/PropertyDefinitionTypePlugIn_attribute_guid.jpg)  
If any property definition types missing the attribute, the test will fail, and report which property definition types that does not contain the “GUID” attribute.  

### Check_PropertyDefinitionTypePlugInDescription
If true: Check if all property definition types has the “Description” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyDefinitionTypePlugIn_attribute_description.jpg)  
If any property definition types missing the attribute or are less then 3 characters, the test will fail, and report which property definition types that does not contain the “Description” attribute.  

### Check_PropertyDefinitionTypePlugInDisplayName
If true: Checks that all property definition types has the “DisplayName” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyDefinitionTypePlugIn_attribute_displayname.jpg)  
If any property definition types missing the attribute or are less then 3 characters, the test will fail, and report which property definition types that does not contain the “DisplayName” attribute.  
  
### Check_PropertyDefinitionTypePlugInLanguagePath
If true: Check if all property definition types has the LanguagePath attribute set.  
 ![Check ContentType GUID](/Documentation/images/PropertyDefinitionTypePlugIn_attribute_languagepath.jpg)  
If any property definition types missing the attribute, the test will fail, and report which property definition types that does not contain the “LanguagePath” attribute.  
 
### Check_PropertyDefinitionTypePlugInSortIndex
If true: Check if all property definition types has the “SortIndex” attribute set.  
![Check ContentType GUID](/Documentation/images/PropertyDefinitionTypePlugIn_attribute_sortindex.jpg)  
If any property definition types missing the attribute, the test will fail, and report which property definition types that does not contain the “SortIndex” attribute.  
   
### Check_PropertyDefinitionTypePlugInDuplicateOrder
If true: Check if all property definition types has unique order index.    
If any property definition types have the same order index, the test will fail, and report which content types that use the same order index number.  
 