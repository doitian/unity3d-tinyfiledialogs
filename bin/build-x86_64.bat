@echo off

if not exist build64 (
    md build64
    cd build64
    if "%1" == "" (
        cmake -G "Visual Studio 11 2012 Win64" ..
    ) else (
        cmake -G "%1" ..
    )
    cd ..
)

cmake --build build64 --config Release
copy build64\Release\tinyfiledialogs.dll Assets\tinyfiledialogs\Plugins\x86_64\