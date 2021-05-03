using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MaxTableMasterId : IMaxTableMasterIdInterface<SPMaxTableMasterIdResult>
    {
        readonly AdminContext _Context;
        public MaxTableMasterId(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPMaxTableMasterIdResult> GetMaxTableMasterId(string TableName)
        {
            var _data =  _Context.SP_ADMaxTableMasterIdResult(TableName).ToList();
            List<SPMaxTableMasterIdResult> objSPMaxTableMasterIdResultList = new List<SPMaxTableMasterIdResult>();
            foreach (var _Item in _data)
            {
                SPMaxTableMasterIdResult _SPMaxTableMasterIdResult = new SPMaxTableMasterIdResult();

                _SPMaxTableMasterIdResult.MasterId = _Item.MasterId;

                objSPMaxTableMasterIdResultList.Add(_SPMaxTableMasterIdResult);
            }

            return objSPMaxTableMasterIdResultList.ToList();
        }

    }
}
