using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProjectRepository.Repository;
using CurrentProject.Database;
using System.Collections.ObjectModel;
using CurrentProjectRepository.GenericRepository;
using System.Transactions;
using System.Collections;

namespace CurrentProject.Class
{
    class ProjectManagement
    {

        ProjectRepository projectRepository;

        GenericRepository<ProjectMember> projectMemberRepository;
        GenericRepository<Role> roleRepository;
        GenericRepository<Difficulty> difficultyRepository;
        GenericRepository<Methodology> methodologyRepository;
        GenericRepository<User> userRepository;
        GenericRepository<Milestone> milestoneRepository;
        GenericRepository<Chat> chatRepository;
        GenericRepository<CurrentProject.Database.Task> taskRepository; 

        public ProjectManagement()
        {
            projectRepository = new ProjectRepository();
            projectMemberRepository = new GenericRepository<ProjectMember>();
            roleRepository = new GenericRepository<Role>();
            difficultyRepository = new GenericRepository<Difficulty>();
            methodologyRepository = new GenericRepository<Methodology>();
            userRepository = new GenericRepository<User>();
            milestoneRepository = new GenericRepository<Milestone>();
            chatRepository = new GenericRepository<Chat>();
            taskRepository = new GenericRepository<Database.Task>();
        }

        public bool NewProject(string name, string description, int difficultyId, int methodologyId, DateTime startDate, DateTime endDate)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    Project newProject = new Project()
                    {
                        Name = name,
                        Description = description,
                        DifficultyId = difficultyId,
                        MethodologyId = methodologyId,
                        StartDate = startDate,
                        EndDate = endDate,
                        RealEndDate = endDate,
                        Registered = Helper.CurrentTimestamp()
                    };

                    projectRepository.Create(newProject);

                    projectRepository.Save();

                    projectMemberRepository.Create(new ProjectMember()
                    {
                        ProjectId = newProject.ProjectId,
                        UserId = Session.Get<int>("U_Id"),
                        RoleId = 1
                    });

                    projectMemberRepository.Save();

                    scope.Complete();

                    return true;

                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool NewProjectMember(int projectId, int userId, int roleId)
        {
            projectMemberRepository.Create(new ProjectMember()
            {
                ProjectId = projectId,
                UserId = userId,
                RoleId = roleId
            });

            projectMemberRepository.Save();

            return true;
        }

        public int CheckNewMemberData(string email, int projectId)
        {
            var userData = userRepository.SearchFor(user => user.Email == email).SingleOrDefault();

            if (userData != null)
            {

                var memberData = projectMemberRepository.SearchFor(member => member.UserId == userData.UserId && member.ProjectId == projectId).SingleOrDefault();

                if (memberData == null)
                {
                    return userData.UserId;
                }

            }

            return -1;
        }

        public List<Difficulty> GetDifficulty()
        {
            return difficultyRepository.Read().ToList();
        }

        public List<Methodology> GetMethodology()
        {
            return methodologyRepository.Read().ToList();
        }

        public List<Role> GetRole()
        {
            return roleRepository.Read().ToList();
        }

        public IEnumerable GetUserProjects()
        {
            return projectRepository.GetUserProjects();
        }

        public Project CurrentProject(int id)
        {
            return projectRepository.GetById(id);
        }

        public IEnumerable CurrentProjectMembers(int id)
        {
            return projectRepository.GetProjectMembers(id);
        }

        public List<Milestone> GetCurrentMilestones(int methodologyId)
        {
            return milestoneRepository.SearchFor(m => m.MethodologyId == methodologyId).ToList();
        }

        public ObservableCollection<IEnumerable> GetAllTasksByMilestones(int projectid)
        {
            ObservableCollection<IEnumerable> list = new ObservableCollection<IEnumerable>();

            foreach (Milestone item in GetCurrentMilestones(CurrentProject(projectid).MethodologyId))
            {
                list.Add(projectRepository.GetTasksByMilestones(projectid, item.MilestoneId));
            }

            return list;
        }  

        public void CreateMessageSend(int projectId, string message)
        {
            chatRepository.Create(new Chat
            {
                ProjectId = projectId,
                UserId = Session.Get<int>("U_Id"),
                Message = message,
                AddedDate = Helper.CurrentTimestamp()
            });

            chatRepository.Save();
        }

        public IEnumerable GetCurrentChatMessages(int projectId)
        {
            return projectRepository.GetProjectChatMessage(projectId);
        }
    }
}
