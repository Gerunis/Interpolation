using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Dot[3] { new Dot(-1,2), new Dot(4, 1), new Dot(5, 3), };
            //Console.WriteLine("X Y");
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
            var k = new double[3];
            for (var i = 0; i < 3; i++)
            {
                k[i] = dots[i].Y;
                for (var j = 0; j < 3; j++)
                {
                    if (i == j) continue;
                    k[i] /= dots[i].X - dots[j].X;
                }
            }
            var res = new double[3];

            for (var i = 0; i < 3; i++)
            {
                res[0] += k[i];
                var sum = 0.0;
                var mul = 1.0;

                for (var j = 0; j < 3; j++)
                {
                    if (i == j) continue;
                    sum -= dots[j].X;
                    mul *= dots[j].X;
                }

                res[1] += k[i] * sum;
                res[2] += k[i] * mul;
            }
            Console.WriteLine("f(x)= {0:0.0000;-0.0000} * x^2 {1:+ 0.0000;- 0.0000} * x {2:+ 0.0000;- 0.0000}", res[0], res[1], res[2]);
        }

        static public void CountNutonPolynomial(Dot[] dots)
        {
            var deltas = new double[3];
            deltas[0] = Delta(dots, 0, 0);
            deltas[1] = Delta(dots, 0, 1);
            deltas[2] = Delta(dots, 0, 2);

            var res = new double[3];
            res[0] = deltas[0] - deltas[1] * dots[0].X + deltas[2] * dots[0].X * dots[1].X;
            res[1] = deltas[1] - deltas[2] * (dots[0].X + dots[1].X);
            res[2] = deltas[2];
            Console.WriteLine("f(x)= {0:0.0000;-0.0000} * x^2 {1:+ 0.0000;- 0.0000} * x {2:+ 0.0000;- 0.0000}", res[2], res[1], res[0]);
        }

        static double Delta(Dot[] dots, int start, int end)
        {
            if (start == end) return dots[start].Y;
            return (Delta(dots, start + 1, end) - Delta(dots, start, end - 1))/(dots[end].X - dots[start].X);
        }
    }
}
