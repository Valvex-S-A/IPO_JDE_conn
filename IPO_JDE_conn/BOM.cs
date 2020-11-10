using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
 

namespace IPO_JDE_conn
{
    public class BOM
    {

        public static List<ListaBOM> GetBom(int ilosc, string LITM)
        {
            DB2008DataContext db = new DB2008DataContext();
            var _itm = from c in db.SLOWNIK_1s
                       where c.IMLITM == LITM
                       select c;

            List<ListaBOM> lista = new List<ListaBOM>();
            if (_itm.Count() == 1)
            {
                var itm = _itm.First();
                lista = BOM.Get_BOM((int)itm.IMITM, ilosc);
                BOM.Sort_BOM(ref lista);
            }
            return lista;
        }


        public static void Sort_BOM(ref List<ListaBOM> boms)
        {
            int lnk = 1;
            while (CheckLNK(ref boms))
            {

                int ID_to_update;
                //weź pierwszy nieopisany węzeł.
                ID_to_update = (from c in boms
                                orderby c.level descending
                                where c.lnk_grp == 0
                                select c.ID).First();


                while (ParentHasLNKEmpty(ref ID_to_update, ref boms, ref lnk) != -1)
                {


                }

                lnk++;

            }





        }

        private static int ParentHasLNKEmpty(ref int ID_to_check, ref List<ListaBOM> boms, ref int lnk)
        {

            int id_tmp = ID_to_check;

            var check = (from c in boms
                         where c.ID == id_tmp
                         select c).First();
            check.lnk_grp = lnk;

            var parent = (from g in boms
                          where g.main_grp == check.main_grp && check.wyrob_s == g.skl_s
                          select g).Take(1);

            if (parent.Count() == 0) return -1;
            else
            {
                ID_to_check = parent.First().ID;
                return 0;

            }
        }

        /// <summary>
        /// sprawdza czy są jeszcze nie opisane wezły na drzewie.
        /// </summary>
        /// <param name="boms">referencja do listy z obiektami </param>
        /// <returns></returns>
        private static bool CheckLNK(ref List<ListaBOM> boms)
        {
            int _to_update_lnk = (from c in boms
                                  orderby c.level descending
                                  where c.lnk_grp == 0
                                  select c).Count();

            if (_to_update_lnk != 0) return true;
            else return false;



        }






        public static List<ListaBOM> Get_BOM(int wyrob_s, double qty)
        {
            List<ListaBOM> all_boms = new List<ListaBOM>();
            Stack<skladnik> do_rozbicia = new Stack<skladnik>();

            skladnik s0 = new skladnik();
            s0.wyrob_s = wyrob_s;
            s0.qty = qty;
            s0.level = 0;
            s0.main_grp = 0;
            int ID = 0;

            do_rozbicia.Push(s0);

            while (do_rozbicia.Count != 0)
            {
                skladnik s1 = do_rozbicia.Pop();


                List<ListaBOM> t_bom = Get_Bom_1Level(s1.wyrob_s, s1.qty, s1.level, s1.main_grp);
                foreach (var bom in t_bom)
                {

                    bom.ID = ID++;
                    all_boms.Add(bom);
                    if (bom.has_child)
                    {

                        skladnik s2 = new skladnik() { wyrob_s = bom.skl_s, qty = bom.ilosc, level = s1.level + 1, main_grp = bom.main_grp };
                        do_rozbicia.Push(s2);
                    }
                }

            }
            return all_boms;
        }




        public static List<ListaBOM> Get_Bom_1Level(int wyrob_s, double qty, int level, int main_grp)
        {

            List<ListaBOM> boms = new List<ListaBOM>();

           
            
            using (DB2008DataContext db = new DB2008DataContext() )
            {
                var bom = from c in db.IPO_Rozpis_mats
                          where c.wyrob_s == wyrob_s
                          select c;
                int _temp_m_grp = 0;
                foreach (var bt in bom)
                {


                    ListaBOM b = new ListaBOM();
                    b.level = level;
                    b.ilosc = (double)bt.ilosc * qty;
                    b.ilosc_norma = (double)bt.ilosc;
                    b.nazwa_s = bt.Nazwa_s;
                    b.nazwa_w = bt.Nazwa_w;
                    b.skl_l = bt.skladnik_l;
                    b.skl_s = (int)bt.skladnik_s;
                    b.wyrob_l = bt.wyrob_l;
                    b.wyrob_s = (int)bt.wyrob_s;
                    b.main_grp = main_grp;

                    if (level == 0) b.main_grp = ++_temp_m_grp;




                    var child = from c in db.IPO_Rozpis_mats
                                where c.wyrob_s == bt.skladnik_s
                                select c;

                    if (child.Count() > 0) { b.has_child = true; }
                    else { b.has_child = false; }

                    boms.Add(b);


                }

            }
            return boms;
        }

        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }


    }



    public class skladnik
    {
        public int wyrob_s { get; set; }
        public double qty { get; set; }
        public int level { get; set; }
        public int main_grp { get; set; }
    }

    public class ListaBOM
    {
        public int ID { get; set; }
        public int level { get; set; }
        public int wyrob_s { get; set; }
        public string wyrob_l { get; set; }
        public int skl_s { get; set; }
        public string skl_l { get; set; }
        public double ilosc { get; set; }
        public double ilosc_norma { get; set; }
        public string nazwa_w { get; set; }
        public string nazwa_s { get; set; }
        public bool has_child { get; set; }
        public int main_grp { get; set; }
        public int lnk_grp { get; set; }

    }


}
