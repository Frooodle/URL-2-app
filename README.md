# URL-App-Launcher
Middleman app to open any app from a URL link/bookmark etc

This app requires a JSON file appsettings.json to be along side it if you wish for debug or file key functionality

[Default appsettings.json example](https://github.com/Frooodle/URL-App-Launcher/blob/main/Defaultappsettings.json)

## App Usage
Place the app and the optional settings file in whichever location you wish this exe to be. 

Run the URL-App-Launcher.exe and type 'yes' to confirm location.

This will setup a registry edit to map ual:// calls to this exe at the location you have ran it.

## URL Usage
Use link in format UAL:/// to call this app, It accepts file paths with and without args. File path and file must be in quotes if args are used)

Triple /// must be used at all times, example reuqest is ual://file:///C:\\Windows\\System32\\notepad.exe note for directory mapping \\ or / are both possible as long as ual and file are both triple ///

ual://file:///PATH for direct file calls or if the file is setup in appsettings.json then ual://key:///KEY


## Examples
Open file .exe with direct path reference

U2A:///file:///C:\\Windows\\System32\\notepad.exe

------
Open file based on key defined in [appsettings.json](https://github.com/Frooodle/URL-App-Launcher/blob/main/Defaultappsettings.json)

U2A:///key:///notepad

------
Open file based on path and run with arguements (Note path to .exe is in double quotes)

U2A:///file:///"C:\\Windows\\System32\\notepad.exe" /A c:\Windows\System32\Drivers\etc\hosts
