namespace WebApplication1.Auth
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Owner { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ApiKeyRole> Roles { get; set; }
    }

    public class ApiKeyRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ApiKeyId { get; set; }
        public ApiKey ApiKey { get; set; }
    }
}
