using DLMS;
using SharedCode.Controllers;
using SharedCode.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Common
{

    public enum MeteringCompany
    {
        MTI = 0,
        ShenzenClu = 1,
        Creative = 2,
        YTL = 3,
    }
    class ObjectFactory
    {
        static MeteringCompany meteringCompany = MeteringCompany.YTL;
        static ObjectFactory()
        {
            //meteringCompany = (MeteringCompany)Settings.Default.HeartBeatType; // HeartBeatType.SHENZHEN_CLOU;

        }
        public static HeartBeat GetHeartBeatObject()
        {
            switch (meteringCompany)
            {
                case MeteringCompany.ShenzenClu:
                    return new SHENZHEN_CLOU_HeartBeat();
                case MeteringCompany.YTL:
                    return new HeartBeat_YTL();
                case MeteringCompany.Creative:
                case MeteringCompany.MTI:
                default:
                    return new HeartBeat_MTI();
            }
        }
        public static InstantaneousController GetInstantaneousControllerObject()
        {
            switch (meteringCompany)
            {
                case MeteringCompany.ShenzenClu:
                case MeteringCompany.Creative:
                case MeteringCompany.MTI:
                default:
                    return new InstantaneousController();
            }
        }

        public static BillingController GetBillingControllerObject()
        {
            switch (meteringCompany)
            {
                case MeteringCompany.ShenzenClu:
                case MeteringCompany.Creative:
                case MeteringCompany.MTI:
                default:
                    return new BillingController();
            }
        }
    }
}

