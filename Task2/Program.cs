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
        /// Задание 2: Программа считает размер папки на диске (вместе со всеми вложенными папками и файлами). 
        /// На вход метод принимает URL директории, в ответ — размер в байтах.
        /// var str = string.Empty; -- если так объявлять, то тратится меньше ресурсов
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const double BestBefore = 30;
            bool paramsExist = args.Length > 0;
            var path = paramsExist ? args[0] : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Task1";

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

            string[] dirs = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);// Проверяем только все SubFolders

            foreach (var item in dirs)
            {
                DirectoryInfo itemInfo = new DirectoryInfo(item);
                TimeSpan duration = DateTime.Now.Subtract(itemInfo.CreationTime);
                Console.WriteLine($"{item}, {itemInfo.CreationTime}\n----------\nСрок годности папки: {itemInfo.CreationTime.AddMinutes(BestBefore)}");
                //if (DateTime.Now > itemInfo.CreationTime.AddMinutes(30)) - как вариант сравнения 
                if (duration.TotalMinutes > BestBefore)
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
