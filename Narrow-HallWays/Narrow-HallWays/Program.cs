using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Narrow_HallWays
{
    public class Program
    {
        static void Main(string[] args)
        {
            string personas = "<---><<<<";

            Console.WriteLine(CountSalutes(personas));
            Console.ReadLine();
        }
        public static int CountSalutes(string personas)
        {
            int salutes = 0;
            int rightPeople = 0;
            int leftPeople = 0;
            for (int i = 0; i < personas.Length; i++)
            {
                if (personas[i] == '>')
                {
                    rightPeople++;
                }
                else if (personas[i] == '<' && rightPeople>0)
                {
                    leftPeople++;
                    salutes = rightPeople * 2;
                    salutes = salutes * leftPeople;
                }               
            }
            return salutes;
        }
    }
}
