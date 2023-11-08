using System;
using Dapper;
using Npgsql;

namespace AviaApp.Lib;

public class FlightSchedule
{
    public int FlightId { get; set; }
    public string AirlineName { get; set; }
    public string FlightNumber { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime? DepartureDate { get; set; }
    public TimeSpan? DepartureTime { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public TimeSpan? ArrivalTime { get; set; }



    public bool Equals(FlightSchedule? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return FlightId == other.FlightId && AirlineName == other.AirlineName && FlightNumber == other.FlightNumber
            && DepartureAirport.Equals(other.DepartureAirport) && ArrivalAirport.Equals(other.ArrivalAirport)
            && DepartureDate.Equals(other.DepartureDate) && DepartureTime.Equals(other.DepartureTime) && ArrivalDate.Equals(other.ArrivalDate)
            && ArrivalTime.Equals(other.ArrivalTime);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FlightSchedule)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FlightId, AirlineName, FlightNumber, DepartureAirport, ArrivalAirport);
    }
}