using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MessageNotification: IMessageNotificationInterface<MessageNotificationResult>
    {
        readonly AdminContext _Context;
        public MessageNotification(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MessageNotificationResult> GetAllADMessageNotification()
        {
            try
            {
                var _data = _Context.ADMessageNotifications.ToList();

                List<MessageNotificationResult> objMessageNotificationList = new List<MessageNotificationResult>();
                foreach (var _Item in _data)
                {
                    MessageNotificationResult _objMessageNotificationResult = new MessageNotificationResult();

                    _objMessageNotificationResult.MasterMessageNotificationId = _Item.MasterMessageNotificationId;
                    _objMessageNotificationResult.MessageDate = _Item.MessageDate;
                    _objMessageNotificationResult.MasterMessageTypeId = _Item.MasterMessageTypeId;
                    _objMessageNotificationResult.MessageFrom = _Item.MessageFrom;
                    _objMessageNotificationResult.MessageTo = _Item.MessageTo;
                    _objMessageNotificationResult.MessageTitle = _Item.MessageTitle;
                    _objMessageNotificationResult.MessageDescription = _Item.MessageDescription;
                    _objMessageNotificationResult.IsRead = _Item.IsRead;
                    _objMessageNotificationResult.IsSend = _Item.IsSend;
                    _objMessageNotificationResult.IsDelete = _Item.IsDelete;
                    _objMessageNotificationResult.ShareTo = _Item.ShareTo;
                    _objMessageNotificationResult.MasterCompanyId = _Item.MasterCompanyId;
                    _objMessageNotificationResult.MasterBranchId = _Item.MasterBranchId;


                    _objMessageNotificationResult.IsActive = _Item.IsActive;
                    _objMessageNotificationResult.ActiveColor = "green";
                    _objMessageNotificationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMessageNotificationResult.IsActive == false)
                    {
                        _objMessageNotificationResult.ActiveColor = "red";
                        _objMessageNotificationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMessageNotificationList.Add(_objMessageNotificationResult);
                }

                return objMessageNotificationList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MessageNotificationResult GetADMessageNotificationByID(long MessageNotification)
        {
            try
            {
                var _Item = _Context.ADMessageNotifications.Find(MessageNotification);

                MessageNotificationResult _objMessageNotificationResult = new MessageNotificationResult();

                if (_Item != null)
                {
                    _objMessageNotificationResult.MasterMessageNotificationId = _Item.MasterMessageNotificationId;
                    _objMessageNotificationResult.MessageDate = _Item.MessageDate;
                    _objMessageNotificationResult.MasterMessageTypeId = _Item.MasterMessageTypeId;
                    _objMessageNotificationResult.MessageFrom = _Item.MessageFrom;
                    _objMessageNotificationResult.MessageTo = _Item.MessageTo;
                    _objMessageNotificationResult.MessageTitle = _Item.MessageTitle;
                    _objMessageNotificationResult.MessageDescription = _Item.MessageDescription;
                    _objMessageNotificationResult.IsRead = _Item.IsRead;
                    _objMessageNotificationResult.IsSend = _Item.IsSend;
                    _objMessageNotificationResult.IsDelete = _Item.IsDelete;
                    _objMessageNotificationResult.ShareTo = _Item.ShareTo;
                    _objMessageNotificationResult.MasterCompanyId = _Item.MasterCompanyId;
                    _objMessageNotificationResult.MasterBranchId = _Item.MasterBranchId;


                    _objMessageNotificationResult.IsActive = _Item.IsActive;
                    _objMessageNotificationResult.ActiveColor = "green";
                    _objMessageNotificationResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMessageNotificationResult.IsActive == false)
                    {
                        _objMessageNotificationResult.ActiveColor = "red";
                        _objMessageNotificationResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMessageNotificationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMessageNotification(ADMessageNotification objADMessageNotification)
        {
            try
            {
                _Context.ADMessageNotifications.Add(objADMessageNotification);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMessageNotification(ADMessageNotification objADMessageNotification)
        {
            try
            {
                _Context.Entry(objADMessageNotification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMessageNotification(long MessageNotification)
        {
            try
            {
                ADMessageNotification objADMessageNotification = _Context.ADMessageNotifications.Find(MessageNotification);
                _Context.ADMessageNotifications.Remove(objADMessageNotification);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMessageNotificationExists(long MessageNotification)
        {
            try
            {
                return _Context.ADMessageNotifications.Any(e => e.MasterMessageNotificationId == MessageNotification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
