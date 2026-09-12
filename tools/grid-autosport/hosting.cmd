@echo off
setlocal

cd /d "%~dp0..\.."

set "ASPNETCORE_ENVIRONMENT=GridAutosportHost"
dotnet run --no-launch-profile --project "%CD%\src\RaceNetShowdown.Server\RaceNetShowdown.Server.csproj"

endlocal