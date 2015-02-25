# LogRotater
A very basic log rotate for IIS logs

## Getting setup
Please excuse my lack of git understanding, but the following commands are needed before you can build the project.

After cloning the project

```
$ cd LogRotater
$ git submodule init
$ git submodule update --recursive --merge
```

This gets the two submodules, but it doesn't get the submodule that CC.Common.Compression uses (weird).

```
$ cd LogRotater\Libs\CC.Common.Compression\CC.Common.Compression
$ git submodule init
$ git submodule update --recursive --merge
```

## First Build
Your first build *MUST* be Release since the application uses CC.Common.Compression.dll and CC.Common.JSON.dll as embeded resources from bin\Release.

This allows the application to be a single file executable.

Debug will then build successfully after that.

## Usage
Command line args are rather sparse. You only have the following:
  - --help or /? - shows you the command line arguments
  - --default-config - creates a LogRotater.default.json file with all the defaults set. You will need to rename it to LogRotator.json before it can be used. This is so the program doesn't stomp on an existing configuration file.
  - --config <filepath> - allows you to specify a different configuration file

By default LogRotater looks for a ./LogRotater.json file. If none is found it dumps out the usage.

You need to run with administrative rights. Since it modifys C:\inetpub\logs\LogFiles\

## Config

The default configuration looks like the following
```json
{
  "logDirectory":"C:\\inetpub\\logs\\LogFiles\\", 
  "fileMask":"*.log", 
  "olderThanDays":30, 
  "deleteFiles":false, 
  "deleteFileMask":"*.zip", 
  "deleteOlderThanDays":90, 
  "fastForward":false, 
  "compressionLevel":7
}
```

Yup it's JSON, easy to parse and you can use a linter to check syntax.

###Basic Settings
  - logDirectory - the directory to look for log files - this is recursive so you don't need to set multiple paths for each site on your server.
  - fileMask - the log files to look for. Just a single pattern for now. (I just call [Directory.GetFiles](https://msdn.microsoft.com/en-us/library/vstudio/ms143316.aspx) in .NET)
  - olderThanDays - the number of days the file needs to be before it's considered.
  - compressionLevel - the zip compression level. 0 - no compression to 9 max. In testing with IIS logs 7 is about the same size as a 9, but takes about 1.5 seconds less time. That's why it's the default.

###Additional Settings
Normal operation of a server can cause the server to crash if the root drive fills up. These options allow you to remove old log files.

  - deleteFiles - Deletes log files that were previously compressed (zip) - defaults to false
  - deleteFileMask - Files to delete the same rules apply as fileMask above
  - deleteOlderThanDays - The number of days the file needs to be before it's considered.

###Bonus setting
Noticing that some log files will be past the deleteOlderThanDays after being compressed the following option was added
  
  - fastForward - if true and deleteFiles is true it will delete files older than deleteOlderThanDays skipping compression first, hence the fastForward name.

After LogRotater compresses the log file it "touches" the file to give the zip file the same timestamp as the original log file.