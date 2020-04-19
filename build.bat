@echo off
REM
REM 
cls 
setlocal enableextensions enabledelayedexpansion
set path=%PATH%;


set VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe

if not exist "%VSWHERE%" (
	echo ""
	EXIT /B 0 
)

set pre=Microsoft.VisualStudio.Product.
set ids=%pre%Professional %pre%Enterprise %pre%Community %pre%BuildTools

for /f "usebackq tokens=1* delims=: " %%i in (`"%VSWHERE%" -latest -products %ids% -requires Microsoft.Component.MSBuild -version 15.9`) do (
if /i "%%i"=="installationPath" set InstallDir=%%j
)

if exist "%InstallDir%\MSBuild\15.0\Bin\MSBuild.exe" (
echo "%InstallDir%\MSBuild\15.0\Bin"
)



REM set VS2010_PATH="C:\Program Files (x86)\Microsoft Visual Studio 10.0"
REM if not exist %VS2010_PATH% set VS2010_PATH="C:\Program Files\Microsoft Visual Studio 10.0"
REM set VS2010_EXE=%VS2010_PATH%\Common7\IDE\devenv.exe
REM if not exist %VS2010_EXE% set VS2010_EXE=%VS2010_PATH%\Common7\IDE\VCExpress.exe
REM echo VS2010 build of Test, configuration "Release|Win32"
REM call %VS2010_PATH%\VC\bin\VCVARS32.BAT
REM %VS2010_EXE% test.sln %1 "Release|Win32" /useenv /out my.txt

REM Above is optional 
set VS2017_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community"
if not exist %VS2017_PATH% set VS2017_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools"

set MSBUILD=%VS2017_PATH%\MSBuild\15.0\Bin\MSBuild.exe

set CONFIG_DBG_RLS=Debug
if not "%1"=="" set CONFIG_DBG_RLS=%1
set CONFIG_32_64=x86
if not "%2"=="" set CONFIG_32_64=%2
set PROJECT_CONFIG="%CONFIG_DBG_RLS%|%CONFIG_32_64%"

echo Build Windows Wrapper with sln project, configuration %PROJECT_CONFIG%

rem call %VS2017_PATH%\VC\Auxiliary\Build\VCVARS32.BAT
echo Build Windows Wrapper winform.sln, configuration %PROJECT_CONFIG%
%MSBUILD% .\winform_gui_wrapper\winform\winform.sln /t:rebuild /p:UseEnv=true;Configuration=%CONFIG_DBG_RLS%;Platform=%CONFIG_32_64%
if %ERRORLEVEL% neq 0 (
        echo ***************** BUILD FAILED *******************    
        goto :build_Exit
 ) else (
    echo ************ Build Windows Wrapper winform.sln SUCCEEDED **************
	
)

echo Build Windows Wrapper Wpf_GUI_Wrapper.sln, configuration %PROJECT_CONFIG%
%MSBUILD% .\wpf_gui_wrapper\Wpf_GUI_Wrapper\Wpf_GUI_Wrapper.sln /t:rebuild /p:UseEnv=true;Configuration=%CONFIG_DBG_RLS%;Platform=%CONFIG_32_64%
if %ERRORLEVEL% neq 0 (
        echo ***************** BUILD FAILED *******************    
        goto :build_Exit
 ) else (
    echo ************ Build Windows Wrapper Wpf_GUI_Wrapper.sln SUCCEEDED **************
)



:build_Exit
endlocal