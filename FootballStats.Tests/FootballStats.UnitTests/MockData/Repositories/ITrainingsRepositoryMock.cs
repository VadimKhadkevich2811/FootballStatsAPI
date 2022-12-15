using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Infrastructure.Services;
using Moq;

namespace FootballStats.UnitTests.MockData.Repositories;

public class ITrainingsRepositoryMock
{
    public static Mock<ITrainingsRepository> GetMock()
    {
        var mock = new Mock<ITrainingsRepository>();
        var sortHelper = new SortHelper<Training>();
        var trainings = new List<Training>()
        {
            new Training()
            {
                Id = 1,
                Name = "John",
                CoachId = 1
            }
        };
        mock.Setup(m => m.GetAllTrainingsAsync())
            .ReturnsAsync(() => trainings);
        mock.Setup(m => m.SaveChangesAsync())
            .ReturnsAsync(true);
        mock.Setup(m => m.GetTrainingByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => trainings.FirstOrDefault(p => p.Id == id));
        mock.Setup(m => m.AddTrainingAsync(It.IsAny<Training>(), It.IsAny<IEnumerable<int>>()))
            .Callback(() => { return; });
        mock.Setup(m => m.UpdateTrainingAsync(It.IsAny<Training>(), It.IsAny<IEnumerable<int>>()))
            .Callback(() => { return; });
        mock.Setup(m => m.GetAllTrainingsCountAsync())
            .ReturnsAsync(() => trainings.Count);
        mock.Setup(m => m.RemoveTraining(It.IsAny<Training>()))
            .Callback(() => { return; });
        mock.Setup(m => m.GetAllTrainingsAsync(It.IsAny<TrainingsQueryStringParams>()))
            .ReturnsAsync((TrainingsQueryStringParams parameters) =>
            {
                var filteredTrainings = parameters.Name == null
                    ? trainings.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                        .Take(parameters.PageSize)
                    : trainings.Where(training =>
                        (training.Name.ToLower() == parameters.Name!.ToLower() ||
                            string.IsNullOrEmpty(parameters.Name)))
                        .Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);

                return sortHelper.ApplySort(filteredTrainings.AsQueryable(), parameters.OrderBy!).ToList();
            });

        return mock;
    }
}