using System;
using System.IO;
using System.Text;

namespace GM.CommonLibs
{
    public class FileEntity
    {
        public string ReturnCode = string.Empty;
        public string Msg = string.Empty;
        public string FileName = string.Empty;
        public string FilePath = string.Empty;
        public string Values = string.Empty;
        public Encoding UseEncoding = null;
    }

    public class WriteFile
    {
        public bool StreamWriter(ref FileEntity fileEntity)
        {
            try
            {
                if (string.IsNullOrEmpty(fileEntity.FileName.Trim()))
                {
                    throw new Exception("FileName Is Empty");
                }

                if (string.IsNullOrEmpty(fileEntity.FilePath.Trim()))
                {
                    throw new Exception("FilePath Is Empty");
                }

                // Step 1 : Check Directory Exists
                if (!Directory.Exists(fileEntity.FilePath))
                {
                    Directory.CreateDirectory(fileEntity.FilePath);
                }

                // Step 2 : Check File Exists
                if (File.Exists(fileEntity.FilePath + "\\" + fileEntity.FileName))
                {
                    File.Delete(fileEntity.FilePath + "\\" + fileEntity.FileName);
                }

                // Step 3 : Write File
                using (StreamWriter objStream = File.CreateText(fileEntity.FilePath + "\\" + fileEntity.FileName))
                {
                    objStream.Write(fileEntity.Values);
                    objStream.Close();
                }

                fileEntity.ReturnCode = "0";
                fileEntity.Msg = "Success";
            }
            catch (Exception ex)
            {
                fileEntity.ReturnCode = "-999";
                fileEntity.Msg = ex.Message;
                return false;
            }
            return true;
        }

        public bool StreamWriterEncoding(ref FileEntity fileEntity)
        {
            try
            {
                if (string.IsNullOrEmpty(fileEntity.FileName.Trim()))
                {
                    throw new Exception("FileName Is Empty");
                }

                if (string.IsNullOrEmpty(fileEntity.FilePath.Trim()))
                {
                    throw new Exception("FilePath Is Empty");
                }

                // Step 1 : Check Directory Exists
                if (!Directory.Exists(fileEntity.FilePath))
                {
                    Directory.CreateDirectory(fileEntity.FilePath);
                }

                // Step 2 : Check File Exists
                if (File.Exists(fileEntity.FilePath + "\\" + fileEntity.FileName))
                {
                    File.Delete(fileEntity.FilePath + "\\" + fileEntity.FileName);
                }

                if (fileEntity.UseEncoding == null)
                {
                    using (StreamWriter objStream = new StreamWriter(fileEntity.FilePath + "\\" + fileEntity.FileName, false, Encoding.Default))
                    {
                        objStream.Write(fileEntity.Values);
                        objStream.Close();
                    }
                }
                else
                {
                    using (StreamWriter objStream = new StreamWriter(fileEntity.FilePath + "\\" + fileEntity.FileName, false, fileEntity.UseEncoding))
                    {
                        objStream.Write(fileEntity.Values);
                        objStream.Close();
                    }
                }

                fileEntity.ReturnCode = "0";
                fileEntity.Msg = "Success";
            }
            catch (Exception ex)
            {
                fileEntity.ReturnCode = "-999";
                fileEntity.Msg = ex.Message;
                return false;
            }
            return true;
        }
    }
}