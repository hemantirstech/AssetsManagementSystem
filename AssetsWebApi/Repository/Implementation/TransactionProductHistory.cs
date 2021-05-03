using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsDAL;
using AssetsWebApi.Model;
using AssetsWebApi.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AssetsWebApi.Repository.Implementation
{
    public class TransactionProductHistory : ITransactionProductHistoryInterface<TransactionProductHistoryResult>
    {
        readonly AssetsContext _Context;

        public TransactionProductHistory(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<TransactionProductHistoryResult> GetAllASTransactionProductHistory()
        {
            try
            {
                var _data = (from TPH in _Context.ASTransactionProductHistorys

                             select new
                             {
                                 TPH.TransactionProductHistoryId,
                                 TPH.MasterProductChildId,
                                // TPH.ASMasterProductChild,
                                 TPH.MasterSubscriptionTypeId,
                                 TPH.SubscriptionPrice,
                                 TPH.MasterSubscriptionVendorId,
                                 TPH.SubscriptionDate,
                                 TPH.SubscriptionStartDate,
                                 TPH.SubscriptionExpiryDate,
                                 TPH.UploadInvoice,
                                 TPH.UploadDocument,
                                 TPH.UploadWarretyCard,                              


                                 TPH.Sequence,
                                 TPH.IsActive,
                                 TPH.EnterById,
                                 TPH.EnterDate,
                                 TPH.ModifiedById,
                                 TPH.ModifiedDate,



                             });

                List<TransactionProductHistoryResult> objTransactionProductHistoryResultList = new List<TransactionProductHistoryResult>();

                foreach (var _Item in _data.ToList())
                {
                    var _TransactionProductHistoryResult = new TransactionProductHistoryResult();

                    _TransactionProductHistoryResult.TransactionProductHistoryId = _Item.TransactionProductHistoryId;
                    _TransactionProductHistoryResult.MasterProductChildId = _Item.MasterProductChildId;
                  //  _TransactionProductHistoryResult.ASMasterProductChild = _Item.ASMasterProductChild;
                    _TransactionProductHistoryResult.MasterSubscriptionTypeId = _Item.MasterSubscriptionTypeId;
                    _TransactionProductHistoryResult.SubscriptionPrice = _Item.SubscriptionPrice;
                    _TransactionProductHistoryResult.MasterSubscriptionVendorId = _Item.MasterSubscriptionVendorId;
                    _TransactionProductHistoryResult.SubscriptionDate = _Item.SubscriptionDate;
                    _TransactionProductHistoryResult.SubscriptionStartDate = _Item.SubscriptionStartDate;
                    _TransactionProductHistoryResult.SubscriptionExpiryDate = _Item.SubscriptionExpiryDate;
                    _TransactionProductHistoryResult.UploadInvoice = _Item.UploadInvoice;
                    _TransactionProductHistoryResult.UploadDocument = _Item.UploadDocument;
                    _TransactionProductHistoryResult.UploadWarretyCard = _Item.UploadWarretyCard;

                    _TransactionProductHistoryResult.Sequence = _Item.Sequence;
                    _TransactionProductHistoryResult.IsActive = _Item.IsActive;
                    _TransactionProductHistoryResult.ActiveColor = "green";
                    _TransactionProductHistoryResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _TransactionProductHistoryResult.EnterById = _Item.EnterById;
                    _TransactionProductHistoryResult.EnterDate = _Item.EnterDate;
                    _TransactionProductHistoryResult.ModifiedById = _Item.ModifiedById;
                    _TransactionProductHistoryResult.ModifiedDate = _Item.ModifiedDate;

                    if (_TransactionProductHistoryResult.IsActive == false)
                    {
                        _TransactionProductHistoryResult.ActiveColor = "red";
                        _TransactionProductHistoryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objTransactionProductHistoryResultList.Add(_TransactionProductHistoryResult);
                }
                return objTransactionProductHistoryResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransactionProductHistoryResult GetASTransactionProductHistoryByID(long TransactionProductHistoryId)
        {
            try
            {
                var _data = (from TPH in _Context.ASTransactionProductHistorys
                             where TPH.TransactionProductHistoryId == TransactionProductHistoryId
                             select new
                             {
                                 TPH.TransactionProductHistoryId,
                                 TPH.MasterProductChildId,
                                // TPH.ASMasterProductChild,
                                 TPH.MasterSubscriptionTypeId,
                                 TPH.SubscriptionPrice,
                                 TPH.MasterSubscriptionVendorId,
                                 TPH.SubscriptionDate,
                                 TPH.SubscriptionStartDate,
                                 TPH.SubscriptionExpiryDate,
                                 TPH.UploadInvoice,
                                 TPH.UploadDocument,
                                 TPH.UploadWarretyCard,


                                 TPH.Sequence,
                                 TPH.IsActive,
                                 TPH.EnterById,
                                 TPH.EnterDate,
                                 TPH.ModifiedById,
                                 TPH.ModifiedDate,
                             });


                var _Item = _data.FirstOrDefault();

                TransactionProductHistoryResult _TransactionProductHistoryResult = new TransactionProductHistoryResult();
                if (_Item != null)
                {
                    _TransactionProductHistoryResult.TransactionProductHistoryId = _Item.TransactionProductHistoryId;
                    _TransactionProductHistoryResult.MasterProductChildId = _Item.MasterProductChildId;
                  //  _TransactionProductHistoryResult.ASMasterProductChild = _Item.ASMasterProductChild;
                    _TransactionProductHistoryResult.MasterSubscriptionTypeId = _Item.MasterSubscriptionTypeId;
                    _TransactionProductHistoryResult.SubscriptionPrice = _Item.SubscriptionPrice;
                    _TransactionProductHistoryResult.MasterSubscriptionVendorId = _Item.MasterSubscriptionVendorId;
                    _TransactionProductHistoryResult.SubscriptionDate = _Item.SubscriptionDate;
                    _TransactionProductHistoryResult.SubscriptionStartDate = _Item.SubscriptionStartDate;
                    _TransactionProductHistoryResult.SubscriptionExpiryDate = _Item.SubscriptionExpiryDate;
                    _TransactionProductHistoryResult.UploadInvoice = _Item.UploadInvoice;
                    _TransactionProductHistoryResult.UploadDocument = _Item.UploadDocument;
                    _TransactionProductHistoryResult.UploadWarretyCard = _Item.UploadWarretyCard;
                   


                    _TransactionProductHistoryResult.Sequence = _Item.Sequence;
                    _TransactionProductHistoryResult.IsActive = _Item.IsActive;
                    _TransactionProductHistoryResult.ActiveColor = "green";
                    _TransactionProductHistoryResult.ActiveIcon = "glyphicon glyphicon-ok";
                    _TransactionProductHistoryResult.EnterById = _Item.EnterById;
                    _TransactionProductHistoryResult.EnterDate = _Item.EnterDate;
                    _TransactionProductHistoryResult.ModifiedById = _Item.ModifiedById;
                    _TransactionProductHistoryResult.ModifiedDate = _Item.ModifiedDate;

                    if (_TransactionProductHistoryResult.IsActive == false)
                    {
                        _TransactionProductHistoryResult.ActiveColor = "red";
                        _TransactionProductHistoryResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                }
                return _TransactionProductHistoryResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertASTransactionProductHistory(ASTransactionProductHistory objADTransactionProductHistory)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {

                    _Context.ASTransactionProductHistorys.Add(objADTransactionProductHistory);
                    await _Context.SaveChangesAsync();

                    ASMasterProductChild objASMasterProductChild = _Context.ASMasterProductChilds.Find(objADTransactionProductHistory.MasterProductChildId);
                    objASMasterProductChild.WarrantyExpiryDate = objADTransactionProductHistory.SubscriptionExpiryDate;

                    _Context.Entry(objASMasterProductChild).State = EntityState.Modified;
                    await _Context.SaveChangesAsync();

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task UpdateASTransactionProductHistory(ASTransactionProductHistory objADTransactionProductHistory)
        {
            try
            {
                _Context.Entry(objADTransactionProductHistory).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteASTransactionProductHistory(long TransactionProductHistoryId)
        {
            try
            {
                var objADTransactionProductHistory = _Context.ASTransactionProductHistorys.Find(TransactionProductHistoryId);
                _Context.ASTransactionProductHistorys.Remove(objADTransactionProductHistory);
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ASTransactionProductHistoryExists(long TransactionProductHistoryId)
        {
            try
            {
                return _Context.ASTransactionProductHistorys.Any(e => e.TransactionProductHistoryId == TransactionProductHistoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
