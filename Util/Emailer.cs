using Microsoft.Extensions.Configuration;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiseRestApi.Util
{
    public static class Emailer
    {
        public static async void SendEmailToAdmin(RiseContext context, string code, RiseProgram program, PersonDetail newUser)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var apiKey = configuration["SendGridEmailSettings:SendGridKey"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage();

            var persons = context.Person.Where(p => !p.IsRemoved
                && (p.Role.RoleName == "SystemAdmin"
                    || ((p.OrganizationId == program.OrganizationId || p.ProgramId == program.ProgramId)
                    && p.Role.RoleName == "Admin"
                   ))
                && p.Email != null
                && p.Email != "");

            msg.SetFrom(new EmailAddress("kduggento@gmail.com", "RISE"));

            var recipients = new List<EmailAddress>();

            foreach (var person in persons)
            {
                //recipients.Add(new EmailAddress(person.Email, $"{person.FirstName} {person.LastName}"));
            }
            recipients.Add(new EmailAddress("kknecht@gmail.org", "Ken Knecht"));
            recipients.Add(new EmailAddress("kknecht@opeeka.com", "Ken Knecht"));
            recipients.Add(new EmailAddress("kduggento@opeeka.com", "Kerry Duggento"));
            msg.AddTos(recipients);

            msg.SetSubject("A new user has registered for RISE");

            msg.AddContent(MimeType.Html, $"<p>User information</p><p>Name {newUser.FirstName} {newUser.LastName}</p><p>Code used {code}</p>");
            
            var response = await client.SendEmailAsync(msg);
        }
    }
}
