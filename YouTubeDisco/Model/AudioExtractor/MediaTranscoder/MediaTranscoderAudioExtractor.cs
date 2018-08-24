using System;
using System.Threading.Tasks;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using Windows.Storage;

namespace YouTubeDisco.Model.AudioExtractor.MediaTranscoder
{
    public class MediaTranscoderAudioExtractor : IAudioExtractor
    {
        private const string Mp3Extension = ".mp3";
        private readonly MediaEncodingProfile _mediaEncodingProfile =
            MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
        private readonly Windows.Media.Transcoding.MediaTranscoder _transcoder = new Windows.Media.Transcoding.MediaTranscoder();

        public async Task<ExtractionResult> ExtractAudio(StorageFile videoFile, StorageFolder outputFolder)
        {
            var outputFile = await outputFolder.CreateFileAsync(videoFile.DisplayName + Mp3Extension, CreationCollisionOption.GenerateUniqueName);
            var prepareOp = await _transcoder.PrepareFileTranscodeAsync(videoFile, outputFile, _mediaEncodingProfile);
            if (prepareOp.CanTranscode)
            {
                await prepareOp.TranscodeAsync();
                return ExtractionResult.Success;
            }

            if (prepareOp.FailureReason == TranscodeFailureReason.CodecNotFound)
            {
                return ExtractionResult.CodecNotFound;
            }

            return ExtractionResult.Failed;
        }

    }
}