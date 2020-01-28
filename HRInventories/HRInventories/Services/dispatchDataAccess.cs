using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class dispatchDataAccess: IdispatchDataAccess
    {
        Connectionstrings _connectionstring;
        public dispatchDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task Adddispatch(DispatchViewModel dispatchViewModel)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Dispatchmaster dbGroup = new Dispatchmaster()
                    {
                        Dispatchdate = dispatchViewModel.DispatchmasterVmodel.Dispatchdate,
                        Employeeid = dispatchViewModel.DispatchmasterVmodel.Employeeid,
                        Userid = dispatchViewModel.DispatchmasterVmodel.Userid,
                        Createddate = dispatchViewModel.DispatchmasterVmodel.Createddate,
                        Isdeleted = dispatchViewModel.DispatchmasterVmodel.Isdeleted,
                    };
                    await context.Dispatchmaster.AddAsync(dbGroup);
                    await context.SaveChangesAsync();
                    var id = dbGroup.Dispatchid;
                    var dbGroupIdCheck = await context.Dispatchmaster.Where(k => k.Dispatchid == id).FirstOrDefaultAsync();

                    foreach (var item in dispatchViewModel.DispatchdetailsVModel)
                    {
                        Dispatchdetails podetail = new Dispatchdetails()
                        { 
                            Dispatchid = dbGroup.Dispatchid, 
                            Productid = item.Productid, 
                            Dispatchdate = item.Dispatchdate, 
                            Quantity = item.Quantity, 
                            Userid = item.Userid, 
                            Createddate = item.Createddate,
                            Isdeleted = item.Isdeleted 
                        };
                        await context.Dispatchdetails.AddAsync(podetail);

                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<DispatchmasterModel>> Getdispatch()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    var sql = context.Dispatchmaster.Include(p => p.Dispatchdetails).Select(s =>
                                 new DispatchmasterModel()
                                 {
                                     Dispatchid=s.Dispatchid,
                                     Dispatchdate = s.Dispatchdate,
                                     Employeeid = s.Employeeid,
                                     Userid = s.Userid,
                                     Createddate = s.Createddate,
                                     Isdeleted = s.Isdeleted,
                                     DispatchdetailsModels = s.Dispatchdetails.Select(g => new DispatchdetailsModel
                                     {
                                         Dispatchdetailid = g.Dispatchdetailid,
                                         Dispatchid = g.Dispatchid,
                                         Productid = g.Productid,
                                         Dispatchdate=g.Dispatchdate,
                                         Quantity = g.Quantity,
                                         Userid = g.Userid,
                                         Createddate = g.Createddate,
                                         Isdeleted = g.Isdeleted

                                     }).ToList()
                                 });
                    return await sql.Where(k => k.Isdeleted == "false").ToListAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task UpdateDispatch(DispatchViewModel dispatchViewModel)
        {
            try
            {
                var dbGroup = new Dispatchmaster();
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    dbGroup = await context.Dispatchmaster.Where(k => k.Dispatchid == dispatchViewModel.DispatchmasterVmodel.Dispatchid).FirstOrDefaultAsync();

                    dbGroup.Dispatchdate = dispatchViewModel.DispatchmasterVmodel.Dispatchdate;
                    dbGroup.Employeeid = dispatchViewModel.DispatchmasterVmodel.Employeeid;
                    dbGroup.Userid = dispatchViewModel.DispatchmasterVmodel.Userid;
                    dbGroup.Createddate = dispatchViewModel.DispatchmasterVmodel.Createddate;
                    dbGroup.Isdeleted = dispatchViewModel.DispatchmasterVmodel.Isdeleted;

                    await context.SaveChangesAsync();
                    foreach (var item in dispatchViewModel.DispatchdetailsVModel)
                    {
                        if (item.Dispatchdetailid == 0)
                        {
                            Dispatchdetails dispatchdetails = new Dispatchdetails()
                            {
                                Dispatchdetailid = item.Dispatchdetailid,
                                Dispatchid = item.Dispatchid,
                                Productid = item.Productid,
                                Dispatchdate = item.Dispatchdate,
                                Quantity = item.Quantity,
                                Userid = item.Userid,
                                Createddate = item.Createddate,
                                Isdeleted = item.Isdeleted
                            };
                            await context.Dispatchdetails.AddAsync(dispatchdetails);
                        }
                        else
                        {
                            if (item.Isdeleted == "true")
                            {
                                var groupStaffData = await context.Dispatchdetails.Where(k => k.Dispatchdetailid == item.Dispatchdetailid).ToListAsync();
                                context.Dispatchdetails.RemoveRange(groupStaffData);
                                await context.SaveChangesAsync();
                            }
                        }

                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task DeleteDispatch(long dispatchid)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                try
                {
                    var groupData = await context.Dispatchmaster.Where(k => k.Dispatchid == dispatchid).FirstOrDefaultAsync();
                    var detailsData = await context.Dispatchdetails.Where(k => k.Dispatchid == dispatchid).ToListAsync();
                    if (groupData != null)
                    {
                        groupData.Isdeleted = "true";
                        await context.SaveChangesAsync();
                        if (detailsData != null)
                        {
                            foreach (var item in detailsData)
                            {
                                item.Isdeleted = "true";
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}
