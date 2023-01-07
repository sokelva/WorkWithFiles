using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static string SettingsFileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Students.dat";
        protected static string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Students";
        protected static string binreader = string.Empty;

        static void Main(string[] args)
        {
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

            //Бинарный Файл пустой, заполним его данными
            WriteStudent();
            //Распределим студентов по группам
            StudentsSort();
        }

        private static void StudentsSort()
        {
            if (File.Exists(SettingsFileName)) // если файл @"\Students.dat"; сущетвует 
            {

                using (BinaryReader reader = new BinaryReader(File.Open(SettingsFileName, FileMode.Open)))
                {
                    // Применяем специализированные методы Read для считывания данных.
                    do
                    {
                        binreader = reader.ReadString();
                        string str = binreader.ToString().Substring(0, binreader.ToString().IndexOf('|'));
                        if (binreader.Contains("CDEV-25"))
                        {
                            string filePath = path + @"\Students_CDEV-25.txt";
                            var fileInfo = new FileInfo(filePath);
                            using (StreamWriter sw = fileInfo.AppendText())
                            {
                                sw.WriteLine(str);
                            }
                        }
                        else if (binreader.Contains("CDEV-24"))
                        {
                            string filePath = path + @"\Students_CDEV-24.txt";
                            var fileInfo = new FileInfo(filePath);
                            using (StreamWriter sw = fileInfo.AppendText())
                            {
                                sw.WriteLine(str);
                            }
                        }
                        else
                        {
                            string filePath = path + @"\Students_CDEV-23.txt";
                            var fileInfo = new FileInfo(filePath);
                            using (StreamWriter sw = fileInfo.AppendText())
                            {
                                sw.WriteLine(str);
                            }
                        }
                        binreader = string.Empty;

                    } while (reader.BaseStream.Position != reader.BaseStream.Length);
                    reader.Close();
                }
            }
        }

        private static void WriteStudent()
        {
            string Name = string.Empty;
            string Group = string.Empty;
            DateTime DateOfBirth = DateTime.Now;

            if (File.Exists(SettingsFileName)) // если файл @"\Students.dat"; сущетвует 
            {
                BinaryReader reader = new BinaryReader(File.Open(SettingsFileName, FileMode.Open));
                string st = reader.ReadString();
                reader.Close();

                if (st == "")
                {
                    try
                    {
                        using (BinaryWriter writer = new BinaryWriter(File.Open(SettingsFileName, FileMode.Create)))
                        {
                            // записываем данные 
                            writer.Write($"Матвей, {DateTime.Now}|CDEV-24");
                            writer.Write($"Алексей, {DateTime.Now}|CDEV-25");
                            writer.Write($"Михаил, {DateTime.Now}|CDEV-23");
                            writer.Write($"Сергей, {DateTime.Now}|CDEV-24");
                            writer.Write($"Александр, {DateTime.Now}|CDEV-25");
                            writer.Write($"Дмитрий, {DateTime.Now}|CDEV-23");
                            writer.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"При чтении файла или записи в файл {SettingsFileName} возникли ошибки", e.Message);
                    }
                }
            }
            
        }
    }
}
