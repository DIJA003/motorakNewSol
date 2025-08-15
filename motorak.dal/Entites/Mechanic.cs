using Motorak.DAL.Entites;
using Motorak.DAL.Enums.MechaincEnums;

namespace motorak.dal.Entites
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string WorkHours { get; set; }
        public decimal Rating { get; set; } = 0;
        public MechanicStatus Status { get; set; } = MechanicStatus.Free;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsUpdated { get; set; } = false;

        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Service>? Services { get; set; } = new List<Service>();

        public void UpdateWorkHours(string newWorkHours)
        {
            WorkHours = newWorkHours;
            Update();
        }

        public void UpdateRating(decimal newRating)
        {
            Rating = newRating;
            Update();
        }

        public void UpdateStatus(MechanicStatus newStatus)
        {
            Status = newStatus;
            Update();
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }

        private void Update()
        {
            IsUpdated = true;
            UpdatedAt = DateTime.Now;
        }
    }
}