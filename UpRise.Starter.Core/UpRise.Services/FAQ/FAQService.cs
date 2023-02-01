using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpRise.Data.Interfaces;
using UpRise.Models.Domain;
using UpRise.Models.Domain.Faq;

namespace UpRise.Services.FAQ
{
    public class FAQService
    {
        IDataProvider _Data;

        public FAQService(IDataProvider data)
        {
            _Data = data;

        }


        public List<baseFAQ> getAll()
        {
            List<baseFAQ> faq = new List<baseFAQ>();

            return faq;

        }


        
    }

}
