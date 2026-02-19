using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public class TbLogDTO
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string? Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; } // بصيغة XML أو JSON حسب إعداد المكتبة
    }
}
