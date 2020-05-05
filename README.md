# ServiceFabCase
Sample Service application to demonstrate Reliable collections 

Ths is a sample application implementing stateful and stateless service fabric applications and how we can use the Reliable collections provided by service fabric SDK

# Steps to run the application
1. Install any lastest visual studio version (I used Visual Studio community edition, its free)  - https://visualstudio.microsoft.com/downloads/
2. Install Service Fabric SDK and setup a local cluster - https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started. If you dont setup a local cluster, visual studio will automatically setup when you run the application for the first time
3. Install VS Code for editing the angular applicaton. You can edit it Visusal Studio, but i prefer VS Code because you can work on UI and run the angular app without running the whole service fabric solution which makes UI development faster.
4. Install MongoDB and update the appsettings.json file with your database and collection name - https://docs.mongodb.com/manual/tutorial/install-mongodb-on-windows/
5. Insall Mongo DB Compass Community Edition to view the data base and create collection and database - https://www.mongodb.com/download-center/compass

Once the service fabric SDK and DB is setup you can run the application locally.

# What the application is about?

The application demonstrate a case submission portal, where the users can submit any case regarding public issues.
Two types of cases are implemented in the application, one is to report fraud activity and the other one is to report isseus with traffic light.

# What are the core concepts used? 
UI is and application build on a stateless asp.net core mvc applicaton with angular 8. 
Form the UI, it calls the Processor Service which is built on Stateful Service fabric Asp.net Core API

The data stored in Service fabric state is consdered "Hot Data" and a copy is stored in Mongo DB is called "Cold Data" 
The used see all the information about the active cases in from Hot data. Once the case is closed. the used query for the case will be directed to DB

The stateful service fabric service has 3 partitions and the UI direcred the cased to a specific partition using the Case category

The sync between Hot and cold data is done throught a command quesuring pattern. When a change happends to the data, we queue a command for that specific change in a reliable queue.
That command then later dequeued and proceced to reflect the changes in DB.

