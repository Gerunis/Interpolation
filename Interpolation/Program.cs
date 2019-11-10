using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Dot[3] { new Dot(-1, 2), new Dot(4, 1), new Dot(5, 3), };
            //Console.WriteLine("X  Y");
            //for (var i = 0; i < 3; i++)
            //{
            //    var line = Console.ReadLine();
            //    var e = line.Split(' ').Select(x => int.Parse(x)).ToArray();
            //    t[i] = new Dot(e[0], e[1]);
            //}
            CountLagrangePolynomial(t);
            CountNutonPolynomial(t);
        }

        static public void CountLagrangePolynomial(Dot[] dots)
        {
            var n = dots.Length;
            var k = new double[n];
            for (var i = 0; i < n; i++)
            {
                k[i] = dots[i].Y;
                for (var j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    k[i] /= dots[i].X - dots[j].X;
                }
            }
            var res = new double[n];

            for (var i = 0; i < n; i++)
            {
                var t = GetFuction(dots.Where(x => x != dots[i]).Select(x => (double)-x.X), n - 1);
                for (var j = 0; j < n; j++)
                {
                    res[j] += t[j] * k[i];
                }
            }
            Console.Write("Лагранж: f(x)= ");
            for (var i = n - 1; i >= 0; i--)
            {
                Console.Write("{0:+ 0.0000;- 0.0000} * x^{1} ", res[i], i);
            }
            Console.WriteLine();
        }

        static public void CountNutonPolynomial(Dot[] dots)
        {
            var n = dots.Length;
            var res = new double[n];
            for(var i = 0; i < n; i++)
            {
                var delta = Delta(dots, 0, i);
                var t = GetFuction(dots.Take(i).Select(x => (double)-x.X), i);
                for (var j = 0; j < t.Length; j++)
                {
                    res[j] += t[j] * delta; 
                }
            }
            Console.Write("Ньютон: f(x)= ");
            for(var i = n - 1; i >= 0; i--)
            {
                Console.Write("{0:+ 0.0000;- 0.0000} * x^{1} ", res[i], i);
            }
            Console.WriteLine();
        }

        static double Delta(Dot[] dots, int start, int end)
        {
            if (start == end) return dots[start].Y;
            return (Delta(dots, start + 1, end) - Delta(dots, start, end - 1))/(dots[end].X - dots[start].X);
        }

        static double[] GetFuction(IEnumerable<double> numbers, int n)
        {
            var res = new double[n + 1];
            res[0] = 1; 
            foreach(var x in numbers)
            {
                var t = new double[n + 1];
                for (var i = 0; i < n; i++)
                {
                    t[i + 1] = res[i];
                    t[i] += res[i] * x;
                }
                res = t;
            }
            return res;
        }
    }
}
