# Vulnerable Dependencies .NET Repository

This repository simulates a .NET 8 solution with multiple projects demonstrating vulnerable direct and transitive dependencies using Central Package Management (CPM). The solution contains three projects with intentionally outdated packages that trigger security warnings.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Initial Setup and Build
- Install .NET 8 SDK if not available: Check with `dotnet --version` (requires 8.0.119 or newer)
- Bootstrap the repository:
  - `dotnet restore` -- takes 10-15 seconds. Downloads packages and shows vulnerability warnings.
  - `dotnet build` -- takes 10-15 seconds. NEVER CANCEL. Set timeout to 60+ seconds minimum.
  - `dotnet test` -- takes 5-10 seconds. NEVER CANCEL. Set timeout to 60+ seconds minimum.

### Core Commands (All Validated)
- Build entire solution: `dotnet build`
- Run tests: `dotnet test`
- Run console application: `dotnet run --project src/VulnerableApp`
- Run web API: `dotnet run --project src/VulnerableWebApi` (starts on http://localhost:5000)
- Security scan: `dotnet list package --vulnerable` -- ALWAYS use this to verify vulnerabilities

### Timing Expectations
- **NEVER CANCEL**: All builds complete within 15 seconds, but set timeouts to 60+ seconds minimum
- Restore: 10-15 seconds (includes vulnerability detection)
- Build: 10-15 seconds 
- Test: 5-10 seconds
- Security scan: 2-3 seconds

## Validation Scenarios

### Essential Validation Steps
After making changes, ALWAYS run these validation steps:
1. `dotnet build` -- must succeed with vulnerability warnings (8 warnings expected)
2. `dotnet test` -- must pass (1 test expected)
3. `dotnet list package --vulnerable` -- must show vulnerable packages in all 3 main projects
4. `dotnet run --project src/VulnerableApp` -- must output "Hello, World!"

### Expected Vulnerability Output
The security scan MUST show these vulnerabilities:
- **VulnerableLibrary**: Newtonsoft.Json 12.0.1 (High severity)
- **VulnerableApp**: Newtonsoft.Json 12.0.1 + System.Net.Http 4.3.0 (both High severity)  
- **VulnerableWebApi**: System.Text.Json 6.0.0 (High severity)
- **VulnerableLibrary.Tests**: No vulnerabilities (expected)

## Repository Structure

### Key Projects
- **src/VulnerableLibrary**: Class library with Newtonsoft.Json + NLog dependencies
- **src/VulnerableApp**: Console application depending on VulnerableLibrary + System.Net.Http
- **src/VulnerableWebApi**: ASP.NET Core Web API with System.Text.Json + JWT authentication
- **tests/VulnerableLibrary.Tests**: xUnit test project (no vulnerable dependencies)

### Central Package Management
- **Directory.Packages.props**: Controls all package versions centrally
- All .csproj files reference packages WITHOUT version numbers
- Vulnerable packages are intentionally pinned to older versions

### Important Files
- `VulnerableDependencies.sln`: Main solution file containing all projects
- `Directory.Packages.props`: CPM configuration with vulnerable package versions
- `src/*/Program.cs`: Entry points for applications

## Common Tasks

### Adding New Vulnerable Packages
1. Add PackageVersion to `Directory.Packages.props` with older version
2. Add PackageReference (no version) to relevant .csproj file
3. Run `dotnet restore` to verify vulnerability warnings appear
4. Run `dotnet list package --vulnerable` to confirm detection

### Updating Package Versions
- NEVER update vulnerable packages to newer versions - this breaks the demonstration purpose
- Only update test-related packages in Directory.Packages.props if needed

### Troubleshooting Build Issues
- If restore fails: Check Directory.Packages.props syntax and ensure no version numbers in .csproj files
- If CPM errors occur: Verify all PackageReference items in .csproj files lack Version attributes
- If vulnerabilities don't appear: Ensure packages are using exact versions specified in Directory.Packages.props

## Development Notes
- This repository demonstrates vulnerable dependencies by design - do NOT fix the security warnings
- The solution builds successfully despite security warnings (warnings are expected, not errors)
- All projects target .NET 8.0 framework
- Web API runs on http://localhost:5000 when started locally
- Tests are minimal but functional to demonstrate complete CI/CD pipeline capability