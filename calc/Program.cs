namespace calc
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Исходное выражение: ");
                Console.WriteLine(ReversePolishNotation.Calculate(Console.ReadLine()));
            }
        }
    }
    class ReversePolishNotation
    {
        static private bool IsDelimeter(char c) { return " =".IndexOf(c) != -1; }
        static private bool IsOperator(char с) { return "+-/*()".IndexOf(с) != -1; }
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                    return 2;
                case '-':
                    return 3;
                case '*':
                    return 4;
                case '/':
                    return 5;
                default:
                    return 6;
            }
        }
        static public double Calculate(string input)
        {
            return Counting(ConvertExpression(input));
        }
        static private string ConvertExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i])) continue;

                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--;
                    continue;
                }

                if (input[i] == '(')
                    operStack.Push(input[i]);
                else if (input[i] == ')')
                {
                    char s = operStack.Pop();

                    while (s != '(')
                    {
                        output += s.ToString() + ' ';
                        s = operStack.Pop();
                    }
                }
                else
                {
                    if (operStack.Count > 0)
                        if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                            output += operStack.Pop().ToString() + " ";

                    operStack.Push(char.Parse(input[i]
                                                  .ToString()));
                }
            }

            while (operStack.Count > 0) output += operStack.Pop() + " ";

            return output;
        }
        static private double Counting(string input)
        {
            double result = 0;
            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]) &&
                           i != input.Length)
                    {
                        a += input[i];
                        i++;
                    }
                    temp.Push(double.Parse(a));
                    i--;
                }
                else if (IsOperator(input[i]))
                {
                    double c = temp.Pop();
                    double d = temp.Pop();

                    switch (input[i])
                    {
                        case '+':
                            result = d + c;
                            break;
                        case '-':
                            result = d - c;
                            break;
                        case '*':
                            result = d * c;
                            break;
                        case '/':
                            result = d / c;
                            break;
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }
    }
}
