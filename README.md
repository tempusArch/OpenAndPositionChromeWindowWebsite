# OpenAndPositionChromeWindows
Open multiple chrome windows, websites and position, minimize or maximize them

## Installation

```bash
git clone https://github.com/tempusArch/OpenAndPositionChromeWindowWebsite.git
```

## Usage

My situation is using it to open some routine websites (like mails, twitter) after booting up

Adjust parameter values to what you want
```
openAndPositionChromeWindow(string[] urls, int dekasa, string windowName, int hidari, int ue, int migi, int shita)
```


`string[] urls`     <br>Adjust to website urls you want to open in order<br><br>
`int dekasa`        <br>1 == Normal, 2 == Minimized, 3 == Maximized <br>If you want to position window, set it to 1<br><br>
`string windowName` <br>
Use `Ctrl + U` or `F12` to find website's title tag <br>
Then combine tab title name with common suffix ` - Google Chrome` to have the `windowName`<br><br>
`int hidari, int ue, int migi, int shita` <br>Four sides of a window<br>Left, top, right, bottom<br>Unit: pixel
<br>Adjust these parameter values to position window to where you want according to your own screen resolution<br><br>
Use `dotnet run` to see the effect<br><br>
Use `dotnet publish -c Release -r win-x64 -p:PublishReadyToRun=true --self-contained true` to have `.exe` file when you are satisfied with the effect

## Tips
Since [hWnd](https://cplusplus.com/forum/windows/95608/) wouldn't be generated immediately after a new window appears, delay time is recommended to be set more than 3 seconds

```
await Task.Delay(5000); 
```

`windowName` is used to find the [hWnd](https://cplusplus.com/forum/windows/95608/) and it is composed with the first tab title name and a common suffix ` - Google Chrome` <br>Websites like youtube, twitter, discord would add the number of notifications (which keeps changing) into the tab title name <br>Hence, the first tab is **NOT** recommended to be SNS sort of websites<br>




## License

[MIT](https://choosealicense.com/licenses/mit/)
