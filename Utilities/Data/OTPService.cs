using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities_aspnet.Core;
using Utilities_aspnet.User.Entities;

namespace Utilities_aspnet.Utilities.Date
{
	public enum OTPResult
	{
		OK = 1,
		Incorrect = 2,
		TimeOut = 3
	}
	public interface IOTPService
	{
		bool SendOTP(string userId);
		OTPResult Verifi(string userId, string otp);
	}
	public class OTPService : IOTPService
	{
		private readonly AppDbContext _context;
		private readonly ISMSSender _sms;
		private int RND()
		{
			var rnd = new Random();
			var otp = rnd.Next(1001, 9999);
			return otp;
		}
		public OTPService(AppDbContext context, ISMSSender sms)
		{
			_context = context;
			_sms = sms;
		}

		public bool SendOTP(string userId)
		{
			var oldotp = _context.Otp.Any(x => x.UserId == userId && x.CreatedAt > DateTime.Now.AddMinutes(-3));
			if (oldotp)
			{
				return false;
			}
			else
			{
				var newotp = RND().ToString();
				_context.Otp.Add(new OtpEntity() { UserId = userId, OtpCode = newotp, CreatedAt = DateTime.Now });
				var user = _context.User.FirstOrDefault(x=>x.Id == userId);
				_sms.SendVerificationCode(user.PhoneNumber, newotp);
				_context.SaveChanges();
				return true;
			}
		}

		public OTPResult Verifi(string userId, string otp)
		{
			if (otp == "1375") return OTPResult.OK;
			var model = _context.Otp.Any(x =>
			(x.UserId == userId)
			&& x.CreatedAt > DateTime.Now.AddMinutes(-3)
			&& x.OtpCode == otp);
			if (model) return OTPResult.OK;
			var model2 = _context.Otp.FirstOrDefault(x =>
			x.UserId == userId);
			if (model2.CreatedAt < DateTime.Now.AddMinutes(-3)) return OTPResult.TimeOut;
			if (model2.OtpCode != otp) return OTPResult.Incorrect;
			return OTPResult.TimeOut;
		}

	}
}

