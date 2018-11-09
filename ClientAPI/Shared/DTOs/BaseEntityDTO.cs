using System;

namespace ClientAPI.Shared.DTOs
{
    public abstract class BaseEntityDTO
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}