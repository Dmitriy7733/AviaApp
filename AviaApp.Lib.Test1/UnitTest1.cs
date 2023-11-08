using Xunit;


namespace AviaApp.Lib.Test1;


    public class DataBaseTest
{
    private readonly DataBase _db;
    private FlightSchedule _schedule;
    private List<FlightSchedule> _expectedSchedules;
    public DataBaseTest()
    {
        _db = new DataBase();
        _schedule = new FlightSchedule()
        {
            FlightId = 1,
            AirlineName = "U100",
            FlightNumber = "7",
            DepartureAirport = "Москва",
            ArrivalAirport = "Краснодар",
            DepartureDate = new DateTime(2023, 11, 4),
            DepartureTime = new TimeSpan(22, 30, 0),
            ArrivalDate = new DateTime(2023, 11, 5),
            ArrivalTime = new TimeSpan(23, 30, 0)
        };
        _expectedSchedules = new List<FlightSchedule>() { _schedule };
    }
    private void InitDb()
    {
        _db.Drop();
        _db.Create();
        _db.Insert(_schedule);
    }
    [Fact]
    public void GetAll_Test()
    {
        InitDb();
        var actual = _db.GetAll();

        Assert.Equal(_expectedSchedules, actual);
    }
    [Fact]
    public void GetById_Test()
    {
        InitDb();
        var actual = _db.GetById(1);

        Assert.Equal(_schedule, actual);
    }

    [Fact]
    public void Insert_Test()
    {
        var schedule = _schedule;
        schedule.FlightId = 1;
        _expectedSchedules.Add(schedule);

        InitDb();
        var result = _db.Insert(_schedule);

        Assert.True(result);

        var actual = _db.GetAll();
        Assert.Equal(_expectedSchedules, actual);
    }
}


