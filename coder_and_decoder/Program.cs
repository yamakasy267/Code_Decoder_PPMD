using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Numerics;

namespace ConsoleApplication
{
    class Program
    {
        static  void Main(string[] args)
        {
            string? path = Console.ReadLine();
            string[] dirs = Directory.GetFiles(path);
            foreach (string p in dirs)
            {
                using (FileStream fstream = File.OpenRead(p))
                {
                    byte[] buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    Dictionary<byte,int> alphabet = new Dictionary<byte,int>();
                    for(byte i=0,j = 255; i <j ; i++)
                    {
                        alphabet[i] = 0;
                        
                    }
                    alphabet[255] = 0;

                }
                break;
            }

        }

        void PPMA(byte[] buffer, Dictionary<byte, int> count)
        {
            Dictionary<string,Dictionary<byte,int>> Contecst = new Dictionary<string, Dictionary<byte, int>>();
            int D = 5;
            int context_nul = 0;
            string S;
            int c = 0;
            double PEsc=0;
            double PS;
            for (int i = 0; i < buffer.Length; i++)
            {
                bool flag = false;
                byte p = buffer[i];
                if (c != D)
                {
                    for(int j = c; j > 0; j--)
                    {
                        
                        S = String.Join(" ", buffer[(i - j)..(i)]);
                        if (Contecst.ContainsKey(S))
                        {
                            if (Contecst[S].ContainsKey(p))
                            {
                                PS = Contecst[S][p] / (Contecst[S].Sum(x => x.Value) + 1);
                                Coder(PS);
                                Contecst[S][p]++;
                                break;
                            }
                            else
                            {
                                PEsc *= 1 / (Contecst[S].Sum(x => x.Value) + 1);
                                Contecst[S].Add(p, 1);
                            }
                        }
                       
                    }
                    
                    c++;
                }
                else
                {
                    for (int j = D; j > 0; j--)
                    {

                        S = String.Join(" ", buffer[(i - j)..(i)]);
                        if (Contecst.ContainsKey(S))
                        {
                            if (Contecst[S].ContainsKey(p))
                            {
                                PS = Contecst[S][p] / (Contecst[S].Sum(x => x.Value) + 1);
                                Coder(PS);
                                Contecst[S][p]++;
                                flag = true;
                                break;
                            }
                            else
                            {
                                PEsc *= 1 / (Contecst[S].Sum(x => x.Value) + 1);
                                Contecst[S].Add(p, 1);
                            }
                        }
                        else
                        {
                            Contecst[S][p] = 1;
                        }

                    }
                    if (!flag)
                    {

                    }

                }
            }

        }

        void Coder(double PS)
        {

        }
    }
}
