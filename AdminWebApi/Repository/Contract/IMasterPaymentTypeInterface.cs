using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterPaymentTypeInterface<MasterPaymentTypeResult>
    {
        IEnumerable<MasterPaymentTypeResult> GetAllADMasterPaymentType();
        MasterPaymentTypeResult GetADMasterPaymentTypeByID(long MasterPaymentTypeId);
        Task InsertADMasterPaymentType(ADMasterPaymentType objADMasterPaymentType);
        Task UpdateADMasterPaymentType(ADMasterPaymentType objADMasterPaymentType);
        Task DeleteADMasterPaymentType(long MasterPaymentTypeId);
        bool ADMasterPaymentTypeExists(long id);
    }
}
