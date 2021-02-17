using System;

namespace Quote.Global
{
    public class BaseModel: IBaseModel
    {
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreateUser { get; set; }
        public int? UpdateUser { get; set; }
    }
}
