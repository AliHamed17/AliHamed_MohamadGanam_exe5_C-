using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHamed_MohamadGanam_exe5
{
    public class DataFile:AD_File,IComparable
    {
        private string dataFile;
       public string Data_File
        {
            get { return dataFile; }
            set { dataFile = value; }
        }
        /// ////////////////////////////////////////////////////////////////////
        public DataFile(string filename,string data) : base(filename)
        {
            Data_File = data;
        }
        //////////////////////////////////////////////////////////////////////// Method get size  

        public override float GetSize()
        {
            float B = 0;
            float KB = 0;
            foreach (char item in Data_File)
            {
                B++;
            }
            if (B == 1024)
            {
                KB++;
                return KB;
            }
            if (B > 1024)
            {
                KB = KB / B;
            }
            else
            {
                KB = B / 1024;
            }
            return KB;
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////
        public override bool Equals(object other)
        {
            DataFile temp = other as DataFile;
            if (temp == null)
                return false;
            if(base.Equals(other)==false)
                return false;
            return  Data_File.Equals(temp.Data_File);
        }
        ///////////////////////////////////////////////////////////////////////
        public DataFile CompareTo(DataFile file1, DataFile file2)/// ask if it is currect
        {
            if (file1.GetSize() > file2.GetSize())
            {
                return file1;
            }
            if (file1.GetSize() < file2.GetSize())
            {
                return file2;
            }
            return null;
        }
        //////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return base.ToString()+GetSize().ToString();
        }
    }
}
