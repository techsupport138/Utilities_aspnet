using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_aspnet.User.Dtos;
using Utilities_aspnet.Utilities.Responses;

namespace Utilities_aspnet.User.Data
{
    public interface IUserRepository
    {
        ApiResponse RequestVerificationCode(RequestVerificationCodeDto dto);
        ApiResponse VerifyMobileForLogin(RequestVerificationCodeDto dto);

    }

    public class UserRepository : IUserRepository
    {
        ApiResponse IUserRepository.RequestVerificationCode(RequestVerificationCodeDto dto)
        {
            throw new NotImplementedException();
        }

        ApiResponse IUserRepository.VerifyMobileForLogin(RequestVerificationCodeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
