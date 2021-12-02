using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Backend.Models
{
    public static class ImageSaver
    {
        public static List<string> SaveImages(MultipartFormDataStreamProvider provider, string root)
        {

            List<string> pathList = new List<string>();
            if (provider.FileData.Count > 10)
            {
                throw new System.ArgumentException();
            }
            foreach (MultipartFileData file in provider.FileData)
            {
                string localFileName = file.LocalFileName;
                if (!IsImage(localFileName))
                {
                    DeleteFiles(provider);
                    throw new FileLoadException();
                }

                string name = file.Headers.ContentDisposition.FileName;
                name = DateTime.Now.Ticks + name.Trim('"');

                string filePath = Path.Combine("G:/Мои документы/ProjectPivtures/", name);
                File.Move(localFileName, filePath);
                pathList.Add("http://localhost:62976/api/Image/GetImage/?name=" + name);
            }
            DeleteFiles(provider);
            return pathList;
        }
        public static string SaveImage(MultipartFormDataStreamProvider provider, string root)
        {
            string fileName = "";
            if (provider.FileData.Count > 1)
            {
                throw new ArgumentException();
            }
            foreach (MultipartFileData file in provider.FileData)
            {
                string localFileName = file.LocalFileName;
                if (!IsImage(localFileName))
                {
                    DeleteFiles(provider);
                    throw new FileLoadException();
                }

                string name = file.Headers.ContentDisposition.FileName;
                name = DateTime.Now.Ticks + name.Trim('"');

                string filePath = Path.Combine("G:/Мои документы/ProjectPivtures/", name);
                File.Move(localFileName, filePath);
                fileName = ("http://localhost:62976/api/Image/GetImage/?name=" + name);
            }
            DeleteFiles(provider);
            return fileName;
        }
        public static void DeleteFiles(MultipartFormDataStreamProvider provider)
        {
            foreach (MultipartFileData file in provider.FileData)
            {
                File.Delete(file.LocalFileName);
            }
        }
        public static bool IsImage(string file)
        {
            List<List<string>> signature = new List<List<string>>() {
                "89 50 4E 47".Split().ToList(),
                "FF D8 FF DB".Split().ToList(),
                "FF D8 FF E0".Split().ToList(),
                "FF D8 FF E1".Split().ToList(),
                "FF D8 FF EE".Split().ToList(),
                "FF D8 FF DB".Split().ToList(),
            };
            //List<string> png = "89 50 4E 47".Split().ToList();
            //List<string> jpg = "FF D8 FF DB".Split().ToList();
            //List<string> jpeg = "FF D8 FF E0".Split().ToList();

            List<string> fileHead = new List<string>();
            using (FileStream stream = File.OpenRead(file))
            {
                for (int i = 0; i < 4; i++)
                {
                    string bit = stream.ReadByte().ToString("X2");
                    fileHead.Add(bit);
                }
            }

            foreach (List<string> img in signature)
            {
                if (!img.Except(fileHead).Any())
                {
                    return true;
                }
            }

            //if (!png.Except(fileHead).Any())
            //{
            //    return true;
            //}
            //if (!jpg.Except(fileHead).Any())
            //{
            //    return true;
            //}
            //if (!jpeg.Except(fileHead).Any())
            //{
            //    return true;
            //}

            return false;
        }
    }
}