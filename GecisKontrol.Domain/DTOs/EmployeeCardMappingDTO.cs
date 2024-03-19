using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GecisKontrol.Domain.DTOs
{
    public class EmployeeCardMappingDTO
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string CardHex { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
