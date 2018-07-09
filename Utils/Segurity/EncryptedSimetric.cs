using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Contract.Segurity;

namespace Utils.Segurity
{
    public   class EncryptedSimetric : IEncyptedSimetric
    {

        /// <summary>
        /// Tamañao por default pára el arreglo de bytes
        /// </summary>
        private const int KEYSIZE = 32;


        /// <summary>
        /// Tamaño por default para el arreglo IV
        /// </summary>
        private const int IVSIZE = 16;


        /// <summary>
        /// cosntructro privado de la clase 
        /// </summary>
        public EncryptedSimetric()
        {

        }

   



        /// <summary>
        /// Metoto publico de de la clase encargado de decifrar un string
        /// </summary>
        /// <param name="plainMessage"></param>
        /// <returns></returns>
        public string EncryptString(string TextPlain, string Key, string IV)
        {
            if (String.IsNullOrEmpty(TextPlain))
                return null;
            byte[] Keya = null;
            byte[] Iva = null;

            Keya = Encoding.UTF8.GetBytes(Key);
            Array.Resize(ref Keya, KEYSIZE);

            Iva = Encoding.UTF8.GetBytes(IV);
            Array.Resize(ref Iva, IVSIZE);

            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            try
            {
                Rijndael RijndaelAlg = Rijndael.Create();
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream,
                                                             RijndaelAlg.CreateEncryptor(Keya, Iva),
                                                             CryptoStreamMode.Write);
                byte[] plainMessageBytes = Encoding.UTF8.GetBytes(TextPlain);
                cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);

                cryptoStream.FlushFinalBlock();
                byte[] cipherMessageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(cipherMessageBytes);
            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();

            }
        }


        /// <summary>
        /// Metoto de lcase encargado de descifrar un string de forma cimetrica
        /// </summary>
        /// <param name="encryptedMessage"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public string DecryptString(string TextPlain, string Key, string IV)
        {
            if (String.IsNullOrEmpty(TextPlain))
                return null;
            byte[] Keya = null;
            byte[] Iva = null;

            Keya = Encoding.UTF8.GetBytes(Key);
            Array.Resize(ref Keya, KEYSIZE);

            Iva = Encoding.UTF8.GetBytes(IV);
            Array.Resize(ref Iva, IVSIZE);

            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(TextPlain);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                Rijndael RijndaelAlg = Rijndael.Create();
                memoryStream = new MemoryStream(cipherTextBytes);
                cryptoStream = new CryptoStream(memoryStream,
                                                             RijndaelAlg.CreateDecryptor(Keya, Iva),
                                                             CryptoStreamMode.Read);
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            finally
            {
                if (cryptoStream != null)
                    cryptoStream.Close();
            }
        }
    }
}

