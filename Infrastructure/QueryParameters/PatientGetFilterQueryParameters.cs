using DataAccessLayer.Models;

namespace reception.QueryParameters;

public class PatientGetFilterQueryParameters
{
    public int Number { get; set; }
    public int Page { get; set; }
    public int? Id { get; set; }
    public string? Surname { get; set; }
    public string? Status { get; set; }
    public string? Priority{ get; set; }
}