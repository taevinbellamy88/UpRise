using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpRise.Models.Interfaces;

namespace UpRise.Models.Requests.FAQ
{
    public class FaqUpdateRequest : FaqAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    
    }
}
