using HRInventories.Controllers;
using HRInventories.Models;
using HRInventories.Services.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRInventories.UnitTests.Controller
{
    public class dispatchTest
    {
        //private IdispatchDataAccess _idispatchDataAccess;
        //private dispatchController _dispatchController;
        //List<Dispatchmaster> _dispatchmaster = null;
        //[SetUp]
        //public void Initialize()
        //{
        //    _category = new List<Catagory>()
        //    {
        //        { new Catagory() { Categoryid = 1, Categoryname = "Bed1", Categorydescription = "Room1", Userid = "1234", Createddate = DateTime.Now, Isdeleted = "false" } },
        //        { new Catagory() { Categoryid = 2, Categoryname = "Bed2", Categorydescription = "Room1", Userid = "5678", Createddate = DateTime.Now, Isdeleted = "false" } },
        //        { new Catagory() { Categoryid = 3, Categoryname = "Bed1", Categorydescription = "Room2", Userid = "3456", Createddate = DateTime.Now, Isdeleted = "false" } },
        //    };


        //    Mock<IdispatchDataAccess> bedLogic = new Mock<IdispatchDataAccess>();
        //    bedLogic.Setup(k => k.GetCategories()).ReturnsAsync(_category.FindAll(k => k.Categoryid == 1));

        //    _dispatchController = new CatagoryController(bedLogic.Object);
        //}

        //[Test]
        //public async Task GetCatagoryValid()
        //{
        //    var response = await _CatagoryController.GetCategories();
        //    Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)response).StatusCode);
        //    (((OkObjectResult)response).Value).Should().BeEquivalentTo(_category.FindAll(k => k.Categoryid == 1), options => options.ExcludingMissingMembers());
        //}


        //[Test]
        //public async Task AddCatagoryValid()
        //{
        //    var response = await _CatagoryController.InsertCategory(new CatagoryModel() { Categoryname = "Bed2", Categorydescription = "Room1", Userid = "1", Isdeleted = "false" });
        //    Assert.AreEqual(StatusCodes.Status201Created, ((StatusCodeResult)response).StatusCode);
        //}

        //[Test]
        //public void UpdateCatagoryValid()
        //{
        //    // var response = await _CatagoryController.UpdateCategory(2, new CatagoryModel() { Categoryname = "Bed2", Categorydescription = "Room2", Userid = "2", Isdeleted = "false" });
        //    var response = _CatagoryController.UpdateCategory(2, new CatagoryModel() { Categoryname = "Bed1", Categorydescription = "Room2", Userid = "2", Isdeleted = "false" });
        //    Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        //}

        //[Test]
        //public void DeleteCatagoryValid()
        //{
        //    var response = _CatagoryController.DeleteCategory(2);
        //    Assert.AreEqual(StatusCodes.Status200OK, ((StatusCodeResult)response).StatusCode);
        //}

    }
}
 