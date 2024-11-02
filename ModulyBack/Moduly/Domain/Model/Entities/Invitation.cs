using ModulyBack.IAM.Domain.Model.Aggregates;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class Invitation
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid TransmitterId { get; set; }
    public User Transmitter { get; set; }

    public string Status { get; set; } = "Sent"; 

    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public void UpdateStatus(string newStatus)
    {
        if (Status == "Sent" && (newStatus == "Accepted" || newStatus == "Rejected"))
        {
            Status = newStatus;
        }
        else
        {
            throw new InvalidOperationException("Invalid status change");
        }
    }
}