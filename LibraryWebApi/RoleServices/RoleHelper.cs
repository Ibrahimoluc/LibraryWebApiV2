namespace LibraryWebApi.RoleServices
{
    //Bu class benim DB de Role ü tek bir harfle "A" gibi tuttugumu dusunerek, gerekli donusumu sagliyor
    //Eger farklı bir sekilde tutan db baglanırsa, projede hicbir yer degismeden sadece burayi degistirerek devam edebilmeliyim.
    //Amacı bu, diger class lar IRoleHelper alıcak, o yüzden o T alıyor, yani degisiklige acık
    //Bu ise T yerine UserRole koyuyor, programa ozgu implementasyon var
    public class RoleHelper : IRoleHelper<UserRole>
    {
        public UserRole FromDb(string DbRole)
        {
            Console.WriteLine("GetRole e gelen Role:" + DbRole);
            if (DbRole == null) throw new ArgumentNullException("Role null"); 
            if(DbRole == "A") return new UserRole { Role = Roles.Admin };
            if(DbRole == "B") return new UserRole { Role = Roles.User };
            throw new ArgumentException("Db den gecerli Role gelmedi");
        }

        public string ToDb(UserRole userRole)
        {
            if (userRole._role == Roles.Admin) return "A";
            if (userRole._role == Roles.User) return "B";
            throw new ArgumentException("UserRole gecerli degil");
        }
    }
}
