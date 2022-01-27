using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Home
{
    public class Zipcode
    {
        private static void showPostOffice(JObject response)
        {
            JArray? postOffices = (JArray?)response["PostOffice"];
            Console.WriteLine(
                $"Number of postoffice(s) found in this pincode: {postOffices!.Count}"
            );
            foreach (var postOffice in postOffices!)
            {
                string address =
                    $"{postOffice["Name"]}, {postOffice["District"]}, {postOffice["State"]}.";
                Console.WriteLine(address);
            }
        }

        private static async Task findPlace(int code)
        {
            HttpClient httpClient = new HttpClient();
            var responseTask = await httpClient.GetAsync(
                $"https://api.postalpincode.in/pincode/{code}"
            );
            var response = await responseTask.Content.ReadAsStringAsync();
            JArray jsonArray = JArray.Parse(response);
            // JObject jsonResponse = JObject.Parse(result);
            showPostOffice((JObject)jsonArray[0]);
        }

        public static async Task Main(string[] args)
        {
            Console.Write("Enter the pincode:");
            int code = Convert.ToInt32(Console.ReadLine());
            await findPlace(code);
        }
    }
}
