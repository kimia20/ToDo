namespace WebEndpoints.ToDo.UI.Models
{
    public class LoginResult
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public DateTime? expiration { get; set; }
    }
}
