
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace IPO_JDE_conn
{
    class pdf_przewodnik
    {
        

        public static void Gen_przew(List<int> tab_nr_zlec, string user_name, bool KKC)
        {
            db_raportyDataContext db = new db_raportyDataContext();


            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");

            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //Create a specific font object
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fb = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fn = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            WebReference.Service1 client = new WebReference.Service1();
            DB2008DataContext dbx = new DB2008DataContext();

            string file_path = System.IO.Path.GetTempPath() + user_name + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + ".pdf";
            FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Document document = new Document(PageSize.A4,10f,10f,10f,20f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            foreach (int nr_zlec in tab_nr_zlec)
            {
                document.NewPage();
                // PdfContentByte cb = writer.DirectContent;
                // cb.Rectangle(20f, 20f, 100f, 200f);

                // cb.Stroke();
               // var zl1 = (from c in db.IPO_ZLECENIAs
               //           where c.ipo_nr_zlec == nr_zlec
                //          select c).Single();
               
               

                WebReference.IPO_Order ord = client.IPO_GET_ORDER(nr_zlec);
                WebReference.Item itm = client.IPO_GET_ITEM_BY_ITM(int.Parse(ord.item_id));


                Paragraph p = new Paragraph("PRZEWODNIK DO ZLECENIA IPO NR: " + nr_zlec.ToString(), fb);
                p.Alignment = 1;
                document.Add(p);
                document.Add(new Paragraph(" ", f));

                p = new Paragraph(itm.index + " " + itm.name, fb);


                p.Alignment = 1;
                document.Add(p);


                document.Add(new Paragraph(" ", f));
                PdfPTable table = new PdfPTable(6);
                float[] widths = new float[] { 15f, 50f, 15f, 25f, 25f, 25f };
                table.SetWidths(widths);


                PdfPCell cell = new PdfPCell(new Phrase("Indeks", f));

                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Nazwa", f)); table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Seria", f)); table.AddCell(cell);
                cell = new PdfPCell(new Phrase("PLAN IPO", f)); table.AddCell(cell);
                cell = new PdfPCell(new Phrase("KKC", f)); table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Wystawił:", f)); table.AddCell(cell);




                //table.AddCell();
                cell = new PdfPCell(new Phrase(itm.index, f)); table.AddCell(cell);

                //table.AddCell(itm.name);
                cell = new PdfPCell(new Phrase(itm.name, f)); table.AddCell(cell);

                
                //table.AddCell(zl.ilosc_zam.ToString());
                cell = new PdfPCell(new Phrase(ord.quantity.ToString(), f)); table.AddCell(cell);
                //table.AddCell(zl.stop_zam.Value.ToShortDateString());
                cell = new PdfPCell(new Phrase(ord.order_no_cust, f)); table.AddCell(cell);
                //table.AddCell("???");
                cell = new PdfPCell(new Phrase(" ", f)); table.AddCell(cell);
                //table.AddCell(user_name);
                cell = new PdfPCell(new Phrase(user_name + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), f)); table.AddCell(cell);


                document.Add(table);

                document.Add(new Paragraph(" ", f));
                document.Add(new Paragraph("LISTA MATERIAŁÓW DO POBRANIA: ", fn));
                document.Add(new Paragraph(" ", f));
                List<WebReference.IPO_BOM_Line> bom = client.IPO_BOM_ITEM(itm.index).ToList<WebReference.IPO_BOM_Line>();


                PdfPTable tbom = new PdfPTable(14);

                widths = new float[] { 15f, 50f, 15f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                tbom.SetWidths(widths);
                cell = new PdfPCell(new Phrase("Indeks", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("Nazwa", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("Do pobrania", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("Pobrano na RW", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("Wykorzystano", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-1", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-2", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-3", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-4", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-5", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-6", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-7", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("BRAK-8", f)); tbom.AddCell(cell);
                cell = new PdfPCell(new Phrase("JW", f)); tbom.AddCell(cell);

                foreach (WebReference.IPO_BOM_Line b in bom)
                {
                    WebReference.Item  bitm = client.IPO_GET_ITEM_BY_ITM(b.item_id);

                    cell = new PdfPCell(new Phrase(bitm.index, f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(bitm.name, f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase((Convert.ToDouble(b.quantity) * (double)ord.quantity).ToString("###.00"), f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(".        .         .         . ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" ", f)); tbom.AddCell(cell);



                }

                document.Add(tbom);

                document.Add(new Paragraph(" ", f));
                document.Add(new Paragraph("Informacja o marszrucie: ", fn));
                document.Add(new Paragraph(" ", f));

                PdfPTable mtab = new PdfPTable(5);
                widths = new float[] { 10f, 15f, 50f, 35f, 15f };
                mtab.SetWidths(widths);

                cell = new PdfPCell(new Phrase("Nr operacji", f)); mtab.AddCell(cell);
                cell = new PdfPCell(new Phrase("Grupa operacji", f)); mtab.AddCell(cell);
                cell = new PdfPCell(new Phrase("Opis", f)); mtab.AddCell(cell);
                cell = new PdfPCell(new Phrase("Operacja", f)); mtab.AddCell(cell);
                cell = new PdfPCell(new Phrase("Nr_karty", f)); mtab.AddCell(cell);

                
                var marsz = from c in dbx.IPO_Marszruties
                            orderby c.Typ, c.Nr_oper
                            where c.Indeks == itm.index && c.Typ == "M"
                            select c;
                foreach (var l in marsz)
                {
                    cell = new PdfPCell(new Phrase(l.Nr_oper.ToString(), f)); mtab.AddCell(cell);
                    cell = new PdfPCell(new Phrase(l.Grupa_oper, f)); mtab.AddCell(cell);
                    cell = new PdfPCell(new Phrase(l.Opis, f)); mtab.AddCell(cell);
                    cell = new PdfPCell(new Phrase(l.Operacja, f)); mtab.AddCell(cell);
                    cell = new PdfPCell(new Phrase(l.Nr_Karty, f)); mtab.AddCell(cell);


                }

                document.Add(mtab);


                var kart = (from c in marsz.Where(x => x.Nr_Karty != "") select new { KARTA = c.Nr_Karty }).Distinct();


                if (kart.Count() > 0)
                {
                    var cykl = from g in kart
                               join k in dbx.F00192s.Where(x => x.CFSY == "48" && x.CFRT == "SN") on g.KARTA equals k.CFKY
                               select new { g.KARTA, NR = k.CFLINS / 100, k.CFDS80 };

                    PdfPTable kkc = new PdfPTable(23);
                    widths = new float[] { 10f, 25f, 45f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f };
                    kkc.SetWidths(widths);


                    cell = new PdfPCell(new Phrase("Nr", f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Nr_karty", f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Wymiar kontr.", f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Pierw, szt 1-...", f)); kkc.AddCell(cell);
                    for (int t = 1; t < 20; t++)
                    {
                        cell = new PdfPCell(new Phrase("Spr" + t.ToString(), f)); kkc.AddCell(cell);
                    }

                    foreach (var c in cykl)
                    {
                        cell = new PdfPCell(new Phrase(c.NR.ToString(), f)); kkc.AddCell(cell);
                        cell = new PdfPCell(new Phrase(c.KARTA, f)); kkc.AddCell(cell);
                        cell = new PdfPCell(new Phrase(c.CFDS80, f)); kkc.AddCell(cell);
                        cell = new PdfPCell(new Phrase(".        .         .        ", f)); kkc.AddCell(cell);
                        for (int t = 1; t < 20; t++)
                        {
                            cell = new PdfPCell(new Phrase(" ", f)); kkc.AddCell(cell);
                        }

                    }

                    if (KKC)
                    {
                        if (cykl.Count() > 0)
                        {
                            document.NewPage();
                            document.Add(new Paragraph("KKC - KARTA KONTROLI CYKLU DO ZLECENIA IPO NR: " + nr_zlec.ToString(), fb));

                            document.Add(new Paragraph(" ", f));

                            document.Add(table);
                            document.Add(new Paragraph(" ", f));

                            document.Add(new Paragraph("Informacja  o marszrucie: ", fn));
                            document.Add(new Paragraph(" ", f));

                            document.Add(mtab);

                            string co_ile = "";

                            if (ord.quantity < 10) co_ile = "Kontrolować wszystkie szt.";
                            if (ord.quantity >= 10) co_ile = "Kontrolować co 10 szt.";
                            if (ord.quantity >= 50) co_ile = "Kontrolować co 20 szt.";
                            if (ord.quantity >= 101) co_ile = "Kontrolować co 50 szt.";
                            if (ord.quantity >= 1001) co_ile = "Kontrolować co 150 szt.";
                            if (ord.quantity >= 3001) co_ile = "Kontrolować co 300 szt.";
                            document.Add(new Paragraph(" ", f));
                            document.Add(new Paragraph("Wymiary kontrolne. " + co_ile, fb));
                            document.Add(new Paragraph(" ", f));

                            document.Add(kkc);
                        } 
                    }
                }


            }


            document.Close();

            writer.Close();

            Process.Start(file_path);


        }

        public static void GenKKC(int nr_zlec, string id_prac, string oper, string id_masz)
        {
            db_raportyDataContext db = new db_raportyDataContext();


            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
            
            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //Create a specific font object
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fb = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fn = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fvb = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLDITALIC);
            WebReference.Service1 client = new WebReference.Service1();
            DB2008DataContext dbx = new DB2008DataContext();

            string file_path = System.IO.Path.GetTempPath() +  "_" + nr_zlec + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + ".pdf";
            FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 20f);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            WebReference.IPO_Order ord = client.IPO_GET_ORDER(nr_zlec);
            WebReference.Item itm = client.IPO_GET_ITEM_BY_ITM(int.Parse(ord.item_id));

            document.Open();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.NewPage();

            Paragraph nri = new Paragraph("39WF01 rev. 27.10.2016", f);
            nri.Alignment = 2;
            document.Add(nri);
            nri = new Paragraph("KKC - KARTA KONTROLI CYKLU DO ZLECENIA IPO NR: " + nr_zlec.ToString(), fb);
            nri.Alignment = 1;

            document.Add(nri);
            document.Add(new Paragraph("NAZWA: " + itm.name, fb));
            document.Add(new Paragraph("INDEKS: "+ itm.index, fb));
            document.Add(new Paragraph("OPERACJA:         " + oper, fvb));
            document.Add(new Paragraph("NR MASZYNY:       " + id_masz, fvb));
            var marsz = from c in dbx.IPO_Marszruties
                        orderby c.Typ, c.Nr_oper
                        where c.Indeks == itm.index && c.Typ == "M"
                        select c;


            string co_ile = "";

             co_ile = "Kontrolować zgodnie z instrukcją 39W.";
             
            document.Add(new Paragraph(" ", f));
            document.Add(new Paragraph("Wymiary kontrolne. " + co_ile, fb));
            document.Add(new Paragraph(" ", f));

            var kart = (from c in marsz.Where(x => x.Nr_Karty != "") select new { KARTA = c.Nr_Karty }).Distinct();

            PdfPTable kkc = new PdfPTable(23);
            float[] widths = new float[] { 10f, 25f, 45f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f, 14f };
            kkc.SetWidths(widths);

            PdfPCell cell;
            cell = new PdfPCell(new Phrase("Nr", f)); kkc.AddCell(cell);
            cell = new PdfPCell(new Phrase("Nr_karty", f)); kkc.AddCell(cell);
            cell = new PdfPCell(new Phrase("Wymiar kontr.", f)); kkc.AddCell(cell);
            cell = new PdfPCell(new Phrase("Pierw, szt 1-...", f)); kkc.AddCell(cell);
            for (int t = 1; t < 20; t++)
            {
                cell = new PdfPCell(new Phrase("Spr" + t.ToString(), f)); kkc.AddCell(cell);
            }


            if (kart.Count() > 0)
            {
                var cykl = from g in kart
                           join k in dbx.F00192s.Where(x => x.CFSY == "48" && x.CFRT == "SN") on g.KARTA equals k.CFKY
                           select new { g.KARTA, NR = k.CFLINS / 100, k.CFDS80 };

               

                foreach (var c in cykl)
                {
                    cell = new PdfPCell(new Phrase(c.NR.ToString(), f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase(c.KARTA, f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase(c.CFDS80, f)); kkc.AddCell(cell);
                    cell = new PdfPCell(new Phrase(".        .         .        ", f)); kkc.AddCell(cell);
                    for (int t = 1; t < 20; t++)
                    {
                        cell = new PdfPCell(new Phrase(" ", f)); kkc.AddCell(cell);
                    }

                }
                
                document.Add(kkc);
                document.Add(new Paragraph(" ", f));
                document.Add(new Paragraph("Utworzono: " + DateTime.Now.ToString(), f));
                document.Add(new Paragraph("ZALECENIA I UWAGI USTAWIACZA:  ", f));

                document.Close();

                writer.Close();
                SendToPrinter(file_path);
                
                //Process.Start(file_path);

            }


                



        }
        private static void SendToPrinter(string FileName)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = FileName;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }




        public static void GenPDFFileRaport(DataTable dt, string logged)
        {
 

                FileStream fs = new FileStream(System.IO.Path.GetTempPath() + logged + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmssfff") + ".pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);



                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                document.Add(new Paragraph("Hello World"));

                document.Close();

                writer.Close();

 



           

        }



    }
}
