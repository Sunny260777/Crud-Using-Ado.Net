using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudApp.DataModel
{
    public class CommonModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "start index in required")]
        public string start_index { get; set; } = "0";

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "paging size in required")]
        public string paging_size { get; set; } = "10";

        public string column { get; set; }
        public string table { get; set; }
        public string order_by { get; set; }
        public string where { get; set; }
        public string id { get; set; }
    }
}