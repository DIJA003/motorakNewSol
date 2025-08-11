
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.MechaincEnums;

namespace motorak.dal.Entites
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string WorkHours { get; private set; }
        public decimal Rating { get; private set; }
        public MechanicStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsUpdated { get; private set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Service>? Services { get; set; }= new List<Service>();


        public void UpdateWorkHours(string newWorkHours)
        {
            WorkHours = newWorkHours;
        }
        public void UpdateRating(decimal newRating)
        {
            Rating = newRating;
        }
        public void UpdateStatus(MechanicStatus newStatus)
        {
            Status = newStatus;
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}
