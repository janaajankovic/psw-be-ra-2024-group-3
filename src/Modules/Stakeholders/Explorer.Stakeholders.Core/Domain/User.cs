using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Stakeholders.Core.Domain;

//[Table("Users", Schema = "stakeholders")]  
public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }

    public User(string username, string password, UserRole role, bool isActive)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Location = new Location(0, 0);
        Validate();
    }


    public bool SetLocation(float longitude, float latitude)
    {
        if (Role == UserRole.Tourist && IsActive)
        {
            Location = new Location(latitude, longitude);
            return true;
        }
        throw new ArgumentException("Unauthorized attempt to set location.");
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}