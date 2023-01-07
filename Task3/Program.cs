using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        private static long sizedir = 0;
        private static long fileCount = 0;

        /// <summary>
        /// Задание 3: 
        /// Показать, сколько весит папка до очистки. Использовать метод из задания 2. 
        /// Выполнить очистку.
        /// Показать сколько файлов удалено и сколько места освобождено.
        /// Показать, сколько папка весит после очистки
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool paramsExist = args.Length > 0;
            string path = paramsExist ? args[0] : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Task3";

            //Создаем папку, если ее нет
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"При создании папки возникла ошибка: {e.Message}");
                }
            }

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                sizedir = DirSize(dirInfo);
                Console.WriteLine($"{dirInfo.FullName}\n==============================\nИсходный размер папки: {sizedir} байт.");
                fileCount = DeleteFile(path);
                Console.WriteLine($"Освобождено: {sizedir} байт (удалено {fileCount} файлов).");
                sizedir = DirSize(dirInfo);
                Console.WriteLine($"Текуший размер папки: {sizedir} байт.");
            }
            Console.ReadKey();
        }


        private static long DeleteFile(string path)
        {
            string[] dirs = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly);// Проверяем только Task1
            
            sizedir = 0;
            fileCount =0;

            DirectoryInfo itemInfo = new DirectoryInfo(path);
            sizedir = DirSize(itemInfo);

            FileInfo [] f = itemInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            fileCount += f.Count();

            foreach (var item in f)
                item.Delete();

            foreach (var item in dirs)
            {
                if (Directory.Exists(item))
                {
                    DirectoryInfo dir = new DirectoryInfo(item);
                    try
                    {
                        FileInfo[] fis = dir.GetFiles("*.*", SearchOption.AllDirectories);
                        fileCount += fis.Count();
                        dir.Delete(true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"При удалении папки {itemInfo} возникла ошибка: {e.Message}");
                    }
                }                
            }
            return fileCount;
        }


        private static long DirSize(DirectoryInfo d)
        {
            sizedir = 0 ;
            fileCount = 0;

            //Top Directory size.
            FileInfo[] f = d.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo fi in f)
                sizedir += fi.Length;

            //Subdirectory sizes.
            foreach (DirectoryInfo di in d.GetDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                //Files sizes.
                FileInfo[] fis = di.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo fi in fis)
                {
                    sizedir += fi.Length;
                }
            }
            return sizedir;
        }
    }
}
