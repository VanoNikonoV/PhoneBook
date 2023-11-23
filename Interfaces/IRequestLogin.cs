namespace PhoneBook.Interfaces
{
    public interface IRequestLogin
    {
         string Email { get; set; }

         string Password { get; set; }

        string Token { get; set; }
    }
}
