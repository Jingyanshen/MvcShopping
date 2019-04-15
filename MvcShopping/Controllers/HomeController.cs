﻿using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;//分页引用

namespace MvcShopping.Controllers
{
    public class HomeController : BaseController
    {
        //MvcShoppingContext db = new MvcShoppingContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var data = db.ProductCategories.ToList();
#if DEBUG
            // 首页
            // 插入演示信息（测试用）
            if (data.Count == 0)
            {
                db.ProductCategories.Add(new ProductCategory() { Id = 1, Name = "文具" });
                db.ProductCategories.Add(new ProductCategory() { Id = 2, Name = "礼品" });
                db.ProductCategories.Add(new ProductCategory() { Id = 3, Name = "书籍" });
                db.ProductCategories.Add(new ProductCategory() { Id = 4, Name = "美劳用具" });

                db.SaveChanges();
                data = db.ProductCategories.ToList();
            }
#endif
            return View(data);
        }

        // 商品列表
        public ActionResult ProductList(int id, int p = 1)
        {
            var productCategory = db.ProductCategories.Find(id);
            if (productCategory != null)
            {
                var data = productCategory.Products.ToList();
                //插入分页相关信息
                var pagedData = data.ToPagedList(pageNumber: p, pageSize: 10);

                ////插入演示信息用
                //if (data.Count == 0)
                //{
                //    productCategory.Products.Add(new Product() { Name = productCategory.Name + "类别下的商品1", Color = Color.Red, Description = "N/A", Price = 99, PublishOn = DateTime.Now, ProductCategory = productCategory });
                //    productCategory.Products.Add(new Product() { Name = productCategory.Name + "类别下的商品2", Color = Color.Blue, Description = "N/A", Price = 150, PublishOn = DateTime.Now, ProductCategory = productCategory });
                //    db.SaveChanges();

                //    data = productCategory.Products.ToList();
                //}
                return View(pagedData);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //商品明细
        public ActionResult ProductDetail(int id)
        {
            var data = db.Products.Find(id);
            
            return View(data);
        }
    }
}
