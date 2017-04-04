using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AnalyzerClass
{
    public class Analyzer
    {
        private static int ErrPosition = 0;
        public static string Expression = "";
        public static bool ShowMessage = true;
        public static bool CheckCurrency()
        {
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
            return Expression;
        }
        public static ArrayList CreateStack()
        {
            ArrayList Stack = new ArrayList();

            return Stack;
        }
        public static string RunEstimate()
        {
            string[] rpn = Expression.Split(' ');
            ArrayList Stack = CreateStack();
            int number = 0;

            foreach(string s in rpn)
            {
                if (int.TryParse(s, out number))
                    Stack.Add(s);
                else
                {
                    switch(s)
                    {

                    }
                }
            }

        }
        public static string Estimate()
        {
            //CheckCurrency();
            //Format();
            //RunEstimate();
        }
    }

    public static class ArrayListExtension
    {
        public static object Pop(this ArrayList list)
        {
            object T = list[list.Capacity];
            list.RemoveAt(list.Capacity);
            return T;
        }
    }
}
