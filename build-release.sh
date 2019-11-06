#!/usr/bin/env bash

dotnet restore
dotnet build

function publish {
    echo $1
    CASSIEPATH=$1
    RID=$1
    CASSIEPATH=release/$CASSIEPATH
    
    dotnet publish ./CassieConsole/CassieConsole.csproj -c Release -r $RID -o $CASSIEPATH  /p:PublishSingleFile=true
    mv ./$CASSIEPATH/CassieConsole ./$CASSIEPATH/casma  
    rm ./$CASSIEPATH/CassieConsole.pdb
};

publish "linux-x64"
publish "linux-musl-x64"
publish "rhel-x64"
publish "osx.10.14-x64"
publish "osx.10.13-x64"

# The outlier odd build for the *.exe file for Windows.
RID="win-x64"
CASSIEPATH=release/$RID
dotnet publish ./CassieConsole/CassieConsole.csproj -c Release -r  -o $CASSIEPATH  /p:PublishSingleFile=true
mv ./$CASSIEPATH/CassieConsole.exe ./$CASSIEPATH/casma.exe  
rm ./$CASSIEPATH/CassieConsole.pdb
