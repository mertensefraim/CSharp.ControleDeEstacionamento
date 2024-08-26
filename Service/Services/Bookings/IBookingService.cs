using Repository.Entities;

namespace Service.Services.Bookings
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        Booking GetById(int id);
        Booking Create(Booking booking);
        Booking Update(Booking booking);
        void Delete(int id);
        void Finalize(string licensePlate);
        int GetInitialValueWithDateTime(DateTime dateTime);
        int GetIncrementalValueWithDateTime(DateTime dateTime);
        string? GetTime(Booking booking);
        double GetPrice(Booking booking);
        TimeSpan? GetChargedHours(Booking booking);
        Booking GetByLicensePlateActive(string licensePlate);
        List<Booking> GetByLicensePlate(string licensePlate);
    }
}
