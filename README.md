<p align="center">
  <h1 align="center">YTP++</h1>
  <h2 align="center">Made with ❤ in C#.</h2>
  <p align="center">
    <a href="https://discord.gg/bzhzRmg"><img alt="YTP+ Hub" src="https://img.shields.io/discord/641428540486844417"></a>
<a href="https://github.com/DevanWolf/YTPPlusPlus/issues"><img alt="GitHub issues" src="https://img.shields.io/github/issues/DevanWolf/YTPPlusPlus"></a>
  <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/DevanWolf/YTPPlusPlus">
  <a href="https://github.com/DevanWolf/YTPPlusPlus/blob/master/LICENSE.txt"><img alt="GitHub license" src="https://img.shields.io/github/license/DevanWolf/YTPPlusPlus"></a>
  <a href="https://github.com/DevanWolf/YTPPlusPlus/releases"><img alt="Downloads" src="https://img.shields.io/github/downloads/DevanWolf/YTPPlusPlus/total"></a>
  <a href="https://github.com/DevanWolf/YTPPlusPlus/releases"><img alt="Latest release" src="https://img.shields.io/github/v/release/DevanWolf/YTPPlusPlus"></a>
  </p>
</p>

## YTP++ is an iteration of [YTP+](https://github.com/philosophofee/YTPPlus), written in C#.
### It includes new features and benefits over the previous iteration, such as:

  - Intro and outro, with toggles
  - Setting width/height for rendered videos
  - Includes FFMPEG, FFPROBE and frei0r
  - Error handling
  - New additional effects (Earrape, Pixelated Video, Sparta Remix, etc.)
  - [Plugin support](https://github.com/DevanWolf/YTPPlusPlus/wiki/Plugin-Creation)

## A warning call
For those of you using plugins, please be aware that YTP++ has no mind in what is a real plugin batch and what isn't. Any non-verified plugins have the potential to be malicious, be on the look out for the malicious use of plugins as there is a possibility that a plugin can be harmful. Remember to **NEVER** start YTP++ as an administrator.

## What is YTP+ and YTP++?
YTP+ is the name given to a set of tools to automatically generate YouTube Poop videos.
YTP++ is the name given to the second iteration of the software, however there was once a [YTP++ made in Electron and Node.JS](https://github.com/TeamPopplio/ytpplus-node-ui).

## How do I configure YTP++?
All configuration settings may be found in the application itself, either on the interface or in the Tools menu. [ImageMagick](https://imagemagick.org) is required for the Squidward effect but is otherwise not needed.
A 32-bit version of [VLC](http://videolan.org/vlc) is recommended to run this software, please install it beforehand for enhanced functionality.

## I can't run YTP++, it just blinks and dissapears!
Try installing the [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472) or newer runtime. Please note that YTP++ does not currently run on Windows Vista or below and has only been tested under Windows 10, thus requiring Windows 7 or later.

## I installed VLC, but it's still giving me an error?
Make sure it is the **32-bit** version of VLC as the 64-bit one will not work unless you select it manually.

## I installed ImageMagick, but Squidward is still disabled?
Try adding magick.exe to your system path. If you've checked the box needed in the installation prompt for ImageMagick, try restarting your computer. If all else fails, try setting magick.exe manually via the Tools menu.

## I got a great idea for the program, where to start?
Anyone is welcome to submit a pull request or suggest ideas via the [Discord server](https://discord.gg/bzhzRmg).

## OSS Credits
[FFmpeg](https://github.com/FFmpeg/FFmpeg), [LibVLC](https://github.com/videolan/vlc), and [Vlc.DotNet](https://github.com/ZeBobo5/Vlc.DotNet).
