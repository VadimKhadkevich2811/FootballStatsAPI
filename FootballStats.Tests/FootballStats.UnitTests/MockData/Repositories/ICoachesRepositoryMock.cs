using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using FootballStats.Infrastructure.Services;
using Moq;

namespace FootballStats.UnitTests.MockData.Repositories;

public class ICoachesRepositoryMock
{
    public static Mock<ICoachesRepository> GetMock()
    {
        var mock = new Mock<ICoachesRepository>();
        var sortHelper = new SortHelper<Coach>();
        var coaches = new List<Coach>()
        {
            new Coach()
            {
                Id = 1,
                Name = "John",
                Lastname = "Test",
                Age = 20,
                Position = PositionGroup.Forward
            }
        };
        mock.Setup(m => m.GetAllCoachesAsync())
            .ReturnsAsync(() => coaches);
        mock.Setup(m => m.SaveChangesAsync())
            .ReturnsAsync(true);
        mock.Setup(m => m.GetCoachByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => coaches.FirstOrDefault(p => p.Id == id));
        mock.Setup(m => m.AddCoachAsync(It.IsAny<Coach>()))
            .Callback(() => { return; });
        mock.Setup(m => m.UpdateCoach(It.IsAny<Coach>()))
            .Callback(() => { return; });
        mock.Setup(m => m.GetAllCoachesCountAsync())
            .ReturnsAsync(() => coaches.Count);
        mock.Setup(m => m.RemoveCoach(It.IsAny<Coach>()))
            .Callback(() => { return; });
        mock.Setup(m => m.GetAllCoachesAsync(It.IsAny<CoachesQueryStringParams>()))
            .ReturnsAsync((CoachesQueryStringParams parameters) =>
            {
                var filteredCoaches = parameters.Name == null && parameters.LastName == null
                    ? coaches.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                        .Take(parameters.PageSize)
                    : coaches.Where(coach =>
                        (coach.Lastname.ToLower() == parameters.LastName!.ToLower() ||
                            string.IsNullOrEmpty(parameters.LastName)) &&
                        (coach.Name.ToLower() == parameters.Name!.ToLower() ||
                            string.IsNullOrEmpty(parameters.Name)))
                        .Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

                return sortHelper.ApplySort(filteredCoaches.AsQueryable(), parameters.OrderBy!).ToList();
            });

        return mock;
    }
}