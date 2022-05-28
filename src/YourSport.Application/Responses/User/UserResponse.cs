namespace YourSport.Application.Responses.User;

public class UserResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EditedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Key { get; set; }
    public int Ddd { get; set; }
    public int PhoneNumber { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string StateInitials { get; set; }
}