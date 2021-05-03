using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
        public interface IMasterStateInterface<MasterStateResult>
        {
            IEnumerable<MasterStateResult> GetAllADMasterState();

            MasterStateResult GetADMasterStateByID(long MasterStateId);

            Task InsertADMasterState(ADMasterState objADMasterState);
            Task UpdateADMasterState(ADMasterState objADMasterState);
            Task DeleteADMasterState(long MasterStateId);
            bool ADMasterStateExists(long id);
        }
    }