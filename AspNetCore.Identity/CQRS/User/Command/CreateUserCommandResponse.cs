namespace AspNetCore.Identity.CQRS.User.Command
{
    public class CreateUserCommandResponse
    {
        public bool Successed { get; set; }
        public string Message { get; set; }
    }
}
