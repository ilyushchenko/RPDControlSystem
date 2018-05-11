using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RPDControlSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Storage
{
    public class FileManager
    {
        private const string FilesFolder = "Files/"; 

        private readonly DatabaseContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FileManager(DatabaseContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public Models.File SaveFile(IFormFile file)
        {
            return SaveFile(file, "");
        }

        public Models.File SaveFile(IFormFile file, string directory)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            // путь к папке Files

            string randomFileName = GetRandomFileName(file);

            if (System.IO.File.Exists(randomFileName))
            {
                throw new Exception("Файл с таким именем существует");
            }

            directory = directory.Trim(' ', '/');

            directory += (directory == String.Empty) ? "" : "/";

            //var folder = Path.Combine(FilesFolder, directory);

            var folder = $"/{FilesFolder}{directory}";

            string pathToSave = $"{_appEnvironment.WebRootPath}{folder}{randomFileName}";

            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(pathToSave, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            Models.File photo = new Models.File { Directory = folder, Name = randomFileName, BaseName = file.FileName };
            _context.Files.Add(photo);
            _context.SaveChanges();

            return photo;
        }

        public string GetRandomFileName(IFormFile file)
        {
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            string extention = Path.GetExtension(file.FileName);
            return $"{fileName}{extention}";
        }

        public bool DeleteFile(Models.File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (DeleteFromDisk(file))
            {
                return DeleteFromDatabase(file);
            }
            return false;
        }

        public bool DeleteFile(int id)
        {
            var file = _context.Files.SingleOrDefault(m => m.Id == id);

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (DeleteFromDisk(file))
            {
                return DeleteFromDatabase(file);
            }
            return false;

        }

        private bool DeleteFromDisk(Models.File file)
        {
            string path = $"{_appEnvironment.WebRootPath}{file.FullPath}";

            FileInfo fileInf = new FileInfo(path);

            if (fileInf.Exists)
            {
                fileInf.Delete();
                return true;
            }
            else
                return false;
        }

        private bool DeleteFromDatabase(Models.File file)
        {
            _context.Files.Remove(file);
            _context.SaveChanges();
            return true;
        }
    }
}
