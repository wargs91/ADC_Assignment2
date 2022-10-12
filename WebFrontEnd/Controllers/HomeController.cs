using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebFrontEnd.Models;


namespace WebFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/AdminCentreNames", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<AdminCentreName> centres = JsonConvert.DeserializeObject<List<AdminCentreName>>(restResponse.Content);
            return View(centres);
        }

        [HttpPost]
        public IActionResult insertCentre([FromBody] AdminCentreName centreName)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/AdminCentreNames", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centreName));
            RestResponse restResponse = restClient.Execute(restRequest);

           AdminCentreName returnName = JsonConvert.DeserializeObject<AdminCentreName>(restResponse.Content);
            if (returnName != null)
            {
                return Ok(returnName);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult createBooking([FromBody] CentreBooking newBooking)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/CentreBookings", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(newBooking));
            RestResponse restResponse = restClient.Execute(restRequest);

            AdminCentreName returnName = JsonConvert.DeserializeObject<AdminCentreName>(restResponse.Content);
            if (returnName != null)
            {
                return Ok(returnName);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult AdminSearch([FromBody] string search)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/Search", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(search));
            RestResponse restResponse = restClient.Execute(restRequest);

            List<CentreBooking> CentreList = JsonConvert.DeserializeObject<List<CentreBooking>>(restResponse.Content);
            
            if (CentreList != null)
            {
                return Ok(CentreList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UserSearch([FromBody] string search)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/UserSearch", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(search));
            RestResponse restResponse = restClient.Execute(restRequest);

            List<AdminCentreName> ReturnBookingList = JsonConvert.DeserializeObject<List<AdminCentreName>>(restResponse.Content);



            if (ReturnBookingList != null)
            {
                return Ok(ReturnBookingList);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost] //THis needs to link to the booking search controller, then only display the next available booking
        public IActionResult UserBookingSearch([FromBody] string search)
        {
            RestClient restClient = new RestClient("http://localhost:49990/");
            RestRequest restRequest = new RestRequest("api/Search", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(search));
            RestResponse restResponse = restClient.Execute(restRequest);

            List<CentreBooking> AdminBookingList = JsonConvert.DeserializeObject<List<CentreBooking>>(restResponse.Content);

            DateOnly nextAvailabe = AdminBookingList.Find(AdminBookingList.endDate > DateTime.Now) + 1; //Confirm if this is how this works

            
            
            if (nextAvailabe != null)
            {
                return Ok(nextAvailabe);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
