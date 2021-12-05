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
                if(vent.Y1 == vent.Y2){
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
            }

             Console.WriteLine($"Buckets");
            foreach (var bucket in buckets)
            {
                Console.WriteLine($"{bucket.Key.x},{bucket.Key.y}: {bucket.Value}");
                
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