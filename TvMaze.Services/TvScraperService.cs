using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Model;
using TvMaze.Services.Interfaces;

namespace TvMaze.Services
{
    public class TvScraperService : ITvScraperService
    {
        public List<Show> Shows { get; set; }
        public int pageSize = 10;
        public string serverUri = "http://api.tvmaze.com/";
        public string showsApiQuery = "shows";
        public string CastApiQuery = "shows/{0}/cast";
        public async Task<List<Person>> GetShowCast(int showId)
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(serverUri);
                client.DefaultRequestHeaders.Accept.Clear();

                //send request
                HttpResponseMessage responseMessage =
                    await client.GetAsync(serverUri + string.Format(CastApiQuery, showId));
                var response = await responseMessage.Content.ReadAsStringAsync();

                try
                {
                    return JsonConvert.DeserializeObject<List<CastJson>>(response)
                            .Select(p => p.Person)
                            .ToList();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("429"))
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                    return await GetShowCast(showId);
                }

            }
        }

        public async Task<List<Show>> GetShows()
        {
            string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            string fileName = Path.Combine(path, "Shows.Json");
            if (File.Exists(fileName))
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json = r.ReadToEnd();
                    return Shows = JsonConvert.DeserializeObject<List<Show>>(json);
                }

            }
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(serverUri);
                client.DefaultRequestHeaders.Accept.Clear();

                //send request
                HttpResponseMessage responseMessage =
                    await client.GetAsync(serverUri + showsApiQuery);
                var response = await responseMessage.Content.ReadAsStringAsync();
                var showList = JsonConvert.DeserializeObject<List<Show>>(response);
                foreach (var show in showList)
                {
                    show.Cast = GetShowCast(show.Id)
                        .Result
                        .OrderByDescending(c =>
                        {
                            DateTime dt;
                            DateTime.TryParse(c.Birthday, out dt);
                            return dt;
                        })
                        .ToList();
                }
                using (FileStream fs = File.Create(fileName))
                {
                    var data = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(showList));
                    fs.Write(data, 0, data.Length);
                }
                return showList;
            }
        }

        public async Task<List<Show>> GetShowsByPage(int page)
        {
            if (Shows == null)
            {
                Shows = await GetShows();
            }
            List<Show> pageShows = Shows
               .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();
            return pageShows;
        }
    }
}
