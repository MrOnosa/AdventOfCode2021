namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
  
            var values = new List<List<int>>();
            // Read the file and display it line by line.  
            /*
00100
11110
10110
-----
10110 Gamma
01?0? Epsilon
            */
            int lengthOfBitsPerLine=0;
            foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\input.txt"))
            {  
                var bits = new List<int>();
                lengthOfBitsPerLine = line.Length;
                for (int i = 0; i < line.Length; i++)
                {
                    bits.Add(int.Parse(line[i].ToString()));
                }
                values.Add(bits);
            }

            var onesCount = new List<int>(lengthOfBitsPerLine);
            for (int onesIndex = 0; onesIndex < lengthOfBitsPerLine; onesIndex++)
            {      
                onesCount.Add(0);          
                for (int i = 0; i < values.Count; i++)
                {          
                    var value = values[i];
                    if(value[onesIndex] == 1){
                        onesCount[onesIndex]++;
                    }
                }
            }

            string gammaString = string.Empty;
            string epsilonString = string.Empty;
            for (int onesIndex = 0; onesIndex < lengthOfBitsPerLine; onesIndex++)
            {
                if(onesCount[onesIndex] > values.Count/2){
                    gammaString += "1";
                } else {
                    gammaString += "0";
                }

                if(onesCount[onesIndex] < values.Count/2){
                    epsilonString += "1";
                } else {
                    epsilonString += "0";
                }
            }
            
           // System.Console.WriteLine("Result {0}", horizontalPosition * depth); 
            Console.WriteLine($"Binary - Gamma {gammaString} - Epsilon {epsilonString}");

            
            int gamma = Convert.ToInt32(gammaString, 2);            
            int epsilon = Convert.ToInt32(epsilonString, 2);

            Console.WriteLine($"Int - Gamma {gamma} - Epsilon {epsilon}");
            Console.WriteLine($"What is the power consumption of the submarine? {gamma * epsilon}");
        }
    }
}