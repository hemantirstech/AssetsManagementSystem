using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssetsWebApi.Repository.Contract;
using AssetsWebApi.Model;

namespace AssetsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardInterface<ProductDetailResult> _IDashboardInterface;


        public DashboardController(IDashboardInterface<ProductDetailResult> IDashboardInterface)
        {
            _IDashboardInterface = IDashboardInterface;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardResult>> Dashboard(long MasterSubCategoryId, long MasterBranchId)
        {
            try
            {
                List<ProductDetailResult> AssetsCategoryList = _IDashboardInterface.GetAllASMasterCategory().ToList();
                List<ProductDetailResult> AssetsSubCategoryList = _IDashboardInterface.GetAllASMasterSubCategory(MasterSubCategoryId, MasterBranchId).ToList();
                List<ProductDetailResult> MicrosoftSubCategoryList = _IDashboardInterface.GetAllMicrosoftSubCategory().ToList();

                DashboardResult objDashboardResult = new DashboardResult();
                objDashboardResult.AssetsCategoryList = AssetsCategoryList;
                objDashboardResult.AssetsSubCategoryList = AssetsSubCategoryList;


                objDashboardResult.TotalAssetsInStock = AssetsSubCategoryList.Sum(a => a.TotalAssetsInStock);
                objDashboardResult.AssetsAssign = AssetsSubCategoryList.Sum(a => a.AssetsAssign);
                objDashboardResult.AssetsInRepair = AssetsSubCategoryList.Sum(a => a.AssetsInRepair);


                objDashboardResult.TotalLaptopInStock = AssetsSubCategoryList.Where(a => a.MasterSubCategoryId == 1).Sum(a => a.TotalAssetsInStock);
                objDashboardResult.LaptopAssign = AssetsSubCategoryList.Where(a => a.MasterSubCategoryId == 1).Sum(a => a.AssetsAssign);
                objDashboardResult.LaptopInRepair = AssetsSubCategoryList.Where(a => a.MasterSubCategoryId == 1).Sum(a => a.AssetsInRepair);


                objDashboardResult.MicrosoftInStock = MicrosoftSubCategoryList.Sum(a => a.TotalAssetsInStock);
                objDashboardResult.MicrosoftAssign = MicrosoftSubCategoryList.Sum(a => a.AssetsAssign);
                objDashboardResult.MicrosoftInExpire = MicrosoftSubCategoryList.Sum(a => a.ServiceInExpire);

                return objDashboardResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductDetailResult>>> Dashboard(long MasterSubCategoryId, long MasterBranchId)
        //{
        //    try
        //    {
        //        var _ProductDetailResult = _IDashboardInterface.GetAllASMasterSubCategory(MasterSubCategoryId, MasterBranchId).ToList();

        //        return _ProductDetailResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return null;
        //}

        [HttpGet("{MasterEmployeeId}")]
        public async Task<ActionResult<IEnumerable<ProductDetailResult>>> DashboardEmployee(long MasterEmployeeId)
        {
            try
            {
                var _ProductDetailResult = _IDashboardInterface.GetAllASMasterSubCategoryEmployee(MasterEmployeeId).ToList();

                return _ProductDetailResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}
