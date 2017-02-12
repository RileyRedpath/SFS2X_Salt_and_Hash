# SFS2X_Salt_and_Hash
A simple application to outline how to salt and hash your user's passwords with SmartFoxServer 2X and Unity using the signup and login components.

# Requirements
  - SmartFoxServer 2X v2.7.0+
  - Unity 5.0+ (<- may be able to use an older version)
  - A database server and client with a JDBC or ODBC driver.
  
# Setup

 In your IDE:

   - Create 2 new Java projects. Import the source files in the server/SignUpExtension folder to one project and import the source files in the server/helloworldSFS2X folder to the other.
    
   - Import the sfs2x and sfs2x-core jar files as external libraries.
    
   - Build the projects as (non-executable) jar files. Place the jar files in folders of the same name in the extensions/_\__lib_\__ folder within the SmartFox installation directory.
    
In SFS2X Admin tool:    

  - Create zones named "HelloWorld" and "SignUpZone".
  
  - Disable custom login on SignUpZone and enable it on HelloWorld.

  In your DB server console:

  - Create a new database and enter the following SQL command:
    
  CREATE TABLE users(id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, username CHAR(30),password CHAR(255), email CHAR(30), salt CHAR(32), message TEXT(500));
  
  - Connect your database through the database manager (in SFS2X admin tool) for both zones and put the JDBC/ODBC driver in the extensions/_\__lib_\__ folder within the SmartFox installation directory.
    
In Unity:
  
  - Import the project in client/SFSHelloWorld into Unity.
    
  - If SmartFoxServer is configured to run on a different port than the defualt (9933), change the connecting port in both HelloWorld.cs and SignUp.cs to the port SmartFoxServer runs on.
    
  - When both the DB server and SmartFoxServer are running, run the project from the editor or in a built exe.
      
      
# Disclaimer
For convenience, this application only uses the MD5 hash algorithm on the server. If you're going to use this repository for any kind of public application, at the very least MD5 should be replaced with a more sophisticated algorithm that meets your security and scalability requirements and the incoming password from the client side should also be hashed before being sent to the server. I assume no responsibility for any damages caused by using this repository.
