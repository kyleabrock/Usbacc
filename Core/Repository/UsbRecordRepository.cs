using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Usbacc.Core.Domain;

namespace Usbacc.Core.Repository
{
    public class UsbRecordRepository : Repository<UsbRecord>
    {
        public IList<UsbRecord> GetBySearchString(string searchStr)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (string.IsNullOrEmpty(searchStr)) return GetAll();
                    
                var deviceName = Restrictions.Like("UsbDevice.DeviceName", searchStr, MatchMode.Anywhere);
                var deviceType = Restrictions.Like("UsbDevice.DeviceType", searchStr, MatchMode.Anywhere);
                var description = Restrictions.Like("UsbDevice.Description", searchStr, MatchMode.Anywhere);
                var vendorId = Restrictions.Like("UsbDevice.VendorId", searchStr, MatchMode.Anywhere);
                var productId = Restrictions.Like("UsbDevice.ProductId", searchStr, MatchMode.Anywhere);
                var serialNumber = Restrictions.Like("UsbDevice.SerialNumber", searchStr, MatchMode.Anywhere);
                //var createdDateTime = Restrictions.Like("CreatedDateTime", searchStr, MatchMode.Anywhere);
                //var lastPlugDateTime = Restrictions.Like("LastPlugDateTime", searchStr, MatchMode.Anywhere);

                var nameType = Restrictions.Or(deviceName, deviceType);
                var descriptionVendor = Restrictions.Or(description, vendorId);
                var productSerial = Restrictions.Or(productId, serialNumber);
                //var createdLast = Restrictions.Or(createdDateTime, lastPlugDateTime);

                var nameVendor = Restrictions.Or(nameType, descriptionVendor);
                //var productLast = Restrictions.Or(productSerial, createdLast);
                
                var mainCriteria = session.CreateCriteria<UsbRecord>();
                var result = mainCriteria.Add(Restrictions.Or(nameVendor, productSerial)).List<UsbRecord>();
                //var result = mainCriteria.Add(Restrictions.Or(nameVendor, productLast)).List<UsbRecord>();

                return result;
            }
        }
    }
}
