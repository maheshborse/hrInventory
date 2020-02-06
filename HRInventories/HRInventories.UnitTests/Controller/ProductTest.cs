using FluentAssertions;
using HRInventories.Controllers;
using HRInventories.Models;
using HRInventories.Services;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRInventories.UnitTests.Controller
{
    public class ProductTest
    {
        private ProductController _ProductController;
        private ProductDataAccess _iProductDataAccess;
        List<ProductModel> _product = null;
        CatagoryModel _catagory = null;
        [SetUp]
        public void Initialize()
        {
            _product = new List<ProductModel>()
            {
                { new ProductModel() { Productid = 1, Categoryid=2,Productname = "Bed1", Productdescription = "Room1", Userid = "1234", Createddate = DateTime.Now, Isdeleted = "false",stock=250 } },
                { new ProductModel() { Productid = 2, Categoryid= 4, Productname = "Bed2", Productdescription = "Room1", Userid = "5678", Createddate = DateTime.Now, Isdeleted = "false" ,stock=250 } },
                { new ProductModel() { Productid = 3,Categoryid= 6, Productname = "Bed1", Productdescription = "Room2", Userid = "3456", Createddate = DateTime.Now, Isdeleted = "false",stock=250 } },
            };
            
            Mock<IProductDataAccess> products = new Mock<IProductDataAccess>();
            products.Setup(s => s.GetProducts()).ReturnsAsync(_product.FindAll(k => k.Productid == 1));

            _ProductController = new ProductController(products.Object);
        }
        [Test]
        public async Task GetProductValid()
        {
            var response = await _ProductController.GetProducts();
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
            (((OkObjectResult)response).Value).Should().BeEquivalentTo(_product.FindAll(k => k.Productid == 1), options => options.ExcludingMissingMembers());
        }
        [Test]
        public async Task AddProductValid()
        {
            var response = await _ProductController.InsertProduct(new ProductModel() { Productname = "Bed2", Productdescription = "Room1", Userid = "1", Isdeleted = "false" });
            Assert.AreEqual(StatusCodes.Status201Created, ((StatusCodeResult)response).StatusCode);
        }
        [Test]
        public void UpdateProductValid()
        {
            // var response = await _CatagoryController.UpdateCategory(2, new CatagoryModel() { Categoryname = "Bed2", Categorydescription = "Room2", Userid = "2", Isdeleted = "false" });
            var response = _ProductController.UpdateProduct(2, new Product() { Productname = "Bed1", Productdescription = "Room2", Userid = "2", Isdeleted = "false" });
            Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void DeleteCatagoryValid()
        {
            var response = _ProductController.DeleteProduct(2);
            Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        }
    }
}
