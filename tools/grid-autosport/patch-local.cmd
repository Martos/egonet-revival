@echo off
setlocal

cd /d "%~dp0..\.."

set "GAME_PATH=%~1"
if "%GAME_PATH%"=="" set "GAME_PATH=C:\Program Files (x86)\Steam\steamapps\common\GRID Autosport"

echo EgoNet Revival - GRID Autosport local patch
echo Game path: %GAME_PATH%
echo.

dotnet run --project "%CD%\src\RaceNetShowdown.Server\RaceNetShowdown.Server.csproj" -- --regenerate-certs
if errorlevel 1 pause & exit /b %ERRORLEVEL%

certutil -addstore -f Root "%CD%\src\RaceNetShowdown.Server\certs\codemasters-local-root-ca.cer"
if errorlevel 1 (
  echo.
  echo Failed to install the local root certificate. Run this script as Administrator.
  pause
  exit /b %ERRORLEVEL%
)

dotnet run --project "%CD%\src\RaceNetShowdown.Patcher" -- patch --game grid-autosport "%GAME_PATH%"
if errorlevel 1 pause & exit /b %ERRORLEVEL%

echo.
echo Done. Start the GRID Autosport discovery server, then open GRID Autosport.
pause
endlocal
