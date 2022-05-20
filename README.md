# URL-2-App
Middleman app to open any app from a URL link/bookmark etc

This app requires a JSON file appsettings.json to be along side it if you wish for debug when facing issues or functionality to open files based on keys (which is also useful for [security](https://github.com/Frooodle/URL-2-app#security-concerns) )

[appsettings.json example](https://github.com/Frooodle/URL-2-app/blob/main/appsettings.json)

## How to download

.exe app can be grabbed from [https://github.com/Frooodle/URL-2-app/releases/](https://github.com/Frooodle/URL-2-app/releases/)

## Examples of usecases

All the below can be called by browser bookmarks/shortcuts/links etc to open the referenced file/app 

Open file .exe with direct path reference

[u2a:///file:/C:\\Windows\\System32\\notepad.exe](u2a:///file://C:/Windows/System32/notepad.exe)

------
Open file based on key defined in [appsettings.json](https://github.com/Frooodle/URL-2-app/blob/main/appsettings.json)

[u2a:///key:/notepad](u2a:///key:/notepad)

------
Open file based on path and run with arguements (Note path to .exe is in double quotes)

u2a:///file:/"C:\\Windows\\System32\\notepad.exe" /A C:\Windows\System32\Drivers\etc\hosts

------
Open file with arguements based on key defined in [appsettings.json](https://github.com/Frooodle/URL-2-app/blob/main/appsettings.json)

[u2a:///key:/leagueOfLegends](u2a:///key:/leagueOfLegends)

------
Any of the above could be used within a custom created webpage as well.

Such as adding <a href="u2a:file:C:\\Windows\\System32\\notepad.exe">Notepad</a> To your websites html.



## App Usage
Place the app and the optional settings file in whichever location you wish this exe to be. 

Run the URL-2-App.exe and type 'yes' to confirm location.

This will setup a registry entry on your windows machine to map [u2a:///](u2a:///) calls directly to this exe at the location you have last ran it.

Then create URLs as shown in examples section.

## URL Usage
Once app has been ran once.

Use link in format [u2a:](u2a:) to call this app, It accepts file paths with and without args. File path and file must be in quotes if args are used)
The link must be in format U2A:[/]{0,3}(file|key):[/]{0,3}).+  (ignoring case)
This means triple /// must be used at all times when calling U2A at the start however for the calling of a file/key it can be done with or without brackets
* U2A:///file:
* U2A:///file:/
* U2A:///file://
* U2A:///file:///
* U2A:file:
* U2A:file:/
* U2A:file://
* U2A:file:///
example reuqest is [u2a:file:C:\\Windows\\System32\\notepad.exe](u2a:file:C:\\Windows\\System32\\notepad.exe) note for directory mapping \\ or / are both possible as long as ual part is ///

[u2a:///file:///PATH](u2a:///file:///PATH) for direct file calls or if the file is setup in appsettings.json then [u2a:///key:///KEY](u2a:///key:///KEY)

## How to run commands
Since this application supports passing command line arguements if you wish to run any commands/code you could pass this in as a arguement to whatever executes such code/commands

Here are a few examples

* Shutdown pc



* Execute python script file



* Execute python code without any file reference





## Security Concerns
This app allows you to execute any exe file with any arguments when you open a URL configured for it. If you are concerned this could be used by someone else to exploit your PC please create/edit you [appsettings.json](https://github.com/Frooodle/URL-2-app/blob/main/appsettings.json) and set isDirectFileAccessAllowed to false.

By doing this only apps predefined in appsettings.json can be opened and used.

Another security option is enablig 'ConfirmationBeforeExecuting' in appsettings.json. THis will mean a messagebox will appear askig for confirmation before running any application.


## My web browser is asking for permission everytime
Depeding on your browser type it will often ask for permission before linking to a external app.

For information on how to display this please follow this [link](iamalink.com)

## App isn't working
Please create an appsettings.json alongside your .exe by copying and/or editting [appsettings.json example](https://github.com/Frooodle/URL-2-app/blob/main/appsettings.json) then change debug to true. This should keep window open after call and display and issue you may be getting.
