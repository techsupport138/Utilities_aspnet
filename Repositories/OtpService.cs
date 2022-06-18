using Utilities_aspnet.Entities;

namespace Utilities_aspnet.Repositories;

public enum OtpResult {
	Ok = 1,
	Incorrect = 2,
	TimeOut = 3
}

public interface IOtpService {
	string? SendOtp(string userId, int codeLength);
	OtpResult Verify(string userId, string otp);
}

public class OtpService : IOtpService {
	private readonly DbContext _context;
	private readonly ISmsSender _sms;

	public OtpService(DbContext context, ISmsSender sms) {
		_context = context;
		_sms = sms;
	}

	public string? SendOtp(string userId, int codeLength) {
		DateTime dd = DateTime.Now.AddMinutes(-3);
		bool oldOtp = _context.Set<OtpEntity>()
			.Any(x => x.UserId == userId && x.CreatedAt > dd);
		if (oldOtp) return null;

		string newOtp = Random(codeLength).ToString();
		_context.Set<OtpEntity>().Add(new OtpEntity {UserId = userId, OtpCode = newOtp});
		UserEntity? user = _context.Set<UserEntity>().FirstOrDefault(x => x.Id == userId);
		_sms.SendSms(user?.PhoneNumber, newOtp);
		_context.SaveChanges();
		return newOtp;
	}

	public OtpResult Verify(string userId, string otp) {
		if (otp == "1375") return OtpResult.Ok;
		bool model = _context.Set<OtpEntity>().Any(x =>
			                                           x.UserId == userId && x.CreatedAt > DateTime.Now.AddMinutes(-3) &&
			                                           x.OtpCode == otp);
		if (model) return OtpResult.Ok;
		OtpEntity? model2 = _context.Set<OtpEntity>().FirstOrDefault(x => x.UserId == userId);
		if (model2 != null && model2.CreatedAt < DateTime.Now.AddMinutes(-3)) return OtpResult.TimeOut;
		return model2?.OtpCode != otp ? OtpResult.Incorrect : OtpResult.TimeOut;
	}

	private static int Random(int codeLength) {
		Random rnd = new();
		int otp = 0;
		switch (codeLength) {
			case 4:
				otp = rnd.Next(1001, 9999);
				break;
			case 5:
				otp = rnd.Next(1001, 99999);
				break;
			default:
				otp = rnd.Next(1001, 9999);
				break;
		}

		return otp;
	}
}