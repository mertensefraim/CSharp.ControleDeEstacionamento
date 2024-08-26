using Repository.Database;
using Repository.Entities;

namespace Repository.Repositories.Parameters
{
    public class ParameterRepository : IParameterRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public ParameterRepository(DataBaseContext context)
        {
            _dataBaseContext = context;
        }

        public Parameter Create(Parameter parameter)
        {
            _dataBaseContext.Parameters.Add(parameter);
            _dataBaseContext.SaveChanges();

            return parameter;
        }

        public void Delete(int id)
        {
            var parametro = _dataBaseContext.Parameters
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _dataBaseContext.Parameters.Remove(parametro);
            _dataBaseContext.SaveChanges();
        }

        public List<Parameter> GetAll() =>
            _dataBaseContext.Parameters
                .ToList();

        public Parameter GetById(int id) =>
            _dataBaseContext.Parameters
                .Where(x => x.Id == id)
                .FirstOrDefault();


        public Parameter Update(Parameter parameter)
        {
            _dataBaseContext.Update(parameter);
            _dataBaseContext.SaveChanges();

            return parameter;
        }
        
        public bool VerifyDateRange(DateOnly? start, DateOnly? end, int id)
        {
            bool exists = _dataBaseContext.Parameters
                            .Any(x => ((start >= x.StartDate && start <= x.EndDate) ||
                                      (end >= x.StartDate && end <= x.EndDate) ||
                                      (start <= x.StartDate && end >= x.EndDate)) && 
                                      x.Id != id);

            return exists;
        }
    }
}
