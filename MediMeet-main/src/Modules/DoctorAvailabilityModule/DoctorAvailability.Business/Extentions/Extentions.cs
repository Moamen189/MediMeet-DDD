using DoctorAvailability.Business.DomainModels;
using DoctorAvailability.DataAccess.Entities;

namespace DoctorAvailability.Business.Extentions
{
    public static class Extentions
    {
        public static SlotDto MapToDto(this Slot slotEntity)
        {
            return new SlotDto
            {
                Id = slotEntity.Id,
                Time = slotEntity.Time,
                DoctorId = slotEntity.DoctorId,
                DoctorName = slotEntity.DoctorName,
                IsReserved = slotEntity.IsReserved,
                Cost = slotEntity.Cost
            };
        }
        public static Slot MapToModule(this SlotDto slotEntity)
        {
            return new Slot
            {
                Id = slotEntity.Id,
                Time = slotEntity.Time,
                DoctorId = slotEntity.DoctorId,
                DoctorName = slotEntity.DoctorName,
                IsReserved = slotEntity.IsReserved,
                Cost = slotEntity.Cost
            };
        }
        public static DoctorDto MapToDto(this Doctor doctorEntity)
        {
            return new DomainModels.DoctorDto
            {
                Id = doctorEntity.Id,
                Name = doctorEntity.Name,
            };
        }
    }
}
