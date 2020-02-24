using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace RenameToFoldername
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 1)
            {
                showHelptext();
            }
            else if (args[0].ToLower() == "test")
            {
                Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
                Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName);
            }
            else if (args[0].ToLower() == "+register")
            {
                Console.WriteLine("registering application in Windows Explorer Context Menu");

                string menuCommand = string.Format("\"{0}\" \"%L\"", System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName);
                FileShellExtension.Register("*", "Rename to foldername",
                                            "Rename to foldername", menuCommand);
            }
            else if (args[0].ToLower() == "-register")
            {
                Console.WriteLine("unregistering application from Windows Explorer Context Menu");
                FileShellExtension.Unregister("*", "Rename to foldername");
            }
            else
            {
                renameFileToFoldername(args[0]);
            }
        }

        static void renameFileToFoldername(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (fi.Exists)
            {
                DirectoryInfo di = fi.Directory;
                string newName = di.FullName + "\\" + di.Name + fi.Extension;

                if (!File.Exists(newName))
                {
                    File.Move(fi.FullName, newName);
                }
                else
                {
                    MessageBox.Show("file already exists (" + newName + ")");
                }
            }
            else
            {
                Console.WriteLine("file does noet exist");
                Console.WriteLine("");
                showHelptext();
            }
        }

        static void showHelptext()
        {
            Console.WriteLine("arguments:  filename / +register / -register");
            Console.WriteLine("");
            Console.WriteLine("filename :  file to rename based on folder name");
            Console.WriteLine("+register : register in file explorer context menu");
            Console.WriteLine("-register : unregister from file explorer context menu");
            Console.WriteLine("");
        }
    }
}
