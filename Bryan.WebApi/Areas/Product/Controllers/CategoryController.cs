using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Areas.Product.Models.Category;
using Bryan.WebApi.Controllers;
using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Bryan.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bryan.WebApi.Areas.Product.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private IGd_GoodsCategoryService _categoryService;
        public CategoryController(IGd_GoodsCategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// 获取商品分类list
        /// </summary>
        /// <returns></returns>
        [HttpGet("getcategorylist")]
        [ProducesResponseType(typeof(Gd_GoodsCategory), 200)]
        public IActionResult GetCategoryList()
        {
            string code = "000000";
            var list = _categoryService.GetList(p => true, p => p.Orders);
            if (list.Count == 0)
                code = "000200";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 根据Id获取分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getcategory")]
        [ProducesResponseType(typeof(Gd_GoodsCategory), 200)]
        public IActionResult GetCategory(int id)
        {
            string code = "000000";
            var entity = _categoryService.GetEntityById(id);
            if (entity == null)
                code = "100070";
            return ReturnJson(code, entity);
        }

        /// <summary>
        /// 添加商品类目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addcategory")]
        [ProducesResponseType(typeof(Gd_GoodsCategory), 200)]
        public IActionResult AddCategory([FromBody]FromAddCategory model)
        {
            string code = "000000";
            var category = AutoMapperExt.MapTo<Gd_GoodsCategory>(model);

            return ReturnJson(code);
        }

    }
}