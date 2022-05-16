using System;
using System.Diagnostics;

namespace ClassLibrary
{
    public delegate void function(int n, double[] a, double[] y, char m);

    [Serializable]
    public class VMTime
    {
        public VMGrid grid { get; set; }
        public double time_HA { get; set; }
        public double time_LA { get; set; }
        public double time_EP { get; set; }
        public double LA_to_HA { get; set; }
        public double EP_to_HA { get; set; }

        public VMTime()
        {
            grid = null;
        }

        public VMTime(VMGrid g, double[] a, ref double[] y1, ref double[] y2, ref double[] y3, function f)
        {
            try
            {
                grid = g;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                f(g.n, a, y1, 'H');
                stopWatch.Stop();
                time_HA = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Reset();
                stopWatch.Start();
                f(g.n, a, y2, 'L');
                stopWatch.Stop();
                time_LA = stopWatch.Elapsed.TotalSeconds;
                stopWatch.Start();
                f(g.n, a, y3, 'E');
                stopWatch.Stop();
                time_EP = stopWatch.Elapsed.TotalSeconds;
                LA_to_HA = time_LA / time_HA;
                EP_to_HA = time_EP / time_HA;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public override string ToString()
        {
            string f = "";
            if (grid.func == VMf.vmdSin)
            {
                f = "Sin";
            }
            else if (grid.func == VMf.vmdCos)
            {
                f = "Cos";
            }
            else if (grid.func == VMf.vmdSinCos)
            {
                f = "SinCos";
            }
            return $"Function: {f}\n[{grid.first}, {grid.last}], {grid.n} nodes\nVML_HA: {time_HA}\nVML_EP: {time_EP}\nVML_LA: {time_LA}\n";
        }
    }
}
