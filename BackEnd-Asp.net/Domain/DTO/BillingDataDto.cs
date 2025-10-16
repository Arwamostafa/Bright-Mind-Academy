using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class BillingDataDto
    {
        [JsonPropertyName("apartment")]
        public string Apartment { get; set; } = "NA";
        [JsonPropertyName("email")]
        public string Email { get; set; } = "test@example.com";
        [JsonPropertyName("floor")]
        public string Floor { get; set; } = "NA";
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = "NA";
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = "NA";
        [JsonPropertyName("street")]
        public string Street { get; set; } = "NA";
        [JsonPropertyName("building")]
        public string Building { get; set; } = "NA";
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = "01000000000";
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = "12345";
        [JsonPropertyName("city")]
        public string City { get; set; } = "Cairo";
        [JsonPropertyName("country")]
        public string Country { get; set; } = "EG";
        [JsonPropertyName("state")]
        public string State { get; set; } = "NA";
    }
}
