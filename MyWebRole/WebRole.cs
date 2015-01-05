using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MyWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //Get the configuration object
            DiagnosticMonitorConfiguration diagObj = DiagnosticMonitor.GetDefaultInitialConfiguration();

            //Set the service to transfer logs every second to the storage account
            //diagObj.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);

            //Start Diagnostics Monitor with the new storage account configuration
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", diagObj);

            return base.OnStart();

        }
    }
}
