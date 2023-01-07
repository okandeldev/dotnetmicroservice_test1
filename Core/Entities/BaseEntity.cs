using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; protected set; }
    }

    public class AuditOnCreateEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
    }
   
    public class AuditOnUpdateEntity
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class AuditOnDeleteEntity
    {
        public DateTime? DeletedOn { get; set; }
    }
}
