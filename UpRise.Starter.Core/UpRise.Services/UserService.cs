using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using UpRise.Data.Extensions;
using UpRise.Data.Interfaces;
using UpRise.Models.Domain;
using UpRise.Models.Interfaces;
using UpRise.Models.Requests.User;
using UpRise.Services.Interfaces;
using UpRise.Services.Interfaces.Security;

namespace UpRise.Services
{
    public class UserService : IUserService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _dataProvider;

        public UserService(IAuthenticationService<int> authSerice, IDataProvider dataProvider)
        {
            _authenticationService = authSerice;
            _dataProvider = dataProvider;
        }

        public async Task<bool> LogInAsync(string email, string password)
        {
            bool isSuccessful = false;

            IUserAuthData response = Get(email, password);

            if (response != null)
            {
                await _authenticationService.LogInAsync(response);
                isSuccessful = true;
            }

            return isSuccessful;
        }

        public async Task<bool> LogInTest(string email, string password, int id, string[] roles = null)
        {
            bool isSuccessful = false;
            var testRoles = new[] { "User", "Super", "Content Manager" };

            var allRoles = roles == null ? testRoles : testRoles.Concat(roles);

            IUserAuthData response = new UserBase
            {
                Id = id
                ,
                Email = email
                ,
                Roles = allRoles

            };

            Claim fullName = new Claim("CustomClaim", "UpRise");
            await _authenticationService.LogInAsync(response, new Claim[] { fullName });

            return isSuccessful;
        }

        //CHECKED
        public int Create(UserAddRequest userModel)
        {
            int userId = 0;
            string password = userModel.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt();
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            string procName = "[dbo].[Users_Insert]";

            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    MapSingleUser(userModel, paramCol, hashedPassword);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    paramCol.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCol)
                {
                    object oId = returnCol["@Id"].Value;

                    int.TryParse(oId.ToString(), out userId);
                });

            return userId;
        }

        private static void MapSingleUser(UserAddRequest userModel, SqlParameterCollection paramCol, string hashedPassword)
        {
            paramCol.AddWithValue("@Email", userModel.Email);
            paramCol.AddWithValue("@Username", userModel.UserName);
            paramCol.AddWithValue("@FirstName", userModel.FirstName);
            paramCol.AddWithValue("@LastName", userModel.LastName);
            paramCol.AddWithValue("@Password", hashedPassword);
            paramCol.AddWithValue("@IsConfirmed", false);
            paramCol.AddWithValue("@Roles", "User");
        }

        public void UpdatePassword(UserUpdatePasswordRequest model)
        {
            string password = model.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt();
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt); //hash password here

            string procName = "[dbo].[Users_Update]";

            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@Email", model.Email);
                    col.AddWithValue("@Password", hashedPassword);
                    col.AddWithValue("@IsConfirmed", true);
                    col.AddWithValue("@UserStatusId", 1);

                },
                returnParameters: null);
        }

        public void ConfirmUser(string token)
        {
            string procName = "[dbo].[Users_UpdateIsConfirm]";

            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Token", token);
                },
                returnParameters: null
                );
        }

        public void InsertToken(string token, int userId, int tokenType)
        {

            string procName = "[dbo].[UserTokens_Insert]";

            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Token", token);
                    paramCol.AddWithValue("@UserId", userId);
                    paramCol.AddWithValue("@TokenType", tokenType);
                },
                returnParameters: null);
        }

        private IUserAuthData Get(string email, string password)
        {
            string passwordFromDb = "";
            UserBase user = null;

            string procName = "[dbo].[Users_Select_AuthData]";

            _dataProvider.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Email", email);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    UserBase aUser = new UserBase();
                    aUser.Id = reader.GetSafeInt32(0);
                    aUser.Email = reader.GetSafeString(1);
                    passwordFromDb = reader.GetSafeString(2);
                    aUser.Roles = new[] { reader.GetSafeString(3) };

                    bool isValidCredentials = BCrypt.BCryptHelper.CheckPassword(password, passwordFromDb);

                    if (isValidCredentials)
                    {
                        user = aUser;
                    }
                    else throw new Exception("Incorrect Password");
                }
                );

            return user;
        }

        public void DeleteToken(string token)
        {
            string procName = "[dbo].[UserTokens_Delete_ByToken]";

            _dataProvider.ExecuteNonQuery(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Token", token);
            });
        }

    }
}