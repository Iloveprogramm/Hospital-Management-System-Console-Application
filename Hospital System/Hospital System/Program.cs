using System;
using System.IO;
using System.Linq;

namespace HospitalManagementSystem
{
    class Program
    {
        static Admin admin = new Admin();
        static Patient patient = new Patient();
        static Doctor doctor = new Doctor();

        static void Main(string[] args)
        {
            ShowLoginMenu();
        }

        static void ShowLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |     WELCOME TO DOTNET Hospital Management System        |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine("\t\t\t |                 PLEASE LOGIN TO START                   |");
                Console.WriteLine("\t\t\t |                                                         |");
                Console.WriteLine("\t\t\t ===========================================================");
                Console.WriteLine(); // 新增一行，使输入处于下方
                Console.Write("\t\t\t Enter User ID: "); // 使用\t进行缩进
                string id = Console.ReadLine();

                Console.Write("\t\t\t Enter Password: ");
                string password = ReadPassword(); // 使用自定义的ReadPassword方法来读取密码并显示为*


                if (IsValidCredentials(id, password))
                {
                    Console.WriteLine("\t\t\t Login successful!");
                    patient.showMenu();
                }
                else if (IsDoctorValidCredentials(id, password))
                {
                    Console.WriteLine("\t\t\t Login successful!");
                    doctor.showMenu();
                }
                else if (IsAdminValidCredentials(id, password))
                {
                    Console.WriteLine("\t\t\t Login successful!");
                    admin.ShowMenu();
                }
                else
                {
                    Console.WriteLine("\n\t\t\t Invalid credentials!");
                    Console.Write("\t\t\t Do you want to retry? (Y/N): "); // 【功能：询问是否重新输入】
                    string choice = Console.ReadLine().ToUpper();

                    if (choice != "Y")
                    {
                        Environment.Exit(0); // 【功能：输入N时退出程序】
                    }
                }
            }
        }

        static bool IsValidCredentials(string id, string password)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // 检查患者文件
            if (CheckCredentialsInFile(id, password, "patients.txt"))
            {
                return true;
            }
            return false;
        }

        static bool IsDoctorValidCredentials(string id, string password)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // 检查患者文件
            if (CheckCredentialsInFile(id, password, "doctors.txt"))
            {
                return true;
            }
            return false;
        }

        static bool IsAdminValidCredentials(string id, string password)
            {
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
                {
                    return false;
                }

                // 检查患者文件
                if (CheckCredentialsInFile(id, password, "admin.txt"))
                {
                    return true;
                }
                return false;
            }

            static bool CheckCredentialsInFile(string id, string password, string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length > 1 && parts[0] == id && parts[1] == password)
                    return true;
            }

            return false;
        }


        static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(intercept: true); // 不在控制台上显示按键

                // 如果按下Enter键，结束读取
                if (key.Key == ConsoleKey.Enter) break;

                // 如果按下Backspace键并且密码不为空
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // 删除密码的最后一个字符
                    password = password.Substring(0, password.Length - 1);

                    // 删除控制台上的最后一个星号
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    // 如果不是Backspace键，添加字符到密码并显示一个星号
                    Console.Write("*");
                    password += key.KeyChar;
                }
            }
            return password;
        }

    }
}
