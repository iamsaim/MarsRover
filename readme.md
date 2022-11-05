# MarsRover Technical Assesment

This solution is developed using .NET 6. 
Onion architecture is used to develop the system and the solution contains different layers which are following:

####Starting from the inner most layer to the outer most

### MarsRover.Domain

This project contains Entities and Contants.

### MarsRover.Persistance

This project contains DbContext and seeding folder where you can seed data when the application starts

### MarsRover.Application

This project contains Features folder in which CQRS is implemented using MediatR. There are also some other folders which contain Interfaces, ViewModels, Exceptions, Exception handler and logging logic.

### MarsRover.Infrastructure

This project contains implementation of interfaces and extension methods for startup.cs class

### MarsRover.WebApp
This project is the web application and MVC design is used in it. So conventional controllers and views folder structure is used. Moreover this is log folder which logs every request in a text file.

### MarsRover.Test

This project contains unit testing for the code. MOQ is used for mocking the dependencies in the code.

#### Working logic

On the start of the application you are asked to give width and height of plateau. Then you are routed to the view where plateau is displayed with rovers.
You can add a rover my using the UI or you can deploy rovers by using csv files (as described in the assignment).
You can also click on the rover on the map to give directions to rover. Once given the direction map will reload and rover will be moved.

#### Demo Video

https://www.loom.com/share/c7c56e5e108e4ad6990a53d7856e6f16

### If I had more time

If I had more time then I would have completed all the unit testing and I would have spent more time in making the perfect UI.