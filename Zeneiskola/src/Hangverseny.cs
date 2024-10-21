using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeneiskola.src
{
    internal class Hangverseny
    {
        public string Cim { get; set; }
        public int Ev { get; set; }
        public int Nehezseg { get; set; }
        public string Szerzo { get; set; }

        public Hangverseny(string sorok) 
        {
            var sor = sorok.Split(';');
            Cim = sor[0];
            Ev = Convert.ToInt32(sor[1]);
            Nehezseg = Convert.ToInt32(sor[2]);
            Szerzo = sor[3];
            
        }
    }
}
