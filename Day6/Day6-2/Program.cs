using System.Diagnostics;
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            long[] buckets = new long[9];
            foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
            {
                Console.WriteLine($"Input: {line}");
                foreach (var number in line.Split(','))
                {
                    buckets[int.Parse(number)]++;
                }
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int day = 0; day < 256; day++)
            {
                long newFish = buckets[0];
                buckets[0] = buckets[1];
                buckets[1] = buckets[2];
                buckets[2] = buckets[3];
                buckets[3] = buckets[4];
                buckets[4] = buckets[5];
                buckets[5] = buckets[6];
                buckets[6] = buckets[7] + newFish;
                buckets[7] = buckets[8];
                buckets[8] = newFish;
            }
            stopWatch.Stop();

            Console.WriteLine($"Total laternfish: {buckets.Sum()} - Elapsed {stopWatch.Elapsed} ");

        }
    }
}