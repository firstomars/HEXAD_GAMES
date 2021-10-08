@echo off

pause

REM : Default install directory for unity via UnityHub
REM : If building on school computers - this directory will need to be edited
SET UnityDir="C:\Program Files\Unity\Hub\Editor\2020.3.5f1\Editor\Unity.exe"



IF exist %UnityDir% (
	
	echo "Building Windows started"
	%UnityDir% -batchmode -logFile -quit -projectPath "%cd%" -executeMethod Builder.BuildWindows
	echo "Building Windows finished"
	
	echo "Building Web started"
	%UnityDir% -batchmode -logFile -quit -projectPath "%cd%" -executeMethod Builder.BuildWeb
	echo "Building Web finished"
	
	echo "Building Android started"
	%UnityDir% -batchmode -logFile -quit -projectPath "%cd%" -executeMethod Builder.BuildAndroid
	echo "Building Android finished"
	
	goto :endofscript
)

REM : Unity has been installed in a different location, or it does not exist
echo "Unity Not found, please update this build script"


:endofscript
pause