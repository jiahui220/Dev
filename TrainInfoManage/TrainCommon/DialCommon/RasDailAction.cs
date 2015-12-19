using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    public class RasDailAction
    {
        public static bool DailOn()
        {
            if (!RasDailAction.GetDailStaute())
            {
                RasDail.DisConnectAll();
            }
            return RasDail.RasDialRun(2);
        }

        public static bool GetDailStaute()
        {
            return RasDail.RasDialStatus("cdma");
        }

        public static bool DailUp()
        {
            return RasDail.RasDialHangUp("cdma");
        }

        public static void DisAllContect()
        {
            RasDail.DisConnectAll();
        }
    }
}
