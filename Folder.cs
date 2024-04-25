using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AliHamed_MohamadGanam_exe5
{
    internal class Folder : AD_File, IComplete
    {
        private AD_File[] files;
        private string folderPath;
        public static Folder Root { get { return root; } }
        public static Folder root = new Folder("root", "");
        private static int index = 0, counter = 0;
        private static Folder CurrentFolder;
        public Folder(string name, string path) : base(name)
        {
            folderPath = path;
            files = new AD_File[1];
        }
        //public string GetPath()
        //{
        //    return this.fileName;
        //}
        public string GetFullPath()
        {
            if (this.folderPath == null)
            {
                return "";
            }
            return folderPath + "\\" + FileName;
        }
        public AD_File[] Files
        {
            get { return files; }
        }
        /// /////////////////////////////////////////////////////////////////////////
        public override float GetSize()
        {
            float size = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DataFile)
                {
                    size += files[i].GetSize();
                }
                if (files[i] is Folder)
                {
                    files[i].GetSize();
                }
            }
            return size;
        }
        /// ///////////////////////////////////////////////////////////////////////////////////
        public bool IsFull(int capacity)
        {
            if (root.GetSize() == capacity)
            {
                return true;
            }
            return false;
        }

        /// //////////////////////////////////////////////////////////////////////////////
        public void addFileToArray(AD_File file)
        {
            int counter = 0;
            if (files.Length == 1)
            {
                int index = files.Length - 1;
                files[index] = file;
                Array.Resize(ref files, files.Length + 1);

            }
            else {
                if (file is DataFile)
                {
                    foreach (var item in files)
                    {
                        if (item != null)
                            if (item.Equals(file))
                            {
                                counter++;
                            }
                    }
                    if (counter == 0)
                    {
                        int index = files.Length - 1;
                        files[index] = file;
                        Array.Resize(ref files, files.Length + 1);
                    }
                    else
                    {
                        throw new Exception("file allready exists");
                    }
                }
                if (file is Folder)
                {
                    foreach (var item in files)
                    {
                        if (item != null)
                            if (((Folder)item).fun(file))
                            {
                                counter++;
                            }
                    }
                    if (counter == 0)
                    {
                        int index = files.Length - 1;
                        files[index] = file;
                        Array.Resize(ref files, files.Length + 1);
                    }
                    else
                    {
                        throw new Exception("file allready exists");
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////

        /// /////////////////////////////////////////////////////////////////////////////////////
        public DataFile Mkfile(string data, string filename)
        {
            DataFile dataFile = new DataFile(filename, data);
            addFileToArray(dataFile);
            return dataFile;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MkDir(string filename)

        {
            Folder folder = new Folder(filename, this.GetFullPath());
            addFileToArray(folder);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToString()// return to fix
        {
            string str = "";
            if (files.Length == 1)
            {
                return this.FileName + "\t" + this.lastUpdateTime + "<DIR>";
            }
            else
            {
                try
                {
                    foreach (AD_File file in files)
                    {
                        if (file is DataFile)
                        {
                            str += file.FileName + file.LastUpdateTime + file.GetSize().ToString();
                        }
                        if (file is Folder)
                        {
                            str += file.FileName + "  " + file.LastUpdateTime + "<DIR>";
                            str += "\n";
                        }
                    }
                }

                catch (Exception e)
                {
                    str = e.ToString();
                }
            }
            return str;
        }
        ///////////////////////////////////////////////////////////////////////////////

        public static Folder Cd(string path)
        {
            if (path.IndexOf('\\') == -1 && root.files.Length <= 2 && counter == 0)
            {
                counter++;
                return root.files[index] as Folder;
            }
            else
            {
                Folder currentFolder = Root;
                string[] folders = path.Split('\\');
                for (int i = 0; i < folders.Length; i++)
                {
                    if (string.IsNullOrEmpty(folders[i]))
                        continue;

                    bool folderExists = false;

                    foreach (AD_File file in currentFolder.files)
                    {
                        if (file is Folder folder && folder.FileName.Equals(folders[i], StringComparison.OrdinalIgnoreCase))
                        {
                            currentFolder = folder;
                            folderExists = true;
                            break;
                        }
                    }

                    if (!folderExists)
                    {
                        Console.WriteLine("Folder does not exist according to the given path.");
                        return null;
                    }
                }

                return currentFolder;

            }
        } 

            public bool fun(AD_File file)
            {
                return base.Equals(file);
            }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        public bool Fc(string source, string dest)
        {
            AD_File sourceFile = FindFileRecursive(this, source);
            AD_File destFile = FindFileRecursive(this, dest);

            if (sourceFile == null || destFile == null)
            {
                throw new ArgumentException("Files not found.");
            }

            if (sourceFile.Equals(destFile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private AD_File FindFileRecursive(Folder folder, string filePath)
        {
            string[] pathComponents = filePath.Split('\\');

            if (pathComponents[0] == "")
            {
                folder = Root;
                pathComponents = pathComponents.Skip(1).ToArray();
            }

            for (int i = 0; i < pathComponents.Length; i++)
            {
                string component = pathComponents[i];

                if (i == pathComponents.Length - 1)
                {
                    return folder.files.FirstOrDefault(f => f.FileName == component);
                }
                else
                {
                    folder = folder.files.FirstOrDefault(f => f is Folder && f.FileName == component) as Folder;
                    if (folder == null)
                    {
                        throw new ArgumentException("Folder does not exist according to the given path.");
                    }
                }
            }

            return null;
        }
    }
    } 
