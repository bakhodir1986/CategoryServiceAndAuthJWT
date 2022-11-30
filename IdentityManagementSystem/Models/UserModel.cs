namespace IdentityManagementSystem.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Permissions = new List<string>();
        }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        
        public List<string> Permissions { get; set; }
    }
}
