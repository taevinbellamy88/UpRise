using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpRise.Data.Extensions;
using UpRise.Data.Interfaces;
using UpRise.Models.Domain;
using UpRise.Models.Domain.Faq;
using UpRise.Models.Requests.FAQ;

namespace UpRise.Services.FAQ
{
    public class FAQService : IFAQService
    {
        IDataProvider _Data;

        public FAQService(IDataProvider data)
        {
            _Data = data;

        }

        public BaseFAQ getById(int id)
        {
            BaseFAQ faq = null;


            string procName = "[dbo].[FAQ_SelectByID]";
            _Data.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    faq = MapFaq(reader);
                });


            return faq;
        }

        public List<BaseFAQ> getAll()
        {
            List<BaseFAQ> list = null;


            string procName = "DBO.FAQ_SelectAll";

            _Data.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    BaseFAQ faq = MapFaq(reader);


                    if (list == null)
                    {
                        list = new List<BaseFAQ>();
                    }
                    list.Add(faq);

                });

            return list;

        }

        public int AddFaq(FaqAddRequest model, int userId)
        {
            int id = 0;


            string procName = "[dbo].[FAQ_Insert]";
            _Data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    AddCommonParams(model, collection , userId);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    collection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {

                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out id);

                });


            return id;
        }

        public void Update(FaqUpdateRequest model, int userId)
        {

            string procName = "[dbo].[FAQ_UPDATE]";
            _Data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    AddCommonParams(model, collection, userId);
                    collection.AddWithValue("@Id", model.Id);


                },
                returnParameters: null);

        }

        public void delete(int id)
        {
            string procName = "[dbo].[FAQ_DELETE]";
            _Data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", id);
                },
                returnParameters: null);
        }

        private static BaseFAQ MapFaq(IDataReader reader)
        {
            BaseFAQ faq = new BaseFAQ();
            int startingIndex = 0;

            faq.Id = reader.GetSafeInt32(startingIndex++);
            faq.Question = reader.GetSafeString(startingIndex++);
            faq.Answer = reader.GetSafeString(startingIndex++);
            faq.CategoryId = reader.GetSafeInt32(startingIndex++);
            faq.SortOrder = reader.GetSafeInt32(startingIndex++);
            faq.DateCreated = reader.GetSafeDateTime(startingIndex++);
            faq.DateModified = reader.GetSafeDateTime(startingIndex++);
            faq.CreatedBy = reader.GetSafeInt32(startingIndex++);
            faq.ModifiedBy = reader.GetSafeInt32(startingIndex++);
            return faq;
        }

        private static void AddCommonParams(FaqAddRequest model, SqlParameterCollection collection, int userId)
        {
            collection.AddWithValue("@Question", model.Question);
            collection.AddWithValue("@Answer", model.Answer);
            collection.AddWithValue("@CategoryId", model.CategoryId);
            collection.AddWithValue("@SortOrder", model.SortOrder);
            collection.AddWithValue("@CreatedBy",  userId);
            collection.AddWithValue("@ModifiedBy", userId);
        }

    }

}
