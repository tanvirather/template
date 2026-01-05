namespace Zuhid.Base;

public interface IMessageService {
  Task<bool> SendEmail(string from, string to, string subject, string message);
  Task<bool> SendSms(string phone, string message);
}
