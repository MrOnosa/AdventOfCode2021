namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var vents = new List<Vent>();
            foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
            {
                Console.WriteLine($"Input: {line}");
                var vent = new Vent();
                string segment = string.Empty;
                int i = 0;
                for (; i < line.Length; i++)
                {
                    char character = line[i];
                    if (character == ',') break;
                    segment += character;
                }
                vent.X1 = int.Parse(segment);
                segment = string.Empty;
                i++;
                for (; i < line.Length; i++)
                {
                    char character = line[i];
                    if (character == ' ') break;
                    segment += character;
                }
                vent.Y1 = int.Parse(segment);
                segment = string.Empty;
                
                for (; i < line.Length; i++)
                {
                    char character = line[i];
                    if (int.TryParse(character.ToString(), out _)) break;
                }

                for (; i < line.Length; i++)
                {
                    char character = line[i];
                    if (character == ',') break;
                    segment += character;
                }
                vent.X2 = int.Parse(segment);
                segment = string.Empty;
                i++;
                for (; i < line.Length; i++)
                {
                    char character = line[i];
                    segment += character;
                }
                vent.Y2 = int.Parse(segment);
                vents.Add(vent);
                
                Console.WriteLine($"Line : {vent}");
            }

            int round = 1;
            var buckets = new Dictionary<(int x, int y),int>();            
            foreach (var vent in vents)
            {
                if(vent.X1 == vent.X2){
                    //Horizontal
                    int min = Math.Min(vent.Y1, vent.Y2);
                    int max = Math.Max(vent.Y1, vent.Y2);
                    for (int i = min; i <= max; i++)
                    {
                        if(buckets.ContainsKey((vent.X1, i))){
                            buckets[(vent.X1, i)] = buckets[(vent.X1, i)] + 1;
                        } else {
                            buckets[(vent.X1, i)] = 1;
                        }
                    }
                }
                else if(vent.Y1 == vent.Y2){
                    //Vert
                    int min = Math.Min(vent.X1, vent.X2);
                    int max = Math.Max(vent.X1, vent.X2);
                    for (int i =min; i <= max; i++)
                    {
                        if(buckets.ContainsKey((i, vent.Y1))){
                            buckets[(i, vent.Y1)] = buckets[(i, vent.Y1)] + 1;
                        } else {
                            buckets[(i, vent.Y1)] = 1;
                        }
                    }
                }
                else
                {
                    //Diag
                    
                    if(vent.X1 < vent.X2)
                    {
                        if(vent.Y1 < vent.Y2)
                        {
                            int delta = 0;
                            for (int i = vent.X1; i <= vent.X2; i++)
                            {
                                var point = (vent.X1 + delta, vent.Y1 + delta);
                                if(buckets.ContainsKey(point)){
                                    buckets[point] = buckets[point] + 1;
                                } else {
                                    buckets[point] = 1;
                                }
                                delta++;
                            }
                        } else {
                            int delta = 0;
                            for (int i = vent.X1; i <= vent.X2; i++)
                            {
                                var point = (vent.X1 + delta, vent.Y1 - delta);
                                if(buckets.ContainsKey(point)){
                                    buckets[point] = buckets[point] + 1;
                                } else {
                                    buckets[point] = 1;
                                }
                                delta++;
                            }
                        }
                    } else {                        
                        if(vent.Y1 < vent.Y2)
                        {
                            int delta = 0;
                            for (int i = vent.X2; i <= vent.X1; i++)
                            {
                                var point = (vent.X1 - delta, vent.Y1 + delta);
                                if(buckets.ContainsKey(point)){
                                    buckets[point] = buckets[point] + 1;
                                } else {
                                    buckets[point] = 1;
                                }
                                delta++;
                            }
                        } else {
                            int delta = 0;
                            for (int i = vent.X2; i <= vent.X1; i++)
                            {
                                var point = (vent.X1 - delta, vent.Y1 - delta);
                                if(buckets.ContainsKey(point)){
                                    buckets[point] = buckets[point] + 1;
                                } else {
                                    buckets[point] = 1;
                                }
                                delta++;
                            }
                        }
                    }

                }

                
             Console.WriteLine($"Round {round}");
             Console.WriteLine($"Line : {vent}");
             
                // for (int row = 0; row <= buckets.Max(b => b.Key.y); row++)
                // {
                //     string r = string.Empty;
                //     for (int col = 0; col <= buckets.Max(b => b.Key.x); col++)
                //     {
                //         if(buckets.ContainsKey((col,row))){
                //             r += buckets[(col,row)];
                //         } else {
                //             r += '.';
                //         }
                //     }
                //     Console.WriteLine(r);
                // }
             round++;
            }
            Console.WriteLine($"Overlaps {buckets.Count(b => b.Value > 1)}");

        }

        class Vent
        {
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
          

            public override string ToString()
            {
                return $"{X1},{Y1} -> {X2},{Y2}";
            }
        }
    }
}