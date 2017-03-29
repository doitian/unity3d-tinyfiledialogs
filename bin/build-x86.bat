@echo off

if not exist build32 (
    md build32
    cd build32
    if "%1" == "" (
        cmake -G "Visual Studio 11 2012" ..
    ) else (
        cmake -G "%1" ..
    )
    cd ..
)

cmake --build build32 --config Release
copy build32\Release\tinyfiledialogs.dll Assets\tinyfiledialogs\Plugins\x86\