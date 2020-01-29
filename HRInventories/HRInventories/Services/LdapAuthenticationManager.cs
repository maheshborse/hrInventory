using HRInventories.Models;
using HRInventories.Services.Interface;
using System;
using System.Collections.Generic;

using System.Security;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;



namespace HRInventories.Services
{
    public class LdapAuthenticationManager : IAuthenticationRepository
    {
        private readonly LdapConfig config;

        PrincipalContext ctx;

        public LdapAuthenticationManager(IOptions<LdapConfig> config)
        {
            this.config = config.Value;

        }

        public User Login(Login login)
        {
            try
            {
                // Additional check to User in perticular group.
              
                User UserObj = new User();

                using (ctx = new PrincipalContext(ContextType.Domain, config.UserDomainName, login.UserName, login.Password))
                {
                    bool bValid = ctx.ValidateCredentials(login.UserName, login.Password, ContextOptions.Negotiate | ContextOptions.Signing | ContextOptions.Sealing);

                    // Additional check to search user in directory.
                    if (bValid)
                    {
                        UserPrincipal prUsr = new UserPrincipal(ctx);
                        prUsr.SamAccountName = login.UserName;
                        PrincipalSearcher srchUser = new PrincipalSearcher(prUsr);

                        using (UserPrincipal foundUsr = srchUser.FindOne() as UserPrincipal)
                        {
                            if (foundUsr != null)
                            {
                                UserObj.Email = foundUsr.EmailAddress;
                                UserObj.DisplayName = foundUsr.DisplayName;
                                UserObj.GivenName = foundUsr.GivenName;
                                UserObj.Surname = foundUsr.Surname;
                                UserObj.isAuthenticated = true;
                            }
                        }
                    }
                }

                return UserinAdminGroup(login, UserObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private User UserinAdminGroup(Login login, User UserObj)
        {
            try
            {
                bool isGroupMember = false;
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, config.UserDomainName))
                {
                    if (context != null)
                    {
                        //UserPrincipal user = UserPrincipal.Current
                        using (UserPrincipal user = UserPrincipal.FindByIdentity(context, login.UserName))
                        {
                            if (user != null)
                            {
                                using (GroupPrincipal group = GroupPrincipal.FindByIdentity(context, config.GroupName))
                                {
                                    if (group != null)
                                    {
                                        isGroupMember = user.IsMemberOf(group);
                                    }
                                }
                            }
                        }
                    }
                }

                if (isGroupMember)
                {
                    UserObj.isAdmin = true;
                }
                return UserObj;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public List<User> GetUsers()
        {
            try
            {
                List<User> allUsers = new List<User>();
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, config.UserDomainName))
                {
                    using (UserPrincipal userPrincipal = new UserPrincipal(context))
                    {
                        using (PrincipalSearcher search = new PrincipalSearcher(userPrincipal))
                        {
                            foreach (UserPrincipal result in search.FindAll())
                                if (!String.IsNullOrEmpty(result.EmployeeId))
                                {
                                    allUsers.Add(new User
                                    {
                                        DisplayName = result.DisplayName,
                                        Email = result.EmailAddress,
                                        GivenName = result.GivenName,
                                        Surname = result.Surname,
                                        Id = result.EmployeeId,
                                    });
                                }
                            var userList = allUsers.OrderBy(s => s.GivenName);

                            return userList.ToList();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
