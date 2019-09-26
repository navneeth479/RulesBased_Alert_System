using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
namespace TempCheckerInterfaceLib
{
    public interface ITempChecker
    {
        Vitals.Temp CheckTemp(int temp);
        bool IsAlertTemp(int temp);

    }
}
