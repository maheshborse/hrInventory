using FluentAssertions;
using HRInventories.Controllers;
using HRInventories.Models;
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
    public class CategoryTest
    {
        private ICatagoryDataAccess _iCatagoryDataAccess;
        private CatagoryController _CatagoryController;
        List<Catagory> _category = null;
        [SetUp]
        public void Initialize()
        {
            _category = new List<Catagory>()
            {
                { new Catagory() { Categoryid = 1, Categoryname = "Bed1", Categorydescription = "Room1", Userid = "1234", Createddate = DateTime.Now, Isdeleted = "false" } },
                { new Catagory() { Categoryid = 2, Categoryname = "Bed2", Categorydescription = "Room1", Userid = "5678", Createddate = DateTime.Now, Isdeleted = "false" } },
                { new Catagory() { Categoryid = 3, Categoryname = "Bed1", Categorydescription = "Room2", Userid = "3456", Createddate = DateTime.Now, Isdeleted = "false" } },
            };
          

            Mock<ICatagoryDataAccess> bedLogic = new Mock<ICatagoryDataAccess>();
            bedLogic.Setup(k => k.GetCategories()).ReturnsAsync(_category.FindAll(k => k.Categoryid == 1));

            _CatagoryController = new CatagoryController(bedLogic.Object);
        }
       
        [Test]
        public async Task GetCatagoryValid()
        {
            var response = await _CatagoryController.GetCategories();
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
            (((OkObjectResult)response).Value).Should().BeEquivalentTo(_category.FindAll(k => k.Categoryid == 1), options => options.ExcludingMissingMembers());
        }


        [Test]
        public async Task AddCatagoryValid()
        {
            var response = await _CatagoryController.InsertCategory(new CatagoryModel() {Categoryname = "Bed2", Categorydescription = "Room1", Userid = "1", Isdeleted = "false" });
            Assert.AreEqual(StatusCodes.Status201Created, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public  void UpdateCatagoryValid()
        {
           // var response = await _CatagoryController.UpdateCategory(2, new CatagoryModel() { Categoryname = "Bed2", Categorydescription = "Room2", Userid = "2", Isdeleted = "false" });
            var response = _CatagoryController.UpdateCategory(2,  new CatagoryModel() { Categoryname = "Bed1", Categorydescription = "Room2", Userid = "2", Isdeleted = "false" });
            Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        }

        [Test]
        public void DeleteCatagoryValid()
        {
            var response = _CatagoryController.DeleteCategory(2);
            Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        }

    }
}
  