
namespace AviaApp.Lib
{
    public interface ICrud1<T>
    {
        public bool Insert(T obj);
        public bool Update(T obj);
        public bool Delete(T obj);

        public IEnumerable<T>? GetAll();
        public T? GetById(int flight_id);

    }
}
