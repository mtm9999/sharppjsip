
namespace PJLib
{
    /// <summary>
    /// defaults: HasTime, HasMicroSeconds, HasSender, HasNewLine, HasSpace
    /// </summary>
    public enum PjLogDecoration
    {
        HasDayName = 1,
        HasYear = 2,
        HasMonth = 4,
        HasDayOfMonth = 8,
        HasTime = 16,
        HasMicroSec = 32,
        HasSender = 64,
        HasNewLine = 128,
        HasCR = 256,
        HasSpace = 512,
        HasColor = 1024,
        HasLevelText = 2048,
        HasThreadID = 4096
    }
}
