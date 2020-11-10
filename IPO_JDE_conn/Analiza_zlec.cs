using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPO_JDE_conn
{
    public static class Analiza_zlec
    {
        public static List<Item> Lista_pozycji(int nr_zlec)
        {
            var srv = new WebReference.Service1();
            List<Item> lst = new List<Item>();
            var db = new db_raportyDataContext();
            var db2008 = new DB2008DataContext();

            //skasuj stare rozpisy
            var do_skas = from c in db.IPO_ZDAWKA_PW_NORMAs where c.Nr_zlecenia_IPO == nr_zlec select c;
            db.IPO_ZDAWKA_PW_NORMAs.DeleteAllOnSubmit(do_skas);
            db.SubmitChanges();

            var linie = from c in db.IPO_ZDAWKA_PWs
                        where c.Nr_zlecenia_IPO == nr_zlec
                        select c;

            double ilosc_zlec = (double)(from c in linie where c.RW_PW == "PW" select c.Ilosc).Sum();
            srv.Dodaj_rozpis_do_Tabeli(nr_zlec, 0, ilosc_zlec);



            var linie_norma = from c in db.IPO_ZDAWKA_PW_NORMAs
                              where c.Nr_zlecenia_IPO == nr_zlec && c.RW_PW == "RW"
                              select c;
            //wycen normę
            foreach (var l in linie_norma)
            {
                var tkw = (from c in db.słownik_TKWs where c.Indeks.Trim() == l.Nr_indeksu.Trim() select c.Koszt).FirstOrDefault();

                if (l.RW_PW == "PW") l.Koszt_IPO = Math.Round((double)tkw * (double)l.Ilosc, 3);
                if (l.RW_PW == "RW") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);
                if (l.RW_PW == "PU") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);

                db.SubmitChanges();

            }


            //wyceń materiały - nie przejmujemy się jednostkami.
            foreach (var l in linie)
            {
                var tkw = (from c in db.słownik_TKWs where c.Indeks.Trim() == l.Nr_indeksu.Trim() select c.Koszt).FirstOrDefault();

                if (l.RW_PW == "PW") l.Koszt_IPO = Math.Round((double)tkw * (double)l.Ilosc, 3);
                if (l.RW_PW == "RW") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);
                if (l.RW_PW == "PU") l.Koszt_IPO = Math.Round((double)tkw * (double)-l.Ilosc, 3);

                db.SubmitChanges();

            }

            
            var total = (from c in linie where c.RW_PW == "RW" select new { TYP = "JDE", c.Nr_zlecenia_IPO, Indeks = c.Nr_indeksu.Trim(), c.Nazwa_pozycji, c.Ilosc, c.Koszt_IPO })
           .Concat(from g in linie_norma select new { TYP = "NORMA", g.Nr_zlecenia_IPO, Indeks = g.Nr_indeksu.Trim(), g.Nazwa_pozycji, g.Ilosc, g.Koszt_IPO }).ToArray();

            // zgrupuj
            
            //utwórz listę indeksów
            var lista_indeksow = (from c in total select c.Indeks).Distinct();

            foreach (var i in lista_indeksow)
            {
                Item itm = new Item();

                var tmp = from c in total where c.Indeks == i select c;
                itm.Nr_zlecenia = nr_zlec;
                itm.Indeks_wyrobu_zl = linie.Where(c => c.RW_PW == "PW").First().Nazwa_pozycji;
                itm.Nr_indeksu = i;
                itm.Nazwa = tmp.First().Nazwa_pozycji;
                itm.Ilość_JDE = (double)(from c in tmp where c.TYP == "JDE" select c.Ilosc).Sum();
                itm.Wartość_JDE = (double)(from c in tmp where c.TYP == "JDE" select c.Koszt_IPO).Sum();
                itm.Ilość_NORMA = (double)(from c in tmp where c.TYP == "NORMA" select c.Ilosc).Sum();
                itm.Wartość_NORMA = (double)(from c in tmp where c.TYP == "NORMA" select c.Koszt_IPO).Sum();
                itm.Różnica_ilość = Math.Round(itm.Ilość_JDE + itm.Ilość_NORMA);
                itm.Różnica_wartość = Math.Round(itm.Wartość_JDE + itm.Wartość_NORMA, 3);

                lst.Add(itm);
            }

            return lst;
        }

       
    }



    public class Item
    {
        public int Nr_zlecenia { get; set; }
        public string Indeks_wyrobu_zl { get; set; }

        public string Nr_indeksu { get; set; }
        public string Nazwa { get; set; }
        public double Ilość_JDE { get; set; }
        public double Ilość_NORMA { get; set; }
        public double Wartość_JDE { get; set; }
        public double Wartość_NORMA { get; set; }
        public double Różnica_ilość { get; set; }
        public double Różnica_wartość { get; set; }
    }


}
