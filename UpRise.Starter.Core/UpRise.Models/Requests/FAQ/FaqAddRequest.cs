using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpRise.Models.Requests.FAQ
{
    public class FaqAddRequest
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int CategoryId { get; set; }
        public int SortOrder { get; set; }
    }
}
