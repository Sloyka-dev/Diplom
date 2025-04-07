using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DataLib.Models;
using Diplom.VoiceEngine;
using Newtonsoft.Json;

namespace Diplom.Utility;

internal class ApiHandler
{

    private const string url = "http://127.0.0.1:5000/api/Tour?search={0}";

    public static async Task GetToursAsync(string text)
    {

        try
        {

            using (var hc = new HttpClient())
            {

                using var response = hc.GetAsync(string.Format(url, text)).Result;

                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var resData = JsonConvert.DeserializeObject<List<Tour>>(jsonResponse);
                if (resData == null) VoiceController.onGetTourError();
                
                MainWindow.Singleton.onTourApiResult(resData);
                VoiceController.onGetTourResult(resData);

            }
        }catch(Exception e)
        {

            MessageBox.Show(e.Message);
            VoiceController.onGetTourError();

        }
    }

}

