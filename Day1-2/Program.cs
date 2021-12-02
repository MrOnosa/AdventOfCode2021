namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int increase = 0;
            var values = new List<int>();
            var slidingWindowA = new Queue<int>();
            var slidingWindowB = new Queue<int>();
  
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@".\input.txt"))
            {  
                var thisValue = int.Parse(line);
                values.Add(thisValue);
            }
            //Seed
            slidingWindowA.Enqueue(values[0]);
            slidingWindowA.Enqueue(values[1]);
            slidingWindowB.Enqueue(values[1]);
            slidingWindowB.Enqueue(values[2]);


            for (int i = 3; i < values.Count; i++)
            {
                slidingWindowA.Enqueue(values[i-1]);
                slidingWindowB.Enqueue(values[i]);
                if(slidingWindowA.Count > 3){
                    slidingWindowA.Dequeue();
                }
                if(slidingWindowB.Count > 3){
                    slidingWindowB.Dequeue();
                }

                var sumA = slidingWindowA.Sum();
                var sumB = slidingWindowB.Sum();
                var listA = slidingWindowA.ToList();
                var listB = slidingWindowB.ToList();
                System.Console.WriteLine($"{i}: SumA: {listA[0]}, {listA[1]}, {listA[2]}: {sumA}."); 
                System.Console.WriteLine($"{i}: SumB: {listB[0]}, {listB[1]}, {listB[2]}: {sumB}."); 
                if(sumB > sumA){
                    increase++;                            
                    System.Console.WriteLine("Increase detected! That's {0}.", increase); 
                }               
                
            }
            
            System.Console.WriteLine("There were {0} increases.", increase); 
        }
    }
}