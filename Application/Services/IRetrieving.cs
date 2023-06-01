namespace Application.Services;

public interface IRetrieving<out T>
{
    public IEnumerable<T> Get();
}