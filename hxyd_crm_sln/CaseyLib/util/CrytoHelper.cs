using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;


namespace CaseyLib.util
{


		public class CryptoHelper
		{
			static CryptoHelper()
			{
				des.Mode = CipherMode.CBC;
				des.Padding = PaddingMode.PKCS7;
			}
       
			private static readonly byte[] CryptogramIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
			private static readonly byte[] CryptogramKEY = new byte[] { 
																		  0xda, 0xef, 0xe3, 0x16, 0x1f, 0x35, 120, 0xe0, 0xdf, 0xdf, 0xab, 210, 140, 0x9e, 0x2f, 0x56, 
																		  0x7a, 0x27, 0xee, 0x5f, 0x2f, 0x8a, 0x2c, 0x9b
																	  };
			private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
       
			public static string CommonDecrypt(string ToDecryptString)
			{
				string str;
				if (ToDecryptString == "")
				{
					return str="";
				}
				try
				{
					byte[] buffer;
					if (Decrypt(CryptogramKEY, CryptogramIV, FromBase64String(ToDecryptString), out buffer))
					{
						//str= ConvertByteArrayToString(buffer);
						return  ConvertByteArrayToString(buffer);
					}
					str = "";
				}
				catch
				{
					str="";
				}
				return str;
			}
       
			public static string CommonEncrypt(string ToEncryptString)
			{
				string str;
				if (ToEncryptString == "")
				{
					return "";
				}
				try
				{
					byte[] buffer;
					if (Encrypt(CryptogramKEY, CryptogramIV, ConvertStringToByteArray(ToEncryptString), out buffer))
					{
						return ToBase64String(buffer);
					}
					str = "";
				}
				catch
				{
					str="";
				}
				return str;
			}
       
			public static string ConvertByteArrayToString(byte[] buf)
			{
				return Encoding.GetEncoding("utf-8").GetString(buf);
			}
       
			public static byte[] ConvertStringToByteArray(string s)
			{
				return Encoding.GetEncoding("utf-8").GetBytes(s);
			}
       
			public static bool Decrypt(byte[] KEY, byte[] IV, byte[] TobeDecrypted, out byte[] Decrypted)
			{
				bool flag;
				Decrypted = null;
				try
				{
					int num;
					byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
					for (num = 0; num < 8; num++)
					{
						rgbIV[num] = IV[num];
					}
					byte[] rgbKey = new byte[] { 
												   0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
												   0, 1, 2, 3, 4, 5, 6, 7
											   };
					for (num = 0; num < 0x18; num++)
					{
						rgbKey[num] = KEY[num];
					}
					Decrypted = des.CreateDecryptor(rgbKey, rgbIV).TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
					des.Clear();
					flag = true;
				}
				catch
				{
					flag=false;
				}
				return flag;
			}
       
			public static bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out byte[] Encrypted)
			{
				bool flag;
				Encrypted = null;
				if ((KEY == null) || (IV == null))
				{
					return false;
				}
				try
				{
					int num;
					byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
					for (num = 0; num < 8; num++)
					{
						rgbIV[num] = IV[num];
					}
					byte[] rgbKey = new byte[] { 
												   0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
												   0, 1, 2, 3, 4, 5, 6, 7
											   };
					for (num = 0; num < 0x18; num++)
					{
						rgbKey[num] = KEY[num];
					}
					Encrypted = des.CreateEncryptor(rgbKey, rgbIV).TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
					des.Clear();
					flag = true;
				}
				catch
				{
				  throw;
				}
				return flag;
			}
       
			public static byte[] FromBase64String(string s)
			{
				return Convert.FromBase64String(s);
			}
       
			public static string ToBase64String(byte[] buf)
			{
				return Convert.ToBase64String(buf);
			}
    
		
	}



}
