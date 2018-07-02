<a href="https://www.servirglobal.net//">
    <img src="https://www.servirglobal.net/Portals/0/Images/Servir-logo.png" alt="SERVIR Global"
         title="SERVIR Global" align="right" />
</a>


Land Use / Land Cover:
Inventory for Africa
============================================


## Introduction:
The primary goal of this document is to highlight the user interface for the [Land Use/Land Cover for Africa Inventory](https://servirglobal.net/MapResources/LULC_Africa/#)  online application. Additional implementation detail is provided where available.


## Background: 
In the context of the AfriGEOSS Working Group on Land Cover for Africa (WGLCA), the Ecological Monitoring Center (CSE) of Senegal, SERVIR Science Coordination Office and SERVIR West Africa joined efforts to develop a dynamic Land Cover Inventory for Africa.

This inventory is a collection of information regarding the multiple efforts on land cover and land use products for the continent of Africa. The input data were originally collected through the members of the AfriGEOSS WGLCA Executive Board. The purpose of this inventory is to have a single point of reference and complete understanding of the available land cover products in the continent, to raise awareness and promote use of the data that exists, and avoid duplication of efforts.

Note - This application does not contain the actual data, nor does it provide direct access to the datasets. Point of contact information is included for each inventory entry that can be used by users to follow up for additional information about the respective dataset.


## Application Overview:
Using a map-based interface segmented by country, users can view existing and submit new data inventory entries.  New inventory entries, categorizing the referenced datasets as Completed, In Progress, or Planned, are entered and submitted for acceptance and must be reviewed and either approved or rejected by application administrators.  Once approved, the new entry will be included in the list of available inventories for the corresponding country.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/1_Overview.png" title="Overview" alt="Overview"></a>
The application operates in three modes:
1.	Anonymous User - This occurs when a user accesses the online application, but is not logged into the application.  Effectively, this is a read only view of the country inventories.
2.	Standard/Logged In User - In addition to viewing existing entries, this mode allows users to submit new entries for consideration for the available list of inventories.
3.	Admin User - In addition to viewing existing and submitting new entries, this allows an admin to review submitted entries for acceptance or rejection, as well as loading and deleting bulk entries.
These three modes are covered in more detail below.


## Anonymous User Experience:
Any user with an internet browser can access the application.  When using the application without logging in, users will only have the capability for read-only viewing approved inventory entries – by clicking on a particular country on the map.  

### Review Inventory by Country:
When the user clicks on a particular country on the map, a pane appears on the right with information about the approved inventories for the selected country.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/2_Inventories.png" title="Inventories" alt="Inventories"></a>
As mentioned above, the inventories are broken down by category (Completed, In Progress, or Planned) and are listed in the corresponding tab within the pane.  Clicking on a different tab with show the list of inventories that corresponds to that category.  Clicking on the title of an inventory entry will display a “View Data” information dialog containing further details about the inventory entry.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/3_ViewData.png" title="ViewData" alt="ViewData"></a>
Note - The numbered and colored bubbles on the map provide a visual indication of how many inventories entries exist associated with each category.

Users have the ability to alternate between a satellite imagery basemap and a topographic basemap by clicking on the basemap icon in the upper left of the map.

Access to the “About” and the “Report a Problem” pages are also available to users who are not logged in.  When reporting a problem, the user will need to manually specify a valid contact email address as the system will have no record of who is currently using the application.  However, when users are logged in, the email address will be populated automatically from the associated user account. (More on user accounts below.)


## Standard/Logged In User Experience:
The application uses Google accounts to control user access.  Users should establish their own Google account completely separate from this application.  If a user desires to contribute inventory submissions to be considered for inclusion into this application, they must log into the application with their Google (gmail) account.  (Ultimately, anyone with a Google account can log into the application and contribute inventory submissions.)

### Sign In:
Once a Google account has been established, the user can click on the “Sign in” button in the upper right and log in to the application using their Google account credentials.  If the user is already signed into their Google account before accessing this application, clicking the “Sign in” button will allow the application to automatically detect that the user is signed in, and will proceed with signing in without prompting the user.  Otherwise, if the user is not already logged into their Google account, clicking “Sign in” will pop up a dialog that can be used to sign into their Google account.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/4_SignIn.png" title="SignIn" alt="SignIn"></a>
Once logged in, the user will see their logged in account info appear in the upper right, and on initial login, a message is displayed to the user indicating how they can contribute new submissions.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/5_HowToAddData.png" title="HowToAddData" alt="HowToAddData"></a>

### Add Inventory Submission:
In addition to the anonymous user experience, standard/logged in users can click on a country and submit a new inventory entry for consideration.  After clicking on a particular country on the map, a pane appears on the right with information about the approved inventories for the selected country, broken down on tabs corresponding to the category of the inventory (Completed, In Progress, or Planned).  At this point, since the user is logged in, a “+” appears to the right of the country name on the pane.  Clicking on the “+” sign opens a dialog that allows the user to populate information about the inventory entry that they would like to submit for that country.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/6_AddData.png" title="AddData" alt="AddData"></a>
The user will populate the data fields in the “Add data” dialog and click the “Submit data!” button.  (If a value is not specified for the “Status” field, the value will default to “Completed”.)  After submission, the entry is recorded into a queue that can be seen by administrator users, which will have the ability to approve or reject the submissions.  If the submission is approved, it will ultimately show up in the list of available inventories for the specified country.  The user’s email address is captured in the submission so that if there are problems with the submissions, the admin can contact the user with details.

Note – New entries are assigned a unique identifier (UID), which is used as the primary key for the entry as long as it exists in the application.  This UID is used by the application when updating or deleting entries.

Note - The completedArray.js data file contains the submitted inventory data and is stored on the server in the following location: C:\inetpub\wwwroot\LULC_Map\js

### Report a Problem:
Just like in the case of an anonymous user, a logged in user can report a problem with the application.  The reported issues can be seen by administrator users.  In this case, since the user is logged in, the user does not have to key in their email address as it is already known from the account login.


## Admin User Experience:
Admin users, once logged in, have access to more functionality for managing the inventory entries.  In addition to the functionality of standard logged in users, admin users have the option to directly edit, delete, or unapprove previously approved inventory entries.  Also, admin users have access to more menu options such as Add Admins, View Requests (for approval or discard), View Problems, and Bulk Data processing – which are all covered in more detail below.

### Edit, Delete, or Unapprove Previously Approved Inventory Entries:
Clicking on a country and further clicking on the title of an existing (previously approved) entry opens the View Data dialog to see the details of the inventory.  When logged in as an admin user, the dialog will contain buttons for Editing, Deleting, and Unapproving the current entry.  These buttons are shown circled in the screenshot below.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/7_ViewDataForEdit.png" title="ViewDataForEdit" alt="ViewDataForEdit"></a>
Here, clicking the “Edit” button allows the admin user to directly edit the fields in the entry.  Once the desired fields have been edited, click “Update” on the bottom of the form to save the edits. 

Clicking the “Delete” button will delete the entry from the application.

Clicking “Unapprove” simply marks the record as such, and removes it from the list of approved entries.  The record will again show up in the “View Requests” list where it can either be reapproved or discarded completely.

### Add Admins:
Only admin users have the ability to add other user’s accounts as administrators.  This is done via the “Add Admins” menu option.  Clicking the “Add Admins” menu opens a dialog that allows the current administrator to enter email addresses of corresponding user accounts so that those users will become administrators.  This interface also displays a list of email addresses currently configured as administrators.  To add a new administrator, type in the email address corresponding to the Google account of the desired user and click “Add Admins(s)”.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/8_AddAdmin.png" title="AddAdmin" alt="AddAdmin"></a>
Currently, there is no admin interface (nor associated requirement) for removing admin users.  So if it is required to remove a user from the list of administrators, this must be done manually by editing the .json configuration file and deleting the email address entry of the desired admin user.

Note - The users.js configuration file contains the admin users is stored on the server in the following location: C:\inetpub\wwwroot\LULC_Map\js  Care should be taken when modifying the configuration file manually as changes to the format can impact the use of the application.

### View Requests:
Clicking the “View Requests” menu item opens a dialog that allows the user to filter and view the pending inventory submissions.  These can be filtered by country and/or by submitting user.  Clicking on an inventory submission row highlights the row, and displays two buttons, “Approve” and “Discard”.  If the entry is approved, the entry will be marked as such, and will show up as an available inventory for future users of the application.  If the entry is to be discarded, the admin user should notify the respective contact and provide details as to why the submission was rejected.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/9_ViewRequests.png" title="ViewRequests" alt="ViewRequests"></a>

### View Problems:
Clicking the “View Problems” menu item opens a dialog that allows the user to filter the list of reported problems by status (Open or Closed).  By default, the rows displayed show the summary information about the issue.  Clicking on a row in the table will display more information about the reported problem.  Once the problem is addressed by an administrator, the issue can be marked as “closed” using the toggle button, and the administrator can choose to notify the user that the issue has been resolved, if desired.  Administrators can toggle the status between Opened to Closed to represent the desired status.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/10_ViewProblems.png" title="ViewProblems" alt="ViewProblems"></a>

### Bulk Data? (Import, Download, and Update/Delete):
As an admin user, there may be times when it is necessary to manipulate data in bulk.  For this case, there are 3 different actions that can be taken when dealing with bulk data:
1.)	Import
2.)	Download
3.)	Update/Delete  

