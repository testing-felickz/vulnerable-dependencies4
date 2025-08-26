# Vulnerable Dependencies Demo

This repository demonstrates a .NET 9 solution with multiple projects that contain vulnerable direct and transitive dependencies. It uses Central Package Management (CPM) to manage these dependencies.

## Project Structure

- **VulnerableApi**: A .NET 9 Web API project with vulnerable dependencies
  - Uses vulnerable Newtonsoft.Json settings
  - Contains SSRF vulnerability through user-controlled HTTP requests
  - Uses vulnerable JWT authentication configuration

- **VulnerableLibrary**: A class library with vulnerable dependencies
  - Uses vulnerable regex patterns (potential ReDoS)
  - Contains outdated SharpZipLib reference
  - Uses vulnerable versions of System.Net.Http

- **VulnerableConsole**: A console application that uses both direct and transitive dependencies
  - Uses vulnerable log4net version
  - Demonstrates vulnerable JSON serialization
  - References the VulnerableLibrary with its transitive dependencies

## Vulnerability Examples

This repository contains examples of:

1. **Direct dependencies with known vulnerabilities**:
   - Newtonsoft.Json 12.0.3 (CVE-2020-0605)
   - System.Text.RegularExpressions 4.3.0 (CVE-2019-0820)
   - System.Net.Http 4.3.0 (CVE-2018-8292)

2. **Transitive dependencies with vulnerabilities**:
   - Dependencies coming through Microsoft.AspNet.WebApi.Client
   - Dependencies coming through Microsoft.Data.OData

3. **Vulnerable code patterns**:
   - TypeNameHandling.All in Newtonsoft.Json (CVE-2017-9785)
   - Regex patterns vulnerable to ReDoS
   - SSRF through unvalidated user input

## Central Package Management

The project uses Central Package Management (CPM) via the Directory.Packages.props file to define all package versions in a central location.

## Running the Sample

```powershell
dotnet restore
dotnet build
```

## Security Analysis

You can use various tools to detect the vulnerabilities in this sample:

- NuGet Package Vulnerability Detection
- Static Code Analysis tools
- Dependency scanning tools

## Note

This is a demonstration repository used to illustrate vulnerable dependencies. Do not use any of this code in a production environment.
