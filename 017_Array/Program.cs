using System.ComponentModel.DataAnnotations;

namespace _017_Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[10];

            // for(int i=0; i<a.Length; i++)
            // {
            //     a[i] = int.Parse(Console.ReadLine());
            // }

            // 배열에 10개의 랜덤값을 저장하시오
            Random r = new Random();
            for (int i=0; i<a.Length; i++)
                a[i] = r.Next(1000);


            // 1. 원소를 모두 출력하시오

            // 1-1. 인덱스 사용
            for(int i=0; i<a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();

            // 1-2. foreach 사용
            foreach (var value in a)
                Console.Write(value + " ");
            Console.WriteLine();

            // 2. 원소의 합을 출력하시오

            // 2-1. 인덱스 사용
            int sum = 0;

            for (int i = 0; i < 10; i++)
                sum += a[i];
            Console.WriteLine("원소의 합 : " + sum);

            // 2-2. foreach 사용
            int sum_2 = 0;

            foreach (var value in a)
                sum_2 += value;
            Console.WriteLine("원소의 합 : {0}", sum_2);

            // 3. 가장 큰 값을 출력하시오
            int max = a[0];
            for (int i = 1; i < 10; i++)
                if (max < a[i])
                    max = a[i];

            Console.WriteLine("최대값 : " + max);
        }
    }
}