using CommonTools.Lib11.GoogleTools;
using CommonTools.Lib11.StringTools;
using CommonTools.Lib45.Cryptography;
using CommonTools.Lib11.JsonTools;
using System;
using System.Management;

namespace CommonTools.Lib45.LicenseTools
{
    public class SeatLicenser
    {
        //add reference: Assemblies > System.Management
        //http://www.nullskull.com/articles/20030511.asp
        private static string GetCPUId()
        {
            string cpuInfo = String.Empty;
            string temp = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == String.Empty)
                {// only return cpuInfo from first CPU
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }


        private static string GeneratePrivateKey()
        {
            var s1 = GetCPUId();
            var s2 = typeof(FirebaseCredentials).Name;
            var s3 = Environment.UserName;
            return $"{s1}{s2}{s3}".SHA1ForUTF8();
        }


        public static string GeneratePublicKey<T>(T credentials) 
            where T : FirebaseCredentials
            => AESThenHMAC.SimpleEncryptWithPassword
                (credentials.ToJson(), GeneratePrivateKey());


        public static bool TryGetCredentials<T>(string publicKey, out T credentials, out string errorDesc)
            where T : FirebaseCredentials
        {
            credentials = null;
            string json = null;
            try
            {
                json = AESThenHMAC.SimpleDecryptWithPassword
                    (publicKey, GeneratePrivateKey());
            }
            catch
            {
                errorDesc = "Invalid key";
                return false;
            }
            try
            {
                credentials = json.ReadJson<T>();
            }
            catch
            {
                errorDesc = "Parse error";
                return false;
            }
            errorDesc = "";
            return true;
        }
    }
}
