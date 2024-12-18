using Microsoft.EntityFrameworkCore;
using Projeto.Service;
using Projeto.Utils.ExtensionMethod;

namespace Projeto.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _dbset;
        private IUnitOfWork _unit;
        protected IUnitOfWork Unit { get { return _unit; } }
        public GenericRepository(IUnitOfWork unit)
        {
            _context = unit._dbContext;
            _dbset = _context.Set<T>();
            _unit = unit;
        }

        public virtual IList<T> GetAll(params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            return query.ToList();
        }

        public virtual async Task<IList<T>> GetAllAsync(params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            return await query.ToListAsync();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = GetQuery(new string[] { });
            int total = 0;
            if (predicate != null)
                total = query.Count(predicate);
            else
                total = query.Count();
            return total;
        }
        public IList<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            query = query.Where(predicate);
            return query.ToList();
        }

        public async Task<IList<T>> FindByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public T FindFirstBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            query = query.Where(predicate);
            return query.FirstOrDefault();
        }

        public async Task<T> FindFirstByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = GetQuery(includeProperties);
            query = query.Where(predicate);
            return await query.FirstOrDefaultAsync();
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return _dbset.AddAsync(entity).Result.Entity;
        }

        public void AddRange(IEnumerable<T> list)
        {
            _dbset.AddRange(list);
        }

        public void RemoveRange(IEnumerable<T> list)
        {
            _dbset.RemoveRange(list);
        }

        public void RemoveAll()
        {
            if (_dbset.Any())
            {
                _dbset.RemoveRange(_dbset.ToList());
            }
        }

        public virtual T Attach(T entity)
        {
            return _dbset.Attach(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = GetQuery();

            foreach (var item in query.Where(predicate))
            {
                Delete(item);
            }
        }

        public void Update(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, T>> value)
        {
            var query = GetQuery();

            foreach (var item in query.Where(predicate))
            {
                Edit(item);
            }
        }

        public virtual void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual ICollection<T> MergeList(IEnumerable<T> listBd, IEnumerable<T> listNew, IEqualityComparer<T> comparer)
        {
            ICollection<T> listReturn = new List<T>();
            IEnumerable<T> listInsert = new List<T>();
            IEnumerable<T> listDelete = new List<T>();
            IEnumerable<T> listUpdate = new List<T>();
            if (listBd != null && listBd.Count() > 0)
            {
                if (listNew != null && listNew.Count() > 0)
                {
                    listInsert = listNew.Except(listBd, comparer).ToList();
                    listDelete = listBd.Except(listNew, comparer).ToList();
                    listUpdate = listNew.Intersect(listBd, comparer).ToList();
                }
                else
                {
                    listDelete = listBd;
                }
            }
            else if (listNew != null && listNew.Count() > 0)
            {
                listInsert = listNew;
            }

            foreach (T entity in listDelete.ToList())
            {
                Delete(entity);

            }

            foreach (T entity in listInsert)
            {
                listReturn.Add(Add(entity));
            }

            foreach (T entity in listUpdate.ToList())
            {
                T entityBd = listBd.Intersect(new List<T>() { entity }, comparer).FirstOrDefault();

                var primaryKeys = GetPrimaryKeyValue(entityBd);

                entity.CopyTo(entityBd);

                foreach (var primarykey in primaryKeys)
                {
                    var propInfo = entityBd.GetType().GetProperty(primarykey.Key);
                    propInfo.SetValue(entityBd, primarykey.Value);
                }

                Edit(Attach(entityBd));
                listReturn.Add(entityBd);
            }


            return listReturn;

        }


        public virtual ICollection<T> MergeList(IEnumerable<T> listBd, IEnumerable<T> listNew, IEqualityComparer<T> comparer, Action<T, IGenericRepository<T>> actionUpdate, Action<T, IGenericRepository<T>> actionDelete)
        {
            ICollection<T> listReturn = new List<T>();
            IEnumerable<T> listInsert = new List<T>();
            IEnumerable<T> listDelete = new List<T>();
            IEnumerable<T> listUpdate = new List<T>();
            if (listBd != null && listBd.Count() > 0)
            {
                if (listNew != null && listNew.Count() > 0)
                {
                    listInsert = listNew.Except(listBd, comparer).ToList();
                    listDelete = listBd.Except(listNew, comparer).ToList();
                    listUpdate = listNew.Intersect(listBd, comparer).ToList();
                }
                else
                {
                    listDelete = listBd;
                }
            }
            else if (listNew != null && listNew.Count() > 0)
            {
                listInsert = listNew;
            }

            foreach (T entity in listDelete.ToList())
            {
                if (actionDelete != null)
                {
                    actionDelete(entity, this);
                }
                else
                {
                    Delete(entity);
                }
            }


            foreach (T entity in listInsert)
            {
                listReturn.Add(Add(entity));
            }

            foreach (T entity in listUpdate.ToList())
            {
                T entityBd = listBd.Intersect(new List<T>() { entity }, comparer).ToList().FirstOrDefault();

                var primaryKeys = GetPrimaryKeyValue(entityBd);

                entity.CopyTo(entityBd);

                foreach (var primarykey in primaryKeys)
                {
                    var propInfo = entityBd.GetType().GetProperty(primarykey.Key);
                    propInfo.SetValue(entityBd, primarykey.Value);
                }

                if (actionUpdate != null)
                {
                    actionUpdate(entityBd, this);
                }
                else
                {
                    Edit(Attach(entityBd));
                }
                listReturn.Add(entityBd);
            }


            return listReturn;

        }

        protected List<KeyValuePair<string, int>> GetPrimaryKeyValue(T entityBd)
        {
            return _context.Model
                .FindEntityType(typeof(T))
                .FindPrimaryKey()
                .Properties
                .Select(x => new KeyValuePair<string, int>(x.Name, (int)entityBd.GetType().GetProperty(x.Name).GetValue(entityBd, null)))
                .ToList();
        }
        protected IQueryable<T> GetQuery(params string[] includeProperties)
        {
            var query = _dbset.AsQueryable<T>();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }
            }
            return query;
        }
    }
}
