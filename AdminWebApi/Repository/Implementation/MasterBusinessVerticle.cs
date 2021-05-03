using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebApi.Repository.Contract;
using AdminWebApi.Model;
using AdminDAL;

namespace AdminWebApi.Repository.Implementation
{
    public class MasterBusinessVerticle : IMasterBusinessVerticleInterface<MasterBusinessVerticleResult>
    {
        readonly AdminContext _Context;
        public MasterBusinessVerticle(AdminContext context)
        {
            _Context = context;
        }
        public IEnumerable<MasterBusinessVerticleResult> GetAllADMasterBusinessVerticle()
        {
            try
            {
                var _data = _Context.ADMasterBusinessVerticles.ToList();

                List<MasterBusinessVerticleResult> objMasterBusinessVerticleResultList = new List<MasterBusinessVerticleResult>();
                foreach (var _Item in _data)
                {
                    MasterBusinessVerticleResult _objMasterBusinessVerticleResult = new MasterBusinessVerticleResult();

                    _objMasterBusinessVerticleResult.MasterBusinessVerticleId = _Item.MasterBusinessVerticleId;
                    _objMasterBusinessVerticleResult.BusinessVerticleTitle = _Item.BusinessVerticleTitle;

                    _objMasterBusinessVerticleResult.IsActive = _Item.IsActive;
                    _objMasterBusinessVerticleResult.ActiveColor = "green";
                    _objMasterBusinessVerticleResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterBusinessVerticleResult.IsActive == false)
                    {
                        _objMasterBusinessVerticleResult.ActiveColor = "red";
                        _objMasterBusinessVerticleResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }

                    objMasterBusinessVerticleResultList.Add(_objMasterBusinessVerticleResult);
                }

                return objMasterBusinessVerticleResultList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public MasterBusinessVerticleResult GetADMasterBusinessVerticleByID(long MasterBusinessVerticleId)
        {
            try
            {
                var _Item = _Context.ADMasterBusinessVerticles.Find(MasterBusinessVerticleId);

                MasterBusinessVerticleResult _objMasterBusinessVerticleResult = new MasterBusinessVerticleResult();

                if (_Item != null)
                {
                    _objMasterBusinessVerticleResult.MasterBusinessVerticleId = _Item.MasterBusinessVerticleId;
                    _objMasterBusinessVerticleResult.BusinessVerticleTitle = _Item.BusinessVerticleTitle;

                    _objMasterBusinessVerticleResult.IsActive = _Item.IsActive;
                    _objMasterBusinessVerticleResult.ActiveColor = "green";
                    _objMasterBusinessVerticleResult.ActiveIcon = "glyphicon glyphicon-ok";

                    if (_objMasterBusinessVerticleResult.IsActive == false)
                    {
                        _objMasterBusinessVerticleResult.ActiveColor = "red";
                        _objMasterBusinessVerticleResult.ActiveIcon = "glyphicon glyphicon-remove";
                    }
                }

                return _objMasterBusinessVerticleResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertADMasterBusinessVerticle(ADMasterBusinessVerticle objADMasterBusinessVerticle)
        {
            try
            {
                _Context.ADMasterBusinessVerticles.Add(objADMasterBusinessVerticle);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateADMasterBusinessVerticle(ADMasterBusinessVerticle objADMasterBusinessVerticle)
        {
            try
            {
                _Context.Entry(objADMasterBusinessVerticle).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteADMasterBusinessVerticle(long MasterBusinessVerticleId)
        {
            try
            {
                ADMasterBusinessVerticle objADMasterBusinessVerticle = _Context.ADMasterBusinessVerticles.Find(MasterBusinessVerticleId);
                _Context.ADMasterBusinessVerticles.Remove(objADMasterBusinessVerticle);

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ADMasterBusinessVerticleExists(long MasterBusinessVerticleId)
        {
            try
            {
                return _Context.ADMasterBusinessVerticles.Any(e => e.MasterBusinessVerticleId == MasterBusinessVerticleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
