using Communicator.MeterConnManager;
using SharedCode.Common;
using SharedCode.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MTI_MDC
{
    internal class MDC_ObjectFactory
    {
        public static MeterConnManager.ApplicationController GetApplicationControllerObject()
        {
            switch (ObjectFactory.meteringCompany)
            {
                case MeteringCompany.YTL:
                    return new ApplicationController_YTL();
                case MeteringCompany.ShenzenClu:
                case MeteringCompany.Creative:
                case MeteringCompany.MTI:
                default:
                    return new MeterConnManager.ApplicationController();
            }
        }

        
    }
}
