//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QueryRoom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_QUESTIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_QUESTIONS()
        {
            this.TBL_ANSWERS = new HashSet<TBL_ANSWERS>();
        }
    
        public System.Guid QID { get; set; }
        public string QUESTION { get; set; }
        public string USERNAME { get; set; }
        public Nullable<System.DateTime> TIMESTAMP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ANSWERS> TBL_ANSWERS { get; set; }
        public virtual TBL_SIGNUP TBL_SIGNUP { get; set; }
    }
}
