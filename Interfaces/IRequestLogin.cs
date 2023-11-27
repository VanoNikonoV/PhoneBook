namespace PhoneBook.Interfaces
{
    public interface IRequestLogin
    {
         string Email { get; set; }

         bool IsToken { get; set; }
    }
}
