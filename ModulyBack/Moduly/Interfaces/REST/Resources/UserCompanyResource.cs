namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class UserCompanyResource
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string Role { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; }
}