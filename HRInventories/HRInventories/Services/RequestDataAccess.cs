using HRInventories.Models;
using HRInventories.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class RequestDataAccess : IRequestDataAccess
    {
        Connectionstrings _connectionstring;
        public RequestDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task InsertRequest(Request request)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Request Arequest = new Request()
                    {
                        Productid = request.Productid,
                        Employeeid=request.Employeeid,
                        Quantity = request.Quantity,
                        Status = request.Status,
                        Isread = request.Isread,
                        Userid = request.Userid,
                        Createddate = request.Createddate,
                        Isdeleted = request.Isdeleted
                    };
                    await context.Request.AddAsync(Arequest);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<Request>> GetRequests()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Request.Where(k => k.Isdeleted == "false").ToListAsync();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Request GetRequestbyID(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                return context.Request.FirstOrDefault(e => e.Requestid == id);
            }
        }
        public Request UpdateRequests(Request item)
        {
            var dbRequest = new Request();
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                dbRequest = context.Request.Where(k => k.Requestid == item.Requestid).FirstOrDefault();
                dbRequest.Productid = item.Productid;
                dbRequest.Employeeid = item.Employeeid;
                dbRequest.Quantity = item.Quantity;
                dbRequest.Status = item.Status;
                dbRequest.Isread = item.Isread;
                dbRequest.Userid = item.Userid;
                dbRequest.Createddate = item.Createddate;
                dbRequest.Isdeleted = item.Isdeleted;

                context.SaveChanges();
            }
            return dbRequest;
        }
        public void DeleteRequests(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                var groupdata = context.Request.FirstOrDefault(e => e.Requestid == id);
                if (groupdata != null)
                {
                    groupdata.Isdeleted = "true";
                }
                //context.Catagory.Remove(catagory);
                context.SaveChanges();
            }
        }
    }
}

