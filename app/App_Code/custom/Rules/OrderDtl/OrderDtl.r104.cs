using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;

namespace Finsoft.Rules
{
	public partial class OrderDtlBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view after an action
        /// with a command name that matches "Insert".
        /// </summary>
        [Rule("r104")]
        public void r104Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            int i = SqlText.ExecuteNonQuery("INSERT INTO OrderRel (OrderHedID,OrderDtlID,OrderRelQty,FOCRelQty,ShipByDate,ShipToSysRowID,ShipToName,Seq) VALUES (@OrderHedID,@OrderDtlID,@OrderQty,@Focqty,@OHShipByDate,@OHShipToSysRowID,@OHShipToName,1)	"
                        , instance.OrderHedID, instance.OrderDtlID,instance.OrderQty,instance.Focqty,instance.OHShipByDate,instance.OHShipToSysRowID,instance.OHShipToName);
        }
    }
}
