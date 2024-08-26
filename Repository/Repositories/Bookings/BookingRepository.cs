using Repository.Database;
using Repository.Entities;

namespace Repository.Repositories.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public BookingRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public Booking Create(Booking booking)
        {
            _dataBaseContext.Bookings.Add(booking);
            _dataBaseContext.SaveChanges();

            return booking;
        }

        public void Delete(int id)
        {
            var booking = _dataBaseContext.Bookings
                .Where(x => x.Id == id)
                .FirstOrDefault();

            _dataBaseContext.Bookings.Remove(booking);
            _dataBaseContext.SaveChanges();
        }

        public List<Booking> GetAll() =>
           _dataBaseContext.Bookings
                 .OrderByDescending(x => x.Id)
                 .ToList();

        public Booking GetById(int id) =>
            _dataBaseContext.Bookings
                .Where(x => x.Id == id)
                .FirstOrDefault();

        public Booking Update(Booking booking)
        {
            var bookingDataBase = _dataBaseContext.Bookings
                .Where(x => x.Id == booking.Id)
                .FirstOrDefault();

            bookingDataBase.LicensePlate = booking.LicensePlate;
            bookingDataBase.StartDate = booking.StartDate;
            bookingDataBase.EndDate = booking.EndDate;

            _dataBaseContext.Update(bookingDataBase);
            _dataBaseContext.SaveChanges();

            return bookingDataBase;
        }

        public List<Booking> GetByLicensePlate(string licensePlate) =>
           _dataBaseContext.Bookings
                 .Where(x => x.LicensePlate == licensePlate)
                 .OrderByDescending(x => x.Id)
                 .ToList();

        public Booking GetByLicensePlateActive(string licensePlate) =>
            _dataBaseContext.Bookings
                .Where(x => x.LicensePlate == licensePlate && x.EndDate == null)
                .FirstOrDefault();

        public int GetInitialValueWithDateTime(DateTime dateTime)
        {
            var dateOnly = DateOnly.FromDateTime(dateTime);

            var parameter = _dataBaseContext.Parameters
                                .Where(x => x.StartDate <= dateOnly && x.EndDate >= dateOnly)
                                .FirstOrDefault();

            return parameter?.InitialValue ?? 0;
        }

        public int GetIncrementalValueWithDateTime(DateTime dateTime)
        {
            var dateOnly = DateOnly.FromDateTime(dateTime);

            var parameter = _dataBaseContext.Parameters
                                .Where(x => x.StartDate <= dateOnly && x.EndDate >= dateOnly)
                                .FirstOrDefault();

            return parameter?.IncrementalValue ?? 0;
        }

    }
}
