# NoUWPAutoSuspend
An application to prevent Windows from automatically suspending UWP apps when they are minimized or cloaked when in fullscreen.

## Aim
**The obvious question is why prevent UWP apps from automatically suspending when not active?**<br>
In some cases, this be undesirable behavior if the target app needs to be running even when not active.<br>
[There is an API available for this which prevents UWP apps from being suspended by system.](https://learn.microsoft.com/en-us/windows/uwp/launch-resume/run-in-the-background-indefinetly)<br>
Execept one may not post these apps onto the store but may sideload them, by using this program or the very implementation this program uses, one may bypass that.<br>

# Usage
1. Download the latest release from GitHub Release.
2. Now obtain the Package Family Name for the desired UWP apps that are supposed to be **not suspended**.<br>
    Only include apps that might require this functionality like real time applications.<br>
    Use the following command in Windows PowerShell to locate the desired names:<br><br>
    ```ps
    (Get-AppxPackage).PackageFamilyName
    ```
3. Once you obtain the desired names, start it like this<br>
    **Note: Package Family Names are case sensitive!**<br><br>
    ```cmd
    NoUWPAutoSuspend.exe windows.immersivecontrolpanel_cw5n1h2txyewy
    ```

4. The application will sliently in the background.<br>Now simply minimize or make the target app(s) fullscreen and switch to a Win32/Desktop app to have the system cloak it.<br>
    The target app(s) will not be suspended by the system.

# Build
1. Download and install the .NET SDK and .NET Framework 4.8.1 Developer Pack from:<br>https://dotnet.microsoft.com/en-us/download/visual-studio-sdks
2. Run the following commands:

    ```cmd
    dotnet restore NoUWPAutoSuspend/NoUWPAutoSuspend.csproj
    dotnet build NoUWPAutoSuspend/NoUWPAutoSuspend.csproj --configuration Release
    ```