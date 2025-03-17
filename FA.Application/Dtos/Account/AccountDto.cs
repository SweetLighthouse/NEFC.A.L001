namespace FA.Application.Dtos.Account;

public class AccountDto
{
    public string Username { get; set; }
    public string Password { get; set; }

    public AccountDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
