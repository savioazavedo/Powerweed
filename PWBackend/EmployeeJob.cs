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
    
    public partial class EmployeeJob
    {
        public int EmployeeJOBSID { get; set; }
        public Nullable<int> AssignID { get; set; }
        public string EmpNAME { get; set; }
    
        public virtual JobsAssigned JobsAssigned { get; set; }
    }
}