#!/bin/sh
dotnet publish --self-contained --use-current-runtime -o debian/api-bitbucket/opt/api -a x64 --os linux -p:PublishSinglefile=true

find debian/ -name '.DS_Store' -exec rm {} \;

dpkg --build debian/api-bitbucket

