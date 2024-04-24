using JofferWebAPI.Models;

namespace JofferWebAPI.Dtos;

public class TalentWithJobOfferId
{
    public Talent Talent { get; set; }
    public int JobOfferId { get; set; }
    public string Auth0Id { get; set; }
}