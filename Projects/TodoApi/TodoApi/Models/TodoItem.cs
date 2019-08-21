using System;
namespace TodoApi.Models
{
    public class Rider
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
    }
}
