using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AuthRepository : IAuthRepository
    {
        //new instance of FStoreDBContext, luu trong bo nho
        // db la tham chieu giu dia chi trong bo nho
        FStoreDBContext db = new FStoreDBContext();
        public Member checkLogin(string username, string password) 
            //Linq - ngon ngu truy van tich hop - khi dung truy van du lieu sql - kieu viet khac
            // FirstOrDefault - extension method (1 trong 5 ae) - di theo Lingq
            // => - lambda expression (1 trong 5 ae) - di theo Lingq 
            // nếu chỉ có 1 dòng thay vì dùng dấu {} thì dùng =>
            => db.Members.FirstOrDefault(x => x.Email.Equals(username) && x.Password.Equals(password));
    }
}
