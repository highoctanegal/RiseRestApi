using Microsoft.EntityFrameworkCore;
using RiseRestApi.Models;

namespace RiseRestApi.Repository
{
    interface IRiseContext
    {
         DbSet<Address> Address { get; set; }
         DbSet<Assessment> Assessment { get; set; }
         DbSet<AssessmentDetail> AssessmentDetail { get; set; }
         DbSet<AssessmentResponse> AssessmentResponse { get; set; }
         DbSet<AssessmentResponseDetail> AssessmentResponseDetail { get; set; }
         DbSet<AreaAuthorization> AreaAuthorization { get; set; }
         DbSet<AreaAuthorizationDetail> AreaAuthorizationDetail { get; set; }
         DbSet<Note> Note { get; set; }
         DbSet<NoteDetail> NoteDetail { get; set; }
         DbSet<Organization> Organization { get; set; }
         DbSet<Person> Person { get; set; }
         DbSet<PersonAssessmentDetail> PersonAssessmentDetail { get; set; }
         DbSet<PersonDetail> PersonDetail { get; set; }
         DbSet<PersonGrid> PersonGrid { get; set; }
         DbSet<RiseProgram> Program { get; set; }
         DbSet<RiseProgramDetail> ProgramDetail { get; set; }
         DbSet<RiseProgramGrid> ProgramGrid { get; set; }
         DbSet<Question> Question { get; set; }
         DbSet<Rating> Rating { get; set; }
         DbSet<Role> Role { get; set; }
         DbSet<SkillSet> SkillSet { get; set; }
         DbSet<OrganizationDetail> OrganizationDetail { get; set; }
         DbSet<OrganizationGrid> OrganizationGrid { get; set; }
         DbSet<Survey> Survey { get; set; }
         DbSet<SurveyQuestion> SurveyQuestion { get; set; }
         DbSet<UsState> UsState { get; set; }
         DbSet<Voice> Voice { get; set; }
         DbSet<PersonAssessmentChart> PersonAssessmentChart { get; set; }
         DbSet<SkillSetPercentageChart> SkillSetPercentageChart { get; set; }
         DbSet<CoachFirstLatestScoreChart> CoachCoachFirstLatestScoreChart { get; set; }
    }
}
