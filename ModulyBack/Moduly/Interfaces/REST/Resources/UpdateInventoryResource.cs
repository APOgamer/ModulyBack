namespace ModulyBack.Moduly.Interfaces.REST.Resources
{
    public class UpdateInventoryResource
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}