namespace DataLayer.Repositories;

public interface IRepos<T, in TDto> : IEnumerable<T>
{
    public void Add(TDto item);
    public void Delete(T item);
    public T? Find(uint id);
    public T? Find(Predicate<T> predicate);
    public IEnumerable<T> FindAll(Predicate<T> predicate);
    public void DeleteFirst(Predicate<T> predicate);
    public event Action<T> OnRemoved;
    public string Serialize();
    public void Deserialize(string json);
}