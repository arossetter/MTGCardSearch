using MTGCardSearch.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MTGCardSearch.ViewModel.Helpers
{
    public class MagicAPIHelper
    {
        private const string baseURL = "https://api.magicthegathering.io/";
        private const string nameSearchEndpoint = "v1/cards?name={0}&orderBy=name";

        public static async Task<CardList> GetCards(string query)
        {
            CardList cards = new CardList();

            string url = baseURL + string.Format(nameSearchEndpoint, query);

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cards = JsonConvert.DeserializeObject<CardList>(json);
            }

            return cards;
        }
    }
}
