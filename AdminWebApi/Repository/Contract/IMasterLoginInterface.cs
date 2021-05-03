using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;


namespace AdminWebApi.Repository.Contract
{
   public interface IMasterLoginInterface<MasterLoginResult>
    {
        IEnumerable<MasterLoginResult> GetAllADMasterLogin();
        MasterLoginResult GetADMasterLoginByID(long MasterLoginId);
        Task InsertADMasterLogin(ADMasterLogin objADMasterLogin);
        Task UpdateADMasterLogin(ADMasterLogin objADMasterLogin);
        Task DeleteADMasterLogin(long MasterLoginId);
        bool ADMasterLoginExists(long id);
    }
}