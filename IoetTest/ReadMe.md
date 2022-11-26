Architecture:

The solution was built using Dependency Injection's design pattern. Besides achieving the dependency inversion principle
other SOLID principles were also followed such as single responsibility, open/close and interface segregation. One of the
biggest advantages of this app consist of the database layer being highly decoupled, a new source of data like an API
returning a JSON could be integrated with very low mainteanace cost.

Approach and Methodology:

Since a very unusual input format was suggested from the beggining, a data layer was going to be needed to create the
data models and based on them implement all the logic to find out the amount to pay each employee. If this app were to be
used in production environments, data would probably come from a different source, the data layer would pull the information
from any source and make it available for the TimeManager. This way a better grade of extendability and mainability is achieved.

Input's data format is similar to the suggested on the test but in order to include more employees I had to include the
character pipe (|). The example data for more than one employee looks like this (See EmployeeData.txt for more):
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00|ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

Dependencies:

Microsoft.Extensions.DependencyInjection 7.0.0
Microsoft.Extensions.Hosting 7.0.0
xUnit 2.4.1
Moq 4.18.2

How to run:

1. Download the repository and open the solution file (.sln) with Visual Studio.
2. Verify that you have both nuget packages (Microsoft.Extensions.DependencyInjection and Microsoft.Extensions.Hosting)
in IoetTest dependencies. You might need to restore them doing right click at the root of the solution.
3. Build IoetTest.
4. Include EmployeeData.txt file in bin/Debug/net6.0. bin is in the same directory where the project is.
5. Run the app in Visual Studio by hitting the play button and see the results in the terminal.
