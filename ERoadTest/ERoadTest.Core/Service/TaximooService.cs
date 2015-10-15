using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace TaximooTestApp
{
	public class Constant
	{
		public const string HostName = "http://www.taximoo.com/taximooAPI/";
		public const string RegisterUser = "users/addUser.php";
		public const string Login = "users/userLogin.php";
		public const string GetUserByID = "users/getUserById.php";
		public const string GetLoggedInUsers = "users/getLoggedInUsers.php";
	}

	public class TaximooService
	{
		#region Post get data

		/// <summary>
		/// PostGetData method use to handle all post & get action.
		/// </summary>
		/// <typeparam name="Tr">It is for Response come from service.</typeparam>
		/// <typeparam name="T">It is Request type model will send to the service.</typeparam>
		/// <param name="endpoint">It is for controller that will be hit.</param>
		/// <param name="method">Service method type.</param>
		/// <param name="content">Data will post to the service.</param>
		/// <returns></returns>
		public static async Task<Tr> PostGetData<Tr, T> (string endpoint, HttpMethod method, T content)
		{
			Tr returnResult = default(Tr);

			HttpClient client = null;
			try {
				client = new HttpClient ();
				client.BaseAddress = new Uri (Constant.HostName);
				client.DefaultRequestHeaders.Add ("Accept", "application/json");

				client.Timeout = new TimeSpan (0, 0, 15);

				HttpResponseMessage result = null;

				StringContent data = null;
				if (content != null)
					data = new StringContent (JsonConvert.SerializeObject (content), Encoding.UTF8, "application/json");

				var apiUri = new Uri (string.Format ("{0}{1}", Constant.HostName, endpoint));

				if (method == HttpMethod.Get)
					result = await client.GetAsync (apiUri);

				if (method == HttpMethod.Put)
					result = await client.PutAsync (apiUri, data);

				if (method == HttpMethod.Delete)
					result = await client.DeleteAsync (endpoint);

				if (method == HttpMethod.Post)
					result = await client.PostAsync (apiUri, data);

				if (result != null) {
					if (result.IsSuccessStatusCode
					    && result.StatusCode == System.Net.HttpStatusCode.OK) {
						var json = result.Content.ReadAsStringAsync ().Result;
						//returnResult = JsonConvert.DeserializeObject<Tr> (json);
					}
				}

			} catch (Exception ex) {
				Debug.WriteLine ("Error fetching data: " + ex.Message);
			} finally {
				if (client != null)
					client.Dispose ();
			}
			return returnResult;
		}

		#endregion
	}
}