# Variables
$framework = "netcoreapp1.0"

# Absolute paths
$authServicePath = "$PSScriptRoot\..\src\Services\Cik.Services.Auth.AuthService"
$magazineServicePath = "$PSScriptRoot\..\src\Services\Cik.Services.Magazine.MagazineService"
$sampleServicePath = "$PSScriptRoot\..\src\Services\Cik.Services.Sample.SampleService"
$gatewayServicePath = "$PSScriptRoot\..\src\Services\Cik.Services.Gateway.API"

# Output service paths
$authServiceOutput = "..\..\..\deploy\publish\auth_service"
$magazineServiceOutput = "..\..\..\deploy\publish\magazine_service"
$sampleServiceOutput = "..\..\..\deploy\publish\sample_service"
$gatewayServiceOutput = "..\..\..\deploy\publish\gateway_service"

# Copy config folder
New-Item -Force -ItemType directory -Path $PSScriptRoot\..\deploy\publish\Config
Copy-Item -Path $PSScriptRoot\..\src\Config\*.* -Destination $PSScriptRoot\..\deploy\publish\Config

# Set location at auth service
sl $authServicePath
# Restore the nuget references
& "C:\Program Files\dotnet\dotnet.exe" restore
# Publish application with all of its dependencies and runtime for IIS to use
& "C:\Program Files\dotnet\dotnet.exe" publish --configuration release -o $authServiceOutput --framework $framework --runtime active 

# Set location at magazine service
sl $magazineServicePath
# Restore the nuget references
& "C:\Program Files\dotnet\dotnet.exe" restore
# Publish application with all of its dependencies and runtime for IIS to use
& "C:\Program Files\dotnet\dotnet.exe" publish --configuration release -o $magazineServiceOutput --framework $framework --runtime active

# Set location at sample service
sl $sampleServicePath
# Restore the nuget references
& "C:\Program Files\dotnet\dotnet.exe" restore
# Publish application with all of its dependencies and runtime for IIS to use
& "C:\Program Files\dotnet\dotnet.exe" publish --configuration release -o $sampleServiceOutput --framework $framework --runtime active

# Set location at gateway service
sl $gatewayServicePath
# Restore the nuget references
& "C:\Program Files\dotnet\dotnet.exe" restore
# Publish application with all of its dependencies and runtime for IIS to use
& "C:\Program Files\dotnet\dotnet.exe" publish --configuration release -o $gatewayServiceOutput --framework $framework --runtime active