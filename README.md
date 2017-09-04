# RedPanda
Calculate the number of happy customers per store and  output into Output.csv file

I used a Store Simulation csv file to simulated the action of the happy Button being pressed. Every time the happy button is pressed, it gets stored in this file 

Project was done for Red Panda technical assessment to show my technical abilities.

The programming language I picked is C# 
I used Visual Studio 2010 with windows forms components. I went with this selection because I am comfortable with these tools and use them in my day to day operations.
The front is a basic page with a button to process.
I used hash table for this task, to help me identify the key and for Lookup efficiency
I catered for columns that might change positions later on.
I took 3  hours doing this assessment based on my understanding of what was needed. The Hardest part for me was understanding how to interpret the store press button simulation, which I eventually did in a CSV file 
I stored the data into a hashtable instead of a database. But due to time constraints a hashtable was easier

Assumptions:
      •	StoreSimulation.csv  is stored in c:/
      •	This application could have catered for a few error handling. But I wanted to keep the project clean, simple and stick to the basic requirements  

Some of the error handling to think of :
•	More fields sent than required
•	Invalid Characters
•	Missing values and fields
•	If there is no file available, it will  display a message 
•	Column names always stay the same
