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
        /// <summary>
        /// Задание 2: Программа считает размер папки на диске (вместе со всеми вложенными папками и файлами). 
        /// На вход метод принимает URL директории, в ответ — размер в байтах.
        /// 
        /// var str = string.Empty; -- если так объявлять, то тратится меньше ресурсов
        /// long dirSize = SafeEnumerateFiles(@"c:\dir", "*.*", SearchOption.AllDirectories).Sum(n => new FileInfo(n).Length);
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool paramsExist = args.Length > 0;
            var path = paramsExist ? args[0] : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Task2";

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
                Console.WriteLine($"\n----------------------------\nОбщий размер папки {dirInfo} (вместе со всеми вложенными папками и файлами): {sizedir} байт.\n----------------------------");
            }

            Console.ReadKey();
        }

        private static long DirSize(DirectoryInfo d)
        {
            long size;

            //Subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo di in dis)
            {
                //Files sizes.
                size = 0;
                Console.WriteLine($"Папка {di}.\n=======================================================");
                FileInfo[] fis = di.GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo fi in fis)
                    {
                        size += fi.Length;
                        Console.WriteLine($"Размер файла {fi}: {fi.Length} байт.\n----------");
                    }
                sizedir += size;
                Console.WriteLine($"Размер папки {di.FullName}: {size} байт.\n----------\n");
            }

            return sizedir;
        }
    }
}
