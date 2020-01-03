using System.Runtime.Serialization;

namespace PdksPersistence.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "firstDegree")]
        FirstDegree = 1,
        [EnumMember(Value = "secondDegree")]
        SecondDegree = 2,
        [EnumMember(Value = "thirdDegree")]
        ThirdDegree = 3,
        [EnumMember(Value = "admin")]
        Admin = 4
    }
}
