using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class CheckAvailable : ICheckAvailableInterface<SPCheckAvailableResult>
    {
        readonly AdminContext _Context;
        public CheckAvailable(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPCheckAvailableResult> GetCheckAvailable(string TableName, string NameAvailable, long NameId)
        {
            var _data =  _Context.SP_ADCheckAvailableResult(TableName, NameAvailable, NameId).ToList();
            List<SPCheckAvailableResult> objSPCheckAvailableResultList = new List<SPCheckAvailableResult>();
            foreach (var _Item in _data)
            {
                SPCheckAvailableResult _SPCheckAvailableResult = new SPCheckAvailableResult();

                _SPCheckAvailableResult.NameAvailable = _Item.NameAvailable;

                objSPCheckAvailableResultList.Add(_SPCheckAvailableResult);
            }

            return objSPCheckAvailableResultList.ToList();
        }

    }
}
