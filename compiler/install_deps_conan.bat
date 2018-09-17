conan install . --build=missing -if conan/debug -s build_type=Debug
conan install . --build=missing -if conan/release -s build_type=Release
pause
