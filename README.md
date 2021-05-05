# NowPlaying
spotify current track to csgo chat very nice

There are WPF and CLI verisons for now

![logo](https://sun9-37.userapi.com/c206828/v206828168/126142/2QOY6DgGLtc.jpg)
![logo](https://sun9-31.userapi.com/gvhRYJeNLGwgAkdDtt4SxzB7yQpi0RxN4wl71A/X-iRumWHWwE.jpg)
![logo](https://sun9-20.userapi.com/impg/be4vbQOclPTLN9lkd0FbmBhewyBTWeaeu7WDiQ/hj9EHqnvKMw.jpg?size=497x109&quality=96&sign=dcdc7a481c51d0eb3f52e7ad28399827&type=album)
![logo](https://sun9-11.userapi.com/impg/ZLTktGxG3b9I8fFJHjwNOtn8VH_8QO55fWHdXA/G1mx2WUv3j8.jpg?size=478x49&quality=96&sign=cc1cebbf92ae2a9d087cca4885f3f1e9&type=album)

# How to use
1. Lauch the program via `NowPlaying.Wpf.exe` file.
2. You will be redirected to the Spotify login form in your default browser (WARNING! If you're already logged on in this browser you'll only see a flash, which is caused by NowPlaying oppening a tab, getting the necessary API access token and closing the tab that does the thing). Login to your Spotify account if not already (it is recommended to use "Remember me" option, which will allow you to skip this step entirely (see the "WARNING!")).
3. NowPlaying will determine the last Steam account you've logged in and will show it in left corner under the timeline (if you're going to use account that is different to the one displayed, you'll need to restart NowPlaying for it to update).
4. Click in the box named "Chat button" and press any key, that you will use later to manually send chat message.
5. Optionally, you can use autosend function, which automatically activates send button for you with every track change in Spotify via WinAPI SendInput (works only when you are in game and haven't opened any kind of menu or chat).
6. Launch CS:GO (It can be done on any stage).
7. On top of NowPlaying window you'll see a `bind "key" "exec audio.cfg"` line. Copy it to the clipboard and head back to CS:GO window.
8. Open console (~) in CS:GO.
9. Paste the string into the console.
10. Now depending on the method (manual or autosend) you'll either automatically send a chat message with track information on each change of it or do it yourself when in game. Aside from that, you can press the assigned button anytime to send the mentioned chat message.



[![CodeFactor](https://www.codefactor.io/repository/github/veselv2010/nowplaying/badge)](https://www.codefactor.io/repository/github/veselv2010/nowplaying)
