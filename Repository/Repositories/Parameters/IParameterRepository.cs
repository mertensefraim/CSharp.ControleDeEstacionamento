using Repository.Entities;

namespace Repository.Repositories.Parameters
{
    public interface IParameterRepository
    {
        List<Parameter> GetAll();
        Parameter GetById(int id);
        Parameter Create(Parameter parameter);
        Parameter Update(Parameter parameter);
        void Delete(int id);
        bool VerifyDateRange(DateOnly? start, DateOnly? end, int id);
    }
}
