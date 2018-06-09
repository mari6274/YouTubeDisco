﻿using System;
using System.Collections.Generic;

namespace YouTubeDisco.Model.SearchEngine.YouTube
{
    public class SearchListResponse
    {
        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
        public List<Resource> Items { get; set; }
    }

    public class Resource
    {
        public Id Id { get; set; }
        public Snippet Snippet { get; set; }
    }

    public class ThumbnailsSet
    {
        public Thumbnail Default { get; set; }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
    }

    public class Id
    {
        public string VideoId { get; set; }
    }

    public class Snippet
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ThumbnailsSet Thumbnails { get; set; }
    }
}