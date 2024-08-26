using Repository.Database;
using Repository.Entities;
using Repository.Repositories.Parameters;

namespace Service.Services.Parameters
{
    public class ParameterService : IParameterService
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterService(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        public Parameter Create(Parameter parameter) =>
            _parameterRepository.Create(parameter);

        public void Delete(int id) =>
            _parameterRepository.Delete(id);

        public List<Parameter> GetAll() =>
            _parameterRepository.GetAll();

        public Parameter GetById(int id) =>
            _parameterRepository.GetById(id);

        public Parameter? Update(Parameter parameter)
        {
            var parameterDataBase = _parameterRepository.GetById(parameter.Id);

            if (parameterDataBase == null)
                return null;

            parameterDataBase.InitialValue = parameter.InitialValue;
            parameterDataBase.IncrementalValue = parameter.IncrementalValue;
            parameterDataBase.StartDate = parameter.StartDate;
            parameterDataBase.EndDate = parameter.EndDate;

            _parameterRepository.Update(parameter);

            return parameter;
        }

        public bool VerifyDateRange(DateOnly? start, DateOnly? end, int id) =>
            _parameterRepository.VerifyDateRange(start, end, id);
    }
}
