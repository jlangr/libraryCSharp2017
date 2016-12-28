using System;
using Library.Reporting;
using NUnit.Framework;

namespace LibraryTest.Reporting
{
    [TestFixture]
    public class ReportMailerTest
    {
        [SetUp]
        public void Initialize()
        {
            Environment.SetEnvironmentVariable(MailDestination.SmtpUser, "user");
            Environment.SetEnvironmentVariable(MailDestination.SmtpPassword, "abc123");
            Environment.SetEnvironmentVariable(MailDestination.SmtpServer, "mail.librarySystem.com");
        }

        [Test, Ignore]
        public void SendMail()
        {
            //const string toAddress = "a@b.com";
            //new MailDestination(toAddress)
            ////MailDestination[] destinations = { ? };

            ////var mailer = new ReportMailer(...); // uh oh!
            //mailer.MailReport(report);
        }

        class StubReport : IReport
        {
            public string Text() { return "text"; }
            public string Name() { return "name"; }
        }
    }
}
