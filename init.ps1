for ($i = 10; $i -le 25; $i += 1)
{
    mkdir "Day$i"
    New-Item "Day$i\test-input.txt"
    New-Item "Day$i\puzzle-input.txt"
    mkdir "Day$i\Day$i-1"
    mkdir "Day$i\Day$i-2"
    cd "Day$i\Day$i-1"
    dotnet new console --framework net6.0
    dotnet new gitignore
    Remove-Item "Program.cs"
    Copy-Item "..\..\Template.cs" -Destination "Program.cs"
    dotnet build
    cd "..\..\Day$i\Day$i-2"
    dotnet new console --framework net6.0
    dotnet new gitignore
    Remove-Item "Program.cs"
    Copy-Item "..\..\Template.cs" -Destination "Program.cs"
    dotnet build
    cd "..\..\"
}