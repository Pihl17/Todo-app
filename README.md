# Todo-app


## Todo database

The data will be stored in a PostgreSQL database.

### Setting up the local database

Follow these steps to setup a local database and connect the app to it.

#### Installing the database

1. Download the PostgreSQL installer for your operating system from EDB through the following link:
https://www.enterprisedb.com/downloads/postgres-postgresql-downloads

2. Run the installer and follow the steps, setting the directory, components, password, and port for the database.
For the components, you will need to include the "PostgreSQL Server" component, the "pgAdmin 4" and "Command Line Tools" components are recommended.

3. Run the CreateDatabaseTables.sql script to populate the database with tables and enum types. The script can be run through either PSQL or pgAdmin 4 application.

##### Setting up the database in pgAdmin 4

1. Open up pgAdmin 4 and connect to the database server.
2. In the Object Explorer, under the server, find the database, right click it and select the "Query tool" option to open up the query window.
3. Click the open file button, navigate to the file and open it.
4. Once the file is opened and the sql code is visible, click the execute button to run the code.
Successful operation should lead to an empty todo table being displayed at the bottom.

##### Setting up the database in PSQL (terminal app)

1. Open up PSQL and connect to the database server and select the database.
2. Run the following command: \i <path-to-file> 
A "permission denied" error might appear. If so, you will have to change the backward-slashes in the file path to forward-slashes.
Successful operation should end with an empty todo table being displayed in the terminal.

#### Configure the app to connect to the database

