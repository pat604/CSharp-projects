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
    
    public partial class SecurityQuestion
    {
        public SecurityQuestion()
        {
            this.User = new HashSet<User>();
        }
    
        public int SecurityQuestionId { get; set; }
        public string Question { get; set; }
    
        public virtual ICollection<User> User { get; set; }
    }
}
