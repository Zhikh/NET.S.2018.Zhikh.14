using System;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] array;

            using (FileStream sourceFlStream = File.OpenRead(sourcePath), 
                destinationFlStream = File.OpenWrite(destinationPath))
            {
                array = new byte[sourceFlStream.Length];

                sourceFlStream.Read(array, 0, array.Length);

               // destinationFlStream.Seek(0, SeekOrigin.End);
                destinationFlStream.Write(array, 0, array.Length);
            }

            return array.Length;
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            string sourceData = string.Empty;

            using (var streamReader = new StreamReader(sourcePath, Encoding.Default))
            {
                sourceData = streamReader.ReadToEnd();
            }

            byte[] streamData =  Encoding.Default.GetBytes(sourceData);

            char[] writeData;
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(streamData, 0, streamData.Length);

                memoryStream.Seek(0, SeekOrigin.Begin);
                
                byte[] buffer = new byte[memoryStream.Length];
                int count = memoryStream.Read(buffer, 0, 20);

                while (count < memoryStream.Length)
                {
                    buffer[count++] = Convert.ToByte(memoryStream.ReadByte());
                }

                var uniEncoding = new UnicodeEncoding();
                writeData = new char[uniEncoding.GetCharCount(buffer, 0, count)];
                uniEncoding.GetDecoder().GetChars(buffer, 0, count, writeData, 0);
            }

            using (var streamWriter = new StreamWriter(destinationPath, false, Encoding.Default))
            {
                streamWriter.Write(writeData);
            }

            return streamData.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            // TODO: Use InMemoryByByteCopy method's approach
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            
            return GetContent(sourcePath) == GetContent(destinationPath);
        }

        #endregion

        #endregion

        #region Private members

        private static string GetContent(string path)
        {
            string result = string.Empty;
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                result = Encoding.ASCII.GetString(array);
            }

            return result;
        }

        #region Validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException(nameof(sourcePath));
            }

            if (!File.Exists(destinationPath))
            {
                throw new FileNotFoundException(nameof(destinationPath));
            }
        }

        #endregion

        #endregion

    }
}
