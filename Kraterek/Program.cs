using System;

namespace Kraterek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //beolvasas
            List<Krater> kraterek = new List<Krater>();
            StreamReader r = new StreamReader("felszin_tvesszo.txt");
            while (!r.EndOfStream)
            {
                string sor = r.ReadLine();
                Krater k = new Krater();
                k.kozeppont_x = Double.Parse(sor.Split('\t')[0]);
                k.kozeppont_y = Double.Parse(sor.Split('\t')[1]);
                k.sugar = Double.Parse(sor.Split('\t')[2]);
                string nev = "";
                for (int i = 3; i < sor.Split('\t').Count(); i++)
                {
                    nev += sor.Split('\t')[i];
                }
                k.nev = nev;
                kraterek.Add(k);
            }

            Console.WriteLine("2. feladat");
            Console.WriteLine("A kráterek száma: "+kraterek.Count);
            Console.WriteLine("3. feladat");
            Console.Write("Kérem egy kráter nevét: ");
            string beker = Console.ReadLine();
            Krater nevegyezik = new Krater();
            try
            {
                nevegyezik = kraterek.First(x => x.nev == beker);
                Console.WriteLine($"A(z) {nevegyezik.nev} középpontja X={nevegyezik.kozeppont_x} Y={nevegyezik.kozeppont_y} sugara R={nevegyezik.sugar}");
            }
            catch (Exception)
            {
                Console.WriteLine("Nincs ilyen nevű kráter.");
            }            
            Console.WriteLine("4. feladat");
            Krater legnagyobb = kraterek.OrderByDescending(x => x.sugar).ToList()[0];
            Console.WriteLine("A legnagyobb kráter neve és sugara: "+legnagyobb.nev+" "+legnagyobb.sugar);

            double tavolsag(double x1, double y1, double x2, double y2)
            {
                double tav = 0;
                tav = Math.Sqrt((x2- x1) * (x2 - x1) + (y2 - y1)* (y2 - y1));
                return tav;
            }

            Console.WriteLine("6. feladat");
            Console.Write("Kérem egy kráter nevét: ");
            beker = Console.ReadLine();
            Krater megadottkrater = new Krater();
            try
            {
                megadottkrater = kraterek.First(x => x.nev == beker);
                List<Krater> nincskozosresze = new List<Krater>();
                foreach (Krater k in kraterek)
                {
                    if (tavolsag(megadottkrater.kozeppont_x, megadottkrater.kozeppont_y, k.kozeppont_x, k.kozeppont_y) > k.sugar + megadottkrater.sugar)
                    {
                        nincskozosresze.Add(k);
                    }
                }
                if (nincskozosresze.Count > 0)
                {
                    Console.Write("Nincs közös része: ");
                    for (int i = 0; i < nincskozosresze.Count; i++)
                    {
                        if (i + 1 != nincskozosresze.Count)
                        {
                            Console.Write(nincskozosresze[i].nev + ", ");
                        }
                        else
                        {
                            Console.WriteLine(nincskozosresze[i].nev + ".");
                        }

                    }
                }
            }
            catch (Exception)
            {
            }

            Console.WriteLine("7. feladat");

            List<string> kiirasok = new List<string>();

            void bennevan(Krater k1, Krater k2)
            {
                Krater nagyobbkrater = new Krater();
                Krater kisebbkrater = new Krater();
                if (k1.sugar > k2.sugar)
                {
                    nagyobbkrater = k1;
                    kisebbkrater = k2;
                }
                else
                {
                    nagyobbkrater = k2;
                    kisebbkrater = k1;
                }
                if (tavolsag(k1.kozeppont_x, k1.kozeppont_y, k2.kozeppont_x, k2.kozeppont_y) < (nagyobbkrater.sugar - kisebbkrater.sugar))
                {
                    kiirasok.Add($"A(z) {nagyobbkrater.nev} kráter tartalmazza a(z) {kisebbkrater.nev} krátert.");
                }
            }

            foreach (Krater k1 in kraterek)
            {
                foreach (Krater k2 in kraterek)
                {
                    if (k1 != k2)
                    {
                        bennevan(k1, k2);
                    }
                }
            }
            kiirasok = kiirasok.Distinct().ToList();
            foreach (string kiiras in kiirasok)
            {
                Console.WriteLine(kiiras);
            }

            StreamWriter s = new StreamWriter("terulet.txt");
            foreach (Krater k in kraterek)
            {
                s.WriteLine(Math.Round(k.sugar*k.sugar*3.14, 2)+"\t"+k.nev);
            }
            s.Close();

        }

        public class Krater
        {
            public double kozeppont_x { get; set; }
            public double kozeppont_y { get; set; }
            public double sugar { get; set; }
            public string nev { get; set; }
        }
    }
}