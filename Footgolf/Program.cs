using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Footgolf
{
    class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();
        static void Main(string[] args)
        {
            Beolvas(@"..\..\fob2016.txt");
            Console.WriteLine($"\n3. feladat: Versenyzők száma: {versenyzok.Count}");
            
            double atl = 100.0 * versenyzok.Count(a => a.Kategoria.Equals("Noi")) / versenyzok.Count;
            Console.WriteLine($"\n4. feladat: A női versenyzők aránya: {atl.ToString("0.00")}%");

            Console.WriteLine("\n6. feladat: A bajnok női versenyző");
            Versenyzo noiBajnok = versenyzok.Find(a => a.Kategoria.Equals("Noi") && a.Osszpont == versenyzok.FindAll(b => b.Kategoria.Equals("Noi")).Max(c => c.Osszpont));
            Console.WriteLine($"\tNév: {noiBajnok.Nev}");
            Console.WriteLine($"\tEgyesület: {noiBajnok.Egyesulet}");
            Console.WriteLine($"\tÖsszpont: {noiBajnok.Osszpont}");

            Console.WriteLine("\n7. feladat: ");
            Kiir();

            Console.WriteLine("\n8. feladat: Egyesület statisztika");

            foreach (var item in versenyzok.GroupBy(a => a.Egyesulet).Select(b => new { egyesulet = b.Key, fo = b.Count() }).Where(c => c.fo > 2 && !c.egyesulet.Equals("n.a."))) 
            {
                Console.WriteLine($"\t{item.egyesulet} - {item.fo} fő");
            }
            Console.WriteLine("\nProgram vége!");
            Console.ReadKey();
        }
        /// <summary>
        /// 2. Olvassa be a fob2016.txt állományban lévő adatokat és tárolja el
        ///    egy olyan adatszerkezetben, amely a további feladatok megoldására 
        ///    alkalmas!
        /// </summary>
        /// <param name="forras">fob2016.txt elérési úttal</param>
        static void Beolvas(string forras)
        {
            Console.WriteLine("2. feladat: Adatok beolvasása...");
            try
            {
                using (StreamReader sr = new StreamReader(forras))
                {
                    while (!sr.EndOfStream)
                    {
                        versenyzok.Add(new Versenyzo(sr.ReadLine().Split(';')));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        static void Kiir()
        {
            Console.WriteLine("Férfi versenyzők kiírása...");
            using (StreamWriter sw = new StreamWriter("osszpontFF.txt"))
            {
                foreach (var item in versenyzok.FindAll(a => a.Kategoria.Equals("Felnott ferfi")))
                {
                    sw.WriteLine($"{item.Nev};{item.Osszpont}");
                }
            }
        }
    }
}
