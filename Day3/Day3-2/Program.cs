namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
  
            List<List<int>> values = new List<List<int>>();
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

            string oxygenGeneratorRatingString = string.Empty;
            string CO2ScrubberRatingString = string.Empty;
            List<List<int>> numbersWithTheRightBitInTheNthPosition = values.ToList();
            for (int onesIndex = 0; onesIndex < lengthOfBitsPerLine && oxygenGeneratorRatingString == string.Empty; onesIndex++)
            {         
                
                Console.WriteLine($"ROUND {onesIndex}");       
                int keepValue;
                
                var onesCount = new List<int>(lengthOfBitsPerLine);
                var zerosCount = new List<int>(lengthOfBitsPerLine);
                for (int onesIndexb = 0; onesIndexb < lengthOfBitsPerLine; onesIndexb++)
                {      
                    onesCount.Add(0); 
                    zerosCount.Add(0);         
                    for (int i = 0; i < numbersWithTheRightBitInTheNthPosition.Count; i++)
                    {          
                        var value = numbersWithTheRightBitInTheNthPosition[i];
                        if(value[onesIndexb] == 1){
                            onesCount[onesIndexb]++;
                        } else {
                            zerosCount[onesIndexb]++;
                        }
                    }
                }

                Console.WriteLine($"Numbers {numbersWithTheRightBitInTheNthPosition.Count}");
                foreach (var numbers in numbersWithTheRightBitInTheNthPosition)
                {
                    Console.WriteLine(String.Join(string.Empty, numbers.Select(n => n.ToString())));                    
                }
                Console.WriteLine($"Ones Count");
                Console.WriteLine(onesCount[onesIndex]);   
                
                if(onesCount[onesIndex] >= zerosCount[onesIndex]){
                    keepValue = 1;
                } else {
                    keepValue = 0;
                }
                numbersWithTheRightBitInTheNthPosition = numbersWithTheRightBitInTheNthPosition.Where(v => v[onesIndex] == keepValue).ToList();
                if(numbersWithTheRightBitInTheNthPosition.Count == 1){
                    oxygenGeneratorRatingString = String.Join(string.Empty, numbersWithTheRightBitInTheNthPosition.First().Select(n => n.ToString()));
                }
                Console.WriteLine($"");    
            }

            //CO2
            Console.WriteLine($"------------");   
            Console.WriteLine($"-----CO2----");   
            Console.WriteLine($"------------");   
            
            numbersWithTheRightBitInTheNthPosition = values.ToList();
            for (int onesIndex = 0; onesIndex < lengthOfBitsPerLine && CO2ScrubberRatingString == string.Empty; onesIndex++)
            {         
                
                Console.WriteLine($"ROUND {onesIndex}");       
                int keepValue;
                
                var onesCount = new List<int>(lengthOfBitsPerLine);
                var zerosCount = new List<int>(lengthOfBitsPerLine);
                for (int onesIndexb = 0; onesIndexb < lengthOfBitsPerLine; onesIndexb++)
                {      
                    onesCount.Add(0); 
                    zerosCount.Add(0);         
                    for (int i = 0; i < numbersWithTheRightBitInTheNthPosition.Count; i++)
                    {          
                        var value = numbersWithTheRightBitInTheNthPosition[i];
                        if(value[onesIndexb] == 1){
                            onesCount[onesIndexb]++;
                        } else {
                            zerosCount[onesIndexb]++;
                        }
                    }
                }

                Console.WriteLine($"Numbers {numbersWithTheRightBitInTheNthPosition.Count}");
                foreach (var numbers in numbersWithTheRightBitInTheNthPosition)
                {
                    Console.WriteLine(String.Join(string.Empty, numbers.Select(n => n.ToString())));                    
                }
                Console.WriteLine($"Ones Count");
                Console.WriteLine(onesCount[onesIndex]);   
                
                if(onesCount[onesIndex] < zerosCount[onesIndex]){
                    keepValue = 1;
                } else {
                    keepValue = 0;
                }
                numbersWithTheRightBitInTheNthPosition = numbersWithTheRightBitInTheNthPosition.Where(v => v[onesIndex] == keepValue).ToList();
                if(numbersWithTheRightBitInTheNthPosition.Count == 1){
                    CO2ScrubberRatingString = String.Join(string.Empty, numbersWithTheRightBitInTheNthPosition.First().Select(n => n.ToString()));
                }
                Console.WriteLine($"");    
            }
            
           // System.Console.WriteLine("Result {0}", horizontalPosition * depth); 
            Console.WriteLine($"Binary - oxygen generator rating {oxygenGeneratorRatingString} - CO2 scrubber rating {CO2ScrubberRatingString}");

            
            int gamma = Convert.ToInt32(oxygenGeneratorRatingString, 2);            
            int epsilon = Convert.ToInt32(CO2ScrubberRatingString, 2);

            Console.WriteLine($"Int - Gamma {gamma} - Epsilon {epsilon}");
            Console.WriteLine($"What is the power consumption of the submarine? {gamma * epsilon}");
        }


    }
}