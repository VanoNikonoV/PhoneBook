namespace PhoneBook.Interfaces
{
    public interface IRequestLogin
    {
        string Email { get; set; }

        string Password { get; set; }

        bool IsToken { get; set; }
    }
}
