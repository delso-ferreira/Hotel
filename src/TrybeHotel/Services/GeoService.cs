using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;
using System.Text.Json;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;        
        public GeoService(HttpClient client)
        {
            _client = client;            
        }

        
        public async Task<object> GetGeoStatus()
        {         

           var client = new HttpRequestMessage(HttpMethod.Get, "https://nominatim.openstreetmap.org/status.php?format=json");
           client.Headers.Add("Accept", "application/json");
           client.Headers.Add("User-Agent", "aspnet-user-agent");
           var response = await _client.SendAsync(client);
           
           if(response.IsSuccessStatusCode)
           {
               var result = await response.Content.ReadAsStringAsync();
               return result;
           }
           else {
            return default(Object);
           };        
        }
        
        
        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var client = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1");
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("User-Agent", "aspnet-user-agent");

            var response = await _client.SendAsync(client);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeoDtoResponse[]>();
                /* var data = JsonConvert.DeserializeObject<List<GeoDtoResponse>>(result);                                             */
                
                return result[0];
            }
            else {
                return default(GeoDtoResponse);
            }
        }


        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {          
            var geo = await GetGeoLocation(geoDto);
            var hotels = repository.GetHotels();

            var hotelResults = new List<GeoDtoHotelResponse>();                     
                        
            foreach (var hotel in hotels)
            {
                var hotelGeo = new GeoDto
                {
                    Address = hotel.Address,
                    City = hotel.CityName,
                    State = hotel.State
                };

                var getHotelGeoLocaltion = await GetGeoLocation(hotelGeo);
                var distance = CalculateDistance(geo.lat, geo.lon, getHotelGeoLocaltion.lat, getHotelGeoLocaltion.lon);
                
                var newHotels = new GeoDtoHotelResponse
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityName = hotel.CityName,
                    State = hotel.State,
                    Distance = distance
                };

                hotelResults.Add(newHotels);                
            };     

            var orderedHotels = hotelResults.OrderBy(h => h.Distance).ToList();

            return orderedHotels;   
                        
        }

       

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}