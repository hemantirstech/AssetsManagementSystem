using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterFunction : IMasterFunctionInterface<MasterFunctionResult>
    {
        readonly AdminContext _Context;

        public MasterFunction(AdminContext context)
        {
            _Context = context;
        }

        public IEnumerable<MasterFunctionResult> GetAllADMasterFunction()
        {
            try
            {
                //var _data = _Context.ADMasterFunctions.Join(_Context.ADMasterFunctions, ADMasterFunction=> ADMasterFunction.ParentMasterFunctionId, ADMasterFunctionsNew => ADMasterFunctionsNew.MasterFunctionId,(ADMasterFunctions, ADMasterFunctionsNew) => new { ADMasterFunctions, ADMasterFunctionsNew }).Select(a=> new { a.ADMasterFunctions, ParentFunctionTitle= a.ADMasterFunctionsNew.FunctionTitle}).ToList();

                var _data = (from MF in _Context.ADMasterFunctions
                             join MMF in _Context.ADMasterFunctions on MF.ParentMasterFunctionId equals MMF.MasterFunctionId into PS
                             from MMF in PS.DefaultIfEmpty()
                             select new { MF.MasterFunctionId, MF.FunctionTitle, MF.FunctionLink, MF.FunctionIcon, MF.FunctionIconColour, MF.ParentMasterFunctionId, ParentFunctionTitle = MMF.FunctionTitle, MF.Sequence, MF.IsActive, MF.EnterById, MF.EnterDate, MF.ModifiedById, MF.ModifiedDate });

                List<MasterFunctionResult> objMasterFunctionResultList = new List<MasterFunctionResult>();

                foreach (var Item in _data.ToList())
                {
                    var _MasterFunctionResult = new MasterFunctionResult
                    {
                        MasterFunctionId = Item.MasterFunctionId,
                        FunctionTitle = Item.FunctionTitle,
                        FunctionLink = Item.FunctionLink,
                        FunctionIcon = Item.FunctionIcon,
                        FunctionIconColour = Item.FunctionIconColour,
                        ParentMasterFunctionId = Item.ParentMasterFunctionId,
                        ParentFunctionTitle = (Item.ParentFunctionTitle != null ? Item.ParentFunctionTitle : "Parent Menu"),
                        Sequence = Item.Sequence,
                        IsActive = Item.IsActive,
                        ActiveColor = "green",
                        ActiveIcon = "glyphicon glyphicon-ok"
                    };

                    if (_MasterFunctionResult.IsActive == false)
                    {
                        _MasterFunctionResult.ActiveColor = "red";
                        _MasterFunctionResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterFunctionResultList.Add(_MasterFunctionResult);
                }

                return objMasterFunctionResultList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public MasterFunctionResult GetADMasterFunctionByID(long MasterFunctionId)
        {
            try
            {
                var _data = (from MF in _Context.ADMasterFunctions
                             join MMF in _Context.ADMasterFunctions on MF.ParentMasterFunctionId equals MMF.MasterFunctionId into PS
                             from MMF in PS.DefaultIfEmpty()
                             where MF.MasterFunctionId == MasterFunctionId
                             select new { MF.MasterFunctionId, MF.FunctionTitle, MF.FunctionLink, MF.FunctionIcon, MF.FunctionIconColour, MF.ParentMasterFunctionId, ParentFunctionTitle = MMF.FunctionTitle, MF.Sequence, MF.IsActive, MF.EnterById, MF.EnterDate, MF.ModifiedById, MF.ModifiedDate }).FirstOrDefault();


                var _MasterFunctionResult = new MasterFunctionResult();

                if (_data != null)
                {
                    //string _ParentTitle = _Context.ADMasterFunctions.Where(a => a.MasterFunctionId == _data.ParentMasterFunctionId.Value).Select(a => a.FunctionTitle).FirstOrDefault();

                    _MasterFunctionResult.MasterFunctionId = _data.MasterFunctionId;
                    _MasterFunctionResult.FunctionTitle = _data.FunctionTitle;
                    _MasterFunctionResult.FunctionLink = _data.FunctionLink;
                    _MasterFunctionResult.FunctionIcon = _data.FunctionIcon;
                    _MasterFunctionResult.FunctionIconColour = _data.FunctionIconColour;
                    _MasterFunctionResult.ParentMasterFunctionId = _data.ParentMasterFunctionId;
                    //ParentFunctionTitle = (_ParentTitle != null ? _ParentTitle : "Parent Menu");
                    _MasterFunctionResult.Sequence = _data.Sequence;
                    _MasterFunctionResult.IsActive = _data.IsActive;
                    _MasterFunctionResult.ActiveColor = "green";
                    _MasterFunctionResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_MasterFunctionResult.IsActive == false)
                    {
                        _MasterFunctionResult.ActiveColor = "red";
                        _MasterFunctionResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _MasterFunctionResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertADMasterFunction(ADMasterFunction objADMasterFunction)
        {
            try
            {
                _Context.ADMasterFunctions.Add(objADMasterFunction);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }       

        public async Task UpdateADMasterFunction(ADMasterFunction objADMasterFunction)
        {
            try
            {
                _Context.Entry(objADMasterFunction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task DeleteADMasterFunction(long MasterFunctionId)
        {
            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.ADProfileTaskMappings.RemoveRange(_Context.ADProfileTaskMappings.Where(a => a.MasterFunctionId == MasterFunctionId));
                    await _Context.SaveChangesAsync();

                    ADMasterFunction objADMasterFunction = _Context.ADMasterFunctions.Find(MasterFunctionId);
                    _Context.ADMasterFunctions.Remove(objADMasterFunction);

                    await _Context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }            
        }

        public bool ADMasterFunctionExists(long id)
        {
            try
            {
                return _Context.ADMasterFunctions.Any(e => e.MasterFunctionId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
