using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ProjectFleetsOfDrones.Controllers;
using ProjectFleetsOfDrones.DAL;
using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;

namespace TestProjectDrone
{
    public class FlightsControllerIntegrationTests:

        //1. IClassFixture ci permette di passare un parametro
        //al costruttore della classe di test
        //2. WebApplicationFactory rappresenta la factory che si occuperà di emulare
        //la nostra web application, la stessa factory è in grado di creare un client Http
        //che "punta" alla stessa web application di cui sopra
        //3. All'interno della WebApplicationFactory posso andare a configurare i servizi
        // registrati per la dependency injection allo scopo di utilizzare dei servizi di test

        IClassFixture<WebApplicationFactory<FlightsController>>
    {
        private readonly HttpClient _sut; //System Under Test

        public FlightsControllerIntegrationTests(
            WebApplicationFactory<FlightsController> factory)
        {
            var setupFactory = factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(services =>
                {
                    var originalServiceDescriptor = services.Single(serviceDescriptor =>
                        serviceDescriptor.ImplementationType == typeof(FileDataAccessService));
                    services.Remove(originalServiceDescriptor);

                    services.AddScoped<IDataAccessService, TestDataAccessService>();
                }));
            _sut = setupFactory
                .CreateClient(); 
            //al create client la waf è pronta
            //all'ascolto e quindi a ricevere richieste
        }

        [Fact]
        public async void GetShouldReturnOKWithAListOfFlights()
        {
            //Act
            var flights = await _sut.GetFromJsonAsync<List<Flight>>("flights");

            //Assert
            Assert.NotNull(flights);
            Assert.True(flights.Any());
        }
        [Fact]
        public async void AddWithProperParameterShouldReturnCreateWithAFlight()
        {
            //Arrange
            var startDate = new DateTime(2022,10,27);
            var endDate = new DateTime(2022,10,28);
            var request = new PostFlightModel
            {
                StartDate = startDate,
                EndDate = endDate,
            };
            var expectedContent = new Flight
            {
                FlightId = 1,
                StartDate = startDate,
                EndDate = endDate,
            };

            //Act
            var response =
                await _sut.PostAsJsonAsync("flights", request);

            var actualContent = await response.Content.ReadFromJsonAsync<Flight>();

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created,response.StatusCode);
            Assert.Equal(expectedContent, actualContent,new FlightComparer());
        }
    }

    public class FlightComparer : IEqualityComparer<Flight?>
    {
        public bool Equals(Flight? x, Flight? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return 
                x.FlightId == y.FlightId 
                && x.StartDate.Equals(y.StartDate) 
                && x.EndDate.Equals(y.EndDate) 
                && x.DroneId == y.DroneId;
        }

        public int GetHashCode(Flight obj)
        {
            return HashCode.Combine(obj.FlightId, obj.StartDate, obj.EndDate, obj.DroneId);
        }
    }

    public class TestDataAccessService : IDataAccessService
    {
        //private readonly Flight _postExpectedModel;

        //public TestDataAccessService(Flight postExpectedModel)
        //{
        //    _postExpectedModel = postExpectedModel;
        //}
        public IEnumerable<Flight> ReadFlights() =>
            new List<Flight>
            {
            };

        public IEnumerable<Drone> ReadDrones()
        {
            throw new NotImplementedException();
        }

        public void WriteFlights(IEnumerable<Flight> flights)
        {
            throw new NotImplementedException();
        }

        public List<Flight> ToList() =>
            new List<Flight>
            {
                new Flight
                {
                }
            };


        public Flight Add(Flight flightToAdd)
            => flightToAdd;
    }
}