using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commons;

public interface IBaseAuditableEntity
{
    int Id { get; set; }
    int? CreatedBy { get; set; }
    int? UpdatedBy { get; set; }
    DateTime? CreateDate { get; set; }
    DateTime? UpdateDate { get; set; }
    bool IsActive { get; set; }
    bool IsDeleted { get; set; }
}
