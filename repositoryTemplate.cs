public class Repository<TEntity>:IBoatInspectorRepository<TEntity> where TEntity: class
{
        private readonly Context _context;

        public BoatInspectorRepository(Context context)
        {
            _context = context;
        }
        
        //Get single entity by ID
        public async Task<TEntity> GetEntity(int entityId)
        {
            return await _context.Set<TEntity>().FindAsync(entityId);
        }
        
        //Get a list of entities order by objects include as many other entities as you want, set how many you want to get
        public async Task<IEnumerable<TEntity>> 
                                GetRecentIncludeMany(
                                    Expression<Func<TEntity, object>> orderBy, 
                                    int quantity, 
                                    params Expression<Func<TEntity, object>>[] include)
                                    {
                                        IEnumerable<TEntity> result = null;
                                        
                                        var query = _context.Set<TEntity>()
                                            .OrderByDescending(orderBy)
                                            .Take(quantity)
                                            .Include(include[0]);
                                        
                                        for (int queryIndex = 1; queryIndex < include.Length; ++queryIndex)
                                        {
                                            query = query.Include(include[queryIndex]);
                            
                                            if (queryIndex == include.Length-1)
                                            {
                                                return result = await query.ToListAsync();
                                            }
                                        }

                                        return null;

                                    }
         
         //Get Single Entity by predicate and include any single other enity
         public async Task<TEntity> GetEntityInclude(Expression<Func<TEntity, bool>> predicateWhere, 
                                                    Expression<Func<TEntity, object>> includes)
        {
            return await _context.Set<TEntity>().Where(predicateWhere).Include(includes).FirstOrDefaultAsync();
        }
        
        //add entity
        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        
        //get a simple list of entities
        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        
        //get entity by predicate
        public async Task<TEntity> GetEntityWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }
        
        
}
