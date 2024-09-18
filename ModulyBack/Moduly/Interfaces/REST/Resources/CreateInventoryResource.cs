namespace ModulyBack.Moduly.Interfaces.REST.Resources
{
    public class CreateInventoryResource
    {
        public Guid UserId { get; set; }
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}