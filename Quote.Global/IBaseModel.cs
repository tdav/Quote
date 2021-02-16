using System;
using System.Collections.Generic;
using System.Text;

namespace Quote.Global
{
    public interface IBaseModel
    {
        int Status { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        int CreateUser { get; set; }
        int? UpdateUser { get; set; }
    }
}
