namespace PlanthorWebApi.Application;

/// <summary>
/// Represents a single action or operation for data retrieval in the application.
/// You can find the article at: https://code-maze.com/cqrs-mediatr-fluentvalidation/
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQuery<out TResponse>
{
}
