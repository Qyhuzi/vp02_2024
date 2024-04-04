using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace _016_Loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1에서 100까지의 합;
            int sum = 0;
            for (int i=1; i<=100; i++)
            {
                sum += i;
            }
            Console.WriteLine("1~100까지의 합 : " + sum);

            // 1에서 100까지의 홀수의 합
            int oddsum = 0;
            for (int i = 1; i <= 100; i++)
            {
                if (i%2 == 1)
                oddsum += i;
            }
            Console.WriteLine("1~100까지 홀수의 합 : " + oddsum);

            // 1에서 100까지의 역수의 합
            double recisum = 0;
            for (int i = 1; i <= 100; i++)
            {
                    recisum += 1.0/i;
            }
            Console.WriteLine("1~100까지 역수의 합 : " + recisum);

            Console.WriteLine();

            // x에서 y까지의 합
            int sum0 = 0;

            Console.Write("첫 번째 값을 입력하세요 : ");
            int x = int.Parse(Console.ReadLine());

            Console.Write("두 번째 값을 입력하세요 : ");
            int y = int.Parse(Console.ReadLine());

            for (int i = x; i <= y; i++)
            {
                sum0 += i;
            }
            Console.WriteLine("{0} ~ {1}까지의 합 : {2}",x, y, sum0);

            // x에서 y까지 홀수의 합
            int sum1 = 0;

            for (int i = x; i <= y; i++)
            {
                if (i % 2 == 1) sum1 += i;
            }
            Console.WriteLine("{0} ~ {1}까지홀수의 합 : {2}", x, y, sum1);

            Console.WriteLine();

            // a의 b승
            int pow = 1;

            Console.Write("첫 번째 값을 입력하세요 : ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("원하는 승을 입력하세요 : ");
            int b = int.Parse(Console.ReadLine());

            for (int i = 0; i <= b; i++)
            {
                pow *= a;
            }
            Console.WriteLine("{0}의 {1}승 : {2}", a, b, pow);

            Console.WriteLine();

            // 팩토리얼
            int fact = 1;

            Console.Write("팩토리얼 값을 입력하세요 : ");
            int c = int.Parse(Console.ReadLine());

            for (int i = 1; i <= c; i++)
                fact *= i;

            Console.WriteLine("{0}! : {1} ", c, fact);

            Console.WriteLine();

            // 구구단
            for (int q = 1; q <= 9; q++)
            {
                for (int p = 2; p <= 9; p++)
                    Console.Write("{0} x {1} = {2}\n", q, p, q * p);
                Console.WriteLine();

            }
        }
    }
}