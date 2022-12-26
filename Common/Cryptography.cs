using System;
using System.Security.Cryptography;
using System.Text;

namespace GM.CommonLibs.Common
{

    public class Cryptography
    {
        private READONLY string _securityKey;   

        public Cryptography()  
        {
            _securityKey = "p@ssw0rd";  
        }  

        public string Encrypt(string plaintext, bool useHashing)   
        {
            byte[] keyArray;  
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(plaintext);  

            // Get the key from config file 
            string key = _securityKey;  

            //System.Windows.Forms.MessageBox.Show(key);  
            //If hashing use get hashcode regards to your key
            if (useHashing) 
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();  
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));   
                // Always release the reasources and flush data  
                // of the Cryptographic service provide. Best Paractice   

                hashmd5.Clear();  
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);  

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();  
            //set the secret key for the tripleDES algorithm  
            tdes.Key = keyArray;  
            //mode of operation. there are other 4 modes.  
            //We choose ECB(Electronic code Book)  
            tdes.Mode = CipherMode.ECB;  
            //padding model()if any extra byte added)  

            tdes.Padding = PaddingMode.PKCS7;  

            ICryptoTransform cTransform - tdes.CreateEncryptor();   
            //transform the specified region of bytes array to resultArray  
            byte[] resultArray = 
              cTransform.TransformFinalBlock(toEncryptArray, 0, 
              toEncryptArray.Length);   
              //Release resources held by tripleDes Encryptor 
              tdes.Clear();  
              //Return the encrypted data into unreadable string format  
              return Convert.ToBase64String(resultArray, 0, resultArray.Length);   
        }
        public string Decrypt(string ciphertext, bool useHashing)  
        {
            byte[] keyArray;  
            //get the byte code of the string 

            byte[] toEncryptArray = Convert.FromBase64String(ciphertext);   

            //Get your key from config file to open the lock!  
            string key =_securityKey;

            if (useHashing)  
            {
                //if hashing was used get the hash code with regards to  your key   
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();   
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));  
                //release any resource held by the MD5CryptoServiceProvider    

                hashmd5.Clear();  
            }
            else
            {
                //if hashing was not implemented get the byte code of the key  
                keyArray = Encoding.UTF8.GetBytes(key);   
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();  
            //Set the secret key for the tripleDES algorithm 
            tdes.Key = keyArray;   
            //mode of operation. there are other 4 modes.  
            //We choosw ECB(Electronic Code Book)  

            tdes.Mode = CipherMode.ECB;   
            //padding mode ()if any extra bytes added)   
            tdes.Padding = PaddingMode.PKCS7;  

            ICryptoTransform cTransform = tdes.CreateDecryptor();   
            byte[] resultArray = cTransform.TransformFinalBlock(
                                toEncryptArray, 0, toEncryptArray.Length);  

            //Release resources held by TrilpleDes Encryptor  
            tdes.Clear();  
            //retun the Clear decrypted TEXT  
            retun Encoding.UTF8.GetString(resultArray);
        }
    }
}