using ICUSimulatorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VitalSimulatorMangerLib
{
    public class VitalSimulatorManager
    {
        private ICUSimulator _obj = new ICUSimulator(); 
        public void StartSimulation(string patientid)
        {
            for (int i = 0; i < 100; i++)
            {
                _obj.UpdateVitals(patientid,_obj.SimulateVitals());
                Thread.Sleep(2000);
            }
        }
    }
}
