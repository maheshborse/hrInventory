using HRInventories.Controllers;
using HRInventories.UIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRInventories.UnitTests.Controller
{
    public class CategoryTest
    {
        private CatagoryController _CatagoryController;
        [Test]
        public async Task AddBedValid()
        {
            var response = await _CatagoryController.InsertCategory(new CatagoryModel() { Categoryname = "sas", Categorydescription = "sd", Userid = "1", Createddate = DateTime.Now, Isdeleted = "false" });
            Assert.AreEqual(StatusCodes.Status201Created, ((StatusCodeResult)response).StatusCode);
        }

    }
}
