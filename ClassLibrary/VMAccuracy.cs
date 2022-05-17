using System;


namespace ClassLibrary
{
    [Serializable]
    public class VMAccuracy
    {
        public VMGrid grid { get; set; }
        public double Abs { get; set; }
        public double x { get; set; }
        public double y_HA { get; set; }
        public double y_EP { get; set; }

        public VMAccuracy()
        {
            grid = null;
        }
        public VMAccuracy(VMGrid g)
        {
            grid = g;
        }
        public bool MaxAbs(double[] a, double[] yHA, double[] yEP)
        {
            try
            {
                double m = Math.Abs(yHA[0] - yEP[0]);
                int i = 0;
                for (int k = 1; k < grid.n; k++)
                {
                    double max = Math.Abs(yHA[k] - yEP[k]);
                    if (max > m)
                    {
                        m = max;
                        i = k;
                    }
                }
                Abs = m;
                x = a[i];
                y_HA = yHA[i];
                y_EP = yEP[i];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
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
            return $"Function: {f}\n[{grid.first}, {grid.last}], {grid.n} nodes\nVML_HA: {y_HA}\nVML_EP: {y_EP}\n";
        }
    }
}