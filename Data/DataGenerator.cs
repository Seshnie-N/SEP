using Bogus;
using SEP.Models.DomainModels;
using SEP.Models.Enums;

namespace SEP.Data
{
    public class DataGenerator
    {
        Faker<Post> postFake;
        public DataGenerator(ApplicationDbContext context) 
        {
            var employers = context.Employers.Where(e => e.IsApproved).Select(e => e.UserId).ToList();
            var faculties = context.Faculties.Select(f => f.FacultyId).ToList();
            var departments = context.Departments.Select(d => d.DepartmentId).ToList();
            var partTimeHours = context.partTimeHours.Select(p => p.TimeRange).ToList();
            var statusList = new List<string> {"Pending", "Approved", "Rejected", "Queried" };
            var postStatusList = new List<string> { "Approved", "Withdrawn", "Closed" };

            Randomizer.Seed = new Random(1969);
            postFake = new Faker<Post>()
                .RuleFor(p => p.PostId, f => f.Random.Guid())
                .RuleFor(p => p.EmployerId, f => f.PickRandom(employers))
                .RuleFor(p => p.FacultyName, f => f.PickRandom(faculties))
                .RuleFor(p => p.DepartmentName, f => f.PickRandom(departments))
                .RuleFor(p => p.JobTitle, f => f.Name.JobTitle())
                .RuleFor(p => p.JobDescription, f => f.Name.JobDescriptor())
                .RuleFor(p => p.JobLocation, f => f.Address.FullAddress())
                .RuleFor(p => p.Responsibilities, f => f.Lorem.Sentences(6))
                .RuleFor(p => p.HourlyRate, f => f.Finance.Amount(100, 350, 2))
                .RuleFor(p => p.ApplicationInstruction, f => f.Lorem.Lines())
                .RuleFor(p => p.MinimumRequirement, f => f.Lorem.Text())
                .RuleFor(p => p.ApprovalStatus, f => f.PickRandom(statusList))
                .RuleFor(p => p.IsApproved, (f,p) => p.ApprovalStatus == "Approved" ? true : false)
                .RuleFor(p => p.PostStatus, (f, p) => p.IsApproved ? f.PickRandom(postStatusList) : "Pending")
                .RuleFor(p => p.PostReviewComment, (f, p) => p.ApprovalStatus != "Approved" ? null : f.Lorem.Sentences(2).OrNull(f, 0.2f))
                .RuleFor(p => p.StartDate, f => f.Date.Future())
                .RuleFor(p => p.EndDate, (f, p) => f.Date.Between(p.StartDate, p.StartDate.AddYears(3)))
                .RuleFor(p => p.ApplicationClosingDate, (f, p) => f.Date.Between(DateTime.Now, p.StartDate))
                .RuleFor(p => p.ContactPersonName, f => f.Name.FullName())
                .RuleFor(p => p.ContactPersonEmail, (f, p) => f.Internet.Email(p.ContactPersonName))
                .RuleFor(p => p.ContactPersonNumber, f => f.Phone.PhoneNumber("0#########"))
                .CustomInstantiator(f => new Post
                {
                    JobType = f.PickRandom<JobType>().ToString(),
                })
                .RuleFor(p => p.PartTimeHour, (f,p) => p.JobType == "FullTime" ? null : f.PickRandom(partTimeHours))
                .RuleFor(p => p.LimitedToSA, f => f.Random.Bool())
                .RuleFor(p => p.LimitedTo1stYear, f => f.Random.Bool())
                .RuleFor(p => p.LimitedTo2ndYear, f => f.Random.Bool())
                .RuleFor(p => p.LimitedTo3rdYear, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToHonours, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToGraduate, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToMasters, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToPhd, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToPostdoc, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToDepartment, f => f.Random.Bool())
                .RuleFor(p => p.LimitedToFaculty, f => f.Random.Bool());
        }  

        public IEnumerable<Post> GeneratePosts()
        {
            return postFake.GenerateForever();
        }
    }
}
