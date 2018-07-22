using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace RLserver.Models
{
    public interface IModelRepository<TModelType>
    {
        List<TModelType> GetAll();
        TModelType GetById(int id);
        void Add(TModelType model);
        List<DbSet> GetAllById<DbSet>(int id);
    }

    public class MatchRepository : IModelRepository<Match>
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public List<Match> GetAll()
        {
            throw new NotImplementedException();
        }

        public Match GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Match model)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllById<T>(int id)
        {

            throw new NotImplementedException();        
        }
    }
}