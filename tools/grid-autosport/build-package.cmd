@echo off
setlocal

cd /d "%~dp0..\.."

rem Self-contained build of the RaceNet server for GRID Autosport hosting.
rem The target PC does not need the .NET SDK or runtime.
rem Usage: build-package.cmd [runtime]   (default runtime: win-x64)

set "RUNTIME=%~1"
if "%RUNTIME%"=="" set "RUNTIME=win-x64"

set "PROJECT=%CD%\src\RaceNetShowdown.Server\RaceNetShowdown.Server.csproj"
set "OUT=%CD%\releases\grid-autosport-server-%RUNTIME%"
set "ZIP=%CD%\releases\grid-autosport-server-%RUNTIME%.zip"

echo EgoNet Revival - GRID Autosport server package
echo Runtime: %RUNTIME%
echo Output:  %OUT%
echo.

if exist "%OUT%" rmdir /s /q "%OUT%"
if exist "%ZIP%" del /q "%ZIP%"

dotnet publish "%PROJECT%" -c Release -r %RUNTIME% --self-contained true -o "%OUT%" -p:DebugType=None -p:DebugSymbols=false
if errorlevel 1 (
  echo.
  echo Build failed.
  pause
  exit /b 1
)

rem Development settings are not needed on the target PC.
if exist "%OUT%\appsettings.Development.json" del /q "%OUT%\appsettings.Development.json"

rem Launcher for the target PC: runs from its own folder so data\, certs\ and logs\ stay next to the exe.
(
  echo @echo off
  echo setlocal
  echo cd /d "%%~dp0"
  echo set "ASPNETCORE_ENVIRONMENT=GridAutosportHost"
  echo RaceNetShowdown.Server.exe
  echo if errorlevel 1 pause
  echo endlocal
) > "%OUT%\start-server.cmd"

powershell -NoProfile -Command "Compress-Archive -Path '%OUT%\*' -DestinationPath '%ZIP%' -Force"
if errorlevel 1 (
  echo.
  echo Zip creation failed. The build folder is still available: %OUT%
  pause
  exit /b 1
)

echo.
echo Done.
echo   Folder: %OUT%
echo   Zip:    %ZIP%
echo.
echo On the other PC: extract the zip and run start-server.cmd.
echo Certificates are generated in certs\ on first start, the database in data\.
pause
endlocal