#### Import:
Import allows a list of inventory dataset entries to be imported into the application all at once.  This action requires using a template .xlsx/.csv file for storing the data that needs to be imported.  The current template file can be downloaded from within the Import tab of the “Bulk Data?” interface.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/11_Bulk_Import.png" title="BulkImport" alt="BulkImport"></a>
After downloading and saving the template .xlsx file locally, simply populate the columns in the file (such as Title, Country Name, Year of Data Coverage, Responsible Organization, Point of Contact, etc.).  There should be one row for each entry that is desired.  Save the template file on your local machine with a .csv extension.  Then, using the “Choose File” button on the Import tab, browse to the source .csv file that contains the desired entries to be imported.  Once identified, click on Upload Files to perform the import.

In a typical workflow where a user has a large number of entries to import, an administrator might download and save an empty template .csv file and send it to someone wanting to submit many entries.  After receiving the populated .csv file from the source provider, and reviewing the input file, the administrator could then use this interface to bulk import the inventory entries into the application for the user.  This workflow scenario would have to be coordinated before-hand between the end user and an administrator user.

Note – Be sure to download and use the template from this screen - saved as a .csv for use with the Import functionality.  This is important because there is no UID field in this template .csv file - as this value will be established during the import.

#### Download:
The Download tab contains functionality to simply generate and download a file containing the inventory entries associated with the particular country that is selected by the user.  The user will select a country from the list of countries and click the “Generate CSV” button.  The format of the file will be .csv.  Once the file is generated, it will start the auto download procedure commonly used in a browser file download.

Note – When using Download, the records exported will contain their corresponding UID field.  This field is used as the key when updating or deleting via the Bulk Data interface, so please be sure to use the proper “Download” version of the .csv template when performing bulk updates or deletes.  The Import functionality uses a slightly different version of the template!

#### Update/Delete:
Using the same template (.csv) file that is used in the Download functionality, the idea is to either update existing entries in the system that are represented in the source .csv file, or delete them.  Only include entries in the source .csv file that are meant to be edited or delete – depending on the desired operation.  As noted above, the UID is used as the key when updating or deleting entries.  It should also be noted that the following fields are not available for update via the Bulk Data Update function:  uid, status, country id, or country name.

Note – Again, be sure to use the .csv template from the “Download” functionality for use with bulk Update or Delete.

Once the source .csv file is chosen, use the corresponding button to accomplish the Update or Delete of the representative entries in the source file.

<a href=""><img src="https://www.servirglobal.net/Portals/0/Images/LULC_Inventory/12_Bulk_UpdateDelete.png" title="BulkUpdateDelete" alt="BulkUpdateDelete"></a>





