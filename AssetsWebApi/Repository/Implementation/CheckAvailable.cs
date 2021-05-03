using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Repository.Implementation
{
    public class CheckAvailable : ICheckAvailableInterface<SPCheckAvailableResult>
    {
        readonly AssetsContext _Context;
        public CheckAvailable(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPCheckAvailableResult> GetCheckAvailable(string TableName, string NameAvailable, long NameId)
        {
            var _data =  _Context.SP_ASCheckAvailableResult(TableName, NameAvailable, NameId).ToList();
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
