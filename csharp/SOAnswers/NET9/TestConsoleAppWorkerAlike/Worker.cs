using System.Numerics;

namespace TestConsoleAppWorkerAlike;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
public class ParsedAddress : ICloneable
{
    public string NumberFirst { get; set; }
    public string NumberFirstSuffix { get; set; }
    public string NumberLast { get; set; }
    public string FlatNumber { get; set; }
    public string FlatNumberSuffix { get; set; }
    public string FlatNumberPrefix { get; set; }
    public string StreetName { get; set; }
    public string State { get; set; }
    public string Postcode { get; set; }
    public string FlatType { get; set; }
    public string LevelNumber { get; set; }
    public string BuildingName { get; set; }
    public bool NumericStreetExists { get; set; }
    public bool HasLot { get; set; }
    public bool FoundLevel { get; set; }
    public bool FoundFlat { get; set; }
    public string LotNumber { get; set; }
    public string RemainingNumbers { get; set; }
    public ParsedAddress()
    {
        NumberFirst = string.Empty;
        NumberFirstSuffix = string.Empty;
        FlatNumber = string.Empty;
        FlatNumberPrefix = string.Empty;
        FlatNumberSuffix = string.Empty;
        StreetName = string.Empty;
        State = string.Empty;
        Postcode = string.Empty;
        FlatType = string.Empty;
        LevelNumber = string.Empty;
        BuildingName = string.Empty;
        HasLot = false;
        NumericStreetExists = false;
        FoundLevel = false;
        FoundFlat = false;
        NumberLast = string.Empty;
        LotNumber = string.Empty;
        RemainingNumbers = string.Empty;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override bool Equals(object? obj)
    {
        ParsedAddress? toCompare = obj as ParsedAddress;

        if (toCompare is null) return false;

        if ((NumberFirst == toCompare.NumberFirst) &&
            (NumberFirstSuffix == toCompare.NumberFirstSuffix) &&
            (NumberLast == toCompare.NumberLast) &&
            (FlatNumber == toCompare.FlatNumber) &&
            (FlatNumberSuffix == toCompare.FlatNumberSuffix) &&
            (FlatNumberPrefix == toCompare.FlatNumberPrefix) &&
            (StreetName == toCompare.StreetName) &&
            (State == toCompare.State) &&
            (Postcode == toCompare.Postcode) &&
            (FlatType == toCompare.FlatType) &&
            (LevelNumber == toCompare.LevelNumber) &&
            (BuildingName == toCompare.BuildingName) &&
            (LotNumber == toCompare.LotNumber) &&
            (RemainingNumbers == toCompare.RemainingNumbers)
            ) return true;

        return false;
    }

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(NumberFirst);
        hash.Add(NumberFirstSuffix);
        hash.Add(NumberLast);
        hash.Add(FlatNumber);
        hash.Add(FlatNumberSuffix);
        hash.Add(FlatNumberPrefix);
        hash.Add(StreetName);
        hash.Add(State);
        hash.Add(Postcode);
        hash.Add(FlatType);
        hash.Add(LevelNumber);
        hash.Add(BuildingName);
        hash.Add(NumericStreetExists);
        hash.Add(HasLot);
        hash.Add(LotNumber);
        hash.Add(RemainingNumbers);
        return hash.ToHashCode();
    }
}