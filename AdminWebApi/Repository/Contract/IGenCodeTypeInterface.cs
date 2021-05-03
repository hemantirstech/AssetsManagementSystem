using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDAL;

namespace AdminWebApi.Repository.Contract
{
    public interface IGenCodeTypeInterface<GenCodeTypeResult>
    {
        IEnumerable<GenCodeTypeResult> GetAllADGenCodeType();

        GenCodeTypeResult GetADGenCodeTypeByID(long GenCodeTypeId);

        Task InsertADGenCodeType(ADGenCodeType objADGenCodeType);
        Task UpdateADGenCodeType(ADGenCodeType objADGenCodeType);
        Task DeleteADGenCodeType(long GenCodeTypeId);
        bool ADGenCodeTypeExists(long id);
    }
}
