using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminDAL;
using AdminWebApi.Model;

namespace AdminWebApi.Repository.Implementation
{
    public class ProfileTaskMapping: IProfileTaskMappingInterface<SPProfileTaskMappingResult>
    {
        readonly AdminContext _Context;
        public ProfileTaskMapping(AdminContext context)
        {
            _Context = context;
        }

        public SPProfileTaskMappingResult GetADProfileTaskMappingById(long MasterProfileTaskMappingId)
        {
            //If pass zero (0) to SP then it will return all row from the table
            var _data = _Context.SP_ADProfileTaskMappingResults(0).ToList().Where(a=>a.MasterProfileTaskMappingId== MasterProfileTaskMappingId).FirstOrDefault();

            SPProfileTaskMappingResult _SPProfileTaskMappingResult = new SPProfileTaskMappingResult();
            
            if(_data!=null)
            {
                _SPProfileTaskMappingResult.MasterProfileTaskMappingId = _data.MasterProfileTaskMappingId;
                _SPProfileTaskMappingResult.MasterProfileId = _data.MasterProfileId;
                _SPProfileTaskMappingResult.ProfileTitle = _data.ProfileTitle;
                _SPProfileTaskMappingResult.ProfileCode = _data.ProfileCode;
                _SPProfileTaskMappingResult.ProfileDescription = _data.ProfileDescription;
                _SPProfileTaskMappingResult.MasterFunctionId = _data.MasterFunctionId;
                _SPProfileTaskMappingResult.FunctionTitle = _data.FunctionTitle;
                _SPProfileTaskMappingResult.FunctionLink = _data.FunctionLink;
                _SPProfileTaskMappingResult.FunctionIcon = _data.FunctionIcon;
                _SPProfileTaskMappingResult.FunctionIconColour = _data.FunctionIconColour;
                _SPProfileTaskMappingResult.ParentMasterFunctionId = _data.ParentMasterFunctionId;
                _SPProfileTaskMappingResult.ParentFunctionTitle = _data.ParentFunctionTitle;

                _SPProfileTaskMappingResult.Sequence = _data.Sequence;
                _SPProfileTaskMappingResult.IsDelete = _data.IsDelete;
                _SPProfileTaskMappingResult.IsInsert = _data.IsInsert;
                _SPProfileTaskMappingResult.IsUpdate = _data.IsUpdate;
                _SPProfileTaskMappingResult.IsSelect = _data.IsSelect;
                _SPProfileTaskMappingResult.IsActive = _data.IsActive;

            }

            return _SPProfileTaskMappingResult;
        }

        public IEnumerable<SPProfileTaskMappingResult> GetAllADProfileTaskMapping()
        {
            var _data = _Context.SP_ADProfileTaskMappingResults(1).ToList();

            List<SPProfileTaskMappingResult> _SPProfileTaskMappingResultList = new List<SPProfileTaskMappingResult>();
            foreach (var _Item in _data)
            {
                SPProfileTaskMappingResult _SPProfileTaskMappingResult = new SPProfileTaskMappingResult();

                _SPProfileTaskMappingResult.MasterProfileTaskMappingId = _Item.MasterProfileTaskMappingId;
                _SPProfileTaskMappingResult.MasterProfileId = _Item.MasterProfileId;
                _SPProfileTaskMappingResult.ProfileTitle = _Item.ProfileTitle;
                _SPProfileTaskMappingResult.ProfileCode = _Item.ProfileCode;
                _SPProfileTaskMappingResult.ProfileDescription = _Item.ProfileDescription;
                _SPProfileTaskMappingResult.MasterFunctionId = _Item.MasterFunctionId;
                _SPProfileTaskMappingResult.FunctionTitle = _Item.FunctionTitle;
                _SPProfileTaskMappingResult.FunctionLink = _Item.FunctionLink;
                _SPProfileTaskMappingResult.FunctionIcon = _Item.FunctionIcon;
                _SPProfileTaskMappingResult.FunctionIconColour = _Item.FunctionIconColour;
                _SPProfileTaskMappingResult.ParentMasterFunctionId = _Item.ParentMasterFunctionId;
                _SPProfileTaskMappingResult.ParentFunctionTitle = _Item.ParentFunctionTitle;

                _SPProfileTaskMappingResult.Sequence = _Item.Sequence;
                _SPProfileTaskMappingResult.IsDelete = _Item.IsDelete;
                _SPProfileTaskMappingResult.IsInsert = _Item.IsInsert;
                _SPProfileTaskMappingResult.IsUpdate = _Item.IsUpdate;
                _SPProfileTaskMappingResult.IsSelect = _Item.IsSelect;
                _SPProfileTaskMappingResult.IsActive = _Item.IsActive;


                _SPProfileTaskMappingResultList.Add(_SPProfileTaskMappingResult);
            }

            return _SPProfileTaskMappingResultList;
        }

