using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CurrentProject.Database;

namespace RuplandTest
{
    class MockUserRepository 
        : CurrentProjectRepository.GenericRepository.Interface.IGenericRepository<CurrentProject.Database.User>
    {
        // private IQueryable<User> userlist;
        private List<User> userlist;

        public MockUserRepository()
        {
            userlist = new List<User>();            
        }

        public void Create(User entity)
        {
            User u = userlist.Find(x => x.Email == entity.Email);
            if (u != null)
                throw new MockEntityException();
            userlist.Add(entity);          
        }

        public void Delete(int id)
        {
            User u = userlist.Find(x => x.UserId == id);
            userlist.Remove(u);
        }

        public void Delete(User entity)
        {
            userlist.Remove(entity);
        }

        public User GetById(int id)
        {
            return userlist.Find(x => x.UserId == id);
        }

        public IEnumerable<User> Read()
        {
            return userlist;
        }

        public void Save()  // does nothing
        {
            ;
        }

        public IQueryable<User> SearchFor(Expression<Func<User, bool>> predicate)
        {
            // Virtually nothing you can instantiate with new in the basic .NET Framework implements IQueryable.

            IQueryable<User> queryableUserList = userlist.AsQueryable<User>();

            IQueryable<User> selectedUserList = queryableUserList.Where<User>(predicate);

            return selectedUserList;
        }

        public void Update(User entity)
        {
            User oldUser = userlist.Find(x => x.UserId == entity.UserId);
            userlist.Remove(oldUser);
            userlist.Add(entity);
        }
    }
}
