namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var cards = new List<BingoCard>();
            var drawPile = new Queue<int>();

            bool readFirstLine = false;
            int cardNumber = 1;
            int row = 0;
            int col = 0;
            var cardToFill = new BingoCard();
            cardToFill.Id = cardNumber;
            cards.Add(cardToFill);

            BingoCard? lastCardToWin = null;

            foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\input.txt"))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (!readFirstLine)
                {
                    readFirstLine = true;
                    //Enqueue bingo numbers
                    var numbers = line.Split(",");
                    foreach (var number in numbers)
                    {
                        drawPile.Enqueue(int.Parse(number));
                    }
                }
                else
                {
                    if (row == BingoCard.ROWS)
                    {
                        //New card
                        cardNumber++;
                        row = 0;
                        cardToFill = new BingoCard();
                        cardToFill.Id = cardNumber;
                        cards.Add(cardToFill);
                    }
                    var cleanLine = line.Replace("  ", " ");
                    var numbers = cleanLine.Split(" ");
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(numbers[i])) continue;
                        Console.WriteLine($"{numbers[i]}");
                        var number = int.Parse(numbers[i]);
                        cardToFill.Numbers[row, col] = number;
                        col++;
                    }
                    col = 0;
                    row++;
                }
            }

            Console.WriteLine($"Cards:");
            foreach (var card in cards)
            {
                card.PrintCardWithNumbers();
            }

            int sumOfUnmarkedNumber = 0;
            int finalScore = 0;
            int round = 0;
            foreach (var drawn in drawPile)
            {
                round++;
                Console.WriteLine($"ROUND {round} - Drew {drawn}");
                foreach (var card in cards)
                {
                    if (card.FinalScore == 0)
                    {
                        card.MarkNumber(drawn);
                    }
                }
                Console.WriteLine($"Cards:");
                foreach (var card in cards)
                {
                    card.PrintCardWithNumbers();
                }

                Console.WriteLine($"Checking for winner...");
                foreach (var card in cards)
                {
                    if (card.IsBingo())
                    {
                        if (card.FinalScore == 0)
                        {
                            Console.WriteLine($"Card {card.Id} wins!");

                            card.PrintCardWithNumbers();
                            card.PrintCardMarkedOnly();

                            sumOfUnmarkedNumber = card.GetAllUnmarkedNumbers().Sum();
                            finalScore = sumOfUnmarkedNumber * drawn;
                            Console.WriteLine($"Final Score: {finalScore}");
                            card.FinalScore = finalScore;
                            lastCardToWin = card;
                        }
                    }
                }
            }

            Console.WriteLine($"Card {lastCardToWin.Id} is the final winner!");

            lastCardToWin.PrintCardWithNumbers();
            lastCardToWin.PrintCardMarkedOnly();

            sumOfUnmarkedNumber = lastCardToWin.GetAllUnmarkedNumbers().Sum();
            Console.WriteLine($"Final Score: {lastCardToWin.FinalScore}");
        }
    }

    class BingoCard
    {
        public int Id;
        public const int ROWS = 5;
        public const int COLS = 5;
        public int[,] Numbers = new int[ROWS, COLS];
        public bool[,] Called = new bool[ROWS, COLS];
        public int FinalScore;

        public bool IsBingo()
        {
            for (int r = 0; r < ROWS; r++)
            {
                if (Called[r, 0] && Called[r, 1] && Called[r, 2] && Called[r, 3] && Called[r, 4])
                {
                    return true;
                }
            }
            for (int c = 0; c < COLS; c++)
            {
                if (Called[0, c] && Called[1, c] && Called[2, c] && Called[3, c] && Called[4, c])
                {
                    return true;
                }
            }
            return false;
        }

        public void MarkNumber(int number)
        {
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    if (Numbers[r, c] == number)
                    {
                        Called[r, c] = true;
                        Console.WriteLine($"Bingo Card #{Id} was marked at {r},{c}");
                    }
                }
            }
        }

        public List<int> GetAllUnmarkedNumbers()
        {
            var list = new List<int>();
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    if (!Called[r, c])
                    {
                        list.Add(Numbers[r, c]);
                    }
                }
            }
            return list;
        }

        public void PrintCardWithNumbers()
        {
            Console.WriteLine($"Bingo Card #{Id} Status:");
            for (int r = 0; r < ROWS; r++)
            {
                string row = string.Empty;
                for (int c = 0; c < COLS; c++)
                {
                    if (Called[r, c])
                    {
                        row += $"*{Numbers[r, c]:00}*";
                    }
                    else
                    {
                        row += $" {Numbers[r, c]:00} ";
                    }
                }
                Console.WriteLine(row);
            }
        }
        public void PrintCardMarkedOnly()
        {
            Console.WriteLine($"Bingo Card #{Id} Status:");
            for (int r = 0; r < ROWS; r++)
            {
                string row = string.Empty;
                for (int c = 0; c < COLS; c++)
                {
                    if (Called[r, c])
                    {
                        row += $"x";
                    }
                    else
                    {
                        row += $" ";
                    }
                }
                Console.WriteLine(row);
            }
        }
    }
}