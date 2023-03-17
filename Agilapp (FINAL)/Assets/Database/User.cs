
public class User
{
    public string nickname;
    public string pronoun;
    public string email_address;
    public string password;
    public string confirm_password;
    public string contact_number;
    public int birthday;

    public User(string nickname, string pronoun, string email_address, string password, string confirm_password, string contact_number, int birthday)
    {
        this.nickname = nickname;
        this.pronoun = pronoun;
        this.email_address = email_address;
        this.password = password;
        this.confirm_password = confirm_password;
        this.contact_number = contact_number;
        this.birthday = birthday;
    }

}
