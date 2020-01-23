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
    public class PomasterDataAccess: IPomasterDataAccess
    {
        Connectionstrings _connectionstring;
        public PomasterDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        //public async Task AddPo(PodetailModel podetail)
        //{
        //    try
        //    {
        //        using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //        {
        //            Podetail pomasters = new Podetail()
        //            {
        //                Poid= podetail.Poid,
        //                Productid= podetail.Productid,
        //                Quantity = podetail.Quantity,
        //                Porate = podetail.Porate,
        //                Discount = podetail.Discount,
        //                Amount = podetail.Amount,
        //                Userid = podetail.Userid,
        //                Createddate = podetail.Createddate,
        //                Isdeleted = podetail.Isdeleted,
        //                Po = new Pomaster()
        //                {
        //                    Podate= podetail.PomasterModels.Podate,
        //                    Totalamount= podetail.PomasterModels.Totalamount,
        //                    Discount= podetail.PomasterModels.Discount,
        //                    Finalamount= podetail.PomasterModels.Finalamount,
        //                    Userid= podetail.PomasterModels.Userid,
        //                    Createddate= podetail.PomasterModels.Createddate,
        //                    Isdeleted= podetail.PomasterModels.Isdeleted
        //                },
        //            };
        //            await context.Podetail.AddAsync(pomasters);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
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
                        Isdeleted = pOViewModel.pomastermodel.Isdeleted
                    };
                    await context.Pomaster.AddAsync(dbGroup);
                    await context.SaveChangesAsync();
                    var id = dbGroup.Poid;
                    var dbGroupIdCheck = await context.Pomaster.Where(k => k.Poid == id).FirstOrDefaultAsync();
                    pOViewModel.pomastermodel = dbGroupIdCheck;

                    foreach (var item in pOViewModel.podetailModel)
                    {
                        Podetail podetail = new Podetail()
                        {Poid=dbGroup.Poid, Productid=item.Productid, Porate=item.Porate, Amount=item.Amount, Discount=item.Discount, Quantity=item.Quantity, Userid=item.Userid, Createddate=item.Createddate,Isdeleted=item.Isdeleted };
                        await context.Podetail.AddAsync(podetail);

                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        //public async Task<List<POViewModel>> GetPomasters()
        //{
        //    try
        //    {
        //        using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //        {
        //            //return context.Podetail.FirstOrDefault(e => e.Podetailid == id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public PodetailModel GetPomasterbyID(long id)
        //{
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        return context.Podetail.FirstOrDefault(e => e.Podetailid== id);
        //    }
        //}
        //public Pomaster UpdatePomaster(Pomaster item)
        //{
        //    var dbPomaster = new Pomaster();
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        dbPomaster = context.Pomaster.Where(k => k.Poid == item.Poid).FirstOrDefault();
        //        dbPomaster.Podate = item.Podate;
        //        dbPomaster.Totalamount = item.Totalamount;
        //        dbPomaster.Discount = item.Discount;
        //        dbPomaster.Finalamount = item.Finalamount;
        //        dbPomaster.Userid = item.Userid;
        //        dbPomaster.Createddate = item.Createddate;
        //        dbPomaster.Isdeleted = item.Isdeleted;
        //        context.SaveChanges();
        //    }
        //    return dbPomaster;
        //}
        //public void DeletePomaster(Pomaster pomaster)
        //{
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        context.Pomaster.Remove(pomaster);
        //        context.SaveChanges();
        //    }
        //}
    }
}
