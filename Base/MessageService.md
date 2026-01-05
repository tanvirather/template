# 🌐 Overview

The `MessageService` class implements `IMessageService` and provides a simple message-sending implementation backed by an `SmtpClient`. It supports sending email and a basic SMS fallback (via the email transport) for simple integrations and tests.

# 🔑 Key Members

- **Constructor**: `MessageService(SmtpClient smtpClient)` — accepts an `SmtpClient` used to send messages.
- **SendEmail(string from, string to, string subject, string message)**: Sends an email asynchronously and returns `true` when the send was initiated.
- **SendSms(string phone, string message)**: Sends an SMS by delegating to `SendEmail`, using the phone number as both sender and recipient and using the message as subject and body.

# 🎯 Purpose

- **Simplicity**: Lightweight wrapper over `SmtpClient` for straightforward message delivery.
- **Reusability**: Centralizes message-sending behavior for the application.
- **Testability**: Can be replaced with fakes or mocks for unit tests.

# 🛠 Unit-testing example using a fake `SmtpClient`:

```csharp
public class FakeSmtpClient : SmtpClient {
		public List<(string from,string to,string subject,string body)> Sent = new();
		public override Task SendMailAsync(string from, string recipients, string subject, string body) {
				Sent.Add((from, recipients, subject, body));
				return Task.CompletedTask;
		}
}

var fake = new FakeSmtpClient();
var svc = new MessageService(fake);
await svc.SendEmail("a@x.com","b@x.com","hi","body");
// assert fake.Sent.Count == 1

```
