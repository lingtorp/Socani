D:
cd \SteamLibrary\steamapps\common\Aseprite
:: dir /b /o:gn
@set ASEPRITE=Aseprite.exe
@set INPUT_NAME=level1
@set OUTPUT_NAME=%INPUT_NAME%
%ASEPRITE% -b --split-layers C:\Users\Alexander\repos\Socani\Assets\levels\%INPUT_NAME%\%INPUT_NAME%.aseprite --save-as %OUTPUT_NAME%-{layer}.png
timeout /t -1