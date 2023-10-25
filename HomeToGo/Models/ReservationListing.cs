using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace HomeToGo.Models;

public class ReservationListing
{
    public int ReservationListingId { get; set; }
    public int ReservationId { get; set; } 
    public virtual Reservation Reservation { get; set; }
}