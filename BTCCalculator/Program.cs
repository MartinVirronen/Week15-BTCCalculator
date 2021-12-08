using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BTCCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.EUR.code} {currentBitcoin.bpi.EUR.rate_float}");

            Console.WriteLine("Palju Bitcoini teil on?");
            float userCoin = float.Parse(Console.ReadLine());
            Console.WriteLine("Vali sobiv valik: EUR, USD or GBP?");
            string userCurrency = Console.ReadLine();
            float currentRate = 0;

            if (userCurrency == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;
            }
            else if (userCurrency == "USD")
            {
                currentRate = currentBitcoin.bpi.USD.rate_float;
            }
            else if (userCurrency == "GBP")
            {
                currentRate = currentBitcoin.bpi.GBP.rate_float;
            }

            float result = currentRate * userCoin;
            Console.WriteLine($"Sinu Bitcoini väärtus on {userCurrency} {result}");


        }

        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoinData;
        }
    }
}