using Bogus;
using SEP.Models.DomainModels;
using SEP.Models.Enums;

namespace SEP.Data
{
    public class DataGenerator
    {
        Faker<Post> postFake;
        public DataGenerator() 
        {
            Randomizer.Seed = new Random(1719);
            //.RuleFor(p => p.EmployerId, f => f.Random.Guid())
            postFake = new Faker<Post>()
                .RuleFor(p => p.PostId, f => f.Random.Guid())
                .RuleFor(p => p.JobTitle, f => f.Name.JobTitle())
                .RuleFor(p => p.JobDescription, f => f.Name.JobDescriptor())
                .RuleFor(p => p.JobLocation, f => f.Address.FullAddress())
                .RuleFor(p => p.Responsibilities, f => f.Lorem.Sentences())
                .RuleFor(p => p.ApplicationClosingDate, f => f.Date.Future())
                .RuleFor(p => p.HourlyRate, f => f.Finance.Amount(0, 50000, 2))
                .RuleFor(p => p.IsApproved, f => f.Random.Bool())
                .CustomInstantiator(f => new Post
                {
                    JobType = f.PickRandom<JobType>().ToString(),
                    StartDate = f.Date.Future(),
                    EndDate = f.Date.Between(f.Date.Future(), f.Date.Soon())
                })
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

        public Post GeneratePost()
        {
            return postFake.Generate();
        }
    }
}
