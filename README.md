# Energy Forecast App
#### Donalam Project
## Description
The project is about helping maintenance and production departments in industrial companies to reduce the levels of breakdowns. It is splitted in two parts: 
### 1) Tracking problems per machines
* Operator, depending on the machine, inputs the reason why the machine is stopped. The status of the problem is unsolved.
* The maintenance department receives an email with the problem imputed by the operator. List of mail is customisable with the interested persons.
* The maintenance department assign a responsible to solve the problem and give a deadline. The status of the problem becomes InProgress.
* After the maintenance announce the production that they solved the problem. Production checks if the problem is solved and set the status of the problem from In progress to solved.
* Select the period to view problems.
* Different access levels by user role.

### 2) AutoMaintenance reports
* Foreach machine operator needs to do some auto maintenance tasks, that are daily, weekly, monthly, trimestrial, semestrial
* Also there are the checks that has to be done by team leaders who checks the operator actions.
* You can check only the task that are you assigned in that day... You can not do checks if the deadline passed.
* If the deadlined passed and there are unchecked tasks they are highlighted in red.
* Different access levels by user role.

# Features
* Everything is dynamic: List of users, List of machines, List of responsables
* You can add, delete, edit all the data
* You can sort, filter and order the data
* Select the period to view data
* Save data To Sql Server
* Retrieve reports from Sql Server in Excel Files
* Mail Service (SMTP)
* Identity to Authorize
* ASP.NET Core MVC
* Save to JSON File Parameters For Mail

# Nuget Sources
* - EPPlus - to read/ write excel files
* - EntityFrameworkCore
* - SPA Blazor

# Project Images
* ![Dashboard](/images/DashboardAM.png)
* ![Problems View](/images/ProblemsView.png)
* ![Machines View](/images/Machines.png)
* ![Sign Up](/images/SignUp.png)
