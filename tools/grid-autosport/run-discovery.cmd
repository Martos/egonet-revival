@echo off
setlocal

cd /d "%~dp0..\.."

echo EgoNet Revival - GRID Autosport local discovery
echo.
echo This starts the local server with request capture enabled.
echo Keep this window open, then open GRID Autosport and visit RaceNet and Global Challenge.
echo Logs will be written to:
echo   src\RaceNetShowdown.Server\logs\grid-2-discovery
echo.

set "ASPNETCORE_ENVIRONMENT=Grid2Discovery"
dotnet run --no-launch-profile --project "%CD%\src\RaceNetShowdown.Server\RaceNetShowdown.Server.csproj"

endlocal
