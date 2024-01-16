
namespace DO;

/// <summary>
/// An entity represents details of an engineer and his skills who is responsible for some task
/// </summary>
/// <param name="EngineerId"></param> The IDnumber of the engineer
/// <param name="EngineerName"></param> The name of the engineer
/// <param name="EngineerEmail"></param> The email of the engineer
/// <param name="LeverOfEngineer"></param> The level of the engineer's skills
/// <param name="SalaryPerHour"></param> The hourly wage of the engineer
public record Engineer
(
    int EngineerId,
    string? EngineerName = null,
    string? EngineerEmail = null,
    EngineerLevels? LeverOfEngineer = null,
    float? SalaryPerHour = null
)
{
    public Engineer() : this(0) { }
}
