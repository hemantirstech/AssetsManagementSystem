using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Repository.Implementation
{
    public class MaxTableMasterId : IMaxTableMasterIdInterface<SPMaxTableMasterIdResult>
    {
        readonly AssetsContext _Context;
        public MaxTableMasterId(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPMaxTableMasterIdResult> GetMaxTableMasterId(string TableName)
        {
            var _data =  _Context.SP_ASMaxTableMasterIdResult(TableName).ToList();
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
