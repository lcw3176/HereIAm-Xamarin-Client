using System;
using System.Collections.Generic;
using System.Text;

namespace HereIAm.Services
{
    public interface IForeground
    {
        void StartService();
        void StopService();
    }
}
