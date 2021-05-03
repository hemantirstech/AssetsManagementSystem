using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface ITransactionVendorFileUploadInterface<TransactionVendorFileUploadResult>
    {
        IEnumerable<TransactionVendorFileUploadResult> GetAllADTransactionVendorFileUpload();

        TransactionVendorFileUploadResult GetADTransactionVendorFileUploadByID(long TransactionVendorFileUploadId);

        Task InsertADTransactionVendorFileUpload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload);
        Task UpdateADTransactionVendorFileUpload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload);
        Task DeleteADTransactionVendorFileUpload(long TransactionVendorFileUploadId);
       // Task Upload();
        Task Upload(ADTransactionVendorFileUpload objADTransactionVendorFileUpload);
        bool ADTransactionVendorFileUploadExists(long TransactionVendorFileUploadId);
    }
}
