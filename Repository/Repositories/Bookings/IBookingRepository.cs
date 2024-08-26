using Repository.Entities;

namespace Repository.Repositories.Bookings
{
    public interface IBookingRepository
    {
        List<Booking> GetAll();
        Booking GetById(int id);
        Booking Create(Booking booking);
        Booking Update(Booking booking);
        void Delete(int id);
        List<Booking> GetByLicensePlate(string licensePlate);
        Booking GetByLicensePlateActive(string licensePlate);
        int GetInitialValueWithDateTime(DateTime dateTime);
        int GetIncrementalValueWithDateTime(DateTime dateTime);
    }
}
