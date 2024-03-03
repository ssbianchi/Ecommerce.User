cls
c:
 
REM Setting default pathes
SET NUGETPKG_PATH=C:\Users\Sergio\.nuget\packages\google.protobuf.tools\3.25.3
SET DTO_PATH="C:\Backup\POS Arquitetura de Software\01_Trabalho\Ecommerce.User\Ecommerce.User.Application\User"
SET FILE_NAME=UserListProto.proto
 
REM Switching to the 'protoc' directory
cd %NUGETPKG_PATH%\tools\windows_x64
 
REM Running the protoc
REM proto_path is the root path to find the further libraries
protoc --proto_path=%NUGETPKG_PATH%\tools -I=%DTO_PATH%\Proto --csharp_out=%DTO_PATH%\Dto %DTO_PATH%\Proto\%FILE_NAME%
 
pause