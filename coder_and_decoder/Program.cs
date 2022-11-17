using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Numerics;

namespace ConsoleApplication
{
    class Program
    {
        struct Segment
        {
           public double left;
           public double right;
        }

        static void Main(string[] args)
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
                    var pr = new Program();
                    pr.PPMA(buffer, alphabet);

                }
                break;
            }

        }

        void PPMA(byte[] buffer, Dictionary<byte, int> count)
        {
            Dictionary<string,Dictionary<byte,int>> Contecst = new Dictionary<string, Dictionary<byte, int>>();
            int D = 5;
            int context_nul = 1;
            string S;
            int c = 1;
            double PEsc=1;
            double PS =1.0/256;
            Coder(PS, PEsc);
            Contecst.Add("#", new Dictionary<byte, int>() { { buffer[0], 1 } });
            count[buffer[0]]++;
            for (int i = 1; i < buffer.Length; i++)
            {
                byte p = buffer[i];
                PEsc = 1.0;
                List<byte> exception = new List<byte>();
                if (c != D)
                {
                    for (int j = c; j > 0; j--)
                    {

                        S = String.Join(" ", buffer[(i - j)..(i)]);
                        if (Contecst.ContainsKey(S))
                        {
                            if (Contecst[S].ContainsKey(p))
                            {
                                PS = Contecst[S][p] / (Contecst[S].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                Coder(PS, PEsc);
                                Contecst[S][p]++;
                                Contecst["#"][p]++;
                                count[p]++;
                                break;
                            }
                            else
                            {
                                PEsc *= 1.0 / (Contecst[S].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                exception.AddRange(Contecst[S].Keys);
                                exception = exception.Distinct().ToList();
                                Contecst[S].Add(p, 1);
                                if (j == 1)
                                {
                                    if (count[p] != 0)
                                    {
                                        PS = 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                        Coder(PS, PEsc);
                                        count[p]++;
                                        Contecst["#"][p]++;
                                    }
                                    else
                                    {
                                        PEsc *= 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                        var M = count.Where(kpv => kpv.Value > 0);
                                        PS = 1.0 / (256 - M.Count());
                                        Coder(PS, PEsc);
                                        count[p]++;
                                        Contecst["#"].Add(p, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (j == 1)
                            {
                                if (count[p] != 0)
                                {
                                    PS = 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                    Coder(PS, PEsc);
                                    Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
                                    count[p]++;
                                    Contecst["#"][p]++;
                                }
                                else
                                {
                                    PEsc *= 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                    var M = count.Where(kpv => kpv.Value > 0);
                                    PS = 1.0 / (256 - M.Count());
                                    Coder(PS, PEsc);
                                    Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
                                    count[p]++;
                                    Contecst["#"].Add(p, 1);
                                }
                            }
                            else
                            {
                                Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
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

                                PS = (double)Contecst[S][p] / (Contecst[S].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                Coder(PS, PEsc);
                                Contecst[S][p]++;
                                Contecst["#"][p]++;
                                count[p]++;
                                break;
                            }
                            else
                            {
                                PEsc *= 1.0 / (Contecst[S].Where(kpv=> !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                exception.AddRange(Contecst[S].Keys);
                                exception = exception.Distinct().ToList();
                                Contecst[S].Add(p, 1);
                                if (j == 1)
                                {
                                    if (count[p] != 0)
                                    {
                                        PS = (double)count[p] / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                        Coder(PS, PEsc);
                                        count[p]++;
                                        Contecst["#"][p]++;
                                    }
                                    else
                                    {
                                        PEsc *= 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                        var M = count.Where(kpv => kpv.Value > 0);
                                        PS = 1.0 / (256 - M.Count());
                                        Coder(PS, PEsc);
                                        count[p]++;
                                        Contecst["#"].Add(p, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (j == 1)
                            {
                                if (count[p] != 0)
                                {
                                    PS = (double)count[p] / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                    Coder(PS, PEsc);
                                    Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
                                    count[p]++;
                                    Contecst["#"][p]++;
                                }
                                else
                                {
                                    PEsc *= 1.0 / (Contecst["#"].Where(kpv => !exception.Contains(kpv.Key)).Sum(x => x.Value) + 1);
                                    var M = count.Where(kpv => kpv.Value > 0);
                                    PS = 1.0 / (256 - M.Count());
                                    Coder(PS, PEsc);
                                    Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
                                    count[p]++;
                                    Contecst["#"].Add(p, 1);
                                }
                            }
                            else
                            {
                                Contecst.Add(S, new Dictionary<byte, int>() { { p, 1 } });
                            }

                        }


                    }

                }
            }
            

        }
        void Coder(double PS,double PEsc)
        {
            //List <List<double>> nach = new List<List<double>>() { new List<double>() {1,1.0/256 }, new List<double>() { 1.0/2, 1.0 / 255 }, new List<double>() { 1.0 / 3, 1.0 / 254 }, new List<double>() { 1.0 / 4, 1.0 / 253 }, new List<double>() { 1.0 / 5, 1.0 / 252 }, new List<double>() { 1.0, 1.0 / 6 }, new List<double>() { 1.0 / 12, 1.0 / 251 }, new List<double>() { 1.0 / 8, 1.0 / 250 }, new List<double>() { 1.0 / 9, 1.0 / 249 }, new List<double>() { 1.0 , 1.0 / 10 }, new List<double>() { 1.0 / 18, 1.0 / 248 }, new List<double>() { 1.0 / 12, 1.0 / 247 }, new List<double>() { 1.0 , 2.0 /13 }, new List<double>() { 1.0 / 36, 1.0 / 246 }, new List<double>() { 1.0 , 1.0 / 15 }, new List<double>() { 1.0 / 2, 3.0 / 15 }, new List<double>() { 1.0 / 4, 1.0 / 14 }, new List<double>() { 1.0 / 32, 1.0 / 245 }, new List<double>() { 1.0 , 4.0 / 19 }, new List<double>() { 1.0 , 1.0 / 5 }, new List<double>() { 1.0 , 1.0 / 2 }, new List<double>() { 1.0 , 1.0 / 2 }, new List<double>() { 1.0 / 2, 2.0 / 5 }, new List<double>() { 1.0 / 3, 2.0 / 22 }, new List<double>() { 1.0 / 54, 1.0 / 244 }, new List<double>() { 1.0 / 26, 1.0 / 243 }, new List<double>() { 1.0, 1.0 / 27 }, new List<double>() { 1.0 / 2, 6.0 / 25 }, new List<double>() { 1.0 , 3.0 / 7 }, new List<double>() { 1.0 , 2.0 / 4 }, new List<double>() { 1.0 , 2.0 / 3 }, new List<double>() { 1.0 / 9, 1.0 / 23 }, new List<double>() { 1.0 / 50, 1.0 / 242 }, new List<double>() { 1.0 , 3.0 / 34 }, new List<double>() { 1.0 , 1.0 / 4 }, new List<double>() { 1.0 , 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0 / 2, 1.0 / 5 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0/2, 1.0 / 3 }, new List<double>() { 1.0, 1.0 / 2 }, new List<double>() { 1.0, 1.0 / 2 } };
            PS = PS * PEsc;
            
            
        }
        Segment[] defineSegments(Dictionary<byte, int> alphabet)
        {
            double p = 1/256;
            double curLeft = 0;
            Segment[] segments = new Segment[256];
            double curRight = p;
            for (int i = 0; i < 255; i++)
            {
                segments[i].left = curLeft;
                segments[i].right = curRight;
                curLeft = curRight;
                curRight = curRight + p;
            }
            return segments;
        }
        Dictionary<byte, Segment> resizeSegments(double PS,byte x, Segment[] segments)
        {
            double l = segments[x].left;
            for (var i = 0; i < 256; i++)
            {

            }

        }
        Dictionary<byte, List<double>> DefineWeights(Dictionary<byte, double> alphabet)
        {
            Dictionary<byte, List<double>> weights = new Dictionary<byte, List<double>>();
            weights.Add(0, new List<double>() { 1/256,0});
            for(byte i = 1; i < 255; i++)
            {
                weights[i][0] = 1 / 256;
                weights[i][1] = weights[i--][1] + weights[i][0];
            }
            return weights;
        }
    }
}
