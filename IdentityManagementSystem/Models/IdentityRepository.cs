namespace IdentityManagementSystem.Models
{
    public class IdentityRepository : IIdentityRepository
    {
        public void AddUser(User user)
        {
            using (var context = new IdentityDbContext())
            {
                context.Users?.Add(user);
                context.SaveChanges();
            }
        }

        public UserModel? GetUser(string login, string password)
        {
            using (var context = new IdentityDbContext())
            {
                var result = context.Users.FirstOrDefault(o => o.UserName.ToLower() == login.ToLower() && o.Password == password);

                if (result == null) return null;

                var userModel = new UserModel();

                userModel.UserName = result.UserName;

                var roleResult = context.UserRole.FirstOrDefault(o => o.UserName == userModel.UserName);

                if (roleResult == null) return null;

                userModel.RoleName = roleResult.RoleName;

                var permissionResult = context.RolePermission.Where(o => o.RoleName == userModel.RoleName).Select(x => x.Action);

                if (permissionResult == null) return null;

                userModel.Permissions = permissionResult.ToList();

                return userModel;
            }
        }

        public void AddRole(Roles role)
        {
            using (var context = new IdentityDbContext())
            {
                context.Role?.Add(role);
                context.SaveChanges();
            }
        }

        public void AddUserRole(UserRoles item)
        {
            using (var context = new IdentityDbContext())
            {
                context.UserRole?.Add(item);
                context.SaveChanges();
            }
        }

        public void AddRolePermissions(RolePermissions item)
        {
            using (var context = new IdentityDbContext())
            {
                context.RolePermission?.Add(item);
                context.SaveChanges();
            }
        }
    }
}
