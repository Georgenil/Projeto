namespace Projeto.Service
{
    public interface IGenericRepository<T> where T : class
    {
        //ICollection<T> Except(ICollection<T> listBd, ICollection<T> listNew, IEqualityComparer<T> comparer);
        IList<T> GetAll(params string[] includeProperties);
        Task<IList<T>> GetAllAsync(params string[] includeProperties);
        int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        IList<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties);
        Task<IList<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties);
        T FindFirstBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties);
        Task<T> FindFirstByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRange(IEnumerable<T> list);
        void RemoveRange(IEnumerable<T> list);
        void RemoveAll();
        T Attach(T entity);
        void Delete(T entity);
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Update(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, T>> value);
        void Edit(T entity);
        ICollection<T> MergeList(IEnumerable<T> listBd, IEnumerable<T> listNew, IEqualityComparer<T> comparer);
        ICollection<T> MergeList(IEnumerable<T> listBd, IEnumerable<T> listNew, IEqualityComparer<T> comparer, Action<T, IGenericRepository<T>> actionUpdate, Action<T, IGenericRepository<T>> actionDelete);

    }
}
