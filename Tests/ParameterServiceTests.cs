using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Repository.Entities;
using Repository.Repositories.Parameters;
using Service.Services.Parameters;
using FluentAssertions;
using Repository.Database;


namespace Tests
{
    public class ParameterServiceTests
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IParameterService _parameterService;

        public ParameterServiceTests()
        {
            _parameterRepository = Substitute.For<IParameterRepository>();
            _parameterService = new ParameterService(_parameterRepository);
        }

        [Fact]
        public void Test_Delete()
        {
            var id = 2;

            _parameterService.Delete(id);

            _parameterRepository
                .Received()
                .Delete(Arg.Is(2));
        }

        [Fact]
        public void Test_Create()
        {
            var parameter = new Parameter
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                IncrementalValue = 4,
                InitialValue = 5
            };

            _parameterService.Create(parameter);

            _parameterRepository.Received(1).Create(Arg.Is(parameter));
        }

        [Fact]
        public void Test_Update_With_Parameter_Found()
        {
            var parameter = new Parameter
            {
                Id = 2,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                IncrementalValue = 4,
                InitialValue = 5
            };

            var parameterToEdit = new Parameter
            {
                Id = 2,
                StartDate = DateOnly.FromDateTime(Convert.ToDateTime("12/03/2024")),
                EndDate = DateOnly.FromDateTime(Convert.ToDateTime("15/03/2024")),
                IncrementalValue = 4,
                InitialValue = 5
            };

            _parameterRepository.GetById(Arg.Is(parameter.Id)).Returns(parameterToEdit);

            _parameterService.Update(parameter);

            _parameterRepository
                .Received(1)
                .Update(Arg.Is(parameter));
        }

        [Fact]
        public void Test_Update_With_Parameter_Not_Found()
        {
            var parameter = new Parameter
            {
                Id = 2,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                IncrementalValue = 4,
                InitialValue = 5
            };

            _parameterRepository.GetById(Arg.Is(parameter.Id)).ReturnsNull();

            _parameterService.Update(parameter);

            _parameterRepository
                .DidNotReceive()
                .Update(Arg.Any<Parameter>());
        }

        [Fact]
        public void Test_GetById_With_Parameter_Found()
        {
            var id = 5;

            var expectedParameter = new Parameter
            {
                Id = id,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                IncrementalValue = 4,
                InitialValue = 5
            };

            _parameterRepository.GetById(Arg.Is(id))
                .Returns(expectedParameter);

            var parameter = _parameterService.GetById(id);

            parameter.StartDate.Should().Be(expectedParameter.StartDate);
            parameter.EndDate.Should().Be(expectedParameter.EndDate);
            parameter.IncrementalValue.Should().Be(expectedParameter.IncrementalValue);
            parameter.InitialValue.Should().Be(expectedParameter.InitialValue);
        }

        [Fact]
        public void Test_GetById_With_Parameter_Not_Found()
        {
            var id = 5;

            _parameterRepository.GetById(Arg.Is(5)).ReturnsNull();

            var parameter = _parameterService.GetById(id);

            parameter.Should().BeNull();

            _parameterRepository.Received(1).GetById(Arg.Is(5));
        }

        [Fact]
        public void VerifyDateRange_ShouldReturnTrue_WhenOverlappingDatesExist()
        {
            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    Id = 1,
                    StartDate = new DateOnly(2024, 08, 01),
                    EndDate = new DateOnly(2024, 08, 31)
                }
            };

            _parameterRepository.GetAll().Returns(parameters);

            var start = new DateOnly(2024, 08, 15);
            var end = new DateOnly(2024, 08, 20);
            var id = 2; 

            var result = _parameterService.VerifyDateRange(start, end, id);

            Assert.True(result);
        }

        [Fact]
        public void VerifyDateRange_ShouldReturnFalse_WhenNoOverlappingDatesExist()
        {
            var parameters = new List<Parameter>
            {
                new Parameter 
                { 
                    Id = 1, 
                    StartDate = new DateOnly(2024, 08, 01), 
                    EndDate = new DateOnly(2024, 08, 31) 
                }
            }.AsQueryable();

            var start = new DateOnly(2024, 09, 01);
            var end = new DateOnly(2024, 09, 30);
            int id = 2; 

            bool result = _parameterService.VerifyDateRange(start, end, id);

            Assert.False(result);
        }

        [Fact]
        public void VerifyDateRange_ShouldReturnFalse_WhenSameIdIsProvided()
        {
            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    Id = 1,
                    StartDate = new DateOnly(2024, 08, 01),
                    EndDate = new DateOnly(2024, 08, 31)
                }
            }.AsQueryable();

            var start = new DateOnly(2024, 08, 01);
            var end = new DateOnly(2024, 08, 31);
            int id = 1;

            bool result = _parameterService.VerifyDateRange(start, end, id);

            Assert.False(result);
        }

    }
}