//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWBackend
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobsAssigned
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobsAssigned()
        {
            this.EmployeeJobs = new HashSet<EmployeeJob>();
        }
    
        public int AssignID { get; set; }
        public string AssignJOBNUM { get; set; }
        public string AssignCLIENT { get; set; }
        public string AssignWORK { get; set; }
        public string AssignAREA { get; set; }
        public string AssignINSTRUCTIONS { get; set; }
        public string AssignTRUCK { get; set; }
        public Nullable<System.DateTime> TextSENT { get; set; }
        public Nullable<System.DateTime> AssignSTARTTIME { get; set; }
        public Nullable<System.DateTime> AssignENDTIME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeJob> EmployeeJobs { get; set; }
    }
}