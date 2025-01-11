namespace reception.OutputModels;

public class OutputGetAll
{
    public List<OutputPatient> Content { get; set; }
    public int TotalElements { get; set; }
    public int TotalPages{ get; set; }
    public int Number{ get; set; }
    public int Size{ get; set; }
}