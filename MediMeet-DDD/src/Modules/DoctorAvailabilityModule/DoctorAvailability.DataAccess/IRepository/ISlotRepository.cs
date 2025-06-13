using DoctorAvailability.DataAccess.Entities;

namespace DoctorAvailability.DataAccess.IRepository;

public interface ISlotRepository
{
    List<Slot> GetAll();
    List<Slot> GetAllAvailable();
    bool DeleteById(Guid slotId);
    void Create(Slot slot);
    void Update(Slot slot);
    Slot GetById(Guid slotId);
}