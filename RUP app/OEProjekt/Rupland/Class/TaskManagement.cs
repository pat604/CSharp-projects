using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentProjectRepository.Repository;
using CurrentProject.Database;
using CurrentProjectRepository.GenericRepository;
using System.Collections;

namespace CurrentProject.Class
{
    class TaskManagement
    {

        GenericRepository<Database.Task> taskRepository;
        GenericRepository<Milestone> milestoneRepository;
        GenericRepository<User> userRepository;
        GenericRepository<ProjectMember> projectmemberRepository;
        ProjectRepository projectRepository;

        public TaskManagement(int currentProjectId)
        {
            taskRepository = new GenericRepository<Database.Task>();
            milestoneRepository = new GenericRepository<Milestone>();          
            userRepository = new GenericRepository<User>();
            projectmemberRepository = new GenericRepository<ProjectMember>();
            projectRepository = new ProjectRepository();

            this.currentProjectId = currentProjectId;
            currentProject = GetCurrentProject();
        }

        private int currentProjectId;

        private Project currentProject;

        private Project GetCurrentProject()
        {
            return projectRepository.SearchFor(p => p.ProjectId == currentProjectId).First();
        }

        public void NewTask(string taskName, string description, int selectedMilestoneId, int selectedMemberId)
        {
            taskRepository.Create(new Database.Task() {
                ProjectId = currentProjectId,
                Task1 = taskName,
                Description = description,
                MilestoneId = selectedMilestoneId,
                UserId = selectedMemberId,
                Completed = 0,
                CompletedDate = Helper.CurrentTimestamp(),
                AddedDate = Helper.CurrentTimestamp()
            });

            taskRepository.Save();
        }

        public List<Milestone> GetCurrentMilestones()
        {
            return milestoneRepository.SearchFor(m => m.MethodologyId == currentProject.MethodologyId).ToList();
        }

        public IEnumerable GetProjectMembers()
        {
            return projectRepository.GetProjectMembers(currentProjectId);
        }

    }
}
