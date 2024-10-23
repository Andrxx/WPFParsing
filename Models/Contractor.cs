using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParsing.Models
{
    [PrimaryKey(nameof(GUID))]
    public class Contractor
    {
        public string GUID { get; set; }
        public DateTime RegDate { get; set; }
        public string? Name { get; set; }
        public string? Addres { get; set; }
        public string? Phone { get; set; }
    }
}
