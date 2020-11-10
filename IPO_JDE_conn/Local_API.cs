using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPO_JDE_conn
{
   








  
    class Local_API
    {
        const string _api_version = "1.0";
        const string _token = "ahp9zee4gi5Oi9Ae";
        const string _URL = @"http://ipo-serwer:13002/";
        const string _ContentType = "application/json";


        public string StartPracy(int id_pracy, string id_pracownika, string id_maszyny)
        {



            _Employee_start str = new _Employee_start();
            str.device_id = id_maszyny;
            str.employee_id = id_pracownika;

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            string tst_rps = JsonConvert.SerializeObject(str, microsoftDateFormatSettings);

            


            var client = new System.Net.WebClient();

            client.Headers.Add("Authorization", _token);
            client.Headers.Add("API-Version", _api_version);
            client.Headers.Add("Content-Type", _ContentType);
            client.Headers.Add("Access-Control-Request-Method", "PUT");
            string addr = $@"http://192.168.1.129:13002/task/" + id_pracy + "/";

            try
            {
                return client.UploadString(addr, "PUT", tst_rps);
            }
            catch (Exception e) {
                return e.Message;
            }

        }

        public string RaportPracy(int id_pracy,int typ_raportu, string empl_id, int qty, string storage_place)
        {
            _FullQtyReport fqr = new _FullQtyReport();
            fqr.device_id = null;
            fqr.employee_id = empl_id;
            fqr.quantity = qty;
            fqr.report_type = typ_raportu;
            fqr.storage_place = storage_place;





            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            string tst_fqr = JsonConvert.SerializeObject(fqr, microsoftDateFormatSettings);
            var client = new System.Net.WebClient();

            client.Headers.Add("Authorization", _token);
            client.Headers.Add("API-Version", _api_version);
            client.Headers.Add("Content-Type", _ContentType);
            client.Headers.Add("Access-Control-Request-Method", "PUT");
            string addr = $@"http://192.168.1.129:13002/task/" + id_pracy + "/";

            try
            {
                return client.UploadString(addr, "PATCH", tst_fqr);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }



    }


    public class _Employee_start
    {

        public string employee_id { get; set; }
        public string device_id { get; set; }


    }

    public class _Partial_report
    {
        /// <summary>
        /// Nr ewidencyjny pracownika składającego raport lub jego RFID poprzedzony znakiem +
        /// </summary>
        public string employee_id { get; set; }
        public string device_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime stop_time { get; set; }
    }

    public class _FullQtyReport
    {
        /// <summary>
        /// Typ raportu (0 – raport ilości; 1 – brak wewnętrzny; 2 – brak zewnętrzny; 8 – zmiana pola odkładczego)
        /// </summary>
        public int report_type { get; set; }

        /// <summary>
        /// Liczba raportowanych elementów (wymagany, gdy report_type ≠ 8; ignorowany, gdy report_type = 8)
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// Nr ewidencyjny pracownika składającego raport (gdy pominięto, wstawiany jest „IPOsystem™”)
        /// </summary>
        public string employee_id { get; set; }
        public string device_id { get; set; }
        /// <summary>
        /// Kod pola odkładczego (wymagany, gdy report_type = 8; w pozostałych przypadkach ignorowany)
        /// </summary>
        public string storage_place { get; set; }


    }




}
