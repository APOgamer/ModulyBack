namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class InvitationResource
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TransmitterId { get; set; }
    public Guid CompanyId { get; set; }
    public string Status { get; set; }
}