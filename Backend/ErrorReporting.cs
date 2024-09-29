namespace Lab1;

public enum AirportInputError
{
    InvalidIDLength,
    DuplicateAirportId,
    InvalidCityLength,
    InvalidTitleLength,
    RatingOutOfBounds,
    InvalidDate,
    DBAdditionError,
    NoError
}

public enum AirportAddError
{
    AirportAlreadyExists,
    NoAirportPased,
    NoError
}
public enum AirportDeletionError
{
    AirportNotFound,
    FailedToDeleteError,
    NoError
}

public enum AirportEditError
{
    DBAdditionError,
    InvalidIDLength,
    DuplicateAirportId,
    InvalidCityLength,
    InvalidTitleLength,
    RatingOutOfBounds,
    InvalidDate,
    IdNotPresent,
    NoError,
    Error
}
