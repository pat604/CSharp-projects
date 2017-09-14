using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProjectRepository.GenericRepository;
using CurrentProjectRepository.Repository;
using CurrentProjectRepository.Repository.Interface;
using CurrentProject.Database;
using CurrentProject.Class;
using System.Collections.ObjectModel;
using System.Collections;
using System.Transactions;

namespace CurrentProjectRepository.Repository
{
    class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {

        GenericRepository<ProjectMember> projectMemberRepository;
        GenericRepository<Role> roleRepository;
        GenericRepository<User> userRepository;
        GenericRepository<CurrentProject.Database.Task> taskRepository;
        GenericRepository<Chat> chatRepository;

        public ProjectRepository()
        {
            projectMemberRepository = new GenericRepository<ProjectMember>();
            roleRepository = new GenericRepository<Role>();
            userRepository = new GenericRepository<User>();
            taskRepository = new GenericRepository<CurrentProject.Database.Task>();
            chatRepository = new GenericRepository<Chat>();
        }

        public IEnumerable GetUserProjects()
        {
            try
            {
                var q1 = from project in Read()
                         join member in projectMemberRepository.Read() on project.ProjectId equals member.ProjectId
                         join role in roleRepository.Read() on member.RoleId equals role.RoleId
                         where member.UserId == Session.Get<int>("U_Id")
                         select new { ProjectId = project.ProjectId, Name = project.Name, EndDate = project.EndDate, RoleName = role.Name };

                return q1.ToList();
            }
            catch (Exception exc)
            {
                Helper.SyncMessageBox(exc.Message);
            }

            return null;
        }

        public IEnumerable GetProjectMembers(int projectId)
        {
            try 
            {
                var q1 = from member in projectMemberRepository.Read()
                         join role in roleRepository.Read() on member.RoleId equals role.RoleId
                         join user in userRepository.Read() on member.UserId equals user.UserId
                         where member.ProjectId == projectId
                         select new { FullName = user.Firstname + " " + user.Lastname, RoleName = role.Name, UserId = user.UserId };

                return q1.ToList();
            }
            catch (Exception exc)
            {
                Helper.SyncMessageBox(exc.Message);
            }

            return null;
        }

        public IEnumerable GetTasksByMilestones(int projectId, int milestoneId)
        {
            try
            {
                var q1 = from task in taskRepository.Read()
                         join user in userRepository.Read() on task.UserId equals user.UserId
                         where (task.ProjectId == projectId) && (task.MilestoneId == milestoneId)
                         select new { FullString = task.Task1 + "\t\t\tDátum: " + task.AddedDate + "  |  " + user.Firstname + " " + user.Lastname, TaskId = task.TaskId, task.Completed };

                return q1.ToList();
            }
            catch (Exception exc)
            {
                Helper.SyncMessageBox(exc.Message);
            }

            return null;
        }

        public IEnumerable GetProjectChatMessage(int projectId)
        {

            try
            {
                var q1 = from message in chatRepository.Read()
                         join user in userRepository.Read() on message.UserId equals user.UserId
                         where message.ProjectId == projectId
                         select new { FullNameWithDate = user.Lastname + " - " + message.AddedDate, Message = message.Message, Date = message.AddedDate };

                return q1.OrderByDescending(message => message.Date).Take(10).Reverse().ToList();
            }
            catch (Exception exc)
            {
                Helper.SyncMessageBox(exc.Message);
            }

            return null;

        }

    }
}
