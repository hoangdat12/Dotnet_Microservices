namespace OrderService.Application.Dto
{
    public record UserDecode
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
