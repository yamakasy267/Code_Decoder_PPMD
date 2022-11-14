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
                    byte[] alphabet = buffer.Distinct().ToArray();
                    
                }
                break;
            }

        }
        Dictionary<byte, int> Add_Dic(Dictionary<byte, int> Count, byte I)
        {
            if (Count.ContainsKey(I))
            {
                Count[I]++;
            }
            else
            {
                Count.Add(I, 1);
            }
            return Count;
        }
        void Coder_PPMA(byte[] buffer)
        {
            Dictionary<string,List<byte>> Contecst = new Dictionary<string,List<byte>>();
            Dictionary<byte,int> Count = new Dictionary<byte,int>();
            int D = 5;
            string S;
            int c = 0;
            int PEsc;
            int PS;
            for(int i = 0;i< buffer.Length;i++)
            {
                if (c == D)
                {
                    D = 5;
                    while (D > 0)
                    {
                        S = string.Join(" ", buffer[(i-D)..(i-1)]);
                        if (Contecst.ContainsKey(S))
                        {
                            if (Contecst[S].Contains(buffer[i]))
                            {
                                
                            }
                            else
                            {
                                
                            }
                        }
                        else
                        {
                            
                            D--;
                        }
                        
                    }
                    Count = Add_Dic(Count, buffer[i]);
                }
                else
                {

                }
            }
        }
    }
}
