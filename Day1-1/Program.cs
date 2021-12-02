namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int counter = 0;  
            int increase = 0;
            int lastValue = int.MinValue;
            bool first = true;
  
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@".\input.txt"))
            {  
                System.Console.WriteLine(line);  
                counter++;  
                var thisValue = int.Parse(line);
                if(!first){
                    if(thisValue > lastValue){
                        increase++;
                        System.Console.WriteLine("Increase detected! That's {0}.", increase); 
                    }

                }
                lastValue = thisValue;
                first = false;
            }  
            
            System.Console.WriteLine("There were {0} lines.", counter);  
            System.Console.WriteLine("There were {0} increases.", increase); 
            // Suspend the screen.  
            System.Console.ReadLine();
        }
    }
}