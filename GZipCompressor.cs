using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pms
{
    public class GZipCompressor
    {
        
        public void compress(FileInfo FileToCompress,ListBox llist)
        {
            fms f = new fms();
            using (FileStream orignalfilestream=FileToCompress.OpenRead())
            {

                if ((File.GetAttributes(FileToCompress.FullName)& FileAttributes.Hidden)!=FileAttributes.Hidden &FileToCompress.Extension!=".gz")
                {
                    using (FileStream compressedFilestream=File.Create(FileToCompress.FullName+".gz"))
                    {
                        using (GZipStream compressionstream= new GZipStream(compressedFilestream,CompressionMode.Compress))
                        {
                            orignalfilestream.CopyTo(compressionstream);
                            llist.Items.Add("compressed " + FileToCompress.Name + " from " + FileToCompress.Length + " to " + compressedFilestream.Length);
                        }
                    }
                }
            }
        }
        public void extract(FileInfo filetodecompress)
        {
            using (FileStream orignalstream=filetodecompress.OpenRead())
            {
                string currentfilename = filetodecompress.FullName;
                string newfile = currentfilename.Remove(currentfilename.Length - filetodecompress.Extension.Length);
                using (FileStream decompressedfilestream=File.Create(newfile))
                {
                    using (GZipStream decompressionstream=new GZipStream(orignalstream,CompressionMode.Decompress))
                    {
                        if (filetodecompress.Extension==".gz")
                        {
                            decompressionstream.CopyTo(decompressedfilestream);

                        }

                    }
                }
            }
        }
    }
}
