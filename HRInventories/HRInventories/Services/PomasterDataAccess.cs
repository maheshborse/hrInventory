﻿using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class PomasterDataAccess : IPomasterDataAccess
    {
        Connectionstrings _connectionstring;
        public PomasterDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task AddPo(POViewModel pOViewModel)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Pomaster dbGroup = new Pomaster()
                    {
                        Podate = pOViewModel.pomastermodel.Podate,
                        Totalamount = pOViewModel.pomastermodel.Totalamount,
                        Discount = pOViewModel.pomastermodel.Discount,
                        Finalamount = pOViewModel.pomastermodel.Finalamount,
                        Userid = pOViewModel.pomastermodel.Userid,
                        Createddate = pOViewModel.pomastermodel.Createddate,
                        Isdeleted = pOViewModel.pomastermodel.Isdeleted,

                    };
                    await context.Pomaster.AddAsync(dbGroup);
                    await context.SaveChangesAsync();
                    var id = dbGroup.Poid;
                    var dbGroupIdCheck = await context.Pomaster.Where(k => k.Poid == id).FirstOrDefaultAsync();

                    foreach (var item in pOViewModel.podetailModel)
                    {
                        Podetail podetail = new Podetail()
                        { Poid = dbGroup.Poid, Productid = item.Productid, Porate = item.Porate, Amount = item.Amount, Discount = item.Discount, Quantity = item.Quantity, Userid = item.Userid, Createddate = item.Createddate, Isdeleted = item.Isdeleted };
                        await context.Podetail.AddAsync(podetail);

                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<PomasterModel>> GetPo()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    //var sql = from s in context.Pomaster
                    //          join g in context.Podetail on s.Poid equals g.Poid
                    var sql = context.Pomaster.Include(p => p.Podetail).Select(s =>
                                 new PomasterModel()
                                 {
                                     Poid = s.Poid,
                                     Discount = s.Discount,
                                     Totalamount = s.Totalamount,
                                     Finalamount = s.Finalamount,
                                     Userid = s.Userid,
                                     Createddate = s.Createddate,
                                     Isdeleted = s.Isdeleted,
                                     PodetailModels = s.Podetail.Select(g => new PodetailModel
                                     {
                                         Podetailid = g.Podetailid,
                                         Poid = g.Poid,
                                         Productid = g.Productid,
                                         Quantity = g.Quantity,
                                         Porate = g.Porate,
                                         Amount = g.Amount,
                                         Discount = g.Discount,
                                         Userid = g.Userid,
                                         Createddate = g.Createddate,
                                         Isdeleted = g.Isdeleted

                                     }
                                     ).ToList()
                               
                                   

                               });
                    return await sql.ToListAsync();

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public async Task<List<POViewModel>> GetPo(int poid)
        //{
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        var pomaster = await context.Pomaster.Where(k => k.Poid == poid).FirstOrDefaultAsync();

        //        if (pomaster == null)
        //        {
        //            return null;
        //        } 
        //        else
        //        {
        //            var sql = from s in context.Pomaster
        //                      join g in context.Podetail on s.Poid equals g.Poid
        //                      select new POViewModel()
        //                      {
        //                          podetailModel = new List<Podetails>() {
        //                              new Podetails() {
        //                              Quantity = g.Quantity,
        //                              Poid=g.Poid,
        //                              Podetailid=g.Podetailid,
        //                              Porate = g.Porate,
        //                              Amount = g.Amount,
        //                              Discount = g.Discount,
        //                              Userid = g.Userid,
        //                              Createddate = g.Createddate,
        //                              Isdeleted = g.Isdeleted} }
        //                      };

        //            return await sql.ToListAsync();
        //        }
        //    }
        //}
        public async Task UpdatePo(POViewModel pOViewModel)
        {
            try
            {
                var dbGroup = new Pomaster();
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    dbGroup = await context.Pomaster.Where(k => k.Poid == pOViewModel.pomastermodel.Poid).FirstOrDefaultAsync();

                    dbGroup.Podate = pOViewModel.pomastermodel.Podate;
                    dbGroup.Userid = pOViewModel.pomastermodel.Userid;
                    dbGroup.Totalamount = pOViewModel.pomastermodel.Totalamount;
                    dbGroup.Createddate = pOViewModel.pomastermodel.Createddate;
                    dbGroup.Discount = pOViewModel.pomastermodel.Discount;
                    dbGroup.Finalamount = pOViewModel.pomastermodel.Finalamount;
                    dbGroup.Isdeleted = pOViewModel.pomastermodel.Isdeleted;

                    await context.SaveChangesAsync();
                    foreach (var item in pOViewModel.podetailModel)
                    {
                        if (item.dbOperation == 1)
                        {
                            Podetail podetail = new Podetail()
                            { Podetailid = item.Podetailid, Poid = item.Poid, Productid = item.Productid, Porate = item.Porate, Amount = item.Amount, Discount = item.Discount, Quantity = item.Quantity, Userid = item.Userid, Createddate = item.Createddate, Isdeleted = item.Isdeleted };
                            await context.Podetail.AddAsync(podetail);
                        }
                        else if (item.dbOperation == 2)
                        {
                            var dbGroups = await context.Podetail.Where(k => k.Podetailid == item.Podetailid).FirstOrDefaultAsync();
                            dbGroups.Porate = item.Porate;
                            dbGroups.Amount = item.Amount;
                            await context.SaveChangesAsync();

                        }
                        else if (item.dbOperation == 3)
                        {
                            var groupStaff = await context.Podetail.Where(k => k.Poid== pOViewModel.pomastermodel.Poid).FirstOrDefaultAsync();
                            if (groupStaff != null)
                            {
                                context.Podetail.Remove(groupStaff);
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
    //public void DeletePomaster(Pomaster pomaster)
    //{
    //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
    //    {
    //        context.Pomaster.Remove(pomaster);
    //        context.SaveChanges();
    //    }
    //}


