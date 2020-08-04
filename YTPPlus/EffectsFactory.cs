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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -i " + toolBox.SOUNDS + randomSound + " -filter_complex \"[1:a]volume=1,apad[A];[0:a][A]amerge[out]\" -ac 2 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -map 0:v -map [out] -y " + video;
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
                string randomSound = pickSound();
                string soundLength = toolBox.getLength(toolBox.SOUNDS + randomSound);
                Console.WriteLine("Doing a mute now. " + randomSound + " length: " + soundLength + ".");
                FileInfo inVid = new FileInfo(video);

                string temp = toolBox.TEMP + "temp.mp4";
                string temp2 = toolBox.TEMP + "temp2.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                if (File.Exists(temp2))
                {
                    File.Delete(temp2);
                }
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -af \"volume=0\" -y " + toolBox.TEMP + "temp2.mp4";
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

                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp2.mp4 -i " + toolBox.SOUNDS + randomSound + " -to " + soundLength + " -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -filter_complex \"[1:a]volume=1,apad[A]; [0:a][A]amerge[out]\" -ac 2 -map 0:v -map [out] -y " + video;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                errorText = null;
                stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
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

                File.Delete(temp);
                File.Delete(temp2);
            }
            catch (Exception ex) { Console.WriteLine("effect" + "\n" + ex); }
        }

        public void effect_Reverse(string video, int width, int height)
        {
            Console.WriteLine("effect_Reverse initiated");
            try
            {
                string randomSound = pickSound();
                string soundLength = toolBox.getLength(toolBox.SOUNDS + randomSound);
                Console.WriteLine("Doing a mute now. " + randomSound + " length: " + soundLength + ".");
                FileInfo inVid = new FileInfo(video);

                string temp = toolBox.TEMP + "temp.mp4";
                string temp2 = toolBox.TEMP + "temp2.mp4";
                if (File.Exists(video))
                {
                    File.Delete(temp);
                    inVid.MoveTo(temp);
                }
                if (File.Exists(temp2))
                {
                    File.Delete(temp2);
                }
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -map 0 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -af \"areverse\" -y " + toolBox.TEMP + "temp2.mp4";
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

                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = toolBox.FFMPEG;
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp2.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -vf reverse -y " + video;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.Start();
                // Read stderr synchronously (on another thread)

                errorText = null;
                stderrThread = new Thread(() => { errorText = process.StandardOutput.ReadToEnd(); });
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

                File.Delete(temp);
                File.Delete(temp2);
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -filter:v setpts=0.5*PTS -filter:a atempo=2.0 -y " + video;
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -filter:v setpts=2*PTS -filter:a atempo=0.5 -y " + video;
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -af chorus=0.5:0.9:50|60|40:0.4|0.32|0.3:0.25|0.4|0.3:2|2.3|1.3 -y " + video;
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -af vibrato=f=7.0:d=0.5 -y " + video;
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -filter:v setpts=2*PTS -af asetrate=44100*0.5,aresample=44100 -y " + video;
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
                startInfo.Arguments = "-i " + toolBox.TEMP + "temp.mp4 -ar 44100 -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30 -filter:v setpts=0.5*PTS -af asetrate=44100*2,aresample=44100 -y " + video;
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
                string temp6 = toolBox.TEMP + "temp6.mp4"; //backwards & forwards concatenated

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

                commands.SetValue("-i " + toolBox.TEMP + "temp.mp4 -map 0"// -c:v copy"
                        + " -ar 44100"
                        + " -to 00:00:0" + randomTime2 + "." + randomTime
                        + " -vf scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1"
                        + " -an"
                        + " -y " + toolBox.TEMP + "temp2.mp4", 0);

                commands.SetValue("-i " + toolBox.TEMP + "temp2.mp4 -map 0"// -c:v copy"
                        + " -ar 44100"
                        + " -vf reverse,scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1"
                        + " -y " + toolBox.TEMP + "temp3.mp4", 1);

                commands.SetValue("-i " + toolBox.TEMP + "temp3.mp4"
                        + " -ar 44100"
                        + " -vf reverse,scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1"
                        + " -y " + toolBox.TEMP + "temp4.mp4", 2);

                commands.SetValue("-i " + toolBox.TEMP + "temp3.mp4"
                        + " -i " + toolBox.TEMP + "temp4.mp4"
                        + " -filter_complex \"[0:v:0][1:v:0][0:v:0][1:v:0][0:v:0][1:v:0][0:v:0][1:v:0]concat=n=8:v=1[outv]\""
                        + " -map \"[outv]\""
                        + " -c:v libx264 -shortest"
                        + " -y " + toolBox.TEMP + "temp5.mp4", 3);

                commands.SetValue("-i " + toolBox.TEMP + "temp5.mp4"
                        + " -map 0"
                        + " -ar 44100"
                        + " -vf \"setpts=0.5*PTS,scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1\""
                        + " -af \"atempo=2.0\""
                        + " -shortest"
                        + " -y " + toolBox.TEMP + "temp6.mp4", 4);

                commands.SetValue("-i " + toolBox.TEMP + "temp6.mp4"
                        + " -i " + toolBox.MUSIC + randomSound
                        + " -c:v libx264"
                        + " -map 0:v:0 -map 1:a:0"
                        + " -vf \"scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1,fps=fps=30\""
                        + " -shortest"
                        + " -y " + video, 5);

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
                File.Delete(temp7);
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
                    args.SetValue("-i " + video + " -vf \"select=gte(n\\,1)\" -vframes 1 -y " + toolBox.TEMP + "squidward0.png", 0);
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
                    args.SetValue("convert " + toolBox.TEMP + "squidward0.png -" + effect + " " + toolBox.TEMP + "squidward" + i + ".png", i);
                }
                commands.SetValue(toolBox.MAGICK, 6);
                args.SetValue("convert -size " + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + " canvas:black " + toolBox.TEMP + "black.png", 6);

                if (File.Exists(toolBox.TEMP + "concatsquidward.txt"))
                    File.Delete(toolBox.TEMP + "concatsquidward.txt");
                StreamWriter writer = new StreamWriter(toolBox.TEMP + "concatsquidward.txt");
                writer.Write("file '" + toolBox.TEMP + "squidward0.png'\nduration 0.467\nfile '" + toolBox.TEMP + "squidward1.png'\nduration 0.434\nfile '" + toolBox.TEMP + "squidward2.png'\nduration 0.4\nfile '" + toolBox.TEMP + "black.png'\nduration 0.834\nfile '" + toolBox.TEMP + "squidward3.png'\nduration 0.467\nfile '" + toolBox.TEMP + "squidward4.png'\nduration 0.4\nfile '" + toolBox.TEMP + "squidward5.png'\nduration 0.467");
                writer.Close();
                string randomSound = pickSquidwardMusic();
                commands.SetValue(toolBox.FFMPEG, 7);
                args.SetValue("-f concat -safe 0 -i " + toolBox.TEMP + "concatsquidward.txt -i " + toolBox.RESOURCES + "squidward/" + randomSound + " -map 0:v:0 -map 1:a:0 -vf \"scale=" + width.ToString("0.#########################", new CultureInfo("en-US")) + "x" + height.ToString("0.#########################", new CultureInfo("en-US")) + ",setsar=1:1\" -pix_fmt yuv420p -y " + video, 7);

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
                startInfo.Arguments = video + " " + width + " " + height + " " + toolBox.TEMP + " " + toolBox.FFMPEG + " " + toolBox.FFPROBE + " " + toolBox.MAGICK + " " + toolBox.RESOURCES + " " + toolBox.SOUNDS + " " + toolBox.SOURCES + " " + toolBox.MUSIC + " " + startOfClip.ToString("0.#########################", new CultureInfo("en-US")) + " " + endOfClip.ToString("0.#########################", new CultureInfo("en-US"));
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
