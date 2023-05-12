using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace slagerlista
{
    class Program
    {
        static Dictionary<string, int> zenek = new Dictionary<string, int>();
        static string honnan = "";
        static string hova = "";
        static void Main(string[] args)
        {
            adatokbetoltese();
            //megkérdezzük a felhasználót, hogy hová szeretné menteni az adatokat
            do
            {
                Console.Clear();
                Console.WriteLine("Hol szeretné tárolni az adatokat?");
                Console.WriteLine("[1] Szöveges állományba");
                Console.WriteLine("[2] Adatbázisba");
                ConsoleKey valasz = Console.ReadKey().Key;
                switch (valasz)
                {
                    case ConsoleKey.D1:
                        //fájlba mentés
                        hova = "file";
                        break;
                    case ConsoleKey.D2:
                        hova = "database";
                        break;
                    default:

                        break;
                }
            } while (hova == "");
            //amíg a program fut addíg a menüt jeleníti meg
            while (true)
            {
                menu();
            }
        }
        static void adatokbetoltese()
        {
            //megkérdezzük a felhasználót, hogy honnan szeretné betölteni az adatokat
            do
            {
                Console.Clear();
                Console.WriteLine("Honnan szeretné betölteni az adatokat?");
                Console.WriteLine("[1] Szöveges állományból");
                Console.WriteLine("[2] Adatbázisból");
                Console.WriteLine("[3] Nem kíván betölteni adatot");
                ConsoleKey valasz = Console.ReadKey().Key;
                switch (valasz)
                {
                    case ConsoleKey.D1:
                        //fájlba mentés
                        honnan = "file";
                        break;
                    case ConsoleKey.D2:
                        honnan = "database";
                        break;
                    case ConsoleKey.D3:
                        honnan = "sehonnan";
                        break;
                    default:

                        break;
                }
            } while (honnan == "");
            
            //ha fájlt választott a felhasználó
            if (honnan == "file")
            {
                //beolvassuk a fájlt ha létezik
                if (File.Exists("slagerlista.txt"))
                {
                    string[] fajl = File.ReadAllLines("slagerlista.txt");
                    for (int i = 0; i < fajl.Length; i++)
                    {
                        int szavazat = int.Parse(fajl[i].Split('\t')[0]);
                        string cim = fajl[i].Split('\t')[1];
                        zenek.Add(cim, szavazat);
                    }

                }
            }//ha adatbázist választott af elhasználó
            if(honnan == "database")
            {
                try
                {

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);;
                }
            }
        }
        static void menu()
        {
            //menü megjelenítése, és a menüpontok kiválasztása
            string[] menupontok = { "Új adatok bevitele" , "Top 10 kiíratása","Kilépés"};

            Console.Clear();
            for (int i = 0; i < menupontok.Length; i++)
            {
                Console.WriteLine($"[{i+1}] {menupontok[i]}");
            }
            ConsoleKey valasz = Console.ReadKey().Key;
            switch (valasz)
            {
                case ConsoleKey.D1:
                    ujadat();
                    mentes();
                    break;
                case ConsoleKey.D2:
                    top10();
                    break;
                case ConsoleKey.D3:
                    System.Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }
        static void ujadat()
        {
          //10 új adat bekérése, és tárolása
            for (int i = 1; i <= 10; i++)
            {
                Console.Clear();
                Console.WriteLine($"Adja meg a(z) {i} zeneszám előadóját!");
                string eloado = Console.ReadLine();
                Console.WriteLine($"Adj meg a(z) {i} zeneszám címét!");
                string cim = Console.ReadLine();

                string eloadocim = $"{eloado} - {cim}";
                if (zenek.ContainsKey(eloadocim))
                {
                    zenek[eloadocim]++;
                }
                else
                {
                    zenek[eloadocim] = 1;
                }
            }
            Console.WriteLine("Nyomj meg egy gombot, hogy visszatérj a menübe!");
            Console.ReadKey();
        }
        static void top10()
        {
            //top 10 kiíratása
            Console.Clear();
            //a zenék rendezése
            var rendezett = zenek.OrderByDescending(zene => zene.Value);
            //megszámoljuk, hogy hanyadik zene következik a kiíratás közben
            var hanyadik = 0;
            foreach (var zene in rendezett)
            {
                Console.WriteLine($"{zene.Value} {zene.Key}.");
                hanyadik++;
                if (hanyadik == 10)
                {
                    break;
                }
            }
            Console.WriteLine("Nyomj meg egy gombot, hogy visszatérj a menübe!");
            Console.ReadKey();
        }
        static void mentes()
        {
           
            //ha fájlt választott a felhasználó
            if (hova == "file")
            {
                //az összes eddigi adatot kiíratjuk a fájlba
                StreamWriter ujFajl = new StreamWriter("slagerlista.txt");
                foreach (var zene in zenek)
                {
                    ujFajl.WriteLine($"{zene.Value}\t{zene.Key}");
                }
                ujFajl.Close();
            }
            //ha adatbázist választott a felhasználó
            if(hova == "database")
            {
                //adatbázis
            }
        }
    }
}
