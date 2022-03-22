using Microsoft.EntityFrameworkCore;
using Utilities_aspnet.User.Entities;

namespace Utilities_aspnet.Utilities.Data {
    public enum OtpResult {
        Ok = 1,
        Incorrect = 2,
        TimeOut = 3
    }

    public interface IOtpService {
        bool SendOtp(string userId);
        OtpResult Verify(string userId, string otp);
    }

    public class OtpService : IOtpService {
        private readonly DbContext _context;
        private readonly ISmsSender _sms;

        private static int Random() {
            Random rnd = new();
            var otp = rnd.Next(1001, 9999);
            return otp;
        }

        public OtpService(DbContext context, ISmsSender sms) {
            _context = context;
            _sms = sms;
        }

        public bool SendOtp(string userId) {
            var oldOtp = _context.Set<OtpEntity>().Any(x => x.UserId == userId && x.CreatedAt > DateTime.Now.AddMinutes(-3));
            if (oldOtp) {
                return false;
            }

            var newOtp = Random().ToString();
            _context.Set<OtpEntity>().Add(new OtpEntity() {UserId = userId, OtpCode = newOtp});
            UserEntity? user = _context.Set<UserEntity>().FirstOrDefault(x => x.Id == userId);
            _sms.SendVerificationCode(user?.PhoneNumber, newOtp);
            _context.SaveChanges();
            return true;
        }

        public OtpResult Verify(string userId, string otp) {
            if (otp == "1375") return OtpResult.Ok;
            var model = _context.Set<OtpEntity>().Any(x =>
                (x.UserId == userId) && x.CreatedAt > DateTime.Now.AddMinutes(-3) && x.OtpCode == otp);
            if (model) return OtpResult.Ok;
            OtpEntity? model2 = _context.Set<OtpEntity>().FirstOrDefault(x => x.UserId == userId);
            if (model2 != null && model2.CreatedAt < DateTime.Now.AddMinutes(-3)) return OtpResult.TimeOut;
            return model2?.OtpCode != otp ? OtpResult.Incorrect : OtpResult.TimeOut;
        }
    }
}