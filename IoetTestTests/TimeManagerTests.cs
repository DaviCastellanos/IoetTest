namespace IoetTestTests;

public class TimeManagerTests
{
    private Mock<IDataSource> dataSource;

    [Fact]
    public void TimeManagerRunReturnsCompleted()
    {
        //Arrange
        dataSource = new Mock<IDataSource>();
        CancellationToken token = new CancellationToken();

        List<Shift> shifts = new List<Shift>()
        {
            new Shift(100, 2300, Day.Monday),
            new Shift(800, 1700, Day.Sunday),
        };

        List<Employee> employees = new List<Employee>()
        {
            new Employee("TEST", shifts)

        };

        var output = new StringWriter();
        Console.SetOut(output);

        dataSource.Setup(d => d.GetEmployeesFromLocalFile(It.IsAny<string>())).Returns(employees);

        TimeManager timeManager = new TimeManager(dataSource.Object);

        //Act
        var result = timeManager.StartAsync(token);

        //Assert
        var outputResult = output.ToString();
        Assert.Equal(outputResult, "The amount to pay TEST is 625 USD.\n");
        Assert.True(result.IsCompleted);
    }

    [Fact]
    public void TimeManagerRunReturnsArgumentExceptionForNullArgument()
    {
        //Arrange
        dataSource = new Mock<IDataSource>();
        CancellationToken token = new CancellationToken();

        List<Employee> employees = new List<Employee>()
        {
            new Employee("TEST", null)

        };

        dataSource.Setup(d => d.GetEmployeesFromLocalFile(It.IsAny<string>())).Returns(employees);

        TimeManager timeManager = new TimeManager(dataSource.Object);

        //Aact & Assert
        Assert.ThrowsAsync<ArgumentException>(() => timeManager.StartAsync(token));
    }

    [Fact]
    public void TimeManagerRunReturnsArgumentExceptionForInvalidTime()
    {
        //Arrange
        dataSource = new Mock<IDataSource>();
        CancellationToken token = new CancellationToken();

        List<Shift> shifts = new List<Shift>()
        {
            new Shift(100, 2500, Day.Monday),
            new Shift(800, 1700, Day.Sunday),
        };

        List<Employee> employees = new List<Employee>()
        {
            new Employee("TEST", shifts)

        };

        dataSource.Setup(d => d.GetEmployeesFromLocalFile(It.IsAny<string>())).Returns(employees);

        TimeManager timeManager = new TimeManager(dataSource.Object);

        //Aact & Assert
        Assert.ThrowsAsync<ArgumentException>(() => timeManager.StartAsync(token));
    }
}
