using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class CreateInvitationResource
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid TransmitterId { get; set; }

    [Required]
    public Guid CompanyId { get; set; }
}