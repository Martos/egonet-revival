@echo off
setlocal

cd /d "%~dp0..\.."

rem Self-contained Linux build of the RaceNet server for GRID Autosport hosting.
rem The target machine does not need the .NET SDK or runtime.
rem Usage: build-package-linux.cmd [runtime]   (default: linux-x64; also linux-arm64, linux-musl-x64)

set "RUNTIME=%~1"
if "%RUNTIME%"=="" set "RUNTIME=linux-x64"

set "PROJECT=%CD%\src\RaceNetShowdown.Server\RaceNetShowdown.Server.csproj"
set "NAME=grid-autosport-server-%RUNTIME%"
set "OUT=%CD%\releases\%NAME%"
set "ARCHIVE=%CD%\releases\%NAME%.tar.gz"

echo EgoNet Revival - GRID Autosport server package (Linux)
echo Runtime: %RUNTIME%
echo Output:  %OUT%
echo.

if exist "%OUT%" rmdir /s /q "%OUT%"
if exist "%ARCHIVE%" del /q "%ARCHIVE%"

dotnet publish "%PROJECT%" -c Release -r %RUNTIME% --self-contained true -o "%OUT%" -p:DebugType=None -p:DebugSymbols=false
if errorlevel 1 (
  echo.
  echo Build failed.
  pause
  exit /b 1
)

rem Development settings are not needed on the target machine.
if exist "%OUT%\appsettings.Development.json" del /q "%OUT%\appsettings.Development.json"

rem Launcher (kept as a file so it keeps LF line endings).
copy /y "%~dp0linux\start-server.sh" "%OUT%\start-server.sh" >nul
if errorlevel 1 (
  echo.
  echo Could not copy start-server.sh.
  pause
  exit /b 1
)

"%SystemRoot%\System32\tar.exe" -czf "%ARCHIVE%" -C "%CD%\releases" "%NAME%"
if errorlevel 1 (
  echo.
  echo Archive creation failed. The build folder is still available: %OUT%
  pause
  exit /b 1
)

echo.
echo Done.
echo   Folder:  %OUT%
echo   Archive: %ARCHIVE%
echo.
echo On the Linux machine:
echo   tar -xzf %NAME%.tar.gz
echo   cd %NAME%
echo   sudo bash start-server.sh
echo.
echo Certificates are generated in certs/ on first start, the database in data/.
pause
endlocal
