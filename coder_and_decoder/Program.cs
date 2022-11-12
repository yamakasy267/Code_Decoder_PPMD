using System;
using System.IO;
using System.Text;
using System.Linq;


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
                    string textFromFile = Encoding.Default.GetString(buffer);
                    Array.ForEach(alphabet, i => Console.WriteLine(i));
                }
                break;
            }

        }
    }
}
