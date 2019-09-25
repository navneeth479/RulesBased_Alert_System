using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICUSimulatorLib
{
    public interface IICUSimulator
    {
        List<int> SimulateVitals();
        void UpdateVitals(string patientid,List<int> VitalsList);
    }
}
