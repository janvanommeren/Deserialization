using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace Deserialization
{
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    [Serializable]
    public class TempFileCollection : ICollection, IEnumerable, IDisposable
    {
        private string basePath;
        private string tempDir;
        private bool keepFiles;
        private Hashtable files;

        public TempFileCollection()
          : this((string)null, false)
        {
        }

        public TempFileCollection(string tempDir)
          : this(tempDir, false)
        {
        }

        public TempFileCollection(string tempDir, bool keepFiles)
        {
            this.keepFiles = keepFiles;
            this.tempDir = tempDir;
            this.files = new Hashtable((IEqualityComparer)StringComparer.OrdinalIgnoreCase);
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Delete();
        }

        ~TempFileCollection()
        {
            this.Dispose(false);
        }

        public IEnumerator GetEnumerator()
        {
            return this.files.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.files.Keys.GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int start)
        {
            this.files.Keys.CopyTo(array, start);
        }

        public void CopyTo(string[] fileNames, int start)
        {
            this.files.Keys.CopyTo((Array)fileNames, start);
        }

        public int Count
        {
            get
            {
                return this.files.Count;
            }
        }

        int ICollection.Count
        {
            get
            {
                return this.files.Count;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return (object)null;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public string TempDir
        {
            get
            {
                if (this.tempDir != null)
                    return this.tempDir;
                return string.Empty;
            }
        }

        public string BasePath
        {
            get
            {
                this.EnsureTempNameCreated();
                return this.basePath;
            }
        }

        private void EnsureTempNameCreated()
        {
            if (this.basePath != null)
                return;
            string path = (string)null;
            int num1 = 5000;
            bool flag;
            do
            {
                try
                {
                    this.basePath = TempFileCollection.GetTempFileName(this.TempDir);
                    new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetFullPath(this.basePath)).Demand();
                    path = this.basePath + ".tmp";
                    FileStream fileStream;
                    using (fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                        ;
                    flag = true;
                }
                catch (IOException ex)
                {
                    --num1;
                    uint num2 = 2147942480;
                    if (num1 == 0 || (long)Marshal.GetHRForException((Exception)ex) != (long)num2)
                        throw;
                    else
                        flag = false;
                }
            }
            while (!flag);
            this.files.Add((object)path, (object)this.keepFiles);
        }

        public bool KeepFiles
        {
            get
            {
                return this.keepFiles;
            }
            set
            {
                this.keepFiles = value;
            }
        }

        private bool KeepFile(string fileName)
        {
            object file = this.files[(object)fileName];
            if (file == null)
                return false;
            return (bool)file;
        }

        public void Delete()
        {
            if (this.files == null || this.files.Count <= 0)
                return;
            string[] strArray = new string[this.files.Count];
            this.files.Keys.CopyTo((Array)strArray, 0);
            foreach (string fileName in strArray)
            {
                if (!this.KeepFile(fileName))
                {
                    this.Delete(fileName);
                    this.files.Remove((object)fileName);
                }
            }
        }

        private void Delete(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch
            {
            }
        }

        private static string GetTempFileName(string tempDir)
        {
            if (string.IsNullOrEmpty(tempDir))
                tempDir = Path.GetTempPath();
            string withoutExtension = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            return !tempDir.EndsWith("\\", StringComparison.Ordinal) ? tempDir + "\\" + withoutExtension : tempDir + withoutExtension;
        }
    }
}
