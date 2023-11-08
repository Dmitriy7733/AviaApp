using System.Data;
using Dapper;
using Npgsql;

namespace AviaApp.Lib;

public class DataBase : ICrud1<FlightSchedule>
{
    private readonly NpgsqlConnection _db;

    public DataBase()
    {
        string connectionString = DbConfig.Import().ToString();
        _db = new NpgsqlConnection(connectionString);
    }


    public bool Insert(FlightSchedule obj)
    {
        _db.Open();

        var sql =
            $"INSERT INTO table_schedules(airline_name, flight_number, departure_airpport, arrival_airport) VALUES ('{obj.AirlineName}','{obj.FlightNumber}', '{obj.DepartureAirport}', '{obj.ArrivalAirport}')";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public bool Update(FlightSchedule obj)
    {
        _db.Open();

        var sql = $"UPDATE table_schedules SET airline_name = '{obj.AirlineName}', " +
            $"flight_number='{obj.FlightNumber}',departure_airpport = '{obj.DepartureAirport:G}', arrival_airport = '{obj.ArrivalAirport:G}' WHERE flight_id = {obj.FlightId}";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public bool Delete(FlightSchedule obj)
    {
        _db.Open();

        var sql = $"DELETE FROM table_schedules WHERE flight_id = {obj.FlightId}";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public IEnumerable<FlightSchedule>? GetAll()
    {
        _db.Open();

        var sql = "SELECT * FROM table_schedules";

        var schedules = _db.Query<FlightSchedule>(sql);

        _db.Close();

        return schedules;
    }

    public FlightSchedule? GetById(int flight_id)
    {
        _db.Open();

        var sql = $"SELECT * FROM table_schedules WHERE flight_id = {flight_id}";
        var schedule = _db.QuerySingleOrDefault<FlightSchedule>(sql);

        _db.Close();

        return schedule;
    }

    public void Drop()
    {
        _db.Open();

        var sql = "DROP TABLE table_schedules";
        var command = new NpgsqlCommand(sql, _db);
        command.ExecuteNonQuery();

        _db.Close();
    }

    public void Create()
    {
        _db.Open();

        var sql = """
                  CREATE TABLE table_schedules
                  (
                      flight_id SERIAL NOT NULL PRIMARY KEY,
                      airline_name VARCHAR(255),
                      flight_number TEXT NOT NULL,
                      departure_airport VARCHAR(255),
                      arrival_airport VARCHAR(255),
                      departure_date date,
                      departure_time time without time zone,
                      arrival_date date,
                      arrival_time time without time zone,
                  );
                  """;
        var command = new NpgsqlCommand(sql, _db);
        command.ExecuteNonQuery();

        _db.Close();
    }
}