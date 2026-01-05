```markdown
# 🌐 Overview

The `IMessageService` interface defines contract for sending messages (email and SMS) from the application. It abstracts underlying delivery mechanisms so implementations can use SMTP, SMS gateways, or test doubles.

# 🔑 Key Methods

- **SendEmail(string from, string to, string subject, string message)**: Sends an email message and returns `true` if delivery was initiated successfully.
- **SendSms(string phone, string message)**: Sends an SMS message to the specified phone number and returns `true` if delivery was initiated successfully.

# 🎯 Purpose

This interface promotes:

- **Decoupling**: Separates message-sending logic from consumers.
- **Testability**: Enable mocking or fakes for unit tests.
- **Flexibility**: Swap different transport implementations without changing callers.
```
