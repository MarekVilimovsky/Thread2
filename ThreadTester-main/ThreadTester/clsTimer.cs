using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTester
{
    internal class clsTimer
    {
        // interval v us
        int mintIntervalIn_us;

        //Timespan
        TimeSpan mtsTimeSpan;

        // thread pro časování timeru
        Thread mobjVlákno1;

        // zastavit thread
        bool mblStop;

        /// deklarace ukazatele na funkci - "public delegate void ..."
        // definice tvaru eventu
        public delegate void dlgTick();
        // deklarace eventu ticku
        public event dlgTick Tick;

        // formulář kde existuje muj objekt
        public Form1 mobjForm; 

        //-------------------------------------
        // interval tick timeru
        //-------------------------------------
        public int Interval
        {
            get { return mintIntervalIn_us; }
            set { mintIntervalIn_us = value; }
        }
        
        //-------------------------------------
        // konstruktor timeru
        //-------------------------------------
        public clsTimer() 
        {
            mtsTimeSpan= TimeSpan.FromTicks(10000000);

            mobjForm = null;
        }

        //-------------------------------------
        // start timeru
        //-------------------------------------
        public bool Start()
        {
            // vytvoření threadu
            mobjVlákno1 = new Thread(MojeRutina);

            //spustit thread
            mblStop = false;
            mobjVlákno1.Start();

            return true;
        }

        //-------------------------------------
        // thread timeru
        //-------------------------------------
        private void MojeRutina()
        {
            do
            {
                {
                    // zavolat eventy v threadu formuláře
                    if (mobjForm!=null)
                        mobjForm.Invoke(Tick);

                    // pozastavit thread
                    Thread.Sleep(mtsTimeSpan);
                    // do{ } while ();
                }
            } while (mblStop == false);
        }

        //-------------------------------------
        // stop timeru
        //-------------------------------------
        public bool Stop()
        {
            mobjForm = null;
            
            // stop thread
            mblStop = true;

            return true;
        }
    }
}
