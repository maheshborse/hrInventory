using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class RequestDataAccess: IRequestDataAccess
    {
       
        private readonly IOptions<LdapConfig> config;
        Connectionstrings _connectionstring;
        public RequestDataAccess(Connectionstrings connectionstring, IOptions<LdapConfig> config)
        {
            _connectionstring = connectionstring;
            this.config = config;
        }

        public async Task AddRequest(RequestViewModel requestViewModel)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Reqestmaster dbGroup = new Reqestmaster()
                    {
                        Employeeid = requestViewModel.Reqestmastermodel.Employeeid,
                        Isread = requestViewModel.Reqestmastermodel.Isread,
                        Userid = requestViewModel.Reqestmastermodel.Userid,
                        Createddate = requestViewModel.Reqestmastermodel.Createddate,
                        Isdeleted = requestViewModel.Reqestmastermodel.Isdeleted,
                    };
                    await context.Reqestmaster.AddAsync(dbGroup);
                    await context.SaveChangesAsync();
                    var id = dbGroup.Requestid;
                    var dbGroupIdCheck = await context.Pomaster.Where(k => k.Poid == id).FirstOrDefaultAsync();

                    foreach (var item in requestViewModel.RequestdetailModel)
                    {
                        Requestdetail detail = new Requestdetail()
                        { Requestid = dbGroup.Requestid, Productid = item.Productid, Status=item.Status, Quantity = item.Quantity, Userid = item.Userid, Createddate = item.Createddate, Isdeleted = item.Isdeleted };
                        await context.Requestdetail.AddAsync(detail);

                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<ReqestMasterModel>> GetReqest()
        {
            try
            {
                LdapAuthenticationManager _LdapAuthentication = new LdapAuthenticationManager(config);
                var user =  _LdapAuthentication.GetUsers();
                
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    var sql = context.Reqestmaster.Include(p => p.Requestdetail).Select(s =>
                                 new ReqestMasterModel()
                                 {
                                     Employeeid =s.Employeeid,
                                     Isread=s.Isread,
                                     Userid = s.Userid,
                                     Createddate = s.Createddate,
                                     Isdeleted = s.Isdeleted,
                                     users = user.Where(k => k.Id == s.Employeeid).ToList(),
                                     requestDetailModels = s.Requestdetail.Select(g => new RequestDetailModel
                                     {
                                         Productid = g.Productid,
                                         Quantity = g.Quantity,
                                         Userid = g.Userid,
                                         Status=g.Status,
                                         Createddate = g.Createddate,
                                         Isdeleted = g.Isdeleted,
                                         ProductModels = new ProductModel
                                         {
                                             Productname = g.Product.Productname,
                                             Productdescription = g.Product.Productdescription,
                                             Createddate = g.Product.Createddate,
                                             Isdeleted = g.Product.Isdeleted,
                                             Categoryid=g.Product.Categoryid,
                                             Category= new CatagoryModel
                                             {
                                                 Categoryname=g.Product.Category.Categoryname,
                                                 Categorydescription=g.Product.Category.Categorydescription,
                                             }
                                         },
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

    }
}
