# SWAPI Challenge

It is a basic application in .NET Core that lists all the ships and through an entry of a distance in MGLT to be traversed. The API list all starships and calculates how many refueling will need to travel that distance.

### Prerequisites

Having docker installed and an IDE (my preference is Visual Studio 2017+)

### Installing

If you are running through Visual Studio, just make sure that the project selected for execution is docker-compose and give play in the debug.

If it is not, just run the docker-compose via terminal, being able to access the project folder and execute the 'docker-compose up' command.

## Running the tests (Unit Tests)

The test project is named 'Swapi_based_resupply_distance.tests', if you are using Visual Studio, simply go to the Test> Run> All Tests tab and follow the results in the 'Test Explorer' window.

![Test Explorer](https://github.com/eduardomonteiro/SWAPI-chalenge/blob/master/img/te.PNG?raw=true)


## Built With

* [ASP.NET Core](https://get.asp.net/) and [C#](https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx)for cross-platform server-side code
* [Redis](https://redis.io/) in-memory data structure store, used as a database
* [React](https://facebook.github.io/react/) and [Redux](https://redux.js.org/) for client-side code
* [Bootstrap](http://getbootstrap.com/)Bootstrap for layout and styling
* [OData](https://www.odata.org/), is one of the best practices for building and consuming RESTful API
* [SWAPI](https://swapi.co/) used to consume the data that will be handled in this application
* [Docker](https://www.docker.com/) used to build the cluster needed to connect and run the servers of all technologies used in a simple way is fast

## Author

* **Eduardo Monteiro** - [SWAPI-chalenge](https://github.com/eduardomonteiro/)

## Urls

[Web Site](http://10.1.0.4/)
[API](http://10.1.0.5/api/starship)
[Redis Server](http://10.1.0.6/)