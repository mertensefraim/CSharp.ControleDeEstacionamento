using Repository.Database;
using Repository.Entities;
using Repository.Repositories.Bookings;

namespace Service.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Booking Create(Booking booking)
        {
            booking.InitialChargedValue = GetInitialValueWithDateTime(DateTime.Now);
            booking.IncrementedChargedValue = GetIncrementalValueWithDateTime(DateTime.Now);

            _bookingRepository.Create(booking);

            return booking;
        }

        public void Delete(int id) =>
           _bookingRepository.Delete(id);

        public void Finalize(string licensePlate)
        {
            var booking = _bookingRepository.GetByLicensePlateActive(licensePlate);

            booking.EndDate = DateTime.Now;
            booking.Price = GetPrice(booking);

            _bookingRepository.Update(booking);
        }

        public List<Booking> GetAll() =>
           _bookingRepository.GetAll();

        public List<Booking> GetByLicensePlate(string licensePlate) => 
            _bookingRepository.GetByLicensePlate(licensePlate);

        public Booking GetById(int id) =>
            _bookingRepository.GetById(id);

        public Booking GetByLicensePlateActive(string licensePlate) =>
            _bookingRepository.GetByLicensePlateActive(licensePlate);

        public TimeSpan? GetChargedHours(Booking booking)
        {
            if (!booking.EndDate.HasValue)
                return null;

            var timeDifference = booking.EndDate.Value - booking.StartDate;

            var hours = (int)timeDifference.TotalMinutes / 60;
            var brokenHours = timeDifference.TotalMinutes % 60;

            int totalMinutes = 0;

            if (hours == 0 && brokenHours <= 30)
                totalMinutes = 30;

            else
            {
                if (hours != 0)
                {
                    for (var i = 0; i < hours; i++)
                        totalMinutes += 60;

                    if (brokenHours > 10)
                        totalMinutes += 60;
                }
                else 
                    totalMinutes = 60;
            }

            return TimeSpan.FromMinutes(totalMinutes);
        }

        public int GetInitialValueWithDateTime(DateTime dateTime) =>      
            _bookingRepository.GetInitialValueWithDateTime(dateTime);


        public int GetIncrementalValueWithDateTime(DateTime dateTime) =>
            _bookingRepository.GetIncrementalValueWithDateTime(dateTime);

        public double GetPrice(Booking booking)
        {
            if (!booking.EndDate.HasValue)
                return 0;

            var timeDifference = booking.EndDate.Value - booking.StartDate;

            var initialValue = GetInitialValueWithDateTime(booking.StartDate);
            var incrementalValue = GetIncrementalValueWithDateTime(booking.StartDate);

            var hours = (int)timeDifference.TotalMinutes / 60;
            var brokenHours = timeDifference.TotalMinutes % 60;

            double totalCharge = 0;

            if (hours == 0)
            {
                if (brokenHours <= 30) 
                    totalCharge = initialValue / 2;
                else
                    totalCharge = initialValue;
            }

            for (var i = 0; i < hours; i++)
            {
                if (i == 0)
                    totalCharge += initialValue;
                else
                    totalCharge += incrementalValue;
            }

            if (brokenHours > 10 && hours != 0)
                totalCharge += incrementalValue;

            return totalCharge;
        }

        public string? GetTime(Booking booking)
        {
            if (booking.EndDate.HasValue)
            {
                TimeSpan timeDifference = booking.EndDate.Value - booking.StartDate;

                var totalHours = (int)(timeDifference.TotalHours);
                var minutes = timeDifference.Minutes;
                var seconds = timeDifference.Seconds;

                string formattedTimeDifference = $"{totalHours:D2}:{minutes:D2}:{seconds:D2}";

                return formattedTimeDifference;
            }
            else
                return null;
        }

        public Booking Update(Booking booking) =>        
            _bookingRepository.Update(booking);

    }
}
