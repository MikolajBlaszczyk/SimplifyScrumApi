using OpenConnectionManagement.Utils;

namespace OpenConnectionManagement.ConnectionManagement;

public static class UidProducer
{
    public static string Produce(HubType type, string userGuid)
    {
        return type + "_" + userGuid;
    }
    
    
}