namespace SmartCode.Studio
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal sealed class RecentFilesCollection : CollectionBase
    {
        private int m_maxFiles;

        public RecentFilesCollection(int maxFiles)
        {
            this.m_maxFiles = maxFiles;
        }

        public void Add(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ArrayList list = new ArrayList();
                foreach (string file in this)
                {
                    if (string.Compare(file, value, true) == 0)
                    {
                        list.Add(file);
                    }
                }
                foreach (string file in list)
                {
                    this.Remove(file);
                }
                if (base.Count == this.m_maxFiles)
                {
                    base.RemoveAt(0);
                }
                this.List.Add(value);
            }
        }

        public void Remove(string value)
        {
            this.List.Remove(value);
        }

        public string this[int index]
        {
            get
            {
                return (string)this.List[index];
            }
        }
    }
}

