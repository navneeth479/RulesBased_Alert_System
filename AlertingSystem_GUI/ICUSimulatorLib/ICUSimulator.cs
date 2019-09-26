using IICUSimulatorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICUSimulatorLib
{
    public class ICUSimulator : IICUSimulator
    {
        public List<int> SimulateVitals()
        {
            List<int> vitalsList=new List<int>();
            int pulse, spo2, temp;
            Random rnd = new Random();
            pulse = rnd.Next(0, 250);
            spo2 = rnd.Next(0, 100);
            temp = rnd.Next(89, 120);
            vitalsList.Add(spo2);
            vitalsList.Add(pulse);
            vitalsList.Add(temp);
            return vitalsList;
        }

        public void UpdateVitals(string patientid,List<int> vitalsList)
        {
            ICUSimulatorClient ICUclient=new ICUSimulatorClient();
            ICUclient.UpdateSpo2(patientid, vitalsList[0]);
            ICUclient.UpdatePulse(patientid, vitalsList[1]);
            ICUclient.UpdateTemp(patientid, vitalsList[2]);

        }
    }
}
