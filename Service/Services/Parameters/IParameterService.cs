using Repository.Entities;

namespace Service.Services.Parameters
{
    public interface IParameterService
    {
        List<Parameter> GetAll();
        Parameter GetById(int id);
        Parameter Create(Parameter parameter);
        Parameter? Update(Parameter parameter);
        void Delete(int id);
        bool VerifyDateRange(DateOnly? start, DateOnly? end, int id);
    }
}
