for ($i = 8; $i -le 9; $i += 1)
{
    mkdir "Day$i"
    New-Item "Day$i\test-input.txt"
    New-Item "Day$i\puzzle-input.txt"
    mkdir "Day$i\Day$i-1"
    mkdir "Day$i\Day$i-2"
    cd "Day$i\Day$i-1"
    dotnet new console --framework net6.0
    dotnet new gitignore
    cd "..\..\Day$i\Day$i-2"
    dotnet new console --framework net6.0
    dotnet new gitignore
    cd "..\..\"
}