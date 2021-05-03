using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;
using AssetsDAL;

namespace AssetsWebApi.Repository.Implementation
{
    public class DropDownFill : IDropDownFillInterface<SPDropDownFillResult>
    {
        readonly AssetsContext _Context;
        public DropDownFill(AssetsContext context)
        {
            _Context = context;
        }

        public IEnumerable<SPDropDownFillResult> GetDropDownFill(string TableName, long MasterId, string Type)
        {
            var _data =  _Context.SP_ASDropDownFillResult(TableName,MasterId,Type).ToList();
            List<SPDropDownFillResult> objSPDropDownFillResultList = new List<SPDropDownFillResult>();
            foreach (var _Item in _data)
            {
                SPDropDownFillResult _SPDropDownFillResult = new SPDropDownFillResult();

                _SPDropDownFillResult.MasterId = _Item.MasterId;
                _SPDropDownFillResult.MasterName = _Item.MasterName;

                objSPDropDownFillResultList.Add(_SPDropDownFillResult);
            }

            return objSPDropDownFillResultList.ToList();
        }

    }
}
