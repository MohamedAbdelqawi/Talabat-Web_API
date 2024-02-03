using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalute<Tentity> where Tentity : BaseEntity
    {
        public static IQueryable<Tentity> GetQuery(IQueryable<Tentity> Inputquery, ISpecification<Tentity> sepc)
        {
            var query = Inputquery;
            if (sepc.Criteria is not null)
                query = query.Where(sepc.Criteria);
            if (sepc.OrderBy is not null)
                query = query.OrderBy(sepc.OrderBy);
            if (sepc.OrderByDescending is not null)
                query = query.OrderByDescending(sepc.OrderByDescending);

            if (sepc.IsPaginationEnable)
                query = query.Skip(sepc.Skip).Take(sepc.Take);
            query = sepc.Includes.Aggregate(query, (currentquery, includes) => currentquery.Include(includes));
            return query;
        }

    }
}
