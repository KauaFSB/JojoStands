namespace JOJOSTANDS.Models;

public class DetailsVM
{
    public Stand Anterior {get; set; }

    public Stand  Atual { get; set; }

    public Stand Proximo { get; set;}

    public List<Tipo> Tipos { get; set; }
}