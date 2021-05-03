using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Repository.Implementation
{
    public class NextMasterId : INextMasterIdInterface<SPNextMasterIdResult>
    {
        readonly AssetsContext _Context;
        public NextMasterId(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPNextMasterIdResult> GetNextMasterId(string TableName)
        {
            var _data =  _Context.SP_ASNextMasterIdResult(TableName).ToList();
            List<SPNextMasterIdResult> objSPNextMasterIdResultList = new List<SPNextMasterIdResult>();
            foreach (var _Item in _data)
            {
                SPNextMasterIdResult _SPNextMasterIdResult = new SPNextMasterIdResult();

                _SPNextMasterIdResult.MasterId = _Item.MasterId;

                objSPNextMasterIdResultList.Add(_SPNextMasterIdResult);
            }

            return objSPNextMasterIdResultList.ToList();
        }

    }
}
