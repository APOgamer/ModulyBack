using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class UpdateInvitationStatusResource
{
    [Required]
    [RegularExpression("^(Accepted|Rejected)$", ErrorMessage = "Status must be either 'Accepted' or 'Rejected'")]
    public string Status { get; set; }
}