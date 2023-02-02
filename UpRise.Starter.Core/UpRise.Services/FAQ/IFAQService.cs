using UpRise.Models.Domain.Faq;
using UpRise.Models.Requests.FAQ;

namespace UpRise.Services.FAQ
{
    public interface IFAQService
    {
        int AddFaq(FaqAddRequest model,int userId);
        void delete(int id);
        List<BaseFAQ> getAll();
        BaseFAQ getById(int id);
        void Update(FaqUpdateRequest model , int userId);
    }
}