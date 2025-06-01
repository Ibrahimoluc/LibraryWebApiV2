namespace LibraryWebApi.RoleServices
{
    public interface IRoleHelper<T>
    {
        //db den role genel olarak string seklinde gelecek
        //suan kullanmıyorum
        T FromDb(string DbRole);
        string ToDb(UserRole userRole);
    }
}
