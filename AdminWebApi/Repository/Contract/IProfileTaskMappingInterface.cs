using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IProfileTaskMappingInterface<SPProfileTaskMappingResult>
    {
        IEnumerable<SPProfileTaskMappingResult> GetAllADProfileTaskMapping();

        SPProfileTaskMappingResult GetADProfileTaskMappingById(long MasterProfileTaskMappingId);

        IEnumerable<SPProfileTaskMappingResult> GetAllADProfileTaskMappingWithFunction(long MasterProfileId);

        Task InsertADProfileTaskMapping(ADProfileTaskMapping objADProfileTaskMapping);

        Task<bool> InsertUpdateADProfileTaskMapping(List<ADProfileTaskMapping> objADProfileTaskMappingList);

        Task UpdateADProfileTaskMapping(ADProfileTaskMapping objADProfileTaskMapping);
        Task DeleteADProfileTaskMapping(long MasterProfileTaskMappingId);
        bool ADProfileTaskMappingExists(long MasterProfileTaskMappingId);
    }
}
