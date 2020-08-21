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
        public static double GetUnixEpoch(DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return unixTime.TotalSeconds;
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
        public void effect_RandomSound(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -i \"" + toolBox.SOUNDS + randomSound + "\" -filter_complex \"[1:a]volume=1,apad[A];[0:a][A]amerge[out]\" -ar 44100 -map 0:v -map [out]" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_RandomSoundMute(string video, int width, int height)
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
                
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -i \"" + toolBox.SOUNDS + randomSound + "\" -filter_complex \"[1:0]apad\" -shortest" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Reverse(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf reverse -af areverse" + toolBox.ACCEL + " -y \"" + video + "\"";
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
        
        public void effect_SpeedUp(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=0.5*PTS -filter:a atempo=2.0" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SlowDown(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=2*PTS -filter:a atempo=0.5" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Chorus(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af chorus=0.5:0.9:50|60|40:0.4|0.32|0.3:0.25|0.4|0.3:2|2.3|1.3" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Vibrato(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af vibrato=f=7.0:d=0.5" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Tremolo(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af tremolo=f=10.0:d=0.7" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Earrape(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af acrusher=.1:1:64:0:log" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SpeedUpHighPitch(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=0.5*PTS -af asetrate=88200,aresample=44100" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SlowDownLowPitch(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf setpts=2*PTS -af asetrate=22050,aresample=44100" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_HighPitch(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af asetrate=88200,aresample=44100,atempo=0.5" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_LowPitch(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -af asetrate=22050,aresample=44100,atempo=2" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_ForwardReverse(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -filter_complex \"[0:v]reverse[vid];[0:a]areverse[aud];[0:v][0:a][vid][aud]concat=n=2:v=1:a=1[outv][outa]\" -map [outv] -map [outa]" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_ReverseForward(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -filter_complex \"[0:v]reverse[vid];[0:a]areverse[aud];[vid][aud][0:v][0:a]concat=n=2:v=1:a=1[outv][outa]\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf scale=iw/32:ih/32 -sws_flags neighbor -s " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + toolBox.ACCEL + " -y \"" + video + "\"";
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf scale=iw/30:ih/30 -sws_flags neighbor -s " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + " -af aresample=7350,aresample=44100" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_Emboss(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf convolution=\"-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2:-2 -1 0 -1 1 1 0 1 2\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SymmetryHorizontal1(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf \"transpose=1,split [main][tmp];[tmp] crop=iw:ih/2:0:0,vflip [flip];[main][flip] overlay=0:H/2,transpose=2\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SymmetryHorizontal2(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf \"hflip,transpose=1,split [main][tmp];[tmp] crop=iw:ih/2:0:0,vflip [flip];[main][flip] overlay=0:H/2,transpose=2\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SymmetryVertical1(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf \"split[main][tmp];[tmp] crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_SymmetryVertical2(string video, int width, int height)
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
                startInfo.Arguments = "-i \"" + toolBox.TEMP + "temp.mp4\" -vf \"vflip,split[main][tmp];[tmp]crop=iw:ih/2:0:0,vflip[flip];[main][flip]overlay=0:H/2\"" + toolBox.ACCEL + " -y \"" + video + "\"";
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

        public void effect_GMajor(string video, int width, int height)
        {
            Console.WriteLine("effect_GMajor initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";   //og file
                string temp1 = toolBox.TEMP + "temp1.aac"; //key -12
                string temp2 = toolBox.TEMP + "temp2.aac"; //key -5
                string temp3 = toolBox.TEMP + "temp3.aac"; //key +4
                string temp4 = toolBox.TEMP + "temp4.aac"; //key +7
                string temp5 = toolBox.TEMP + "temp5.aac"; //key +12

                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                if (File.Exists(temp1))
                {
                    File.Delete(temp1);
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


                string[] commands = new string[6];

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -vn -acodec aac -af asetrate=22050,aresample=44100,atempo=2" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp1.aac\"", 0);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -vn -acodec aac -af asetrate=33037.671045130824,aresample=44100,atempo=1.3348398541700344" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp2.aac\"", 1);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -vn -acodec aac -af asetrate=55562.51830036391,aresample=44100,atempo=0.7937005259840998" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp3.aac\"", 2);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -vn -acodec aac -af asetrate=66075.34209026165,aresample=44100,atempo=0.6674199270850172" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp4.aac\"", 3);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -vn -acodec aac -af asetrate=88200,aresample=44100,atempo=0.5" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp5.aac\"", 4);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -i \"" + toolBox.TEMP + "temp1.aac\" -i \"" + toolBox.TEMP + "temp2.aac\" -i \"" + toolBox.TEMP + "temp3.aac\" -i \"" + toolBox.TEMP + "temp4.aac\" -i \"" + toolBox.TEMP + "temp5.aac\" -filter_complex \"[0:a][1:a][2:a][3:a][4:a][5:a]amix=inputs=6[a]\" -map 0:v -map [a] -vf negate" + toolBox.ACCEL + " -y \"" + video + "\"", 5);

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
                File.Delete(temp6);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Dance(string video, int width, int height)
        {
            Console.WriteLine("effect_Dance initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);
                string temp = toolBox.TEMP + "temp.mp4";   //og file
                string temp2 = toolBox.TEMP + "temp2.mp4"; //1st cut
                string temp3 = toolBox.TEMP + "temp3.mp4"; //backwards (silent)
                string temp4 = toolBox.TEMP + "temp4.mp4"; //forwards (silent)
                string temp5 = toolBox.TEMP + "temp5.mp4"; //backwards & forwards concatenated
                string temp6 = toolBox.TEMP + "temp6.mp4"; //backwards & forwards concatenated sped up

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
                if (File.Exists(temp6))
                {
                    File.Delete(temp6);
                }

                string randomSound = pickMusic();

                string[] commands = new string[6];
                int randomTime = randomInt(3, 9);
                int randomTime2 = randomInt(0, 1);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp.mp4\" -to 00:00:0" + randomTime2 + "." + randomTime + " -an" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp2.mp4\"", 0);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp2.mp4\" -vf reverse" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp3.mp4\"", 1);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp3.mp4\" -vf reverse" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp4.mp4\"", 2);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp3.mp4\" -i \"" + toolBox.TEMP + "temp4.mp4\" -filter_complex \"[0:v:0][1:v:0][0:v:0][1:v:0][0:v:0][1:v:0][0:v:0][1:v:0]concat=n=8:v=1[outv]\" -map [outv] -c:v libx264" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp5.mp4\"", 3);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp5.mp4\" -vf setpts=0.5*PTS" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "temp6.mp4\"", 4);

                commands.SetValue("-i \"" + toolBox.TEMP + "temp6.mp4\" -i \"" + toolBox.MUSIC + randomSound + "\" -ar 44100 -c:v libx264 -map 0:v:0 -map 1:a:0 -shortest" + toolBox.ACCEL + " -y \"" + video + "\"", 5);

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
                File.Delete(temp6);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Squidward(string video, int width, int height)
        {
            Console.WriteLine("effect_Squidward initiated");
            try
            {
                FileInfo inVid = new FileInfo(video);

                if (File.Exists(video))
                {
                    string[] commands = new string[8];
                    string[] args = new string[8];
                    commands.SetValue(toolBox.FFMPEG, 0);
                    args.SetValue("-i \"" + video + "\" -vf \"select=gte(n\\,1)\" -vframes 1" + toolBox.ACCEL + " -y \"" + toolBox.TEMP + "squidward0.png\"", 0);
                    File.Delete(video);
                }

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
                args.SetValue("-f concat -safe 0 -i \"" + toolBox.TEMP + "concatsquidward.txt\" -i \"" + toolBox.RESOURCES + "squidward/" + randomSound + "\" -map 0:v:0 -map 1:a:0 -pix_fmt yuv420p" + toolBox.ACCEL + " -y \"" + video + "\"", 7);

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

        public void effect_Plugin(string video, int width, int height, string plugin, double startOfClip, double endOfClip)
        {
            Console.WriteLine(plugin+" initiated");
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = plugin;
                startInfo.Arguments = video + " " + width + " " + height + " " + toolBox.TEMP + " " + toolBox.FFMPEG + " " + toolBox.FFPROBE + " " + toolBox.MAGICK + " " + toolBox.RESOURCES + " " + toolBox.SOUNDS + " " + toolBox.SOURCES + " " + toolBox.MUSIC + " " + toolBox.ACCEL + " " + startOfClip.ToString("0.#########################", new CultureInfo("en-US")) + " " + endOfClip.ToString("0.#########################", new CultureInfo("en-US"));
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
