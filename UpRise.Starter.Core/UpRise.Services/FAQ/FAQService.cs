using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpRise.Data.Interfaces;

namespace UpRise.Services.FAQ
{
    public class FAQService
    {
        IDataProvider _Data;
        public FAQService(IDataProvider data)
        {
            _Data = data;

        }


        public FAQ getAll()
        {
            FAQ faq = new FAQ();

            return faq;

        }
    }

}
