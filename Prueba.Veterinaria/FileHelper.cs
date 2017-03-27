using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Prueba.Veterinaria
{
    public static class FileHelper
    {
        /// <summary>
        /// Diccionario de MIME types con extensiones de imagen correspondiente
        /// </summary>
        public static Dictionary<string, string> MimeTypeExtensiones = new Dictionary<string, string>()
        {
            { "image/jpeg", "jpg"},
            { "image/pjpeg", "jpeg"},
            { "image/vnd.sealedmedia.softseal.jpg", "jpeg"},
            { "image/gif", "gif"},
            { "image/vnd.sealedmedia.softseal.gif", "gif"},
            { "image/png", "png"},
            { "image/x-png", "png"},
            { "image/vnd.sealed.png", "png"}
        };

        #region Validación MIME Type urlmon.dll (No se usa actualmente)
        // No usado actualmente porque requiere configuración sensible en servidores de clientes

        /// <summary>
        /// Lee los bytes de un archivo
        /// </summary>
        /// <param name="input">Stream</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Determina el MIME type
        /// </summary>
        /// <param name="buffer">buffer byte[]</param>
        /// <returns>MIME type</returns>
        public static string GetMimeFromFile(byte[] buffer)
        {
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }

        /// <summary>
        /// Operación interna de urlmon.dll
        /// </summary>        
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
            System.UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            System.UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
            System.UInt32 dwMimeFlags,
            out System.UInt32 ppwzMimeOut,
            System.UInt32 dwReserverd
        );

        #endregion
    }
}