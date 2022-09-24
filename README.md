# GapDetection
An Application to Detect Gaps in an Excel Postion Rotation System.

## About The Project
A position rotation system assigns positions to an employee's shift, to perform that position's tasks. When creating this for multiple employees, errors can occur. Like when a certain position only needs one employee to have that position at any given moment in time. Sometimes multiple employees may be assigned that position at the same time, when only one employee should have that position at that given time. Or the certain position will not be assigned at all. 

Therefore, the need to check for gaps or duplications arises. To do this manually works but becomes harder when dealing with enormous amounts of employees. This application checks to see if there are any gaps for given positions that need to be assigned throughout the shifts. It also checks to see if any position has been assigned multiple times at a given time that should only be assigned once.

The application allows you to create an area and create that area's positions and visually displays the gaps or duplications.

## Project Description

The main menu has a dropdown and two buttons. The dropdown allows you to choose a venue. The button on the left goes to the gap display window. The button on the right goes the settings window. 

<img src="GapPics/MainMenu2.JPG" alt="Logo" width="600" height="300">

The settings window allows you to create a venue or select one. One the left side of the window is where you add or delete a selected venue’s positions. You can also arrange the order of the positions as how they will appear in the gap detection display. Once you are done creating a venue/position, you can save the progress.

<img src="GapPics/Settings2.JPG" alt="Logo" width="600" height="300">

From the main, select a venue and click the gap button. This will take you to the gap window. The Gap window consists of a top and bottom time bar, two position legends and the center where the gap information will be displayed. In the middle there are dark gray squares. Each row of gray squars falls in line with a specific position on the position legend. The program will check if the position on the postion legend is represented in the excel file within that particular time interval. If it is, then the gray square will turn blue. If not, then it will stay gray.

<img src="GapPics/Display1.jpg" alt="Logo" width="400" height="250">     <img src="GapPics/Display2.jpg" alt="Logo" width="400" height="251">

This is the menu tab. It has three options. The first is, Open File. This will allow you to open specific excel files to use with the application. The second is Check For Gaps. This will read the information on the excel file and translate it to the gap display window. The third is Main Menu. This will take you back to the main menu.

<img src="GapPics/GapMenu.JPG" alt="Logo" width="200" height="150">

Select Open File. Choose the excel file. The excel will open. This is what it will look like. 

<img src="GapPics/Shifts1.JPG" alt="Logo" width="1050" height="300">

After the excel is opened select Check Gaps from the display menue. The gap display will now be filled with light blue squares.

<img src="GapPics/Legend.JPG" alt="Logo" width="60" height="240"><img src="GapPics/Gaps.JPG" alt="Logo" width="750" height="240">

To show an example: If you delete the "GTR" position from the excel file as it is shown here and then click on the Check Gaps button, the Gap Display should change the corrosponding blue squares back to gray. 

<img src="GapPics/EmptyPosition.JPG" alt="Logo" width="1050" height="300">

As it is shown here. The blue squares have disappeared at the same time as it appears on the excel file. As well as within the same row that the position exists on the position legend.

<img src="GapPics/Legend.JPG" alt="Logo" width="60" height="240"><img src="GapPics/Gaps2.JPG" alt="Logo" width="750" height="240">

The program also checks for duplicatation in positions. If a position should only exist one time at any given point in time, the gray square will turn red and will display the number of times that position exist at that time. 

As you can see the GTR position has changed to LD3. There is also another LD3 position three rows down as well.

<img src="GapPics/SHIFTSLD3.JPG" alt="Logo" width="1050" height="300">

As you see here, not only does the program show the GTR position empty in the same spot but it also show four red squares with the number of times LD3 exists at the same time.

<img src="GapPics/Legend.JPG" alt="Logo" width="60" height="240"><img src="GapPics/Gaps3.JPG" alt="Logo" width="750" height="240">

## What I Learned
This application uses several design patterns and frameworks. It mostly focuses on ASP.NET WEB/API. Although it does use a little MVC, to talk to the database with api/controllers. The UI is setup wiith MVVM with the help of an IoC container. Using Caliburn.Micro, the container itializes dependency injection to decrease the dependencies higher level classes have to lower level classes, mostly through constructor injection, with Interface parameters.

This project has helped me to learn how to properly setup an application for testability and scalability. How to separate my UI code from the logic, database and libraries. I've learned different design patterns and tools and how to implement them. This project is still ongoing and I hope to continue my knowledge and progress.  
