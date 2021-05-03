using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMessageNotificationInterface<MessageNotificationResult>
    {
        IEnumerable<MessageNotificationResult> GetAllADMessageNotification();

        MessageNotificationResult GetADMessageNotificationByID(long MessageNotificationId);

        Task InsertADMessageNotification(ADMessageNotification objADMessageNotification);
        Task UpdateADMessageNotification(ADMessageNotification objADMessageNotification);
        Task DeleteADMessageNotification(long MasterTimeZoneId);
        bool ADMessageNotificationExists(long id);
    }
}
