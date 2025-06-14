using DoctorAvailability.Business.DomainModels;

namespace DoctorAvailability.Business.Extentions
{
	public static class Extentions
	{
		public static SlotDto MapToDto(this SlotDto slotEntity)
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
		public static SlotDto MapToModule(this SlotDto slotEntity)
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
		public static DoctorDto MapToDto(this Doctor doctorEntity)
		{
			return new DoctorDto
			{
				Id = doctorEntity.Id.Value,
				Name = doctorEntity.Name.Value,
			};
		}
	}
}
