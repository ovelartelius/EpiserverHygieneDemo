# EPiServer 
EPiServer Alloy project with unittests that test the hygiene of attributes set on content types and properties.  
  
## Features
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
1. Clone the project from GitHub. https://github.com/ovelartelius/EpiserverHygieneDemo/  
2. In your unittest project/structure. Create a new class with the name “ContentTypesHygieneTest.cs”.  
3. Copy the code (from your clone or directly from GitHub) and paste into the new created class.  
4. Change the namespace in the copied code to fit your location in your project.  

5. Set the correct namespace to your website assembly. You simply check the website project settings and use “[Assembly name].Global”.  

6. Set which tests you want to run on your project. The properties that are set to true will be run. We strongly recommend that you run them all. Always!  
  
Now you should be ready to compile and run the tests.  
