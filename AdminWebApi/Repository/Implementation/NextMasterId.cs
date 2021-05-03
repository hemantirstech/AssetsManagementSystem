using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class NextMasterId : INextMasterIdInterface<SPNextMasterIdResult>
    {
        readonly AdminContext _Context;
        public NextMasterId(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPNextMasterIdResult> GetNextMasterId(string TableName)
        {
            var _data =  _Context.SP_ADNextMasterIdResult(TableName).ToList();
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
