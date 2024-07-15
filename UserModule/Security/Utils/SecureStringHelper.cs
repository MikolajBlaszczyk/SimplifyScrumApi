using System.Runtime.InteropServices;
using System.Security;

namespace UserModule.Utils;

internal class SecureStringHelper
{
    internal static string ConvertToUnsecuredString(SecureString secureString)
    {
        IntPtr unmanagedStr = IntPtr.Zero;
        try
        {
            unmanagedStr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(unmanagedStr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedStr);
        }
    }

    internal static SecureString ConvertToSecureString(string notSecuredString)
    {
        var secureString = new SecureString();
        foreach(var character in notSecuredString.ToCharArray()) 
            secureString.AppendChar(character);

        return secureString;
    }
}