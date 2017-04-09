using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using CalcClass;

namespace AnalyzerClass
{
    public class Analyzer
    {
        private static int ErrPosition = 0;
        public static string Expression = "";
        public static bool ShowMessage = true;
        public static bool CheckCurrency()
        {
            if (string.IsNullOrEmpty(Expression)) throw new Exception("Error 05");
            Expression.Replace(" ", string.Empty);
            if (Expression.Length > 30) throw new Exception("Error 08");

            Expression.ToLower();
            // % = mod
            Expression.Replace("mod", "%");
            
            int OpenBracketsCount = 0;
            for (int i = 0; i < Expression.Length; i++)
            {
                switch(Expression[i])
                {
                    case '-':
                    case '+':
                    case 'm':
                    case 'p':
                        {
                            if (i == Expression.Length - 1)
                                throw new Exception("Error 05");
                            if (!"0123456789(".Contains(Expression[i + 1]))
                            {
                                ErrPosition = i + 1;
                                throw new Exception(string.Format("Error 04 at <{0}>", ErrPosition));
                            }
                            continue;
                        }
                    case '*':
                    case '/':
                    case '%':
                        {
                            if (i == 0)
                                throw new Exception("Error 03");
                            if (i == Expression.Length - 1)
                                throw new Exception("Error 05");
                            if (!"0123456789(".Contains(Expression[i+1]))
                            {
                                ErrPosition = i + 1;
                                throw new Exception(string.Format("Error 04 at <{0}>", ErrPosition));
                            }
                            continue;
                        }
                    case '(':
                        {
                            OpenBracketsCount++;
                            if (OpenBracketsCount > 3)
                            {
                                ErrPosition = i + 1;
                                throw new Exception(string.Format("Error 01 at <{0}>", ErrPosition));
                            }
                            if (i != 0)
                                if ("0123456789".Contains(Expression[i-1]))
                                {
                                    ErrPosition = i;
                                    throw new Exception("Error 03");
                                }
                            continue;
                        }
                    case ')':
                        {
                            OpenBracketsCount--;
                            if (OpenBracketsCount < 0)
                            {
                                ErrPosition = i + 1;
                                throw new Exception(string.Format("Error 01 at <{0}>", ErrPosition));
                            }
                            continue;
                        }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        continue;
                    default:
                        ErrPosition = i + 1;
                        throw new Exception(string.Format("Error 02 at <{0}>", ErrPosition));
                }
            }
            return true;
        }
        public static string Format()
        {
            for(int i = 0; i < Expression.Length; i++)
            {
                if (i == Expression.Length - 1) break;
                if ("()+-mp%*/".Contains(Expression[i])) Expression = Expression.Insert(i + 1, " ");
                if ("0123456789".Contains(Expression[i]))
                {
                    if ("0123456789".Contains(Expression[i + 1])) continue;
                    else Expression = Expression.Insert(i + 1, " ");
                }
            }
            return Expression;
        }
        public static ArrayList CreateStack()
        {
            ArrayList Stack = new ArrayList();
            string[] tokens = Expression.Split(' ');

            Stack<string> tmp = new Stack<string>();
            int n;

            foreach (string s in tokens)
            {
                if (int.TryParse(s.ToString(), out n))
                {
                    Stack.Add(s);
                }
                if (s == "(")
                {
                    tmp.Push(s);
                }
                if (s == ")")
                {
                    while (tmp.Count != 0 && tmp.Peek() != "(")
                    {
                        Stack.Add(tmp.Pop());
                    }
                    tmp.Pop();
                }
                if ("+-mp*/%".Contains(s))
                {
                    while (tmp.Count != 0 && Priority(tmp.Peek()) >= Priority(s))
                    {
                        Stack.Add(tmp.Pop());
                    }
                    tmp.Push(s);
                }
            }
            while (tmp.Count != 0)
            {
                Stack.Add(tmp.Pop());
            }

            return Stack;
        }
        public static string RunEstimate()
        {
            ArrayList Stack = CreateStack();

            Stack<int> tmp = new Stack<int>();
            int n;

            foreach(string s in Stack)
            {
                if (int.TryParse(s, out n))
                {
                    tmp.Push(n);
                }
                else
                {
                    switch(s)
                    {
                        case "*":
                            {
                                tmp.Push(MathFunction.Mult(tmp.Pop(), tmp.Pop()));
                                break;
                            }
                        case "/":
                            {
                                n = tmp.Pop();
                                tmp.Push(MathFunction.Div(tmp.Pop(), n));
                                break;
                            }
                        case "%":
                            {
                                n = tmp.Pop();
                                tmp.Push(MathFunction.Mod(tmp.Pop(), n));
                                break;
                            }
                        case "+":
                            {
                                tmp.Push(MathFunction.Add(tmp.Pop(), tmp.Pop()));
                                break;
                            }
                        case "-":
                            {
                                n = tmp.Pop();
                                tmp.Push(MathFunction.Sub(tmp.Pop(), n));
                                break;
                            }
                        case "m":
                            {
                                tmp.Push(MathFunction.IABS(tmp.Pop()));
                                break;
                            }
                        case "p":
                            {
                                tmp.Push(MathFunction.ABS(tmp.Pop()));
                                break;
                            }
                        default:
                            throw new Exception("Error in Calculating");
                    }
                }
            }

            return tmp.Pop().ToString();
        }
        public static string Estimate()
        {
            if (CheckCurrency())
            {
                Expression = Format();
                return RunEstimate();
            }
            throw new Exception("Error 03");
        }
        public static int Priority(string s)
        {
            if (s == "*" || s == "/" || s == "%")
                return 2;
            else if (s == "+" || s == "-" || s == "m" || s == "p")
                return 1;
            else return 0;            
        }
}
