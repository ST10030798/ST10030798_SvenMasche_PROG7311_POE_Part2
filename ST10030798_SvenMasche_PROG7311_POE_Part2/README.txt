Sven Kimi Masche 
ST10030798

PROG7311 POE Part 2

****************************************************************************************
IMPORTANT NOTE: All sample data info is at the bottom of the README
****************************************************************************************
========================================================================================
Running the project:
========================================================================================
Prerequiset:
In order to run this project you must have have visual studio 2022 installed with .net version 6.0 as that is the version this project file runs on. 

Opening the project:
Extract the zipped folder

Find the solution file poe.sln within the unzipped folder

Double clicking the file with visual studio installed should automatically open the project in the IDE.

Finally build and run the program in the IDE

NOTE:
If database connection string is somehow incorrect, which it shouldnt be, do this:

Navigate to bin/debug/6.0 and find AgriEnergy.mdf within visual studio.

Right click the database file and copy the connection string.

Navigate to dbController under the controller folder.

In the constructor for the dbController, make con equal to the connection string.

=======================================================================================
Operating the program
=======================================================================================
The login page:

This page allows the user to login to either a farmer or employee account.

Entering a valid Username and password, as well as selecting one of the radio buttons is necessary to complete the login action.

A succesfull login will redirect the logged in user to the store page

----------------------------------------------------------------------------------------
The store page:
----------------------------------------------------------------------------------------
Getting to this page involves either logging in, or if a user clicked the store button in the top left of the layout nav bar.

This page has two similar yet disctinct functionalities depending on the user role

Farmer Role:
Farmers will see a page featuring all of thier uploaded store products.
That wraps it up for the farmer role

Employee Role:
Employees will be met with a empty page with a nav bar featuring 3 inputs namely:
-Search farmer
-Filter date
-Filter Type

This page alows employees to search for all products pertaining to a farmer by searching the user name of the farmer. The results can be further filtered by selecting a radio button and entering the filter information such as the start and end dates as well as the product type.

there is a limitation where once the filter butto is clicked all the input fields will annoyingly reset to thier default values.

----------------------------------------------------------------------------------------
Profile page
----------------------------------------------------------------------------------------
The user can get to this page by clicking the empty image icon in the top right above the logout button in the nav bar.

Similar to the store page, the profile page has two different functions depending on the role of the user.

Farmer role:
Farmers can create and post new products.
Products have a name, image, price, description and type field that needs to be entered
All criteria must be entered to create new product.
Post date for product is also captured automatically and added to database when product is created.

Employee role:
Employees can create new farmer accounts.
Farmer accounts need a userName (that is unique), full name, and password, in order to be created and entered into the system.

========================================================================================
Limitations of the program
========================================================================================
Employees need to be created using the Entity Framework, there is no functionality in this prototype to create new employees.

Created Users and products cannot be managed/altered from within the program

The values of comboboxes in views are hard coded and not selected from the database, its just a prototype after all.
========================================================================================
Sample Data
========================================================================================
There are already two existing farmer accounts and one admin account.

The login details are:
 UserName        | Password     |Role
-------------------------------------------
farmerBrown      | batman       |Farmer
Leonida          | residentevil |Farmer
Admin            | admin        |Employee

All images used for sample data are included in the project file under /sampleImages

Image references:

Pixabay, 2016 Wheat Grains Closeup Photography.[online]
Available at: https://www.pexels.com/photo/wheat-grains-closeup-photography-158603/
[Accessed 31 05 2024] 

Jannis Knorr, 2019 Green Tractor Plowing The Fields on Focus Photography[online]
Available at: https://www.pexels.com/photo/green-tractor-plowing-the-fields-on-focus-photography-2933243/
[Accessed 31 05 2024]

Mike Bird, 2016 Blue Hose on Wall
Available at: https://www.pexels.com/photo/blue-hose-on-wall-127944/
[Accessed 31 05 2024]

Trinity Kubassek, 2017 Sheep
Available at: https://www.pexels.com/photo/sheep-288621/
[Accessed 31 05 2024]

========================================================================================











