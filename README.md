# StackBrothers-YassineBenAli

In this repository you will find the Project with the sqlite database file included.


## Running 

To run the project, follow these instructions :

    1- Clone this project on your computer
    2- Open it with Visual studio (double click on "StackBrothers-YassineBenAli.sln" file )
    3- Run the project


## Testing

Once the project is running, you will find a swagger interface with 6 endpoints included:
   
    1- "/Address" [GET]
        - This is the endpoint to retreive all the addresses from the database.
        - To test the filter in the /addresses endpoint you need to enter the word you are 
        trying to match in the first input.

        - To test the sorting function in the same endpoint you need to enter the field you want
          to sort by in the second input (orderBy) and the order you want to sort with
          "a" for ascending and "d" for descending.
    2- "​/Address​/{id}" [POST]
        - This is the endpoint to insert one address in the database.
        - Enter a valid address there are no tests on inputs.
         example : 
            "street": "Lake",
            "houseNumber": 620,
            "zipCode": 46403,
            "city": "gary",
            "country": "US"
    3- "​/Address​/{id}" [GET]
        - This is the endpoint to retreive one address from the database by id.
    4- "​/Address​/{id}" [PUT]
        - This is the endpoint to update one address from the database by id.
    5- "​/Address​/{id}" [DELETE]
        - This is the endpoint to delete one address from the database by id.
    6- "​/Address​/distance" [GET]
        - This is the endpoint to calculate distance between an adress "A" and an address "B" 
          from the database.
        - Enter address "A" and address "B" ids 
        
        
-----------------------------------------------------------------

Everything is working fine in this project, to filter the addresses i have retreived all of them from the database and i converted them to string then i have seached for occurences inside the string.
It's one of the ways of filtering data but it's not recommended on big databases because we want to have less reading/writing instructions so it's better we use a query. But this is the way with least if/else instructions.
And to calculate the distance between 2 addresses i have used a Bing map REST api, they have many endpoints but i have choose the one that matches the most our addresses with the most pressision possible on the coodinates retreiving.
    
        
    

