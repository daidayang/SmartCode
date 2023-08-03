using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCode.Studio
{
    internal sealed class Preferences
    {
        private RecentFilesCollection m_recentProjects;
        private string lastDirectorySelected;
    
        internal int RecentProjectsCount;

        internal Preferences()
        {
            this.RecentProjectsCount = 4;
            this.lastDirectorySelected = "";
            this.m_recentProjects = new RecentFilesCollection(this.RecentProjectsCount);
        }

        public void Load()
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    this.m_recentProjects.Add(SmartCode.Studio.Utils.Configuration.GetRecentProjects(i));
                    continue;
                }
                catch 
                {
                    continue;
                }
            }
        }


        public void Save()
        {

            int i = 0;
            foreach (string file in this.m_recentProjects)
            {
                SmartCode.Studio.Utils.Configuration.SetRecentProjects(i, file);
                i += 1;
            }
            SmartCode.Studio.Utils.Configuration.LastDirectorySelected = lastDirectorySelected;
        }

        public RecentFilesCollection RecentProjects
        {
            get
            {
                return this.m_recentProjects;
            }
            set
            {
                this.m_recentProjects = value;
            }
        }

        public string LastDirectorySelected
        {
            get { return lastDirectorySelected; }
            set { lastDirectorySelected = value; }
        }
    }
}
