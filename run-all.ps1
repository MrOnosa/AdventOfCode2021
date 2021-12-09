echo "This wasn't as interesting as I thought it would be since part of the time captured is the dotnet run command itself which takes nearly a second"
$fullStopwatch =  [system.diagnostics.stopwatch]::StartNew()
for ($i = 1; $i -le 25; $i += 1)
{
    $stopwatchOne =  [system.diagnostics.stopwatch]::StartNew()
    cd "Day$i\Day$i-1"
    dotnet run | Out-Null
    $stopwatchOne.Stop()
    $r1 = $stopwatchOne.Elapsed.ToString('hh\:mm\:ss\.fffffff')
    echo "Day $i-1 - $r1"
    cd "..\..\Day$i\Day$i-2"
    $stopwatchTwo =  [system.diagnostics.stopwatch]::StartNew()
    dotnet run | Out-Null
    cd "..\..\"
    $stopwatchTwo.Stop()
    $r2 = $stopwatchTwo.Elapsed.ToString('hh\:mm\:ss\.fffffff')
    echo "Day $i-2 - $r2"
}
$fullStopwatch.Stop()
$r2 = $fullStopwatch.Elapsed.ToString('hh\:mm\:ss\.fffffff')
echo "All AoC time - $r2"