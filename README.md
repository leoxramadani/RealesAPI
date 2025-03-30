1st step: -configure your sql server in your machine;
2nd step: -paste your connection string in appsettings.json, realestDb field.
3rd step: -open your NuGet Package Manage Console and run "update-database" command.
          -we are using both "code first" and "database first or reverse engineering" approaches.
          -if you change any field in your models in your code, make sure to add a migration for that by running "add-migration 'AddedNewField' " and then "update-database".
          -if you change a field, whether an attribute of it or adding a new one or even deleting it first in your database, you have to add it to your code manually, with the same attributes that you have added in your database.
          
4th step: run the added test sql query to add some dummy data so you can proceed on testing the api. **soon to be added**
