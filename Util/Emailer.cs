using Microsoft.Extensions.Configuration;
using RiseRestApi.Models;
using RiseRestApi.Repository;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseRestApi.Util
{
    public class Emailer
    {
        private SendGridClient _client;
        private SendGridMessage _msg;
        private RiseContext _context = new RiseContext();

        public Emailer()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var apiKey = configuration["SendGridEmailSettings:SendGridKey"];
            _client = new SendGridClient(apiKey);
            _msg = new SendGridMessage();
        }

        ~Emailer()
        {
            _context.Dispose();
        }

        public async Task<Response> SendEmailToAdminNewUser(string code, RiseProgram program, PersonDetail newUser)
        {
            _msg.SetFrom(new EmailAddress("kduggento@gmail.com", "RISE"));
            var recipients = GetSystemAdmins();
            recipients.AddRange(GetProgramAdmins(program.ProgramId, program.OrganizationId));
            _msg.AddTos(recipients);
            _msg.SetSubject("A new user has registered for RISE");
            _msg.AddContent(MimeType.Html, $"<p>User information</p><p>Name {newUser.FullName}</p><p>Code used {code}</p>");

            return await _client.SendEmailAsync(_msg);
        }
        
        public List<EmailAddress> GetSystemAdmins()
        {
            var recipients = new List<EmailAddress>();
            var persons = _context.Person.Where(p => !p.IsRemoved
                && p.Role.RoleName == "SystemAdmin"
                && p.Email != null
                && p.Email != "");

            foreach (var person in persons)
            {
                //recipients.Add(new EmailAddress(person.Email, person.FullName));
            }

            recipients.Add(new EmailAddress("kknecht@gmail.com", "Ken Knecht"));
            recipients.Add(new EmailAddress("kknecht@opeeka.com", "Ken Knecht"));
            recipients.Add(new EmailAddress("kduggento@opeeka.com", "Kerry Duggento"));

            return recipients;
        }

        public List<EmailAddress> GetProgramAdmins(int? programId, int? organizationId)
        {
            var recipients = new List<EmailAddress>(); 
            var persons = _context.Person.Where(p => !p.IsRemoved
                && ((p.OrganizationId == organizationId || p.ProgramId == programId)
                    && p.Role.RoleName == "Admin")
                && p.Email != null
                && p.Email != "");

            foreach (var person in persons)
            {
                //recipients.Add(new EmailAddress(person.Email, person.FullName));
            }

            return recipients;
        }

        private EmailAddress GetEmailAddress(int personId)
        {
            var persons = _context.Person.FirstOrDefault(p => p.PersonId == personId
                && !p.IsRemoved
                && p.Email != null
                && p.Email != "");

            return persons == null ? new EmailAddress() : new EmailAddress(persons.Email, persons.FullName);
        }

        public async Task<Response> SendEmailToAdminUnauthorizedLogin(string email)
        {
            _msg.SetFrom(new EmailAddress("kduggento@gmail.com", "RISE"));
            var recipients = GetSystemAdmins();
            var person = _context.Person.Where(p => p.Email == email).FirstOrDefault();
            int? programId = null;
            int? organizationId = null;

            if (person != null)
            {
                if (person.ProgramId.HasValue)
                {
                    var program = _context.Program.FirstOrDefault(p => p.ProgramId == person.ProgramId);
                    programId = program?.ProgramId;
                    organizationId = program?.OrganizationId;
                }
                else if (person.OrganizationId.HasValue)
                {
                    var organization = _context.Organization.FirstOrDefault(o => o.OrganizationId == person.OrganizationId);
                    organizationId = organization?.OrganizationId;
                }
                recipients.AddRange(GetProgramAdmins(programId, organizationId));
            }

            _msg.AddTos(recipients.ToList());
            _msg.SetSubject("An unauthorized user has signed in");
            _msg.AddContent(MimeType.Html, $"This user has attempted to log in although they have been disabled in the system.  To prevent this from happening, please delete the user's login access.<p>User information</p><p>Email: {email}</p>");
            return await _client.SendEmailAsync(_msg);
        }
    }
}
