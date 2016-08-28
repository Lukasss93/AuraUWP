using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;

namespace AuraUWP.Media
{
    public class MicRecorder
    {
        private MediaCapture mediaCaptureManager;
        private StorageFile recordStorageFile;

        private bool isAudioInizialized = false;
        private MicRecorderStatus status = MicRecorderStatus.NotRecording;

        #region TIMER

        private DispatcherTimer timer;
        private int time;

        private int Time
        {
            get
            {
                return this.time;
            }

            set
            {
                if(this.Time != value)
                {
                  Object old = this.time;
                  this.time = value;
                  OnPropertyChange(this, new TimeChangedEventArgs("Time", value));
                }
            }
        }

        public delegate void PropertyChangeHandler(object sender, TimeChangedEventArgs e);
        public event PropertyChangeHandler TimeChanged;
        protected void OnPropertyChange(object sender, TimeChangedEventArgs e)
        {
            if(TimeChanged != null)
            {
                TimeChanged(this, e);
            }
        }

        private void timer_Tick(object sender, object e)
        {
            Time++;
        }

        #endregion

        

        private async void _mediaCaptureManager_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            await Stop(errorEventArgs.Message);
        }

        private async void _mediaCaptureManager_RecordLimitationExceeded(MediaCapture sender)
        {
            await Stop("limitation_error");
        }

        
        public MicRecorder()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            Time = 0;
        }

        public async Task InitializeAudioRecording()
        {
            if(isAudioInizialized)
            {
                throw new MicRecorderException("InitializeAudioRecording is already inizialized.");
            }
            else
            {
                try
                {
                    mediaCaptureManager = new MediaCapture();

                    var settings = new MediaCaptureInitializationSettings();
                    settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
                    settings.MediaCategory = MediaCategory.Other;
                    settings.AudioProcessing = AudioProcessing.Default;

                    await mediaCaptureManager.InitializeAsync(settings);


                    mediaCaptureManager.RecordLimitationExceeded += _mediaCaptureManager_RecordLimitationExceeded;
                    mediaCaptureManager.Failed += _mediaCaptureManager_Failed;

                    isAudioInizialized = true;
                }
                catch(UnauthorizedAccessException e1)
                {
                    throw new MicRecorderException("Please add Microphone in capatibilies tab.", e1);
                }
                catch(Exception e2)
                {
                    throw new MicRecorderException("Internal Exception", e2);
                }
            }            
        }

        public async Task Start(StorageFolder folder, string filename)
        {
            if(!isAudioInizialized)
            {
                throw new MicRecorderException("InitializeAudioRecording is not inizialized.");
                
            }
            else if(status == MicRecorderStatus.Recording)
            {
                throw new MicRecorderException("MicRecorder is already recording.");
            }
            else
            {
                try
                {
                    timer.Start();
                    recordStorageFile = await folder.CreateFileAsync(filename + ".m4a", CreationCollisionOption.GenerateUniqueName);
                    MediaEncodingProfile recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);
                    await mediaCaptureManager.StartRecordToStorageFileAsync(recordProfile, this.recordStorageFile);

                    status = MicRecorderStatus.Recording;
                }
                catch(Exception e)
                {
                    throw new MicRecorderException("Internal Exception", e);
                }
            }            
        }

        public async Task Stop(string er=null)
        {
            if(!isAudioInizialized)
            {
                throw new MicRecorderException("InitializeAudioRecording is not inizialized.");
            }
            else if(status == MicRecorderStatus.NotRecording)
            {
                throw new MicRecorderException("MicRecorder is not recording.");
            }
            else
            {
                try
                {
                    timer.Stop();
                    await mediaCaptureManager.StopRecordAsync();
                    
                    status = MicRecorderStatus.NotRecording;
                }
                catch(Exception e)
                {
                    throw new MicRecorderException("Internal Exception", e);
                }
            }

            if(er!=null)
            {
                throw new MicRecorderException(er);
            }            
        }

        public MicRecorderStatus GetStatus()
        {
            return status;
        }
    }

    public class TimeChangedEventArgs : EventArgs
    {
        public string PropertyName { get; internal set; }
        public int Time { get; internal set; }

        public TimeChangedEventArgs(string propertyName, int time)
        {
            this.PropertyName = propertyName;
            this.Time = time;
        }
    }

    public class MicRecorderException : Exception
    {
        public MicRecorderException() { }

        public MicRecorderException(string message) : base(message) { }

        public MicRecorderException(string message, Exception inner) : base(message, inner) { }
    }

    public enum MicRecorderStatus
    {
        Recording,
        NotRecording
    }
}
