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