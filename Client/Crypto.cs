﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace Client
{
    internal class Crypto
    {
        #region AES
        public string EncryptAES(string plainText, string keyString)
        {
            byte[] cipherData;
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(keyString);
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                cipherData = ms.ToArray();
            }

            byte[] combinedData = new byte[aes.IV.Length + cipherData.Length];
            Array.Copy(aes.IV, 0, combinedData, 0, aes.IV.Length);
            Array.Copy(cipherData, 0, combinedData, aes.IV.Length, cipherData.Length);
            return Convert.ToBase64String(combinedData);
        }

        public string DecryptAES(string combinedString, string keyString)
        {
            string plainText;
            byte[] combinedData = Convert.FromBase64String(combinedString);
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[combinedData.Length - iv.Length];
            Array.Copy(combinedData, iv, iv.Length);
            Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        plainText = sr.ReadToEnd();
                    }
                }

                return plainText;
            }
        }
        #endregion

        #region RSA
        public BigInteger q = new BigInteger();
        public BigInteger p = new BigInteger();
        public BigInteger n = new BigInteger();
        public BigInteger omegan = new BigInteger();
        public BigInteger ex = new BigInteger();
        public BigInteger d = new BigInteger();
        public int[] c = new int[1000000];
        BigInteger[] cc = new BigInteger[10000000];
        BigInteger[] mm = new BigInteger[10000000];
        long qt, pt;
        int bit;

        public void RandomKey()
        {
            Random rand = new Random();
            //Random 2 Số nguyên tố rất lớn q và p
            do
            {
                q = BigInteger.genPseudoPrime(bit, 2, rand);
                p = BigInteger.genPseudoPrime(bit, 2, rand);

            } while (q == p);
            calculate();
        }

        public void calculate()
        {
            n = q * p;
            omegan = (q - 1) * (p - 1);
            int b = n.bitCount();
            Random rand = new Random();
            //Tìm e (public Key)
            do
            {
                ex = omegan.genCoPrime(b, rand);
            } while (ex.isProbablePrime() == false || ex.gcd(omegan) != 1 || ex >= omegan);
        }

        public string EncryptRSA(string s)
        {
            string cipher = "";
            byte[] m = Encoding.ASCII.GetBytes(s);
            for (int i = 0; i < s.Length; i++)
            {
                c[i] = m[i];
                cc[i] = c[i];
                cc[i] = cc[i].modPow(ex, n);
                cipher += (" " + cc[i]);
            }
            return cipher;
        }

        private string DecryptRSA(string input)
        {
                try
                {
                    string s = input;
                    for (int i = 0; i < s.Length; i++)
                    {
                        mm[i] = cc[i].modPow(d, n);
                    }
                    //Giải mã về dạng Văn bản
                    string textoutput = "";
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    byte[] rsa = new byte[10];
                    for (int i = 0; i < s.Length; i++)
                    {
                        rsa = mm[i].getBytes();
                        textoutput += (ascii.GetString(rsa));
                    }
                    return textoutput;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return "";
                }
            

        }
        #endregion

        #region Hash SHA256
        public string SHA256(string plainText)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();

            sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText));

            byte[] result = sha256.Hash;
            return byteToString(result);
        }

        public string byteToString(byte[] result)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        #endregion

        #region HMAC SHA256
        public static void SignFile(byte[] key, String sourceFile, String destFile)
        {
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(destFile, FileMode.Create))
                    {
                        // Compute the hash of the input file.
                        byte[] hashValue = hmac.ComputeHash(inStream);
                        // Reset inStream to the beginning of the file.
                        inStream.Position = 0;
                        // Write the computed hash value to the output file.
                        outStream.Write(hashValue, 0, hashValue.Length);
                        // Copy the contents of the sourceFile to the destFile.
                        int bytesRead;
                        // read 1K at a time
                        byte[] buffer = new byte[1024];
                        do
                        {
                            // Read from the wrapping CryptoStream.
                            bytesRead = inStream.Read(buffer, 0, 1024);
                            outStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }
            }
            return;
        }

        public static bool VerifyFile(byte[] key, String sourceFile)
        {
            bool err = false;
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                // Create an array to hold the keyed hash value read from the file.
                byte[] storedHash = new byte[hmac.HashSize / 8];
                // Create a FileStream for the source file.
                using (FileStream inStream = new FileStream(sourceFile, FileMode.Open))
                {
                    // Read in the storedHash.
                    inStream.Read(storedHash, 0, storedHash.Length);
                    // Compute the hash of the remaining contents of the file.
                    // The stream is properly positioned at the beginning of the content,
                    // immediately after the stored hash value.
                    byte[] computedHash = hmac.ComputeHash(inStream);
                    // compare the computed hash with the stored value

                    for (int i = 0; i < storedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i])
                        {
                            err = true;
                        }
                    }
                }
            }
            if (err)
            {
                Console.WriteLine("Hash values differ! Signed file has been tampered with!");
                return false;
            }
            else
            {
                Console.WriteLine("Hash values agree -- no tampering occurred.");
                return true;
            }
        } //end VerifyFile
        #endregion
    }
}