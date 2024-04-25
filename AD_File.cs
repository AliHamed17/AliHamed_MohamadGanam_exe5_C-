using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
namespace AliHamed_MohamadGanam_exe5
{
    public abstract class AD_File
    {
        protected string fileName;
        protected DateTime lastUpdateTime;


        public string FileName
        {
            get { return fileName; }
            set
            {
                char[] Keywoeds = { '>', '?', '*', ':', '/', '\\', '|', '<' };
                foreach (char c in value)
                {
                    foreach (char c2 in Keywoeds)
                    {
                        if (c == c2)
                        {
                            throw new ArgumentException("Eror!! Enter just letters");
                        }

                        else
                        {
                            fileName = value;
                        }
                    }
                }
            }
        }

        public DateTime LastUpdateTime
        {
            get { return this.lastUpdateTime; }
            set { this.lastUpdateTime =value; }
        }

        public AD_File(string filename)
        {
            FileName = filename;
            LastUpdateTime=DateTime.Now;
        }

        public abstract float GetSize();
 
        public override bool Equals(object other)
        {
            AD_File temp = other as AD_File;
            if (temp == null)
                return false;
            return fileName.Equals(temp.FileName);
        }
      
        public override string ToString()
        {
            string str = "";
            str += $"{fileName}\t{LastUpdateTime.ToString()} KB";
            str += "\r\n";
            return str;
        }
    }
}