        public IEnumerable<SPProfileTaskMappingResult> GetAllADProfileTaskMappingWithFunction(long MasterProfileId)
        {
            var _data = _Context.SP_ADProfileTaskMappingResults(MasterProfileId).ToList();
            List<SPProfileTaskMappingResult> _SPProfileTaskMappingResultList = new List<SPProfileTaskMappingResult>();
            foreach (var _Item in _data)
            {
                SPProfileTaskMappingResult _SPProfileTaskMappingResult = new SPProfileTaskMappingResult();

                _SPProfileTaskMappingResult.MasterProfileTaskMappingId = _Item.MasterProfileTaskMappingId;
                _SPProfileTaskMappingResult.MasterProfileId = _Item.MasterProfileId;
                _SPProfileTaskMappingResult.ProfileTitle = _Item.ProfileTitle;
                _SPProfileTaskMappingResult.ProfileCode = _Item.ProfileCode;
                _SPProfileTaskMappingResult.ProfileDescription = _Item.ProfileDescription;
                _SPProfileTaskMappingResult.MasterFunctionId = _Item.MasterFunctionId;
                _SPProfileTaskMappingResult.FunctionTitle = _Item.FunctionTitle;
                _SPProfileTaskMappingResult.FunctionLink = _Item.FunctionLink;
                _SPProfileTaskMappingResult.FunctionIcon = _Item.FunctionIcon;
                _SPProfileTaskMappingResult.FunctionIconColour = _Item.FunctionIconColour;
                _SPProfileTaskMappingResult.ParentMasterFunctionId = _Item.ParentMasterFunctionId;
                _SPProfileTaskMappingResult.ParentFunctionTitle = _Item.ParentFunctionTitle;

                _SPProfileTaskMappingResult.Sequence = _Item.Sequence;
                _SPProfileTaskMappingResult.IsDelete = _Item.IsDelete;
                _SPProfileTaskMappingResult.IsInsert = _Item.IsInsert;
                _SPProfileTaskMappingResult.IsUpdate = _Item.IsUpdate;
                _SPProfileTaskMappingResult.IsSelect = _Item.IsSelect;
                _SPProfileTaskMappingResult.IsActive = _Item.IsActive;


                _SPProfileTaskMappingResultList.Add(_SPProfileTaskMappingResult);
            }

            return _SPProfileTaskMappingResultList;            
        }

        public async Task InsertADProfileTaskMapping(ADProfileTaskMapping objADProfileTaskMapping)
        {
            _Context.ADProfileTaskMappings.Add(objADProfileTaskMapping);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> InsertUpdateADProfileTaskMapping(List<ADProfileTaskMapping> objADProfileTaskMappingList)
        {
            bool _CompleteOperation = false;

            using (var transaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    _Context.ADProfileTaskMappings.AddRange(objADProfileTaskMappingList.Where(a=>a.MasterProfileTaskMappingId==0).ToList());
                    await _Context.SaveChangesAsync();

                    foreach(var Item in objADProfileTaskMappingList.Where(a => a.MasterProfileTaskMappingId > 0).ToList())
                    {
                        _Context.Entry(Item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }                    
                    await _Context.SaveChangesAsync();

                    transaction.Commit();

                    _CompleteOperation = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return _CompleteOperation;
        }

        public async Task UpdateADProfileTaskMapping(ADProfileTaskMapping objADProfileTaskMapping)
        {
            _Context.Entry(objADProfileTaskMapping).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteADProfileTaskMapping(long MasterProfileTaskMappingId)
        {
            ADProfileTaskMapping objADProfileTaskMapping = _Context.ADProfileTaskMappings.Find(MasterProfileTaskMappingId);

            _Context.ADProfileTaskMappings.Remove(objADProfileTaskMapping);
            await _Context.SaveChangesAsync();
        }

        public bool ADProfileTaskMappingExists(long MasterProfileTaskMappingId)
        {
            try
            {
                return _Context.ADProfileTaskMappings.Any(e => e.MasterProfileTaskMappingId == MasterProfileTaskMappingId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
