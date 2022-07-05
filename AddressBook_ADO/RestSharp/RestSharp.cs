using RestSharp;
using Newtonsoft.Json;
using System.Net;
using AddressBook_ADO;

namespace RestSharp
{
    [TestClass]
    public class Address
    {
        AddressBook_Model model = new AddressBook_Model();
        RestClient client;
        [TestMethod]
        public void OnGetMethod_Should_ReturnAddressBookDetails()
        {
            client = new RestClient("http://localhost:3000");  
            RestRequest request = new RestRequest("/friends", Method.Get);
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<AddressBook_Model> list = JsonConvert.DeserializeObject<List<AddressBook_Model>>(response.Content);
            Assert.AreEqual(2, list.Count);
            foreach (AddressBook_Model data in list)
            {
                Console.WriteLine("{0,-5}{1,-20}{2,-20}{3,-10}{4,-20}{5,-20}{6,-10}{7,-15}{8,-20}", data.id, data.firstName, data.lastName,
                    data.address, data.city, data.state, data.zipCode, data.phone, data.email);
            }
        }

        [TestMethod]
        public void OnPostingAddressDetails_SholudAddToJsonServer()
        {
            client = new RestClient("http://localhost:3000");
            List<AddressBook_Model> list = new List<AddressBook_Model>();
            list.Add(new AddressBook_Model
            {
                firstName = "Soniya",
                lastName = "Kamble",
                address = "13A",
                city = "Goa",
                state = "Maharashtra",
                zipCode = 654221,
                phone = 7890654321,
                email = "soni@gmail.com"
            });
            list.Add(new AddressBook_Model
            {
                firstName = "Anita",
                lastName = "Rangari",
                address = "15A",
                city = "New Mumbai",
                state = "Maharashtra",
                zipCode = 654321,
                phone = 6789045123,
                email = "ani@gmail.com"
            });
            list.ForEach(body =>
            {
                RestRequest request = new RestRequest("/friends", Method.Post);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                AddressBook_Model data = JsonConvert.DeserializeObject<AddressBook_Model>(response.Content);
                Assert.AreEqual(body.firstName, data.firstName);
                Console.WriteLine(response.Content);
            });
        }

        [TestMethod]
        public void OnUpdatingAddressBook_ShouldUpdateOnJsonServer()
        {
            client = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/friends/3", Method.Put);
            AddressBook_Model body = new AddressBook_Model
            {
                firstName = "Harshita",
                lastName = "Rana",
                address = "15A",
                city = "Jabalpur",
                state = "Madhya Pradesh",
                zipCode = 654321,
                phone = 9704102176,
                email = "har@gmail.com"
            };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            AddressBook_Model data = JsonConvert.DeserializeObject<AddressBook_Model>(response.Content);
            Assert.AreEqual("Harshini", data.firstName);
            Assert.AreEqual(9704102176, data.phone);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void OnDeleteCall_ShouldDeleteDataOnJsonServer()
        {
            client = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/friends/3", Method.Delete);
            RestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(response.Content);
        }

    }

}
