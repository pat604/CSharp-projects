//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CurrentProject.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Methodology
    {
        public Methodology()
        {
            this.Milestone = new HashSet<Milestone>();
            this.Project = new HashSet<Project>();
        }
    
        public int MethodologyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Milestone> Milestone { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}