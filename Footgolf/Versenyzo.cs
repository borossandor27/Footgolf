using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footgolf
{
    class Versenyzo
    {
        static readonly int fordulok = 8;
        string nev;
        string kategoria;
        string egyesulet;
        int[] pontok = new int[fordulok];
        int osszpont;

        public string Nev { get => nev; set => nev = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }
        public string Egyesulet { get => egyesulet; set => egyesulet = value; }
        public int[] Pontok { get => pontok; set => pontok = value; }
        public int Osszpont { get => osszpont; set => osszpont = value; }

        public Versenyzo(string[] sor)
        {
            Nev = sor[0];
            Kategoria = sor[1];
            Egyesulet = sor[2];
            for (int i = 0; i < fordulok; i++)
            {
                pontok[i] = int.Parse(sor[i + 3]);
            }
            osszpont = osszpontszam();
        }

        int osszpontszam()
        {
            int ossz = 0;
            int[] p = pontok.OrderByDescending(a => a).ToArray(); //-- rendezett másolat készítés
            for (int i = 0; i < 6; i++)
            {
                ossz += p[i];
            }
            /*
             * Ha a versenyző legrosszabb egy vagy két eredménye nem nulla,
             * akkor a versenyzőnek az összpontszámába bele kell számítani
             * azt a 10 pont bónuszt, amelyet ezekben a fordulókban megkapott.
             */
            ossz += p[6] > 0 ? 10 : 0;
            ossz += p[7] > 0 ? 10 : 0;
            return ossz;
        }

    }
}
