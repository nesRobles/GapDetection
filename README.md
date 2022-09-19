# GapDetection
An Application to Detect Gaps in an Excel Postion Rotation System.

## About The Project
A position rotation system assigns positions to an employee's shift, to perform that position's tasks. When creating this for multiple employees, errors can occur. Like when a certain position only needs one employee to have that position at any given moment in time. Sometimes multiple employees may be assigned that position at the same time, when only one employee should have that position at that given time. Or the certain position will not be assigned at all. 

Therefore, the need to check for gaps or duplications arises. To do this manually works but becomes harder when dealing with enormous amounts of employees. This application checks to see if there are any gaps for given positions that need to be assigned throughout the shifts. It also checks to see if any position has been assigned multiple times at a given time that should only be assigned once.

The application allows you to create an area and create that area's positions and visually displays the gaps or duplications.

## Project Description

<img src=".JPG" alt="Logo" width="800" height="450">


<img src=".JPG" alt="Logo" width="800" height="450">

There is a list of items to purchase and cart to purchase the selected items. There are three buttons, one to add items to the cart, one to remove items from the cart and one to check out. Each button will only activate if item(s) are valid. As well as a subtotal, tax and total display. The items display the product name, Price, description, and quantity count. The cart display shows the product and quantity to be purchased. A check out button is used to purchase "remove" the items from the cart and stored the sale in the database.

## What I Learned 
This application uses several design patterns and frameworks. It mostly focuses on ASP.NET WEB/API. Although it does use a little MVC, to talk to the database with api/controllers. The UI is setup wiith MVVM with the help of an IoC container. Using Caliburn.Micro, the container itializes dependency injection to decrease the dependencies higher level classes have to lower level classes, mostly through constructor injection, with Interface parameters.

This project has helped me to learn how to properly setup an application for testability and scalability. How to separate my UI code from the logic, database and libraries. I've learned different design patterns and tools and how to implement them. This project is still ongoing and I hope to continue my knowledge and progress.  
