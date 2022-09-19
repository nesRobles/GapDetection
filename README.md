# GapDetection


# NesCo Retail Manager
An on going retail management system built from a tutorial by https://www.iamtimcorey.com/

## About The Project
This is a mini retail register application with a simple UI design. This is a very in-depth tutorial based project designed to teach how to create a MVP application from start to finish.

## Project Description

<img src=".JPG" alt="Logo" width="800" height="450">

The program starts with a sign-in screen. There is a File tab to exit the application. Once signed in, the register window will open. An account tab is visible, which allows to sign-out back to the sign-in window. 

<img src=".JPG" alt="Logo" width="800" height="450">

There is a list of items to purchase and cart to purchase the selected items. There are three buttons, one to add items to the cart, one to remove items from the cart and one to check out. Each button will only activate if item(s) are valid. As well as a subtotal, tax and total display. The items display the product name, Price, description, and quantity count. The cart display shows the product and quantity to be purchased. A check out button is used to purchase "remove" the items from the cart and stored the sale in the database.

## What I Learned 
This application uses several design patterns and frameworks. It mostly focuses on ASP.NET WEB/API. Although it does use a little MVC, to talk to the database with api/controllers. The UI is setup wiith MVVM with the help of an IoC container. Using Caliburn.Micro, the container itializes dependency injection to decrease the dependencies higher level classes have to lower level classes, mostly through constructor injection, with Interface parameters.

This project has helped me to learn how to properly setup an application for testability and scalability. How to separate my UI code from the logic, database and libraries. I've learned different design patterns and tools and how to implement them. This project is still ongoing and I hope to continue my knowledge and progress.  
