using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
   public interface IMasterDesignationInterface<MasterDesignationResult>
    {
        IEnumerable<MasterDesignationResult> GetAllADMasterDesignation();

        MasterDesignationResult GetADMasterDesignationByID(long MasterDesignationId);

        Task InsertADMasterDesignation(ADMasterDesignation objADMasterDesignation);
        Task UpdateADMasterDesignation(ADMasterDesignation objADMasterDesignation);
        Task DeleteADMasterDesignation(long MasterDesignationId);
        bool ADMasterDesignationExists(long id);
    }
}
