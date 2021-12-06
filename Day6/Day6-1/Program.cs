using System.Diagnostics;
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var lanternfishes = new List<int>();
            foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\test-input.txt"))
            {
                Console.WriteLine($"Input: {line}");
                foreach (var number in line.Split(','))
                {
                    lanternfishes.Add(int.Parse(number));
                }
            }
            Stopwatch stopWatch = new Stopwatch();
            Console.WriteLine($"Inital State: {string.Join(',', lanternfishes)}");
            stopWatch.Start();
            for (int day = 0; day < 80; day++)
            {
                var agedFish = new List<int>();
                int newFish = 0;
                foreach (var lanternfish in lanternfishes)
                {
                    if(lanternfish == 0){
                        newFish++;
                        agedFish.Add(6);
                    } else {
                        agedFish.Add(lanternfish - 1);
                    }
                }

                lanternfishes = agedFish;
                for (int i = 0; i < newFish; i++)
                {
                    lanternfishes.Add(8);
                }
                Console.WriteLine($"After {day:00} Days: {stopWatch.Elapsed}");
                //Console.WriteLine($"After {day:00} Days: {string.Join(',', lanternfishes)}");
            }
            stopWatch.Stop();
            //Console.WriteLine($"After 80 Days: {string.Join(',', lanternfishes)}");

            Console.WriteLine($"Total laternfish: {lanternfishes.Count}");

        }
    }
}