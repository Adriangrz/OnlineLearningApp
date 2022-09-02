using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Repositories;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly UnitOfWork _unitOfWork;

        public TeamService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
