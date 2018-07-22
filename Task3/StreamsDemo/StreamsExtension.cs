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
        private static readonly int _bufferSize = 1024;

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

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            int result = 0;

            using (FileStream sourceStream = File.OpenRead(sourcePath),
                destinationStream = File.OpenWrite(destinationPath))
            {
                destinationStream.SetLength(sourceStream.Length);
                
                byte[] bytes = new byte[_bufferSize];
                int bytesRead;
                while ((bytesRead = sourceStream.Read(bytes, 0, _bufferSize)) > 0)
                {
                    destinationStream.Write(bytes, 0, bytesRead);
                    result += bytesRead;
                }
            }

            return result;
        }

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            //string sourceData = ReadByStreamReader(sourcePath);
            //byte[] streamData = Encoding.Unicode.GetBytes(sourceData);
            //char[] writeData = { };

            //ConvertByteArrayByMemoryStream(streamData, writeData);
            //WriteByStreamWriter(destinationPath, writeData);

            //byte[] block = new byte[OxlOOO]; // блоками no 4 Кбайт.
            //MemoryStream ms = new MemoryStream();
            //while (true)
            //{
            //    int bytesRead = input.Read(block, 0, block.Length);
            //    if (bytesRead == 0) return ms;
            //    ms.Write(block, 0, bytesRead);
            //}

            return 0;
        }

        #endregion

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            int bytesCount = 0;
            using (FileStream fileStream = File.OpenRead(sourcePath),
                destinationStream = File.OpenWrite(destinationPath))
            {
                byte[] sourceData = new byte[_bufferSize];
                using (var buffer = new BufferedStream(fileStream, _bufferSize))
                {
                    int bytesRead;
                    while ((bytesRead = buffer.Read(sourceData, 0, _bufferSize)) > 0)
                    {
                        destinationStream.Write(sourceData, 0, bytesRead);
                        bytesCount += bytesRead;
                    }
                }

            }

            return bytesCount;
        }

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int stringsAmount = 0;
            string text = string.Empty;

            using (TextReader reader = File.OpenText(sourcePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text += line;
                    stringsAmount++;
                }
            }

            using (TextWriter writer = File.CreateText(destinationPath))
            {
                writer.WriteLine();
            }

            return stringsAmount;
        }
        
        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            
            return GetContent(sourcePath) == GetContent(destinationPath);
        }
        #endregion

        #region Private members
        private static string GetContent(string path)
        {
            string result = string.Empty;
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                result = Encoding.Unicode.GetString(array);
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
