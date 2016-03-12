using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputationalMethods.Helpers
{
    static class Parser
    {
        public static double? ParseInput(string input)
        {
            double? result = null;
            try
            {
                result = Double.Parse(input);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
            return result;
        }
    }
}
