namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;
            var values = new List<Step>();
  
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@".\input.txt"))
            {  
                var direction = line.Split()[0];// Read text file into string record
                var thisValue = int.Parse(line.Split()[1]);
                values.Add(new Step{Direction = direction, Delta = thisValue});
            }

            for (int i = 0; i < values.Count; i++)
            {          
                var value = values[i];
                System.Console.WriteLine("Step: {0} {1}", value.Direction, value.Delta); 
                if(value.Direction == "forward"){
                    horizontalPosition += value.Delta;
                    depth += aim * value.Delta;
                } else if(value.Direction == "down"){
                    aim += value.Delta;
                } else {
                    aim -= value.Delta;
                }
                System.Console.WriteLine("Heading: {0} {1}", horizontalPosition, depth); 
            }
            
            System.Console.WriteLine("Result {0}", horizontalPosition * depth); 
        }
    }

    class Step
    {
        public string Direction {get;set;}
        public int Delta {get;set;}
    }
}
