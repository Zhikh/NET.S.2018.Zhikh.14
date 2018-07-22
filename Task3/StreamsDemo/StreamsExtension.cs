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

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            string sourceData = ReadByStreamReader(sourcePath);
            byte[] streamData = Encoding.Unicode.GetBytes(sourceData);
            char[] writeData = { };

            ConvertByteArrayByMemoryStream(streamData, writeData);
            WriteByStreamWriter(destinationPath, writeData);

            return streamData.Length;
        }

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

        private static void ConvertByteArrayByMemoryStream(byte[] streamData, char[] writeData)
        {
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

                var encorder = new UnicodeEncoding();
                writeData = new char[encorder.GetCharCount(buffer, 0, count)];
                encorder.GetDecoder().GetChars(buffer, 0, count, writeData, 0);
            }
        }

        private static void WriteByStreamWriter(string destinationPath, char[] writeData)
        {
            using (var streamWriter = new StreamWriter(destinationPath, false, Encoding.Unicode))
            {
                streamWriter.Write(writeData);
            }
        }

        private static string ReadByStreamReader(string sourcePath)
        {
            string sourceData;
            using (var streamReader = new StreamReader(sourcePath, Encoding.Unicode))
            {
                sourceData = streamReader.ReadToEnd();
            }

            return sourceData;
        }

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

    }
}
