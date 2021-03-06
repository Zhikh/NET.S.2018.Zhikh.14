﻿using System;
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
        private static readonly Encoding _encoding = Encoding.Unicode;

        #region Public members
        /// <summary>
        /// Copy data from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Bytes count </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] array;
            
            using (FileStream reader = File.OpenRead(sourcePath), 
                writer = File.OpenWrite(destinationPath))
            {
                array = new byte[reader.Length];

                reader.Read(array, 0, array.Length);
                
                writer.Write(array, 0, array.Length);
            }

            return array.Length;
        }

        /// <summary>
        /// Copy data from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Bytes count </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);
            
            byte[] streamData = _encoding.GetBytes(ReadByStreamReader(sourcePath));
            char[] writeData = { };

            WriteInStream(streamData, writeData);
            WriteByStreamWriter(destinationPath, writeData);

            return streamData.Length;
        }

        /// <summary>
        /// Copy data by block from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Bytes count </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int result = 0;

            using (FileStream reader = File.OpenRead(sourcePath),
                writer = File.OpenWrite(destinationPath))
            {
                writer.SetLength(reader.Length);
                
                byte[] bytes = new byte[_bufferSize];
                int bytesRead;
                while ((bytesRead = reader.Read(bytes, 0, _bufferSize)) > 0)
                {
                    writer.Write(bytes, 0, bytesRead);
                    result += bytesRead;
                }
            }

            return result;
        }

        /// <summary>
        /// Copy data by block from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Bytes count </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int bytesCount = 0;
            char[] writeData = { };

            using (var reader = new StreamReader(sourcePath, _encoding))
            {
                char[] streamData = new char[_bufferSize];

                int charsRead = reader.Read(streamData, 0, streamData.Length);
                byte[] block = _encoding.GetBytes(streamData);
                int bytesRead = block.Length;
                if (bytesRead != 0)
                {
                    bytesCount += bytesRead;
                    WriteInStream(block, writeData);
                }
            }

            WriteByStreamWriter(destinationPath, writeData);

            return bytesCount;
        }

        /// <summary>
        /// Copy data by BufferedStream from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Bytes count </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int bytesCount = 0;
            using (FileStream reader = File.OpenRead(sourcePath),
                writer = File.OpenWrite(destinationPath))
            {
                byte[] sourceData = new byte[_bufferSize];
                using (var buffer = new BufferedStream(reader, _bufferSize))
                {
                    int bytesRead;
                    while ((bytesRead = buffer.Read(sourceData, 0, _bufferSize)) > 0)
                    {
                        writer.Write(sourceData, 0, bytesRead);
                        bytesCount += bytesRead;
                    }
                }

            }

            return bytesCount;
        }

        /// <summary>
        /// Copy data by line from file by sourcePath in file by destinationPath
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Lines amount </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int stringsCount = 0;
            string text = string.Empty;

            using (TextReader reader = File.OpenText(sourcePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text += line;
                    stringsCount++;
                }
            }

            using (TextWriter writer = File.CreateText(destinationPath))
            {
                writer.WriteLine();
            }

            return stringsCount;
        }

        /// <summary>
        /// Compare data from files
        /// </summary>
        /// <param name="sourcePath"> File path </param>
        /// <param name="destinationPath"> File path  </param>
        /// <returns> Result of comparing </returns>
        /// <exception cref="FileNotFoundException"> When one of passes is uncorrect or file doesn't exist</exception>
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
                result =_encoding.GetString(array);
            }

            return result;
        }

        private static void WriteInStream(byte[] sourceData, char[] data)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(sourceData, 0, sourceData.Length);

                stream.Seek(0, SeekOrigin.Begin);

                byte[] buffer = new byte[stream.Length];
                int count = stream.Read(buffer, 0, 20);

                while (count < stream.Length)
                {
                    buffer[count++] = Convert.ToByte(stream.ReadByte());
                }

                var encorder = new UnicodeEncoding();
                data = new char[encorder.GetCharCount(buffer, 0, count)];
                encorder.GetDecoder().GetChars(buffer, 0, count, data, 0);
            }
        }

        private static void WriteByStreamWriter(string destinationPath, char[] writeData)
        {
            using (var streamWriter = new StreamWriter(destinationPath, false, _encoding))
            {
                streamWriter.Write(writeData);
            }
        }

        private static string ReadByStreamReader(string sourcePath)
        {
            string sourceData;
            using (var streamReader = new StreamReader(sourcePath, _encoding))
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
