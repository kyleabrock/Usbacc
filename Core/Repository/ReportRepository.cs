using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Usbacc.Core.Domain;

namespace Usbacc.Core.Repository
{
    public class ReportRepository : Repository<Report>
    {
        public Report GetById(int id, bool eagerLoading)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.Get<Report>(id);
                if (eagerLoading)
                    NHibernateUtil.Initialize(result.UsbRecords);

                return result;
            }
        }

        public IList<Report> GetAll(bool eagerLoading)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var query = session.QueryOver<Report>().Fetch(x => x.UsbRecords);
                
                var result = eagerLoading ? query.Eager.List() : query.Lazy.List();

                return result;
            }
        }

        public IList<Report> GetByUsbRecord(UsbRecord record)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var criteria =
                    session.CreateCriteria<Report>()
                           .CreateCriteria("UsbRecords")
                           .Add(Restrictions.Eq("UsbDevice", record.UsbDevice));

                var result = criteria.List<Report>();

                return result;
            }
        }
    }
}
