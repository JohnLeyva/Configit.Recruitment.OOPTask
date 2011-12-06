using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigIt.DesertRace.Entities;

namespace ConfigIt.DesertRace.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DesertRaceGame.Play(global::System.Console.In, global::System.Console.Out);
        }
    }
}
