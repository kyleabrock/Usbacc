using System.Collections.Generic;
using NHibernate;
using Usbacc.Core.Domain;

namespace Usbacc.Core.Repository
{
    public class DeviceAccountRepository : Repository<DeviceAccount>
    {
        public IList<DeviceAccount> GetAll(bool eagerLoading)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var query = session.QueryOver<DeviceAccount>().Fetch(x => x.Status);
                var result = eagerLoading ? query.Eager.List() : query.Lazy.List();

                return result;
            }
        }
    }
}
