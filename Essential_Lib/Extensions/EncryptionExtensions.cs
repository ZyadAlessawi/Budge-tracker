//using Microsoft.AppCenter.Crashes;
using Microsoft.Maui.Storage;
using SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Essential_Lib.Extensions
{
    public static class EncryptionExtensions
    {
        public static readonly string cacheDir = FileSystem.Current.CacheDirectory;
        public class SecureKeyValuesObject
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Key { get; set; }
            public string? Value { get; set; }
        }
        public static async Task<T?> DecryptObjectsFromSecureStorage_Aes<T>(this string SecureStorageKey)
        {
            // Check arguments.
            if (SecureStorageKey == null)
                return default;
            //var encryptedobj =  await SecureStorage.GetAsync(SecureStorageKey); 
            //var encryptedobj = await LocalDatabaseAPIs.GetFirstItemAsync<SecureKeyValuesObject>(a => a.Key == SecureStorageKey); 
            var filePath = FileSystem.AppDataDirectory.CreatePath(["EncryptedFiles"], SecureStorageKey + ".dat", false);
            byte[]? encryptedData = File.Exists(filePath) ? await File.ReadAllBytesAsync(filePath) : null;
            if (encryptedData == null)
                return default;
            var encryptedobjKey = await SecureStorage.GetAsync(SecureStorageKey + "Key");
            var encryptedobjIV = await SecureStorage.GetAsync(SecureStorageKey + "IV");

            //var seializedencryption =  encryptedobj.Value.Deserialize<byte[]>();
            var seializedencryptionKey = encryptedobjKey.Deserialize<byte[]>();
            var seializedencryptionIV = encryptedobjIV.Deserialize<byte[]>();

            try
            {
                var orginal = await DecryptStringFromBytes_Aes(encryptedData, seializedencryptionKey, seializedencryptionIV);
                var orginalObj = orginal.Deserialize<T>();
                return orginalObj;

            }
            catch (Exception ex)
            {
               // ex.DisplayExecptionAlert();

                return default;
            }



        }
        public static async Task<bool> EncryptObjectsAndSaveToSecureStorage_Aes(this object? obj, string SecureStorageKey)
        {
            // Check arguments.
            if (obj == null)
                return false;
            try
            {
                var original = obj.Serialize();

                using Aes myAes = Aes.Create();

                byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
                //var seializedencryption = encrypted.Serialize();
                var seializedencryptionKey = myAes.Key.Serialize();
                var seializedencryptionIV = myAes.IV.Serialize();
                //SecureKeyValuesObject secureObject = new()
                //{
                //    Key = SecureStorageKey,
                //    Value = seializedencryption
                //};
                //var check = await LocalDatabaseAPIs.GetFirstItemAsync<SecureKeyValuesObject>(a => a.Key == SecureStorageKey);
                //if (check != null)
                //{
                //    secureObject.Id = check.Id;

                //    await secureObject.UpdateItemAsync();
                //}
                //else  
                //    await secureObject.AddItemAsync();

                //await SecureStorage.SetAsync(SecureStorageKey, seializedencryption);
                var filePath = FileSystem.AppDataDirectory.CreatePath(["EncryptedFiles"], SecureStorageKey + ".dat", false);

                await File.WriteAllBytesAsync(filePath, encrypted);
                await SecureStorage.SetAsync(SecureStorageKey + "Key", seializedencryptionKey);
                await SecureStorage.SetAsync(SecureStorageKey + "IV", seializedencryptionIV);
                return true;
            }
            catch (Exception ex)
            {
               // ex.DisplayExecptionAlert();
                return false;
            }


        }

        public static byte[] EncryptStringToBytes_Aes(this string? plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                return [];
            if (Key == null || Key.Length <= 0)
                return [];
            if (IV == null || IV.Length <= 0)
                return [];


            byte[] encrypted = [];

            // Create an Aes object
            // with the specified key and IV.
            try
            {

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // aesAlg.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                using ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);


                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
                //csEncrypt.FlushFinalBlock();
            }
            catch (Exception ex)
            {
               // ex.DisplayExecptionAlert();
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static async Task<string> DecryptStringFromBytes_Aes(byte[]? cipherText, byte[]? Key, byte[]? IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0 || Key == null || Key.Length <= 0 || IV == null || IV.Length <= 0)
                return string.Empty;

            // Declare the string used to hold
            // the decrypted text.
            // string plaintext = string.Empty;

            // Create an Aes object
            // with the specified key and IV
            try
            {

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                //aesAlg.Padding = PaddingMode.PKCS7;
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using MemoryStream msDecrypt = new(cipherText);
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                //byte[] decryptedData = new byte[cipherText.Length];
                //csDecrypt.Read(decryptedData, 0, decryptedData.Length);
                //return Convert.ToBase64String(decryptedData);
                using (StreamReader srDecrypt = new(csDecrypt))
                {

                    return await srDecrypt.ReadToEndAsync();
                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                }



                //csDecrypt.FlushFinalBlock();

            }
            catch (Exception ex)
            {
                //ex.DisplayExecptionAlert();
            }
            return string.Empty;
        }


        public static string ComputeHash(this string?[]? inputs)
        {
            inputs = inputs?.Where(a => a != null)?.ToArray();

            if (inputs == null)
                return string.Empty;
            // Sort the inputs 
            Array.Sort(inputs);

            // Combine the sorted strings
            string combinedInput = string.Join("", inputs);

            // Convert the combined string to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(combinedInput);

            // Compute the hash
            byte[] hashBytes = SHA256.HashData(bytes);

            // Convert the hash bytes to a hexadecimal string
            StringBuilder hashString = new();
            foreach (byte b in hashBytes)
            {
                hashString.Append(b.ToString("x2"));
            }

            return hashString.ToString();
        }
        public static string CreatePath(this string RootPath, string[]? Folders = null, string? FileName = null, bool DeleteOldfile = true)
        {
            var fullpath = Path.Combine(RootPath ?? cacheDir);
            if (Folders != null && Folders.Length > 0)
            {
                foreach (var folder in Folders)
                {
                    fullpath = Path.Combine(fullpath, folder);
                }
            }
            if (!Directory.Exists(fullpath))
                Directory.CreateDirectory(fullpath);
            if (FileName != null)
                fullpath = Path.Combine(fullpath, FileName);
            if (File.Exists(fullpath) && DeleteOldfile)
                File.Delete(fullpath);
            return fullpath;
        }
        //public static async void DisplayExecptionAlert(this Exception ex, bool ReportError = true, string? ExtraInfo = null)
        //{
        //    try
        //    {
        //        string Title = "SystemError".Localize() + ": " + ex.Source;
        //        if (Application.Current?.MainPage != null)
        //        {

        //            if (ReportError)
        //            {
        //                try
        //                {
        //                    var result = await Application.Current.MainPage.DisplayAlert(Title, ex.Message + (ExtraInfo == null ? "" : $"\n\n{ExtraInfo}"), "Reportanerror".Localize(), "ok".Localize());
        //                    if (result)
        //                    {
        //                        await Clipboard.Default.SetTextAsync(Title + "\n\n" + ex.Message);
        //                        Crashes.TrackError(ex);

        //                    }
        //                }
        //                catch (Exception)
        //                {

        //                }

        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert(Title, ex.Message + (ExtraInfo == null ? "" : $"\n\n{ExtraInfo}"), "ok".Localize());
        //            }
        //        }
        //        //else
        //        //    await Clipboard.Default.SetTextAsync(Title + "\n\n" + ex.Message);

        //    }
        //    catch (Exception) { return; }


        //}
    }
}
