using System.Runtime.Serialization;

namespace MealPlannerClient.App.Enums
{
    public enum ProductUnitOfMeasurement
    {
        [EnumMember(Value = "unit")]
        Unit,

        [EnumMember(Value = "g")]
        Gram,

        [EnumMember(Value = "mL")]
        Milliliter
    }
}
