﻿using System;
using Windows.Devices.Printers;

namespace YouTubeDisco.Model.SearchEngine
{
    public class SearchResult
    {
        public SearchResult(string title, string description, string url)
        {
            Title = title;
            Description = description;
            Url = url;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}