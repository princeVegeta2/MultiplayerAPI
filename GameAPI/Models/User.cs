namespace GameAPI.Models
{
   public class User
{
    public int UserID { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string CharacterName { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public bool IsActive { get; set; }
}

}    
