using DoctorAvailability.Business.DomainModels;

namespace DoctorAvailability.Business.Extensions
{
	public static class Extensions
	{
		// These methods don't make sense as they're just returning the same object
		// Assuming we need to map between entity and DTO, we should have different types
		// For now, I'll keep them but rename to make their purpose clearer
		public static SlotDto Clone(this SlotDto slotDto)
		{
			return new SlotDto
			{
				Id = slotDto.Id,
				Time = slotDto.Time,
				DoctorId = slotDto.DoctorId,
				DoctorName = slotDto.DoctorName,
				IsReserved = slotDto.IsReserved,
				Cost = slotDto.Cost
			};
		}
		
		// This method should be removed or properly implemented with different types
		// Keeping it for now with a better name
		public static SlotDto ToViewModel(this SlotDto slotDto)
		{
			return new SlotDto
			{
				Id = slotDto.Id,
				Time = slotDto.Time,
				DoctorId = slotDto.DoctorId,
				DoctorName = slotDto.DoctorName,
				IsReserved = slotDto.IsReserved,
				Cost = slotDto.Cost
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
