using NHibernate;
using Usbacc.Core.Domain;

namespace Usbacc.Core.Repository
{
    public class StatusRepository : Repository<Status>
    {
        public Status GetById(int id, bool eagerLoading)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.Get<Status>(id);
                if (eagerLoading)
                    NHibernateUtil.Initialize(result.DeviceAccounts);

                return result;
            }
        }
    }
}
