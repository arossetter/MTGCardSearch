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
        private const string colorSearchEndpoint = "&colors={0}";
        private const string cmcSearchEndpoint = "&cmc={0}";
        private const string typeSearchEndpoint = "&type={0}";

        public static async Task<CardList> GetCards(CardQuery query)
        {
            CardList cards = new CardList();

            string url = GenerateQueryString(query);

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cards = JsonConvert.DeserializeObject<CardList>(json);
            }

            return cards;
        }

        private static string GenerateQueryString(CardQuery query)
        {
            string queryText = baseURL + string.Format(nameSearchEndpoint, query.cardName);

            // Do we have any colors?
            string colorQuery = "";
            if (query.isWhite)
                colorQuery += "white,";
            if (query.isBlue)
                colorQuery += "blue,";
            if (query.isBlack)
                colorQuery += "black,";
            if (query.isRed)
                colorQuery += "red,";
            if (query.isGreen)
                colorQuery += "green,";

            if(!string.IsNullOrWhiteSpace(colorQuery))
            {
                colorQuery.TrimEnd(',');
                queryText += string.Format(colorSearchEndpoint, colorQuery);
            }

            if(!string.IsNullOrWhiteSpace(query.cmc))
            {
                queryText += string.Format(cmcSearchEndpoint, query.cmc);
            }

            if(!string.IsNullOrWhiteSpace(query.cardType))
            {
                queryText += string.Format(typeSearchEndpoint, query.cardType);
            }

            return queryText;
        }
    }
}
