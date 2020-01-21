using System;
using System.Data;
using System.Collections.Generic;
//using System.Linq;
using Finsoft.Data;
using System.IO;

namespace Finsoft.Rules
{
	public partial class SharedBusinessRules : Finsoft.Data.BusinessRules
    {
        
        public SharedBusinessRules()
        {
        }
        
        protected override void EnumerateDynamicAccessControlRules(string controllerName)
        {
            //base.EnumerateDynamicAccessControlRules(controllerName);
           
            if (controllerName.Equals("OrderHeader", StringComparison.OrdinalIgnoreCase))
            {
                
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0}"
                       , 1)
                       , AccessPermission.Allow
                       );
                }
                else 
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID={1} "
                       , UserName, 1)
                       , AccessPermission.Allow
                       );

                }                

            }

            if (controllerName.Equals("OrdersWaitingForPriceChange", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0}"
                       , 2)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if(UserIsInRole("SalesPerson"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID={1} "
                       , UserName, 2)
                       , AccessPermission.Allow
                       );
                    }
                    else if(UserIsInRole("SalesClerk"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 2)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID={1} "
                       , UserName, 2)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID={1} "
                       , UserName, 2)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }


                }

            }

            if (controllerName.Equals("OrdersWaitingForApproval", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 5,6)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesPerson"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID IN ({1},{2}) "
                       , UserName, 5, 6)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesClerk"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select a.OrderHedID from OrderHed a inner join ( select OrderHedID From OrderStatusLog Where UserID={0}) b ON a.OrderHedID=b.OrderHedID Where a.OrderStatusID IN ({1},{2}) "
                       , UserId, 5, 6)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("Owner"
                       , string.Format("Select [UserName] from Users Where ManagerID={0} "
                       , UserId)
                       , AccessPermission.Allow
                       );

                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 5, 6)
                       , AccessPermission.Allow
                       );
                    }                    
                    else if(UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 6)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }

                    
                }

            }

            if (controllerName.Equals("OrdersApproved", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0}"
                       , 7)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesPerson"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID = {1} "
                       , UserName, 7)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesClerk"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select a.OrderHedID from OrderHed a inner join ( select OrderHedID From OrderStatusLog Where UserID={0}) b ON a.OrderHedID=b.OrderHedID Where a.OrderStatusID = {1} "
                       , UserId, 7)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("Owner"
                       , string.Format("Select [UserName] from Users Where ManagerID={0} "
                       , UserId)
                       , AccessPermission.Allow
                       );

                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 7)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select a.OrderHedID from OrderHed a inner join ( select OrderHedID From OrderStatusLog Where UserID={0}) b ON a.OrderHedID=b.OrderHedID Where a.OrderStatusID = {1} "
                       , UserId, 7)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }
                    
                }

            }
            if (controllerName.Equals("OrdersRejected", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0}"
                       , 8)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesPerson"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where (Creator='{0}' Or [Owner]='{0}') AND OrderStatusID={1} "
                       , UserName, 8)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesClerk"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select a.OrderHedID from OrderHed a inner join ( select OrderHedID From OrderStatusLog Where UserID={0}) b ON a.OrderHedID=b.OrderHedID Where a.OrderStatusID = {1} "
                       , UserId, 8)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("Owner"
                       , string.Format("Select [UserName] from Users Where ManagerID={0} "
                       , UserId)
                       , AccessPermission.Allow
                       );

                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 8)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select a.OrderHedID from OrderHed a inner join ( select OrderHedID From OrderStatusLog Where UserID={0}) b ON a.OrderHedID=b.OrderHedID Where a.OrderStatusID = {1} "
                       , UserId, 8)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }
                    
                }

            }
            //ApprovalNormalOrders
            if (controllerName.Equals("ApprovalNormalOrders", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 5, 6)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesManager"))
                    {
                        //  RegisterAccessControlRule("OrderHedID"
                        //, string.Format("Select OrderHedID From OrderHed where Exception=0 AND OrderStatusID IN ({0})"
                        //, 5)
                        //, AccessPermission.Allow
                        //);

                        //  RegisterAccessControlRule("Owner"
                        // , string.Format("Select [UserName] from Users Where ManagerID={0} "
                        // , UserId)
                        // , AccessPermission.Allow
                        // );
                        //*************************************************//

                        var top1OrderHedID = SqlText.Execute("Select OrderHedID From OrderHed a inner join Users b on a.[Owner]=b.UserName where a.Exception=0 AND a.OrderStatusID = 5 and b.ManagerID=@ManagerID",
                            new { @ManagerID = UserId });
                        if (top1OrderHedID != null)
                        {
                            //Show related records
                            string query = string.Format("Select OrderHedID From dbo.OrderHed inner join Users on [Owner]=UserName where Exception=0 AND OrderStatusID =5 and ManagerID={0} "
                                , UserId);

                            RegisterAccessControlRule("OrderHedID"
                                , query
                                , AccessPermission.Allow
                                );
                        }
                        else
                        {
                            //Show none of the records
                            RegisterAccessControlRule("OrderHedID"
                               , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                               , 0)
                               , AccessPermission.Allow
                               );
                        }


                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where Exception=0 AND OrderStatusID={0} "
                       , 6)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }


                }

            }
            //ApprovalExceptionOrders
            if (controllerName.Equals("ApprovalExceptionOrders", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 5, 6)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesManager"))
                    {
                       // RegisterAccessControlRule("Owner"
                       //, string.Format("Select [UserName] from Users Where ManagerID={0} "
                       //, UserId)
                       //, AccessPermission.Allow
                       //);

                       // RegisterAccessControlRule("OrderHedID"
                       //, string.Format("Select OrderHedID From OrderHed where Exception=1 AND OrderStatusID IN ({0})"
                       //, 5)
                       //, AccessPermission.Allow
                       //);

                        var top1OrderHedID = SqlText.ExecuteScalar("Select Top 1 OrderHedID From OrderHed a inner join Users b on a.[Owner]=b.UserName where a.Exception=1 AND a.OrderStatusID = 5 and b.ManagerID=@ManagerID",
                            new { @ManagerID = UserId });
                        if (top1OrderHedID != null)
                        {
                            //Show related records
                            string query = string.Format("Select OrderHedID From OrderHed  inner join Users  on [Owner]=UserName where Exception=1 AND OrderStatusID =5 and ManagerID={0} "
                                , UserId);

                            RegisterAccessControlRule("OrderHedID"
                                , query
                                , AccessPermission.Allow
                                );
                        }
                        else
                        {
                            //Show none of the records
                            RegisterAccessControlRule("OrderHedID"
                               , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                               , 0)
                               , AccessPermission.Allow
                               );
                        }
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where Exception=1 AND OrderStatusID={0} "
                       , 6)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }


                }

            }
            //ApprovedOrders
            if (controllerName.Equals("ApprovedOrders", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 7, 9)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("Owner"
                       , string.Format("Select [UserName] from Users Where ManagerID={0} "
                       , UserId)
                       , AccessPermission.Allow
                       );

                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 7, 9)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0},{1})"
                       , 7, 9)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }


                }

            }
            //RejectedOrders
            if (controllerName.Equals("RejectedOrders", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("Administrators"))
                {
                    RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0})"
                       , 8)
                       , AccessPermission.Allow
                       );
                }
                else
                {
                    if (UserIsInRole("SalesManager"))
                    {
                        RegisterAccessControlRule("Owner"
                       , string.Format("Select [UserName] from Users Where ManagerID={0} "
                       , UserId)
                       , AccessPermission.Allow
                       );

                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0})"
                       , 8)
                       , AccessPermission.Allow
                       );
                    }
                    else if (UserIsInRole("MarketingManager"))
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID IN ({0})"
                       , 8)
                       , AccessPermission.Allow
                       );
                    }
                    else
                    {
                        RegisterAccessControlRule("OrderHedID"
                       , string.Format("Select OrderHedID From OrderHed where OrderStatusID={0} "
                       , 0)
                       , AccessPermission.Allow
                       );
                    }


                }

            }

            if (!string.IsNullOrEmpty(UserName) && !UserIsInRole("Administrators"))
            {
                if (controllerName.Equals("Users", StringComparison.OrdinalIgnoreCase))
                {
                    RegisterAccessControlRule("UserName", AccessPermission.Allow, UserName);
                    return ;
                }
                if (controllerName.Equals("Roles", StringComparison.OrdinalIgnoreCase))
                {
                    RegisterAccessControlRule("RoleName", AccessPermission.Allow, UserRoles);
                    return;
                }
                RegisterAccessControlRule("Company"
                   , string.Format("select a.CompanyName from Company a inner join UsersCompany b on a.CompanyID=b.CompanyID inner join Users c on b.UserID=c.UserID Where c.UserName='{0}' ", UserName)
                   , AccessPermission.Allow);
                RegisterAccessControlRule("CompanyName"
                   , string.Format("select a.CompanyName from Company a inner join UsersCompany b on a.CompanyID=b.CompanyID inner join Users c on b.UserID=c.UserID Where c.UserName='{0}' ", UserName)
                   , AccessPermission.Allow);

                //if (UserIsInRole("SalesClerk"))
                //{
                //    RegisterAccessControlRule("OrderHedID"
                //       , string.Format("Select OrderHedID From OrderHed where Creator='{0}' Or OrderStatusID={1}"
                //       , UserName,2)
                //       , AccessPermission.Allow
                //       );
                //}
                //if (UserIsInRole("SalesPerson"))
                //{
                //    RegisterAccessControlRule("OrderHedID"
                //       , string.Format("Select OrderHedID From OrderHed where Creator='{0}' Or [Owner]='{0}'"
                //       , UserName)
                //       , AccessPermission.Allow
                //       );
                //}
                //if (UserIsInRole("SalesManager"))
                //{
                //    RegisterAccessControlRule("Owner"
                //       , string.Format("Select [UserName] from [Users] Where ManagerID={0}",UserId)
                //       , AccessPermission.Allow
                //       );
                //    //RegisterAccessControlRule("Creator", AccessPermission.Allow
                //    //   , UserName
                //    //   );
                //}
                //if (UserIsInRole("MarketingManager"))
                //{
                    
                //}

            }
            else
            {

            }
               


        }

        public override bool SupportsVirtualization(string controllerName)
        {
            //COT Tables
            if (controllerName.Equals("Company", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("Orders", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderHeader", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderDtl", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderDtlVw", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderDtlVw2", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderRel", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderRelVw", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderRelVw2", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderStatus", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("Promotion", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderStatusLog", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderStatusLogV2", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderStatusLogV2Clerk", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrderStatusLogV2Approval", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrdersWaitingForApproval", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrdersWaitingForPriceChange", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrdersApproved", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("OrdersRejected", StringComparison.OrdinalIgnoreCase))
                return true;

            //Approval
            if (controllerName.Equals("ApproveOrders", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("ApprovalNormalOrders", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("ApprovalExceptionOrders", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("ApprovedOrders", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("RejectedOrders", StringComparison.OrdinalIgnoreCase))
                return true;

            //COT Views
            if (controllerName.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("Part", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("PriceList", StringComparison.OrdinalIgnoreCase))
                return true;                       
            if (controllerName.Equals("ShipTo", StringComparison.OrdinalIgnoreCase))
                return true;

            //COT Membership
            if (controllerName.Equals("Membership", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("Roles", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("UserRoles", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("Users", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("UsersCompany", StringComparison.OrdinalIgnoreCase))
                return true;
            if (controllerName.Equals("AuditUsers", StringComparison.OrdinalIgnoreCase))
                return true;

            return base.SupportsVirtualization(controllerName);
        }
        protected override void VirtualizeController(string controllerName)
        {
            base.VirtualizeController(controllerName);

            NodeSet().SelectActionGroup("ag5").Delete();
            NodeSet().SelectActionGroup("ag7").Delete();
            if (controllerName.Equals("Users", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();

                    NodeSet().SelectViews().SelectDataFields("UserName", "ManagerID", "Roles", "Company").SetTextMode("Static");
                }

            }
            if (controllerName.Equals("AuditUsers", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();

                    NodeSet().SelectViews().SelectDataFields("UserName", "RoleName", "UserID", "RoleID").SetTextMode("Static");
                }

            }
            if (controllerName.Equals("Roles", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();

                }

            }
            if (controllerName.Equals("Membership", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                    .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

            }
            if (controllerName.Equals("Part", StringComparison.OrdinalIgnoreCase)
                   || controllerName.Equals("PriceList", StringComparison.OrdinalIgnoreCase)
                   || controllerName.Equals("ShipTo", StringComparison.OrdinalIgnoreCase)
                   || controllerName.Equals("Customer", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }

            }
            if (controllerName.Equals("Company", StringComparison.OrdinalIgnoreCase)
                   || controllerName.Equals("OrderStatus", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }

            }

            if (controllerName.Equals("OrdersWaitingForApproval", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

                NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();

                if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                       .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }

                    NodeSet().SelectViews().SelectDataFields("OrderStatusID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("CustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustomerName").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ShipToName").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("Creator").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CreatedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedBy").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CompanyID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Owner").SetTextMode("Static");


                NodeSet().SelectView("grid1")
                    .SelectDataFields("OrderStatusID", "CustNum", "ShipToName", "ReqShipDate", "PromotionID"
                    , "OrderComments", "PromotionRemarks", "Owner", "Creator", "CreatedOn"
                    , "ModifiedBy", "ModifiedOn", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");

                NodeSet().SelectView("editForm1").SelectDataFields("CustNum", "OrderStatusID", "TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }
            if(controllerName.Equals("OrderDtl", StringComparison.OrdinalIgnoreCase))
            {

                if(UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectViews().SelectDataFields("Focqty").SetTextMode("Static");
                    NodeSet().SelectViews().SelectDataFields("ChangeSellingPrice").SetTextMode("Static");
                    NodeSet().SelectViews().SelectDataFields("ProposedSellingPrice").SetTextMode("Static");
                    NodeSet().SelectViews().SelectDataFields("ChangeBasePrice").SetTextMode("Static"); 
                    NodeSet().SelectViews().SelectDataFields("Amount").SetTextMode("Static");

                }
            }

               

            
            if (controllerName.Equals("OrdersWaitingForPriceChange", StringComparison.OrdinalIgnoreCase))
            {

                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                    .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                //NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();



                NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();
                if (!UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActionGroup("ag1").SelectAction("a100").Delete();
                    NodeSet().SelectActionGroup("ag1").SelectAction("a101").Delete();
                    NodeSet().SelectActionGroup("ag100").Delete();
                    NodeSet().SelectActionGroup("ag101").Delete();
                }
                NodeSet().SelectViews().SelectDataFields("OrderStatusID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("CustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustomerName").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ShipToName").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("Creator").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CreatedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedBy").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CompanyID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Owner").SetTextMode("Static");


                NodeSet().SelectView("grid1")
                    .SelectDataFields("OrderStatusID", "CustNum", "ShipToName", "ReqShipDate", "PromotionID"
                    , "OrderComments", "PromotionRemarks", "Owner", "Creator", "CreatedOn"
                    , "ModifiedBy", "ModifiedOn", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");

                NodeSet().SelectView("editForm1").SelectDataFields("CustNum", "OrderStatusID", "TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }
            if (controllerName.Equals("OrdersApproved", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

                NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();

                NodeSet().SelectViews().SelectDataFields("OrderStatusID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("CustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustomerName").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ShipToName").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("Creator").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CreatedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedBy").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CompanyID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Owner").SetTextMode("Static");


                NodeSet().SelectView("grid1")
                    .SelectDataFields("OrderStatusID", "CustNum", "ShipToName", "ReqShipDate", "PromotionID"
                    , "OrderComments", "PromotionRemarks", "Owner", "Creator", "CreatedOn"
                    , "ModifiedBy", "ModifiedOn", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");

                NodeSet().SelectView("editForm1").SelectDataFields("CustNum", "OrderStatusID", "TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }
            if (controllerName.Equals("OrdersRejected", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

                NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();

                NodeSet().SelectViews().SelectDataFields("OrderStatusID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("CustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustomerName").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ShipToName").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("Creator").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CreatedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedBy").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CompanyID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Owner").SetTextMode("Static");


                NodeSet().SelectView("grid1")
                    .SelectDataFields("OrderStatusID", "CustNum", "ShipToName", "ReqShipDate", "PromotionID"
                    , "OrderComments", "PromotionRemarks", "Owner", "Creator", "CreatedOn"
                    , "ModifiedBy", "ModifiedOn", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");

                NodeSet().SelectView("editForm1").SelectDataFields("CustNum", "OrderStatusID", "TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }

            if (controllerName.Equals("Orders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
            }
            if (controllerName.Equals("OrderHeader", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Duplicate", "Import")
                    .Delete();
                NodeSet().SelectActionGroup("ag1").SelectAction("a101").Delete();
                //NodeSet().SelectActionGroup("ag3").Delete();
                //NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

                NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();

                NodeSet().SelectViews().SelectDataFields("OrderStatusID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("CustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CustomerName").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ShipToName").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("Creator").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CreatedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedBy").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("ModifiedOn").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("CompanyID").SetTextMode("Static");
                if (!UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectViews().SelectDataFields("Owner").SetTextMode("Static");
                }

                NodeSet().SelectView("grid1")
                    .SelectDataFields("OrderStatusID", "CustNum", "ShipToName", "ReqShipDate", "PromotionID"
                    , "OrderComments", "PromotionRemarks", "Owner", "Creator", "CreatedOn"
                    , "ModifiedBy", "ModifiedOn", "CompanyName", "CompanyID", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");

                NodeSet().SelectView("editForm1")
                    .SelectDataFields("CustNum", "OrderStatusID", "Exception", "TargetReportingDate").Hide();

                NodeSet().SelectView("createForm1")
                    .SelectDataFields("CustNum", "OrderStatusID", "Exception", "TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }


            if (controllerName.Equals("OrderDtl", StringComparison.OrdinalIgnoreCase))
            {

                NodeSet().SelectViews().SelectDataFields("PartNum").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("PartDescription").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Uom").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCompany").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCustNum").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("OHShipByDate").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToSysRowID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToName").SetTextMode("Static");

                //NodeSet().SelectActionGroup("ag2").SelectAction("a7").Delete();

                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        (
                        "Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();
                    NodeSet().SelectView("editForm1").SelectDataFields
                        ("PartSysRowID",
                        //, "Uom",
                        //"SellingPrice",
                        //"ChangeSellingPrice",
                        //"ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();

                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }
                else if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        ("Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();
                    NodeSet().SelectView("editForm1").SelectDataFields
                        (
                        //"PartSysRowID",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID"
                        ).Hide();
                    NodeSet().SelectView("createForm1").SelectDataFields
                        ("PartNum",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID"
                        ).Hide();
                    NodeSet().SelectActions("Duplicate", "Import")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag2").Delete();
                    //NodeSet().SelectActionGroup("ag3").Delete();
                    //NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }
                else if (UserIsInRole("SalesPerson"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        ("Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();
                    NodeSet().SelectView("editForm1").SelectDataFields
                        (
                        //"PartSysRowID",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();
                    NodeSet().SelectView("createForm1").SelectDataFields
                        ("PartNum",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();
                }
            }

            if (controllerName.Equals("OrderDtlVw", StringComparison.OrdinalIgnoreCase))
            {

                NodeSet().SelectViews().SelectDataFields("PartSysRowID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("PartDescription").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Uom").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCompany").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCustNum").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("OHShipByDate").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToSysRowID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToName").SetTextMode("Static");

                if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                      .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }
                else
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();

                    NodeSet().SelectViews()
                        .SelectDataFields("BasePrice", "SellingPrice", "SellingPricePerBottle").SetTextMode("Static");
                }

                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        ("Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();
                    NodeSet().SelectView("editForm1").SelectDataFields
                        ("PartSysRowID"
                        , "Uom",
                        "SellingPrice",
                        "SellingPricePerBottle",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();


                }
                else if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        ("Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder"
                        ).Hide();
                    NodeSet().SelectViews()
                    .SelectDataFields("PartNum"
                    //, "ChangeSellingPrice"
                    //, "ChangeBasePrice"
                    )
                    .SetTextMode("Static");

                }
                else if (UserIsInRole("SalesPerson"))
                {
                    NodeSet().SelectView("grid1").SelectDataFields
                        ("Uom",
                        "ChangeSellingPrice",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderHedBulkOrder",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();
                    NodeSet().SelectView("editForm1").SelectDataFields
                        (
                        //"PartSysRowID",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID",
                        "OrderHedBulkOrder",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();
                    NodeSet().SelectView("createForm1").SelectDataFields
                        ("PartNum",
                        "ChangeBasePrice",
                        "ProposedBasePrice",
                        "OHCompany",
                        "OHCustNum",
                        "OHShipByDate",
                        "OHShipToSysRowID",
                        "OHShipToName",
                        "OrderDtlID",
                        "OrderHedBulkOrder",
                        "BasePrice", "CtnConv", "BasePricePerBottle", "ProposedBasePricePerBottle"
                        ).Hide();

                    NodeSet().SelectViews()
                    .SelectDataFields("OrderQty")
                    .SetTextMode("Static");
                }

            }

            if (controllerName.Equals("OrderDtlVw2", StringComparison.OrdinalIgnoreCase))
            {

                NodeSet().SelectViews().SelectDataFields("PartSysRowID").SetTextMode("Static");

                NodeSet().SelectViews().SelectDataFields("PartDescription").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("Uom").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCompany").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHCustNum").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OrderQty").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipByDate").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToSysRowID").SetTextMode("Static");
                NodeSet().SelectViews().SelectDataFields("OHShipToName").SetTextMode("Static");

                if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    //NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }
                else
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();

                    NodeSet().SelectViews()
                        .SelectDataFields("BasePrice", "SellingPrice", "SellingPricePerBottle").SetTextMode("Static");
                }
            }

            if (controllerName.Equals("OrderRel", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectViews("grid1", "editForm1", "createForm1").SelectDataFields
                        ("OHCompany",
                        "OHCustNum",
                        "OHCustID"
                        ).Hide();

                //if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager") || UserIsInRole("SalesClerk"))
                if (UserIsInRole("SalesPerson"))
                {
                   
                }
                else if(UserIsInRole("SalesClerk"))
                {

                }

                if (UserIsInRole("SalesManager", "MarketingManager"))
                {
                    //NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                    //    .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                    //NodeSet().Hide();

                }


            }


            if (controllerName.Equals("OrderRelVw", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectViews("grid1", "editForm1", "createForm1").SelectDataFields
                        ("OHCompany",
                        "OHCustNum",
                        "OHCustID",
                        "OrderHedBulkOrder"
                        ).Hide();

                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                    .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                
            }
            if (controllerName.Equals("OrderRelVw2", StringComparison.OrdinalIgnoreCase))
            {
                //This is for only PriceChange Tab
                NodeSet().SelectViews("grid1", "editForm1", "createForm1").SelectDataFields
                        ("OHCompany",
                        "OHCustNum",
                        "OHCustID",
                        "OrderHedBulkOrder"
                        ).Hide();
                if (UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                    .Delete();
                    //NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                }                
                else 
                {
                    //NodeSet().Hide();
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }
            }
            if (controllerName.Equals("OrderStatusLogV2", StringComparison.OrdinalIgnoreCase))
            {

            }
            if (controllerName.Equals("OrderStatusLogV2Clerk", StringComparison.OrdinalIgnoreCase))
            {
                //NodeSet().SelectField("Status")
                //    .SetItemsStyle("DropDownList")
                //    .CreateItem("5", "Price Change Done")
                //    .CreateItem("8", "Price Change Not Done");
            }

            if (controllerName.Equals("OrderStatusLogV2Approval", StringComparison.OrdinalIgnoreCase))
            {
                //NodeSet().SelectField("Status")
                //    .SetItemsStyle("DropDownList")
                //    .CreateItem("7", "Approved")
                //    .CreateItem("8", "Rejected");
            }

                //Approval Management
            if (controllerName.Equals("ApproveOrders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                //NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                NodeSet().SelectView("grid1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("editForm1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();
            }

            if (controllerName.Equals("ApprovalNormalOrders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();

                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                NodeSet().SelectActionGroup("ag9").Delete();

                NodeSet().SelectView("grid1")
                    .SelectDataFields
                    ("CustNum", "CustID", "ShipToName", "ReqShipDate", "OrderType",
                    "PromotionID", "OrderComments", "PromotionRemarks", "Creator", "CreatedOn",
                    "ModifiedBy", "ModifiedOn", "Exception", "TargetReportingDate")
                    .Hide();
                NodeSet().SelectView("editForm1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();

                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");
                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectActionGroup("ag2").SelectAction("a4").Delete();
                    NodeSet().SelectView("editForm1").SelectDataFields("CompanyID", "CompanyName").Hide();
                    NodeSet().SelectView("editForm1")
                        .SelectDataFields("CompanyID", "OrderHedID", "OrderDate", "CustID", "CustomerName", "TotalAmount"
                        , "Owner")
                        .SetTextMode("Static");


                }
                if (UserIsInRole("SalesPerson") || UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }
                if (UserIsInRole("SalesClerk"))
                {
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a101").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a101").Delete();
                }
            }

            if (controllerName.Equals("ApprovalExceptionOrders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();

                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                NodeSet().SelectActionGroup("ag9").Delete();

                NodeSet().SelectView("grid1")
                    .SelectDataFields
                    ("CustNum", "CustID", "ShipToName", "ReqShipDate", "OrderType",
                    "PromotionID", "OrderComments", "PromotionRemarks", "Creator", "CreatedOn",
                    "ModifiedBy", "ModifiedOn", "Exception", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");
                NodeSet().SelectView("editForm1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();

                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectActionGroup("ag2").SelectAction("a4").Delete();
                    NodeSet().SelectView("editForm1").SelectDataFields("CompanyID", "CompanyName").Hide();
                    NodeSet().SelectView("editForm1")
                        .SelectDataFields("CompanyID", "OrderHedID", "OrderDate", "CustID", "CustomerName", "TotalAmount"
                        , "Owner")
                        .SetTextMode("Static");


                }
                if (UserIsInRole("SalesPerson") || UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }
                if (UserIsInRole("SalesClerk"))
                {
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a101").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a101").Delete();
                }
            }
            if (controllerName.Equals("ApprovedOrders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                NodeSet().SelectActionGroup("ag9").Delete();

                NodeSet().SelectView("grid1")
                    .SelectDataFields
                    ("CustNum", "CustID", "ShipToName", "ReqShipDate", "OrderType",
                    "PromotionID", "OrderComments", "PromotionRemarks", "Creator", "CreatedOn",
                    "ModifiedBy", "ModifiedOn", "Exception", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");
                NodeSet().SelectView("editForm1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();

                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectActionGroup("ag2").SelectAction("a4").Delete();
                    NodeSet().SelectView("editForm1").SelectDataFields("CompanyID", "CompanyName").Hide();
                    NodeSet().SelectView("editForm1")
                        .SelectDataFields("CompanyID", "OrderHedID", "OrderDate", "CustID", "CustomerName", "TotalAmount"
                        , "Owner")
                        .SetTextMode("Static");


                }
                if (UserIsInRole("SalesPerson") || UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }
                if (UserIsInRole("SalesClerk"))
                {
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a101").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a101").Delete();
                }
            }
            if (controllerName.Equals("RejectedOrders", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();
                NodeSet().SelectActionGroup("ag9").Delete();

                NodeSet().SelectView("grid1")
                    .SelectDataFields
                    ("CustNum", "CustID", "ShipToName", "ReqShipDate", "OrderType",
                    "PromotionID", "OrderComments", "PromotionRemarks", "Creator", "CreatedOn",
                    "ModifiedBy", "ModifiedOn", "Exception", "TargetReportingDate")
                    .Hide();
                //NodeSet().SelectView("grid1").SetFilter("OrderHedID=20");
                NodeSet().SelectView("editForm1").SelectDataFields("TargetReportingDate").Hide();
                NodeSet().SelectView("createForm1").SelectDataFields("TargetReportingDate").Hide();

                if (UserIsInRole("SalesManager") || UserIsInRole("MarketingManager"))
                {
                    NodeSet().SelectActionGroup("ag2").SelectAction("a4").Delete();
                    NodeSet().SelectView("editForm1").SelectDataFields("CompanyID", "CompanyName").Hide();
                    NodeSet().SelectView("editForm1")
                        .SelectDataFields("CompanyID", "OrderHedID", "OrderDate", "CustID", "CustomerName", "TotalAmount"
                        , "Owner")
                        .SetTextMode("Static");


                }
                if (UserIsInRole("SalesPerson") || UserIsInRole("SalesClerk"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    //NodeSet().SelectActionGroup("ag1").Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }
                if (UserIsInRole("SalesClerk"))
                {
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a100").Delete();
                    //NodeSet().SelectActionGroup("ag1").SelectActions("a101").Delete();
                    //NodeSet().SelectActionGroup("ag2").SelectActions("a101").Delete();
                }
            }

            if (controllerName.Equals("OrderStatusLog", StringComparison.OrdinalIgnoreCase))
            {
                NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                NodeSet().SelectActionGroup("ag2").Delete();
                NodeSet().SelectActionGroup("ag3").Delete();
                NodeSet().SelectActionGroup("ag4").Delete();
                NodeSet().SelectActionGroup("ag6").Delete();
                NodeSet().SelectActionGroup("ag8").Delete();

            }
               
            
            //Promotion
            if (controllerName.Equals("Promotion", StringComparison.OrdinalIgnoreCase))
            {
                if (!UserIsInRole("Administrators"))
                {
                    NodeSet().SelectActions("Edit", "Delete", "Duplicate", "Import", "New")
                        .Delete();
                    NodeSet().SelectActionGroup("ag2").Delete();
                    NodeSet().SelectActionGroup("ag3").Delete();
                    NodeSet().SelectActionGroup("ag4").Delete();
                    NodeSet().SelectActionGroup("ag5").Delete();
                    NodeSet().SelectActionGroup("ag6").Delete();
                    NodeSet().SelectActionGroup("ag7").Delete();
                    NodeSet().SelectActionGroup("ag8").Delete();
                    NodeSet().SelectActionGroup("ag9").Delete();
                }


            }
        }

        public override bool VirtualizeControllerConditionally(string controllerName)
        {
            if (controllerName.Equals("Part", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("SalesPerson"))
                {
                    //var OrderType = Convert.ToString(SelectFieldValue("OrderType"));
                    //var CtxOrderType = Convert.ToString(SelectFieldValue("ContextFields_ExOrderType"));
                    //NodeSet().SelectView("grid1").SetFilter("Ium = 'ctn' ");
                }
                
            }
            if (controllerName.Equals("OrderHeader", StringComparison.OrdinalIgnoreCase))
            {
                if (UserIsInRole("SalesPerson"))
                {
                    //var OrderHedID = Convert.ToInt32(SelectFieldValue("OrderHedID"));
                    //int i = (int)SqlText.ExecuteScalar("Select isnull(Count(OrderDtlID),0) from OrderDtl Where OrderHedID=@OrderHedID", OrderHedID);
                    //if (i > 0)
                    //{
                    //    //NodeSet().SelectViews()
                    //    //    .SelectDataFields(
                    //    //    "CustSysRowID"
                    //    //    , "ShipToSysRowID"
                    //    //    , "OrderType").SetReadOnly(true);

                    //}
                    //else
                    //{
                    //    //NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("CustSysRowID").SetTextMode("Auto");
                    //    //NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("ShipToSysRowID").SetTextMode("Auto");
                    //}
                    //NodeSet().CreateActionGroup("ag100").SetScope("ActionBar").SetHeaderText("SubmitForApproval").SetFlat(true);
                    //NodeSet().SelectActionGroup("ag100").CreateAction("Custom", "SubmitForApproval")
                    //.SetHeaderText("Submit For Approval").Attr("cssClass", "material-icon-group-add");

                    //NodeSet().SelectActionGroup("ag1").CreateAction("Custom", "SubmitForApproval").SetConfirmation("_controller=OrderStatusLogV2")
                    //               .SetHeaderText("Submit For Approval").Attr("cssClass", "material-icon-group-add");

                    //NodeSet().SelectActionGroup("ag1").CreateAction("Custom", "SubmitForPriceChange").SetConfirmation("_controller=OrderStatusLogV2")
                    //        .SetHeaderText("Submit For Price Change").Attr("cssClass", "material-icon-group-add");
                }

            }
            if (controllerName.Equals("OrderDtl", StringComparison.OrdinalIgnoreCase))
            {
                var ordhedid = Convert.ToInt32(SelectFieldValue("OrderHedID"));
                
                //int i = (int)SqlText.ExecuteScalar("Select Count(OrderHedID) from OrderHed where OrderHedID=@OrderHedID and BulkOrder=1 "
                //                , new { @OrderHedID = ordhedid });
                //if (i >= 1)               
                //{
                //    NodeSet().SelectViews("editForm1", "createForm1")
                //        .SelectDataFields("OrderQty")
                //        .SetTextMode("Static");
                //}

            }
            if (controllerName.Equals("OrderRel", StringComparison.OrdinalIgnoreCase))
            {
                var ordhedid = Convert.ToInt32(SelectFieldValue("OrderHedID"));
                //var comp = SelectFieldValue("OHCompany"); 
                //var custNum = SelectFieldValue("OHCustNum");
                //var custid = SelectFieldValue("OHCustID");

                //int i = (int) SqlText.ExecuteScalar("Select Count(SysRowID) from vwCustomer where Company=@Company and CustID=@CustID "
                //                , new { @Company = comp, @CustID = custid });
                int i = (int)SqlText.ExecuteScalar("Select Count(OrderHedID) from OrderHed where OrderHedID=@OrderHedID and BulkOrder=1 "
                                , new { @OrderHedID = ordhedid });
                if (i<=0)
                {
                    NodeSet().SelectActions("Delete", "Duplicate", "Import", "New")
                       .Delete();
                    
                }
                else
                {
                    NodeSet().SelectViews("editForm1","createForm1")
                        .SelectDataFields("OrderQty")
                        .SetTextMode("Static");
                }
                
            }
            if (controllerName.Equals("OrdersWaitingForPriceChange", StringComparison.OrdinalIgnoreCase))
            {
                //if (UserIsInRole("SalesClerk"))
                //{
                //    NodeSet().SelectActionGroup("ag1").CreateAction("Custom", "ApprovePriceChange").SetConfirmation("_controller=OrderStatusLogV2Clerk")
                //                   .SetHeaderText("Approve PriceChange").Attr("cssClass", "material-icon-group-add");
                //}
            }
            if (controllerName.Equals("OrdersWaitingForApproval", StringComparison.OrdinalIgnoreCase))
            {
                //if (UserIsInRole("SalesManager"))
                //{
                //    NodeSet().SelectActionGroup("ag1").CreateAction("Custom", "Lv1Approval").SetConfirmation("_controller=OrderStatusLogV2Approval")
                //                   .SetHeaderText("Approve Order").Attr("cssClass", "material-icon-group-add");
                //}
                //if (UserIsInRole("MarketingManager"))
                //{
                //    NodeSet().SelectActionGroup("ag1").CreateAction("Custom", "Lv2Approval").SetConfirmation("_controller=OrderStatusLogV2Approval")
                //                   .SetHeaderText("Approve Order").Attr("cssClass", "material-icon-group-add");
                //}
            }
            
            return base.VirtualizeControllerConditionally(controllerName);
        }
        protected override void Filter(string controller, string view, SortedDictionary<string, object> filter)
        {
            //filter.Add("ADMSB", "CompanyName");
            base.Filter(controller, view, filter);
        }

        [ControllerAction("OrderHeader", "Custom", "SubmitForApproval")]
        public void HandleSubmitForApproval()
        {
            var OrderHedID = SelectFieldValue("OrderHedID");
            var OrderStatusID = SelectFieldValue("OrderStatusID");
            if(OrderStatusID!=null && Convert.ToInt32(OrderStatusID) >= 5)
            {
                Result.ShowAlert("Already Submitted!");
                return;
            }
            var comment = SelectFieldValue("Parameters_UserComment");
            //var attachment = (Stream)SelectFieldValue("Parameters_Attachment");
            //byte[] data = new byte[attachment.Length];
            //attachment.Read(data, 0, data.Length);
            //attachment.Position = 0;

            if (OrderHedID != null)
            {
                int i = (int)SqlText.ExecuteScalar(
                    "select Count(*) from OrderDtl Where OrderHedID=@OrderHedID "
                    , new { OrderHedID });

                if (i > 0)
                {
                    int j = (int)SqlText.ExecuteScalar(
                    "select Count(*) from OrderDtl Where (ChangeSellingPrice=1 Or ChangeBasePrice=1) and OrderHedID=@OrderHedID "
                    , new { OrderHedID });

                    if (j <= 0)
                    {
                        //Submit For Approval...
                        // StatusID=5	Waiting For Lv1 Approval
                        SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=5 Where OrderHedID=@OrderHedID ", new { OrderHedID });
                        SqlText.ExecuteNonQuery(
                            "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                            "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                            , new
                            {
                                @OrderHedID = OrderHedID,
                                @OrderStatusID = 5,
                                @UserID = UserId,
                                @UserComment = comment
                            });

                    }
                    else
                    {
                        //Submit For PriceChange...
                        // StatusID=2	Waiting For Price Change
                        SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=2 Where OrderHedID=@OrderHedID ", new { OrderHedID });
                        SqlText.ExecuteNonQuery(
                            "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                            "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                            , new
                            {
                                @OrderHedID = OrderHedID,
                                @OrderStatusID = 2,
                                @UserID = UserId,
                                @UserComment = comment
                            });
                    }

                    //Result.ShowAlert("Found Items in OrderDtl!");

                }
                else
                {
                    Result.ShowAlert("Not found any Item in OrderDtl!");
                }

            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

        }

        [ControllerAction("OrdersWaitingForPriceChange", "Custom", "ApprovePriceChange")]
        public void HandleApprovePriceChange()
        {
            var OrderHedID = SelectFieldValue("OrderHedID");
            var OrderStatusID = SelectFieldValue("OrderStatusID");
            
            var comment = SelectFieldValue("Parameters_UserComment");
            var status = SelectFieldValue("Parameters_Status");
            
            //var attachment = (Stream)SelectFieldValue("Parameters_Attachment");
            //byte[] data = new byte[attachment.Length];
            //attachment.Read(data, 0, data.Length);
            //attachment.Position = 0;

            if (OrderHedID != null)
            {
                //Submit For Approval...
                // StatusID=5	Waiting For Lv1 Approval
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=5 Where OrderHedID=@OrderHedID ",
                    new { OrderHedID, @OrderStatusID=status });
                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                    "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                    , new
                    {
                        @OrderHedID = OrderHedID,
                        @OrderStatusID = status,
                        @UserID = UserId,
                        @UserComment = comment
                    });

                Result.ShowAlert("Done!");
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

        }

        [ControllerAction("OrdersWaitingForApproval", "Custom", "Lv1Approval")]
        public void HandleLv1Approval()
        {
            var OrderHedID = SelectFieldValue("OrderHedID");
            var OrderStatusID = SelectFieldValue("OrderStatusID");

            var comment = SelectFieldValue("Parameters_UserComment");
            var status = SelectFieldValue("Parameters_Status");

            //var attachment = (Stream)SelectFieldValue("Parameters_Attachment");
            //byte[] data = new byte[attachment.Length];
            //attachment.Read(data, 0, data.Length);
            //attachment.Position = 0;

            if (OrderHedID != null)
            {
                //Submit For Approval...
                // StatusID=5	Waiting For Lv1 Approval
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { OrderHedID, @OrderStatusID=status });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                    "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                    , new
                    {
                        @OrderHedID = OrderHedID,
                        @OrderStatusID = status,
                        @UserID = UserId,
                        @UserComment = comment
                    });

                Result.ShowAlert("Done!");
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

        }

        [ControllerAction("OrdersWaitingForApproval", "Custom", "Lv2Approval")]
        public void HandleLv2Approval()
        {
            var OrderHedID = SelectFieldValue("OrderHedID");
            var OrderStatusID = SelectFieldValue("OrderStatusID");

            var comment = SelectFieldValue("Parameters_UserComment");
            var status = SelectFieldValue("Parameters_Status");

            //var attachment = (Stream)SelectFieldValue("Parameters_Attachment");
            //byte[] data = new byte[attachment.Length];
            //attachment.Read(data, 0, data.Length);
            //attachment.Position = 0;

            if (OrderHedID != null)
            {
                //Submit For Approval...
                // StatusID=5	Waiting For Lv1 Approval
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { OrderHedID, @OrderStatusID = (int)status });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                    "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                    , new
                    {
                        @OrderHedID = OrderHedID,
                        @OrderStatusID = (int)status,
                        @UserID = UserId,
                        @UserComment = comment
                    });

                Result.ShowAlert("Done!");
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

        }

        [ControllerAction("OrderHeader", "Custom", "SubmitForPriceChange")]
        public void HandleSubmitForPriceChange_NotUsing()
        {
            var OrderHedID = SelectFieldValue("OrderHedID");
            var OrderStatusID = SelectFieldValue("OrderStatusID");
            if (OrderStatusID != null && Convert.ToInt32(OrderStatusID) >= 2)
            {
                Result.ShowAlert("Already Submitted!");
                return;
            }
            var comment = SelectFieldValue("Parameters_UserComment");
            //var attachment = (Stream)SelectFieldValue("Attachment");
            //byte[] data = new byte[attachment.Length];
            //attachment.Read(data, 0, data.Length);
            //attachment.Position = 0;

            if (OrderHedID != null)
            {
                int i = (int)SqlText.ExecuteScalar(
                    "select Count(*) from OrderDtl Where OrderHedID=@OrderHedID "
                    , new { OrderHedID });

                if (i > 0)
                {
                    int j = (int)SqlText.ExecuteScalar(
                    "select Count(*) from OrderDtl Where (ChangeSellingPrice=1 Or ChangeBasePrice=1) and OrderHedID=@OrderHedID "
                    , new { OrderHedID });

                    if (j <= 0)
                    {
                        Result.ShowAlert("Can't Submit for PriceChange because not any item marked for PriceChange!");                        
                    }
                    else
                    {
                        //Submit For PriceChange...
                        // StatusID=2	Waiting For Price Change
                        SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=2 Where OrderHedID=@OrderHedID ", new { OrderHedID });
                        SqlText.ExecuteNonQuery(
                            "Insert into OrderStatusLog (OrderHedID,OrderStatusID,UserID,UserComment) Values " +
                            "(@OrderHedID,@OrderStatusID,@UserID,@UserComment) "
                            , new { @OrderHedID = OrderHedID, @OrderStatusID = 2, @UserID = UserId, @UserComment = comment
                            });

                    }

                    //Result.ShowAlert("Found Items in OrderDtl!");

                }
                else
                {
                    Result.ShowAlert("Not found any Item in OrderDtl!");
                }

            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

        }

        //[ControllerAction("OrderStatusLogV2","New",ActionPhase.Execute)]
        //public void HandleLog()
        //{
        //    var OrderHedID = SelectFieldValue("OrderHedID");
        //    var comment = SelectFieldValue("Parameters_UserComment");
        //    var attachment = SelectFieldValue("Parameters_Attachment");


        //}

        protected override void BeforeSqlAction(ActionArgs args, ActionResult result)
        {
            base.BeforeSqlAction(args, result);
        }


    }
}
