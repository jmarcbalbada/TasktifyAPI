using Microsoft.VisualBasic;
using System.ComponentModel;

namespace TasktifyAPI.Enums
{
    public class Enums
    {
        public enum taskStatus
        {
            [Description("In progress")]
            InProgress,
            [Description("Done")]
            Done,
            [Description("Cancelled")]
            Cancelled,
        }
    }
}
