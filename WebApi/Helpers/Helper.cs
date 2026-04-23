using System.Collections.Generic;

namespace WebApi.Helpers
{
    public static class Helper
    {
        public static IDictionary<string, object> TempData { get; } = new Dictionary<string, object>();

        public static void ShowMessage(string messageType, string messageBody)
        {
            
            // This method can be used to set TempData for messages
            // Example usage:
            TempData["MessageType"] = messageType;
            TempData["MessageBody"] = messageBody;
        }
    }
}
