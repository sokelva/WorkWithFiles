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
        /// <summary>
        /// Задание 1: Программа чистит нужную нам папку от файлов и папок, которые не использовались более 30 мину
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const double BestBefore = 30;

            bool paramsExist = args.Length > 0;
            string path = paramsExist ? args[0] : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Task1";

            //Создаем папку, если ее нет
            if (! Directory.Exists(path))
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

            string[] dirs = Directory.GetDirectories(path,"*.*", SearchOption.TopDirectoryOnly);// Проверяем только Task1

            foreach (var item in dirs)
            {
                DirectoryInfo itemInfo = new DirectoryInfo(item);
                TimeSpan duration = DateTime.Now.Subtract(itemInfo.CreationTime);
                Console.WriteLine($"{item}, {itemInfo.CreationTime}\n----------\nСрок годности папки: {itemInfo.CreationTime.AddMinutes(BestBefore)}");
                //if (DateTime.Now > itemInfo.CreationTime.AddMinutes(30)) - как вариант сравнения 
                if (duration.TotalMinutes> BestBefore)
                {
                    if (Directory.Exists(item))
                    {
                        DeleteFile(itemInfo);
                    }
                }
            }
            Console.ReadKey();
        }
   

        public static void DeleteFile(DirectoryInfo itemInfo)
        {
            try
            {
                itemInfo.Delete(true);
                Console.WriteLine($"Удалена папка: {itemInfo}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"При удалении папки {itemInfo} возникла ошибка: {e.Message}");
            }
        }
    }
}
