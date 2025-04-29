using System.Runtime.Serialization;

namespace BugTicketingSystem.BL
{
    public enum UserRole
    {
        [EnumMember(Value = "Manager")]
        Manager,
        [EnumMember(Value = "Developer")]
        Developer,
        [EnumMember(Value = "Tester")]
        Tester ,
        [EnumMember(Value = "User")]
        User
    }
}
