using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApi.Repository.Implementation
{
    public class TransactionVendorFileUpload : ITransactionVendorFileUploadInterface<TransactionVendorFileUploadResult>
    {
        readonly AdminContext _Context;

        public TransactionVendorFileUpload(AdminContext context)
        {
            _Context = context;
        }

        public async Task Upload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload)
        {
            try
            {
                _Context.ADTransactionVendorFileUploads.Add(objADTransactionVendorFileUpload);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TransactionVendorFileUploadResult> GetAllADTransactionVendorFileUpload()
        {
            try
            {
                var _data = _Context.ADTransactionVendorFileUploads.ToList();


                List<TransactionVendorFileUploadResult> objTransactionVendorFileUploadResulttList = new List<TransactionVendorFileUploadResult>();

                foreach (var _Item in _data)
                {
                    TransactionVendorFileUploadResult _objTransactionVendorFileUploadResult = new TransactionVendorFileUploadResult();

                    _objTransactionVendorFileUploadResult.TransactionVendorFileUploadId = _Item.TransactionVendorFileUploadId;
                    _objTransactionVendorFileUploadResult.TransactionVendorFileName = _Item.TransactionVendorFileName;
                    _objTransactionVendorFileUploadResult.UploadFile = _Item.UploadFile;
                    _objTransactionVendorFileUploadResult.Sequence = _Item.Sequence;

                    _objTransactionVendorFileUploadResult.IsActive = _Item.IsActive;
                    _objTransactionVendorFileUploadResult.ActiveColor = "green";
                    _objTransactionVendorFileUploadResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objTransactionVendorFileUploadResult.IsActive == false)
                    {
                        _objTransactionVendorFileUploadResult.ActiveColor = "red";
                        _objTransactionVendorFileUploadResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objTransactionVendorFileUploadResulttList.Add(_objTransactionVendorFileUploadResult);
                }

                return objTransactionVendorFileUploadResulttList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TransactionVendorFileUploadResult GetADTransactionVendorFileUploadByID(long TransactionVendorFileUploadId)
        {
            try
            {
                var _Item = _Context.ADTransactionVendorFileUploads.Find(TransactionVendorFileUploadId);

                TransactionVendorFileUploadResult _objTransactionVendorFileUploadResult = new TransactionVendorFileUploadResult();

                if (_Item != null)
                {
                    _objTransactionVendorFileUploadResult.TransactionVendorFileUploadId = _Item.TransactionVendorFileUploadId;
                    _objTransactionVendorFileUploadResult.TransactionVendorFileName = _Item.TransactionVendorFileName;
                    _objTransactionVendorFileUploadResult.UploadFile = _Item.UploadFile;
                    _objTransactionVendorFileUploadResult.Sequence = _Item.Sequence;

                    _objTransactionVendorFileUploadResult.IsActive = _Item.IsActive;
                    _objTransactionVendorFileUploadResult.ActiveColor = "green";
                    _objTransactionVendorFileUploadResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objTransactionVendorFileUploadResult.IsActive == false)
                    {
                        _objTransactionVendorFileUploadResult.ActiveColor = "red";
                        _objTransactionVendorFileUploadResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objTransactionVendorFileUploadResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADTransactionVendorFileUpload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload)
        {
            try
            {
                _Context.ADTransactionVendorFileUploads.Add(objADTransactionVendorFileUpload);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADTransactionVendorFileUpload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload)
        {
            try
            {
                _Context.Entry(objADTransactionVendorFileUpload).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADTransactionVendorFileUpload(long TransactionVendorFileUploadId)
        {
            try
            {
                ADTransactionVendorFileUpload objADTransactionVendorFileUpload = _Context.ADTransactionVendorFileUploads.Find(TransactionVendorFileUploadId);
                _Context.ADTransactionVendorFileUploads.Remove(objADTransactionVendorFileUpload);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADTransactionVendorFileUploadExists(long TransactionVendorFileUploadId)
        {
            try
            {
                return _Context.ADTransactionVendorFileUploads.Any(e => e.TransactionVendorFileUploadId == TransactionVendorFileUploadId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
