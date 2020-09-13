using System;
using System.Globalization;
using System.IO;
using System.Threading;
/**
* TimeStamp class for YTP+
*
* @author benb
* @author LimeQuartz
*/
namespace YTPPlus
{
    public class EffectsFactory
    {
        public Utilities toolBox;
        public Random rnd = new Random();

        public EffectsFactory(Utilities utilities)
        {
            this.toolBox = utilities;
        }
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
        public string pickSound()
        {
            string[] d = Directory.GetFiles(toolBox.SOUNDS, "*.mp3");
            FileInfo file = new FileInfo(d[randomInt(0, d.Length - 1)]);
            return file.Name;
        }
        public string pickSource()
        {
            string[] d = Directory.GetFiles(toolBox.SOURCES, "*.mp4");
            Console.WriteLine(d.Length);
            FileInfo file = new FileInfo(d[randomInt(0, d.Length - 1)]);
            return file.Name;
        }
        public string pickMusic()
        {
            string[] d = Directory.GetFiles(toolBox.MUSIC, "*.mp3");
            FileInfo file = new FileInfo(d[randomInt(0, d.Length - 1)]);
            return file.Name;
        }
        public string pickSquidwardMusic()
        {
            string[] d = Directory.GetFiles(toolBox.RESOURCES + "squidward", "*.wav");
            FileInfo file = new FileInfo(d[randomInt(0, d.Length - 1)]);
            return file.Name;
        }
        /* EFFECTS */
        public void effect_RandomSound(string video)
        {
            Console.WriteLine("effect_RandomSound initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                string randomSound = pickSound();

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -i \"" + toolBox.SOUNDS + randomSound + "\" -c:v copy -filter_complex \"[1:a]apad[A];[0:a][A]amerge[out]\" -ar 44100 -ac 2 -map 0:v -map [out] -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_RandomSoundMute(string video)
        {
            Console.WriteLine("effect_RandomSoundMute initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);

                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                
                string randomSound = pickSound();
                string soundLength = toolBox.getLength(toolBox.SOUNDS + randomSound);
                
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -i \"" + toolBox.SOUNDS + randomSound + "\" -map 0:v -map 1:a -c:v copy -ar 44100 -ac 2 -to " + soundLength + " -shortest -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Reverse(string video)
        {
            Console.WriteLine("effect_Reverse initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);

                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf reverse -af areverse -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }
        
        public void effect_SpeedUp(string video)
        {
            Console.WriteLine("effect_SpeedUp initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=0.5*PTS -af atempo=2 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SlowDown(string video)
        {
            Console.WriteLine("effect_SlowDown initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=2*PTS -af atempo=.5 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Chorus(string video)
        {
            Console.WriteLine("effect_Chorus initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af chorus=.5:.9:50|60|40:.4|.32|.3:.25|.4|.3:2|2.3|1.3 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Vibrato(string video)
        {
            Console.WriteLine("effect_Vibrato initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af vibrato=f=7:d=.5 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Tremolo(string video)
        {
            Console.WriteLine("effect_Tremolo initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af tremolo=f=10:d=.7 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Earrape(string video)
        {
            Console.WriteLine("effect_Earrape initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af acrusher=.1:1:64:0:log -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SpeedUpHighPitch(string video)
        {
            Console.WriteLine("effect_SpeedUpHighPitch initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=.5*PTS -af asetrate=88200,aresample=44100 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SlowDownLowPitch(string video)
        {
            Console.WriteLine("effect_SlowDownLowPitch initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=2*PTS -af asetrate=22050,aresample=44100 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_HighPitch(string video)
        {
            Console.WriteLine("effect_HighPitch initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af asetrate=88200,aresample=44100,atempo=.5 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_LowPitch(string video)
        {
            Console.WriteLine("effect_LowPitch initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:v copy -af asetrate=22050,aresample=44100,atempo=2 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_ForwardReverse(string video)
        {
            Console.WriteLine("effect_ForwardReverse initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -filter_complex \"[0:v]reverse[vid];[0:a]areverse[aud];[0:v][0:a][vid][aud]concat=n=2:v=1:a=1[outv][outa]\" -map [outv] -map [outa] -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_ReverseForward(string video)
        {
            Console.WriteLine("effect_ReverseForward initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -filter_complex \"[0:v]reverse[vid];[0:a]areverse[aud];[vid][aud][0:v][0:a]concat=n=2:v=1:a=1[outv][outa]\" -map [outv] -map [outa] -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Pixelate(string video, int width, int height)
        {
            Console.WriteLine("effect_Pixelate initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -sws_flags neighbor -vf scale=iw/32:ih/32 -s " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + " -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_BadQuality(string video, int width, int height)
        {
            Console.WriteLine("effect_BadQuality initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf scale=-2:36 -s " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + " -af aresample=7350,aresample=44100 -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Emboss(string video)
        {
            Console.WriteLine("effect_Emboss initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -vf convolution=\"-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2\" -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SymmetryHorizontal1(string video)
        {
            Console.WriteLine("effect_SymmetryHorizontal1 initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -vf \"transpose=1,split[main][tmp];[tmp]crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2,transpose=2\" -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SymmetryHorizontal2(string video)
        {
            Console.WriteLine("effect_SymmetryHorizontal2 initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -vf \"hflip,transpose=1,split[main][tmp];[tmp]crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2,transpose=2\" -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SymmetryVertical1(string video)
        {
            Console.WriteLine("effect_SymmetryVertical1 initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -vf \"split[main][tmp];[tmp]crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2\" -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SymmetryVertical2(string video)
        {
            Console.WriteLine("effect_SymmetryVertical2 initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -c:a copy -vf \"vflip,split[main][tmp];[tmp]crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2\" -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_GMajor(string video)
        {
            Console.WriteLine("effect_GMajor initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -filter_complex \"[0:a]asetrate=22050,aresample=44100,atempo=2[lowc];[0:a]asetrate=33037.671045130824,aresample=44100,atempo=1.3348398541700344[lowg];[0:a]asetrate=55562.51830036391,aresample=44100,atempo=.7937005259840998[e];[0:a]asetrate=66075.34209026165,aresample=44100,atempo=.6674199270850172[g];[0:a]asetrate=88200,aresample=44100,atempo=.5[highc];[0:a][lowc][lowg][e][g][highc]amix=inputs=6[aud]\" -map 0:v -map [aud] -vf negate -y \"" + video + "\"";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
                File.Delete(temp);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Dance(string video)
        {
            Console.WriteLine("effect_Dance initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                string temp2 = toolBox.TEMP + "temp2.mp4";
                string temp3 = toolBox.TEMP + "temp3.mp4";

                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                if (File.Exists(temp2))
                {
                    File.Delete(temp2);
                }
                if (File.Exists(temp3))
                {
                    File.Delete(temp3);
                }

                string randomSound = pickMusic();

                string[] commands = new string[3];
                double randomTime = randomDouble(0.2, 1.0);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -an -vf setpts=.5*PTS -t " + randomTime + " -y \"" + toolBox.TEMP + "temp2.mp4\"", 0);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp2.mp4\" -vf reverse -y \"" + toolBox.TEMP + "temp3.mp4\"", 1);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp3.mp4\" -i \"" + toolBox.TEMP + "temp2.mp4\" -i \"" + toolBox.MUSIC + randomSound + "\" -filter_complex \"[0:v][1:v][0:v][1:v][0:v][1:v][0:v][1:v]concat=n=8:v=1[out]\" -map [out] -map 2:a -shortest -y \"" + video + "\"", 2);

                int exitValue;
                foreach (string cmd in commands)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = toolBox.FFMPEG;
                    startInfo.Arguments = cmd;
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    process.StartInfo = startInfo;
                    process.Start();
                    // Read stderr synchronously (on another thread)

                    string errorText = null;
                    var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                    stderrThread.Start();

                    // Read stdout synchronously (on this thread)

                    while (true)
                    {
                        var line = process.StandardOutput.ReadLine();
                        if (line == null)
                            break;

                        Console.WriteLine(line);
                    }

                    process.WaitForExit();
                    stderrThread.Join();
                    exitValue = process.ExitCode;
                }

                File.Delete(temp);
                File.Delete(temp2);
                File.Delete(temp3);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Squidward(string video, int width, int height)
        {
            Console.WriteLine("effect_Squidward initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";

                string[] commands = new string[8];
                string[] args = new string[8];
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }

                commands.SetValue(toolBox.FFMPEG, 0);
                args.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -an -vf \"select=gte(n\\,1)\" -vframes 1 -y \"" + toolBox.TEMP + "squidward0.png\"", 0);

                for (int i = 1; i <= 5; i++)
                {
                    string effect = "";
                    int random = randomInt(0, 38);
                    switch (random)
                    {
                        case 0:
                            effect = "flop";
                            break;
                        case 1:
                            effect = "flip";
                            break;
                        case 2:
                            effect = "rotate 180";
                            break;
                        case 3:
                            effect = "implode -" + randomInt(1, 3);
                            break;
                        case 4:
                            effect = "implode " + randomInt(1, 3);
                            break;
                        case 5:
                            effect = "swirl " + randomInt(1, 180);
                            break;
                        case 6:
                            effect = "swirl -" + randomInt(1, 180);
                            break;
                        case 7:
                            effect = "channel RGB -negate";
                            break;
                        case 8:
                            effect = "flip -implode -" + randomInt(1, 3);
                            break;
                        case 9:
                            effect = "flop -implode -" + randomInt(1, 3);
                            break;
                        case 10:
                            effect = "rotate 180 -implode -" + randomInt(1, 3);
                            break;
                        case 11:
                            effect = "flip -implode " + randomInt(1, 3);
                            break;
                        case 12:
                            effect = "flop -implode " + randomInt(1, 3);
                            break;
                        case 13:
                            effect = "rotate 180 -implode " + randomInt(1, 3);
                            break;
                        case 14:
                            effect = "flip -swirl " + randomInt(1, 180);
                            break;
                        case 15:
                            effect = "flop -swirl " + randomInt(1, 180);
                            break;
                        case 16:
                            effect = "rotate 180 -swirl " + randomInt(1, 180);
                            break;
                        case 17:
                            effect = "flip -swirl -" + randomInt(1, 180);
                            break;
                        case 18:
                            effect = "flop -swirl -" + randomInt(1, 180);
                            break;
                        case 19:
                            effect = "rotate 180 -swirl -" + randomInt(1, 180);
                            break;
                        case 20:
                            effect = "flip -channel RGB -negate";
                            break;
                        case 21:
                            effect = "flop -channel RGB -negate";
                            break;
                        case 22:
                            effect = "rotate 180 -channel RGB -negate";
                            break;
                        case 23:
                            effect = "implode -" + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 24:
                            effect = "implode " + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 25:
                            effect = "swirl " + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 26:
                            effect = "swirl -" + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 27:
                            effect = "flip -implode -" + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 28:
                            effect = "flop -implode -" + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 29:
                            effect = "rotate 180 -implode -" + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 30:
                            effect = "flip -implode " + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 31:
                            effect = "flop -implode " + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 32:
                            effect = "rotate 180 -implode " + randomInt(1, 3) + " -channel RGB -negate";
                            break;
                        case 33:
                            effect = "flip -swirl " + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 34:
                            effect = "flop -swirl " + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 35:
                            effect = "rotate 180 -swirl " + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 36:
                            effect = "flip -swirl -" + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 37:
                            effect = "flop -swirl -" + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                        case 38:
                            effect = "rotate 180 -swirl -" + randomInt(1, 180) + " -channel RGB -negate";
                            break;
                    }
                    commands.SetValue(toolBox.MAGICK, i);
                    args.SetValue("convert \"" + toolBox.TEMP + "squidward0.png\" -" + effect + " \"" + toolBox.TEMP + "squidward" + i + ".png\"", i);
                }
                commands.SetValue(toolBox.MAGICK, 6);
                args.SetValue("convert -size " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + " canvas:black \"" + toolBox.TEMP + "black.png\"", 6);

                if (File.Exists(toolBox.TEMP + "concatsquidward.txt"))
                    File.Delete(toolBox.TEMP + "concatsquidward.txt");
                StreamWriter writer = new StreamWriter(toolBox.TEMP + "concatsquidward.txt");
                writer.Write("file '" + toolBox.TEMP + "squidward0.png'\nduration 0.467\nfile '" + toolBox.TEMP + "squidward1.png'\nduration 0.434\nfile '" + toolBox.TEMP + "squidward2.png'\nduration 0.4\nfile '" + toolBox.TEMP + "black.png'\nduration 0.834\nfile '" + toolBox.TEMP + "squidward3.png'\nduration 0.467\nfile '" + toolBox.TEMP + "squidward4.png'\nduration 0.4\nfile '" + toolBox.TEMP + "squidward5.png'\nduration 0.467");
                writer.Close();
                string randomSound = pickSquidwardMusic();
                commands.SetValue(toolBox.FFMPEG, 7);
                args.SetValue("-f concat -safe 0 -i \"" + toolBox.TEMP + "concatsquidward.txt\" -i \"" + toolBox.RESOURCES + "squidward\\" + randomSound + "\" -pix_fmt yuv420p -y \"" + video + "\"", 7);

                int exitValue;
                for (int i = 0; i < commands.Length; i++)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = commands[i];
                    startInfo.Arguments = args[i];
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    process.StartInfo = startInfo;
                    process.Start();
                    // Read stderr synchronously (on another thread)

                    string errorText = null;
                    var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                    stderrThread.Start();

                    // Read stdout synchronously (on this thread)

                    while (true)
                    {
                        var line = process.StandardOutput.ReadLine();
                        if (line == null)
                            break;

                        Console.WriteLine(line);
                    }

                    process.WaitForExit();
                    stderrThread.Join();
                    exitValue = process.ExitCode;
                }

                File.Delete(temp);
                for (int i = 0; i <= 5; i++)
                {
                    File.Delete(toolBox.TEMP + "squidward" + i + ".png");
                }
                File.Delete(toolBox.TEMP + "black.png");
                File.Delete(toolBox.TEMP + "concatsquidward.txt");
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_SpartaRemix(string video, int width, int height)
        {
            Console.WriteLine("effect_SpartaRemix initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";
                string temp2 = toolBox.TEMP + "sparta1.mp4";
                string temp3 = toolBox.TEMP + "sparta2.mp4";
                string temp4 = toolBox.TEMP + "sparta3.mp4";
                string temp5 = toolBox.TEMP + "spartavideo.mp4";

                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                if (File.Exists(temp2))
                {
                    File.Delete(temp2);
                }
                if (File.Exists(temp3))
                {
                    File.Delete(temp3);
                }
                if (File.Exists(temp4))
                {
                    File.Delete(temp4);
                }
                if (File.Exists(temp5))
                {
                    File.Delete(temp5);
                }

                string[] commands = new string[5];

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -ss .03 -t .1 -af volume=18 -y \"" + toolBox.TEMP + "sparta1.mp4\"", 0);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -ss .05 -t .05 -af volume=18 -y \"" + toolBox.TEMP + "sparta2.mp4\"", 1);

                commands.SetValue("-f lavfi -i anullsrc=channel_layout=stereo:sample_rate=44100 -f lavfi -i color=c=black:s=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ":r=5 -t .05 -y \"" + toolBox.TEMP + "sparta3.mp4\"", 2);

                commands.SetValue("-i \"" + toolBox.TEMP + "sparta1.mp4\" -i \"" + toolBox.TEMP + "sparta2.mp4\" -i \"" + toolBox.TEMP + "sparta3.mp4\" -filter_complex \"[0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][2:v][2:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][2:v][2:a][0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][0:v][0:a][2:v][2:a][0:v][0:a][2:v][2:a][0:v][0:a][2:v][2:a][0:v][0:a][0:v][0:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][1:v][1:a][2:v][2:a][1:v][1:a][1:v][1:a][2:v][2:a]concat=n=86:v=1:a=1[outv][outa]\" -map [outv] -map [outa] -y \"" + toolBox.TEMP + "spartavideo.mp4\"", 3);

                commands.SetValue("-i \"" + toolBox.TEMP + "spartavideo.mp4\" -i \"" + toolBox.RESOURCES + "spartaremix.mp3\" -c:v copy -filter_complex \"[0:a][1:a]amix=inputs=2[aud]\" -map 0:v -map [aud] -shortest -y \"" + video + "\"", 4);

                int exitValue;
                foreach (string cmd in commands)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = toolBox.FFMPEG;
                    startInfo.Arguments = cmd;
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    process.StartInfo = startInfo;
                    process.Start();
                    // Read stderr synchronously (on another thread)

                    string errorText = null;
                    var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                    stderrThread.Start();

                    // Read stdout synchronously (on this thread)

                    while (true)
                    {
                        var line = process.StandardOutput.ReadLine();
                        if (line == null)
                            break;

                        Console.WriteLine(line);
                    }

                    process.WaitForExit();
                    stderrThread.Join();
                    exitValue = process.ExitCode;
                }

                File.Delete(temp);
                File.Delete(temp2);
                File.Delete(temp3);
                File.Delete(temp4);
                File.Delete(temp5);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Plugin(string video, int width, int height, string plugin)
        {
            Console.WriteLine(plugin+" initiated");
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = plugin;
                startInfo.Arguments = video + " " + width + " " + height + " " + toolBox.TEMP + " " + toolBox.FFMPEG + " " + toolBox.FFPROBE + " " + toolBox.MAGICK + " " + toolBox.RESOURCES + " " + toolBox.SOUNDS + " " + toolBox.SOURCES + " " + toolBox.MUSIC;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                string errorText = null;
                var stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
                stderrThread.Start();

                // Read stdout synchronously (on this thread)

                while (true)
                {
                    var line = process.StandardOutput.ReadLine();
                    if (line == null)
                        break;

                    Console.WriteLine(line);
                }

                process.WaitForExit();
                stderrThread.Join();

                int exitValue = process.ExitCode;
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public int randomInt(int min, int max)
        {
            return rnd.Next((max - min) + 1) + min;
        }
    }
}
