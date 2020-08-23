using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace YTPPlus
{
    public class YTPGenerator
    {
        public double MAX_STREAM_DURATION = 0.4; //default: 2s
        public double MIN_STREAM_DURATION = 0.2; //default: 0.2s
        public int MAX_CLIPS = 20; //default: 5 clips
        public string INPUT_FILE; //Input video file
        public string OUTPUT_FILE; //the video file that will be produced in the end

        public bool failed = false; //if the application has failed
        public Exception exc; //set when the application fails to give more information

        public bool effect1;
        public bool effect2;
        public bool effect3;
        public bool effect4;
        public bool effect5;
        public bool effect6;
        public bool effect7;
        public bool effect8;
        public bool effect9;
        public bool effect10;
        public bool effect11;
        public bool effect12;
        public bool effect13;
        public bool effect14;
        public bool effect15;
        public bool effect16;
        public bool effect17;
        public bool effect18;
        public bool effect19;
        public bool effect20;
        public bool effect21;
        public bool effect22;
        public bool effect23;
        public bool effect24;
        public bool effect25;

        public bool pluginTest = false;
        public int pluginCount = 0;
        public List<string> plugins = new List<string>();

        public bool insertTransitionClips;

        public int width = 640;
        public int height = 480;
        public bool intro = false;
        public bool outro = false;

        public Utilities toolBox = new Utilities();

        EffectsFactory effectsFactory;
        ArrayList sourceList = new ArrayList();
        public bool done = false;
        public decimal doneCount = 0;

        public YTPGenerator(string output)
        {
            this.OUTPUT_FILE = output;
        }
        public void setMaxClips(int clips)
        {
            this.MAX_CLIPS = clips;
        }
        public void setMinDuration(double min)
        {
            this.MIN_STREAM_DURATION = min;
        }
        public void setMaxDuration(double max)
        {
            this.MAX_STREAM_DURATION = max;
        }

        public void addSource(String sourceName)
        {
            sourceList.Add(sourceName);
        }
        public BackgroundWorker vidThreadWorker = new BackgroundWorker();

        public void vidThread(object sender, DoWorkEventArgs e)
        {
            if (sourceList.Count == 0)
            {
                Console.WriteLine("No sources added...");
                return;
            }

            if (File.Exists(OUTPUT_FILE))
                File.Delete(OUTPUT_FILE);

            try
            {
                Directory.CreateDirectory(toolBox.TEMP);
                int endofclips = MAX_CLIPS - 1;
                failed = false;
                for (int i = 0; i < MAX_CLIPS; i++)
                {
                    if (i == 0 && intro)
                    {
                        MAX_CLIPS++;
                        Console.WriteLine("Intro clip enabled, adding 1 to max clips. New max clips is " + MAX_CLIPS + ".");
                        Console.WriteLine("Done: " + Decimal.Divide(i, MAX_CLIPS));
                        vidThreadWorker.ReportProgress(Convert.ToInt32(Decimal.Divide(i, MAX_CLIPS) * 100, new CultureInfo("en-US")));
                        Console.WriteLine(toolBox.intro);
                        Console.WriteLine("STARTING CLIP " + "video" + i);
                        toolBox.copyVideo(toolBox.intro, toolBox.TEMP + "video" + i, width, height);
                    }
                    else
                    {
                        Console.WriteLine("Done: " + Decimal.Divide(i, MAX_CLIPS));
                        vidThreadWorker.ReportProgress(Convert.ToInt32(Decimal.Divide(i, MAX_CLIPS) * 100, new CultureInfo("en-US")));
                        string sourceToPick = sourceList[randomInt(0, sourceList.Count - 1)].ToString();
                        Console.WriteLine(sourceToPick);
                        
                        decimal source = decimal.Parse(toolBox.getLength(sourceToPick), NumberStyles.Any, new CultureInfo("en-US"));
                        string output = source.ToString("0.#########################", new CultureInfo("en-US"));
                        Console.WriteLine(toolBox.getLength(sourceToPick) + " -> " + output + " -> " + double.Parse(output, NumberStyles.Any, new CultureInfo("en-US")));
                        double boy = double.Parse(output, NumberStyles.Any, new CultureInfo("en-US"));
                        Console.WriteLine(boy);
                        Console.WriteLine("STARTING CLIP " + "video" + i);
                        double startOfClip = randomDouble(0.0, boy - MAX_STREAM_DURATION);
                        //Console.WriteLine("boy seconds = "+  boy.getLengthSec());
                        double endOfClip = startOfClip + randomDouble(MIN_STREAM_DURATION, MAX_STREAM_DURATION);
                        Console.WriteLine("Beginning of clip " + i + ": " + startOfClip.ToString("0.#########################", new CultureInfo("en-US")));
                        Console.WriteLine("Ending of clip " + i + ": " + endOfClip.ToString("0.#########################", new CultureInfo("en-US")) + ", in seconds: ");
                        if (randomInt(0, 15) == 15 && insertTransitionClips == true)
                        {
                            Console.WriteLine("Tryina use a diff source");
                            toolBox.copyVideo(toolBox.SOURCES + effectsFactory.pickSource(), toolBox.TEMP + "video" + i, width, height);
                        }
                        else
                        {
                            toolBox.snipVideo(sourceToPick, startOfClip, endOfClip, toolBox.TEMP + "video" + i, width, height);
                        }
                        //Add a random effect to the video
                        int effect = 0;
                        if (pluginTest)
                            effect = 27;
                        else
                        {
                            effect = giveProbability(0, 26 + pluginCount);
                            if (effect > 0)
                            Console.WriteLine("STARTING EFFECT ON CLIP " + i + " EFFECT" + effect);
                        }
                        String clipToWorkWith = toolBox.TEMP + "video" + i + ".mp4";
                        switch (effect)
                        {
                            case 1:
                                if (effect1 == true)
                                    effectsFactory.effect_RandomSound(clipToWorkWith);
                                break;
                            case 2:
                                if (effect2 == true)
                                    effectsFactory.effect_RandomSoundMute(clipToWorkWith);
                                break;
                            case 3:
                                if (effect3 == true)
                                    effectsFactory.effect_Reverse(clipToWorkWith);
                                break;
                            case 4:
                                if (effect4 == true)
                                    effectsFactory.effect_SpeedUp(clipToWorkWith);
                                break;
                            case 5:
                                if (effect5 == true)
                                    effectsFactory.effect_SlowDown(clipToWorkWith);
                                break;
                            case 6:
                                if (effect6 == true)
                                    effectsFactory.effect_Chorus(clipToWorkWith);
                                break;
                            case 7:
                                if (effect7 == true)
                                    effectsFactory.effect_Vibrato(clipToWorkWith);
                                break;
                            case 8:
                                if (effect8 == true)
                                    effectsFactory.effect_Tremolo(clipToWorkWith);
                                break;
                            case 9:
                                if (effect9 == true)
                                    effectsFactory.effect_Earrape(clipToWorkWith);
                                break;
                            case 10:
                                if (effect10 == true)
                                    effectsFactory.effect_SpeedUpHighPitch(clipToWorkWith);
                                break;
                            case 11:
                                if (effect11 == true)
                                    effectsFactory.effect_SlowDownLowPitch(clipToWorkWith);
                                break;
                            case 12:
                                if (effect12 == true)
                                    effectsFactory.effect_HighPitch(clipToWorkWith);
                                break;
                            case 13:
                                if (effect13 == true)
                                    effectsFactory.effect_LowPitch(clipToWorkWith);
                                break;
                            case 14:
                                if (effect14 == true)
                                    effectsFactory.effect_ForwardReverse(clipToWorkWith);
                                break;
                            case 15:
                                if (effect15 == true)
                                    effectsFactory.effect_ReverseForward(clipToWorkWith);
                                break;
                            case 16:
                                if (effect16 == true)
                                    effectsFactory.effect_Pixelate(clipToWorkWith, width, height);
                                break;
                            case 17:
                                if (effect17 == true)
                                    effectsFactory.effect_BadQuality(clipToWorkWith, width, height);
                                break;
                            case 18:
                                if (effect18 == true)
                                    effectsFactory.effect_Emboss(clipToWorkWith);
                                break;
                            case 19:
                                if (effect19 == true)
                                    effectsFactory.effect_SymmetryHorizontal1(clipToWorkWith);
                                break;
                            case 20:
                                if (effect20 == true)
                                    effectsFactory.effect_SymmetryHorizontal2(clipToWorkWith);
                                break;
                            case 21:
                                if (effect21 == true)
                                    effectsFactory.effect_SymmetryVertical1(clipToWorkWith);
                                break;
                            case 22:
                                if (effect22 == true)
                                    effectsFactory.effect_SymmetryVertical2(clipToWorkWith);
                                break;
                            case 23:
                                if (effect23 == true)
                                    effectsFactory.effect_GMajor(clipToWorkWith);
                                break;
                            case 24:
                                if (effect24 == true)
                                    effectsFactory.effect_Dance(clipToWorkWith);
                                break;
                            case 25:
                                if (effect25 == true)
                                    effectsFactory.effect_Squidward(clipToWorkWith, width, height);
                                break;
                            default:
                                if (effect >= 27 && effect <= 26+pluginCount)
                                    effectsFactory.effect_Plugin(clipToWorkWith, width, height, plugins[rnd.Next(plugins.Count)], startOfClip, endOfClip);
                                break;
                        }
                    }
                }
                if (outro)
                {
                    Console.WriteLine("Outro clip enabled.");
                    Console.WriteLine("Done: " + Decimal.Divide(MAX_CLIPS - 1, MAX_CLIPS));
                    vidThreadWorker.ReportProgress(Convert.ToInt32(Decimal.Divide(MAX_CLIPS - 1, MAX_CLIPS) * 100, new CultureInfo("en-US")));
                    Console.WriteLine(toolBox.outro);
                    Console.WriteLine("STARTING CLIP " + "video" + MAX_CLIPS);
                    toolBox.copyVideo(toolBox.outro, toolBox.TEMP + "video" + MAX_CLIPS, width, height);
                    MAX_CLIPS++;
                }
                toolBox.concatenateVideo(MAX_CLIPS, OUTPUT_FILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                exc = ex;
                failed = true;
            }
            cleanUp();
            rmDir(toolBox.TEMP);
        }
        void vidThreadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke :)
            // Update the progressBar with the integer supplied to us from the
            // ReportProgress() function.
            doneCount = e.ProgressPercentage;
        }
        void vidThreadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            done = true;
        }
        public YTPGenerator go(ProgressChangedEventHandler progressReporter, RunWorkerCompletedEventHandler completedReporter)
        {
            effectsFactory = new EffectsFactory(toolBox); //hacky but works
            Console.WriteLine("My FFMPEG is: " + toolBox.FFMPEG);
            Console.WriteLine("My FFPROBE is: " + toolBox.FFPROBE);
            Console.WriteLine("My MAGICK is: " + toolBox.MAGICK);
            Console.WriteLine("My TEMP is: " + toolBox.TEMP);
            Console.WriteLine("My SOUNDS is: " + toolBox.SOUNDS);
            Console.WriteLine("My SOURCES is: " + toolBox.SOURCES);
            Console.WriteLine("My MUSIC is: " + toolBox.MUSIC);
            Console.WriteLine("My RESOURCES is: " + toolBox.RESOURCES);
            vidThreadWorker.DoWork += new DoWorkEventHandler(vidThread);
            vidThreadWorker.WorkerReportsProgress = true;
            vidThreadWorker.WorkerSupportsCancellation = true;
            vidThreadWorker.ProgressChanged += progressReporter;
            vidThreadWorker.RunWorkerCompleted += completedReporter;
            vidThreadWorker.RunWorkerAsync();
            return this;
        }

        public bool isDone()
        {
            return done;
        }
        public static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
        }
        public Random rnd = new Random();
        public double randomDouble(double min, double max)
        {
            double finalVal = -1;
            while (finalVal < 0)
            {
                double x = (rnd.NextDouble() * ((max - min) + 0)) + min;
                finalVal = Math.Round(x * 100.0) / 100.0;
            }
            return finalVal;
        }
        public int randomInt(int min, int max)
        {
            return rnd.Next((max - min) + 1) + min;
            //return new Random((int)GetUnixEpoch(DateTime.UtcNow)).Next((max - min) + 1) + min;
        }
        public int giveProbability(int min, int max)
        {
            return rnd.Next((max - min) + 1) + min;
        }

        public void cleanUp()
        {
            if (File.Exists(toolBox.TEMP + "temp.mp4"))
                File.Delete(toolBox.TEMP + "temp.mp4");
            for (int i = 0; i < MAX_CLIPS; i++)
            {
                if (File.Exists(toolBox.TEMP + "video" + i + ".mp4"))
                {
                    Console.WriteLine(i + " Exists");
                    File.Delete(toolBox.TEMP + "video" + i + ".mp4");
                }
            }

        }
        public void rmDir(string file)
        {
            foreach (string fi in Directory.GetFiles(file))
            {
                File.Delete(fi);
            }
            Directory.Delete(file);
        }

    }
}
