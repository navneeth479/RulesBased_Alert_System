using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spo2CheckerInterfaceLib;
using DataModelsLib;
namespace Spo2CheckerLib
{
    public class Spo2Checker:ISpo2Checker
    {
        public Vitals.Spo2Level CheckSpo2(int spo2)
        {
            if (spo2 >= 95 && spo2 <= 100)
            {
                
                return Vitals.Spo2Level.NormalHealthy;
            }
            else if (spo2 >= 91 && spo2 < 95)
            {
                
                return Vitals.Spo2Level.ClinicallyAcceptable;
            }
            else if (spo2 >= 70 && spo2 <= 90)
            {
                
                return Vitals.Spo2Level.Hypoxemia;
            }
            else if (spo2 >= 0 && spo2 < 70)
            {
                return Vitals.Spo2Level.LackOfO2;
            }
            else
            {
                
                return Vitals.Spo2Level.InvalidInput;
            }
        }
    }
}
