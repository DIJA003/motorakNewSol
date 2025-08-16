
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


        public void UpdateMechanicInfo(string name,string newWorkHours,MechanicStatus newStatus)
        {
            User.Name = name;
            WorkHours = newWorkHours;
            Status = newStatus;
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }

        public Mechanic()
        {
            CreatedAt = DateTime.Now;
            Status = MechanicStatus.Free;
            WorkHours = "1";
            IsDeleted = false;
            IsUpdated = false;

        }
    }
}
