using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GM.CommonLibs.Common
{
    public class Generator
    {
        public string Encrypt(string strText, string strEncrKey = "&%#@?,:*")   
        {
            byte[] IV = new byte[] {18, 52, 86, 120, 144, 171, 205, 239 };  

            try 
            {
                var byKey = Encoding.UTF8.GetBytes(strEncrKey);     

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();   
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);  
                MemoryStream ms = new MemoryStream();  
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);  
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();   
                return Convert.ToBase64String(ms.ToArray());            
            }
            catch (Exception ex)   
            {
                return ex.Message;  
            }
        }

        public string Decrypt(string strText, string strDecrKey = "&%#@?,:*")  
        {
            byte[] IV = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };  

            try 
            {
                var byKey = Encoding.UTF8.GetBytes(strDecrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                var inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}