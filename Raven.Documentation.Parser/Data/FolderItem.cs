﻿namespace Raven.Documentation.Parser.Data
{
    public class FolderItem
    {
        public FolderItem(bool isFolder)
        {
            IsFolder = isFolder;
        }

	    public FolderItem(FolderItem item)
	    {
		    IsFolder = item.IsFolder;
		    Name = item.Name;
		    Description = item.Description;
		    Language = item.Language;
	    }

		public bool IsFolder { get; private set; }

	    public string Name { get; set; }

        public string Description { get; set; }

	    public Language Language { get; set; }
    }
}