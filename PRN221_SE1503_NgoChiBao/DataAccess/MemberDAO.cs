using BusinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        
        public IEnumerable<Member> GetMembersList()
        {
            List<Member> members;
            try
            {
                var FStoreDB = new FStoreDBContext();
                members = FStoreDB.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public Member GetMemberByID(int MemberId)
        {
            Member member = null;
            try
            {
                var FStoreDB = new FStoreDBContext();
                member = FStoreDB.Members.SingleOrDefault(member => member.MemberId == MemberId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }

        public void AddNew(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member == null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Members.Add(member);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Member member)
        {
            try
            {
                Member m = GetMemberByID(member.MemberId);
                if (m != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Entry<Member>(member).State = EntityState.Modified;
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Members.Remove(member);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
