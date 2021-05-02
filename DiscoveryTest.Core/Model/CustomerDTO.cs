using System;
using Newtonsoft.Json;

namespace DiscoveryTest.Core.Model
{
    public class CustomerDTO : IEquatable<CustomerDTO>
    {
        /// <summary>
        /// The reservation identifier for this guest
        /// </summary>
        public string ReservationId { get; set; }

        /// <summary>
        /// The guest's full name
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// The guest's mobile number
        /// </summary>
        public string GuestMobile { get; set; }

        /// <summary>
        /// When the guest is arriving
        /// </summary>
        public string Arrived { get; set; }

        /// <summary>
        /// When the guest is expected to depart
        /// </summary>
        public string Depart { get; set; }

        /// <summary>
        /// The market segment category of this guest
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The number of nights they are staying for this reservation
        /// </summary>
        public string Nights { get; set; }

        /// <summary>
        /// The name of the area the guest is staying in (i.e. room name)
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// If there is one, the previous score the customer gave us
        /// </summary>
        public int? PreviousNPS { get; set; }

        /// <summary>
        /// If there is one, the previous comment the customer gave us
        /// </summary>
        public string PreviousNPSComment { get; set; }

        public bool Equals(CustomerDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ReservationId == other.ReservationId &&
                   GuestName == other.GuestName &&
                   GuestMobile == other.GuestMobile &&
                   Arrived == other.Arrived &&
                   Depart == other.Depart &&
                   Category == other.Category &&
                   Nights == other.Nights &&
                   AreaName == other.AreaName &&
                   PreviousNPS == other.PreviousNPS &&
                   PreviousNPSComment == other.PreviousNPSComment;
        }

        [JsonIgnore]
        public string Title => GuestName;
        
        [JsonIgnore]
        public string Detail => $"Arrival: {Arrived}, Departure: {Depart}";
    }
}
