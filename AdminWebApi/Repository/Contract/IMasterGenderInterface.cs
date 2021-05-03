using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IMasterGenderInterface<MasterGenderResult>
    {
        IEnumerable<MasterGenderResult> GetAllADMasterGender();
        MasterGenderResult GetADMasterGenderByID(long MasterGenderId);
        Task InsertADMasterGender(ADMasterGender objADMasterGender);
        Task UpdateADMasterGender(ADMasterGender objADMasterGender);
        Task DeleteADMasterGender(long MasterGenderId);
        bool ADMasterGenderExists(long id);
    }
}
