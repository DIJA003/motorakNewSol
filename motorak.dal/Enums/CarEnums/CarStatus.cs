using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorak.DAL.Enums.CarEnums
{
    public enum CarStatus
    {
        Available,
        Rented,
        Sold,
        UnderMaintenance,
        Reserved,
        Inactive,
        PendingApproval,
        Rejected,
        Archived
    }
}
