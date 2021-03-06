﻿using System.Threading.Tasks;
using Windows.Storage;

namespace YouTubeDisco.Model.AudioExtractor
{
    public interface IAudioExtractor
    {
        Task<ExtractionResult> ExtractAudio(StorageFile videoFile, StorageFolder outputFolder);
    }
}