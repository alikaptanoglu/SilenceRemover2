using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using NAudio;

//NEXT:
/* DONE-Cut up video according to silencedict
 * DONE-Concat video again
 * DONE-slider controls
 * DB meter
 * Add preview
 * clean ui
 * add video player
 * */

namespace SilenceRemover2
{
    public partial class Form1 : Form
    {
        private string inputVidLocation = "";
        private string outputVidLocation = "";
        private List<string> tempFiles = new List<string>();
        private int currentTempIndex = -1;
        private float minDecibel = 90;
        private float sampleLength = 1;
        private Dictionary<int, bool> silenceDict = new Dictionary<int, bool>();
        private bool currConverting = false;
        private bool currPreviewing = false;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //clean up temp files
            foreach (string tempLoc in tempFiles)
            {
                try
                {
                    File.Delete(tempLoc);
                }
                catch { }
            }
        }

        private void decibelValue_Changed(object sender, EventArgs e)
        {
            NumericUpDown decibelPicker = sender as NumericUpDown;
            minDecibel = Convert.ToInt32(decibelPicker.Value);
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            handleFilePicker();
        }

        private void fileSaveButton_Click(object sender, EventArgs e)
        {
            handleFileSaver();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            sampleLength = (float)((sender as NumericUpDown).Value);
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (inputVidLocation == null)
            {
                textBlockBoye.Text = "Please choose a file first";
            }
            else
            {
                currConverting = true;
                //start processing
                textBlockBoye.Text = "Extracting audio...";
                var tsk = Task<int>.Factory.StartNew(() => audioExtractTask()).ContinueWith(a => audioParseCompleted(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            if (inputVidLocation == null)
            {
                textBlockBoye.Text = "Please choose a file first";
            }
            else
            {
                currPreviewing = true;
                //start processing
                textBlockBoye.Text = "Extracting audio...";
                var tsk = Task<int>.Factory.StartNew(() => audioExtractTask()).ContinueWith(a => audioParseCompleted(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void handleFilePicker()
        {
            var picker = new OpenFileDialog();
            picker.Filter = "mp4 file (*.mp4)|*.mp4|All files (*.*)|*.*";
            picker.FilterIndex = 1;
            picker.Title = "Select the video file";
            picker.Multiselect = false;
            if (picker.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    inputVidLocation = picker.FileName;
                    textBlockBoye.Text = inputVidLocation;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void handleFileSaver()
        {
            var picker = new SaveFileDialog();
            picker.Filter = "mp4 file (*.mp4)|*.mp4";
            picker.Title = "Save the video file";
            if (picker.ShowDialog() == DialogResult.OK && picker.FileName != "")
            {
                try
                {
                    outputVidLocation = picker.FileName;
                    textBlockBoye.Text = outputVidLocation;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }
            }
        }

        //processing from here onwards:
        private int audioExtractTask()
        {
            tempFiles.Add(Path.GetTempFileName());
            currentTempIndex++;
            if (File.Exists(tempFiles[currentTempIndex]))
            {
                File.Delete(tempFiles[currentTempIndex]);
            }
            var ffmpegProcess = new Process();
            ffmpegProcess.StartInfo.UseShellExecute = false;
            //ffmpegProcess.StartInfo.RedirectStandardInput = true;
            //ffmpegProcess.StartInfo.RedirectStandardOutput = true;
            //ffmpegProcess.StartInfo.RedirectStandardError = true;
            ffmpegProcess.StartInfo.CreateNoWindow = true;
            ffmpegProcess.StartInfo.FileName = "ffmpeg/ffmpeg.exe";
            ffmpegProcess.StartInfo.Arguments = " -i " + inputVidLocation + " -vn -f wav -ar 48000 " + tempFiles[currentTempIndex];
            ffmpegProcess.Start();
            //ffmpegProcess.StandardOutput.ReadToEnd();
            //ffmpegProcess.StandardError.ReadToEnd();
            ffmpegProcess.WaitForExit();
            if (!ffmpegProcess.HasExited)
            {
                ffmpegProcess.Kill();
            }
            return 1;
        }

        private int audioParseCompleted()
        {
            //now process audio
            //textBlockBoye.Text = "Processing audio...";
            textBlockBoye.Text = tempFiles[currentTempIndex];
            var tsk = Task<int>.Factory.StartNew(() => audioProcessTask()).ContinueWith(a => audioProcessCompleted(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            return 1;
        }

        //audio processing
        private int audioProcessTask()
        {
            silenceDict.Clear();
            using (NAudio.Wave.AudioFileReader wave = new NAudio.Wave.AudioFileReader(tempFiles[currentTempIndex]))
            {
                int samplesPerSecond = Convert.ToInt32(Math.Round(wave.WaveFormat.SampleRate * wave.WaveFormat.Channels * sampleLength)); ;
                var readBuffer = new float[samplesPerSecond];
                int samplesRead;
                int i = 1;
                do
                {
                    samplesRead = wave.Read(readBuffer, 0, samplesPerSecond);
                    if (samplesRead == 0) break;
                    var max = readBuffer.Take(samplesRead).Max();
                    if ((int)(max * 100) > minDecibel)
                        silenceDict.Add(i, false);
                    else
                        silenceDict.Add(i, true);

                    i++;
                } while (samplesRead > 0);
            }
            return 1;
        }

        private int audioProcessCompleted()
        {
            //now process audio
            int trues = 0;
            for (int i = 1; i <= silenceDict.Count; i++)
            {
                if (silenceDict[i]) { trues++; }
            }
            textBlockBoye.Text = "There are " + Convert.ToString(trues * sampleLength) + " seconds of silence";
            if (currConverting)
            {
                var tsk = Task<int>.Factory.StartNew(() => videoCutTask()).ContinueWith(a => videoCutComplete(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            if (currPreviewing)
            {
                var tsk = Task<int>.Factory.StartNew(() => previewTask()).ContinueWith(a => previewTaskComplete(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            return 1;
        }

        private int videoCutTask()
        {
            var startTime = new List<float>();
            var endTime = new List<float>();
            bool wasSilent = true;

            for (int i = 1; i <= silenceDict.Count; i++)
            {
                if (!silenceDict[i])
                {
                    if (wasSilent)
                    {
                        startTime.Add((i - 1) * sampleLength);
                    }
                    wasSilent = false;
                }
                else if (silenceDict[i])
                {
                    if (!wasSilent)
                    {
                        endTime.Add((i - 1) * sampleLength);
                    }
                    wasSilent = true;
                }
            }
            //now cut the video

            int originalTempIndex = currentTempIndex;
            string inputString = "";
            for (int i = 0; i < startTime.Count; i++)
            {
                tempFiles.Add(Path.GetTempPath() + Guid.NewGuid().ToString() + ".mp4");
                currentTempIndex++;
                //ffmpeg
                inputString = " -ss " + startTime[i] + " -i " + inputVidLocation + " -t " + Convert.ToString(endTime[i] - startTime[i]) + " -c copy " + tempFiles[currentTempIndex];

                var ffmpegProcess = new Process();
                ffmpegProcess.StartInfo.UseShellExecute = false;
                ffmpegProcess.StartInfo.CreateNoWindow = true;
                ffmpegProcess.StartInfo.FileName = "ffmpeg/ffmpeg.exe";
                ffmpegProcess.StartInfo.Arguments = inputString;
                ffmpegProcess.Start();
                ffmpegProcess.WaitForExit();
                if (!ffmpegProcess.HasExited)
                {
                    ffmpegProcess.Kill();
                }
            }
            //making input text file list
            tempFiles.Add(Path.GetTempFileName());
            currentTempIndex++;
            using (StreamWriter sw = File.CreateText(tempFiles[currentTempIndex]))
            {
                for (int i = originalTempIndex + 1; i < currentTempIndex; i++)
                {
                    sw.WriteLine("file '{0}'", tempFiles[i]);
                }
                sw.Close();
            }
            //concatenating
            inputString = "-f concat -safe 0 -i " + tempFiles[currentTempIndex] + " -c copy " + outputVidLocation;
            var ffmpegP = new Process();
            ffmpegP.StartInfo.UseShellExecute = false;
            ffmpegP.StartInfo.CreateNoWindow = true;
            ffmpegP.StartInfo.FileName = "ffmpeg/ffmpeg.exe";
            ffmpegP.StartInfo.Arguments = inputString;
            ffmpegP.Start();
            ffmpegP.WaitForExit();
            if (!ffmpegP.HasExited)
            {
                ffmpegP.Kill();
            }
            return 1;
        }

        private int videoCutComplete()
        {
            textBlockBoye.Text = "Succesfully finished!";
            return 1;
        }

        private int previewTask()
        {
            return 1;
        }

        private int previewTaskComplete()
        {
            //textBlockBoye.Text = "Succesfully generated preview!";
            return 1;
        }
    }
}