# Vulnerable Dependencies Sample

This repository demonstrates a .NET 8 solution with multiple projects that include various vulnerable direct and transitive dependencies, managed through Central Package Management (CPM).

## Purpose

This sample showcases:
- **Direct vulnerable dependencies**: Packages directly referenced in projects that have known security vulnerabilities
- **Transitive vulnerable dependencies**: Vulnerable packages that are pulled in as dependencies of other packages
- **Central Package Management**: Centralized package version management using `Directory.Packages.props`
- **Security scanning**: How .NET detects and reports vulnerable packages during build/restore

## Solution Structure

```
├── VulnerableDependencies.sln
├── Directory.Packages.props          # Central Package Management configuration
└── src/
    ├── VulnerableWebApi/             # ASP.NET Core Web API with vulnerable packages
    ├── VulnerableLibrary/            # Class library with vulnerable dependencies
    └── VulnerableConsole/            # Console application with vulnerable packages
```

## Vulnerable Dependencies Included

### Direct Vulnerable Dependencies

| Package | Version | Severity | CVE/Advisory | Project |
|---------|---------|----------|--------------|---------|
| `Newtonsoft.Json` | 10.0.1 | High | [GHSA-5crp-9r3c-p9vr](https://github.com/advisories/GHSA-5crp-9r3c-p9vr) | VulnerableWebApi |
| `NLog` | 4.4.0 | Various | Multiple vulnerabilities | VulnerableLibrary |
| `System.IdentityModel.Tokens.Jwt` | 5.1.0 | Moderate | [GHSA-59j7-ghrg-fj52](https://github.com/advisories/GHSA-59j7-ghrg-fj52) | VulnerableLibrary |
| `Microsoft.Data.SqlClient` | 1.0.19239.1 | High/Moderate | Multiple ([GHSA-8g2p-5pqh-5jmc](https://github.com/advisories/GHSA-8g2p-5pqh-5jmc), [GHSA-98g6-xh36-x2p7](https://github.com/advisories/GHSA-98g6-xh36-x2p7)) | VulnerableConsole |

### Packages with Vulnerable Transitive Dependencies

| Package | Version | Brings Vulnerable Dependencies | Project |
|---------|---------|-------------------------------|---------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 3.1.0 | Moderate vulnerability [GHSA-q7cg-43mg-qp69](https://github.com/advisories/GHSA-q7cg-43mg-qp69) | VulnerableWebApi |
| `Microsoft.Extensions.Logging.Console` | 3.1.0 | Various transitive vulnerabilities | VulnerableConsole |

## Central Package Management

This solution uses Central Package Management (CPM) configured in `Directory.Packages.props`:

- **Centralized version control**: All package versions are defined in one place
- **Consistent versioning**: Ensures all projects use the same package versions
- **Simplified maintenance**: Easy to update package versions across all projects

## Building and Running

### Prerequisites
- .NET 8 SDK or later

### Build the Solution
```bash
dotnet restore
dotnet build
```

### Run Individual Projects

**Console Application:**
```bash
dotnet run --project src/VulnerableConsole
```

**Web API:**
```bash
dotnet run --project src/VulnerableWebApi
```
Then navigate to `https://localhost:5001/swagger` to see the API documentation.

## Security Warnings

When you build or restore this solution, you'll see security warnings like:

```
warning NU1902: Package 'Microsoft.Data.SqlClient' 1.0.19239.1 has a known moderate severity vulnerability
warning NU1903: Package 'Newtonsoft.Json' 10.0.1 has a known high severity vulnerability
```

These warnings are **intentional** and demonstrate how .NET's built-in security scanning works.

## Educational Use Cases

This sample is useful for:

1. **Security Training**: Understanding how vulnerable dependencies affect applications
2. **Tool Testing**: Testing dependency scanning tools and security scanners
3. **DevOps Pipeline Testing**: Verifying that CI/CD pipelines properly detect and handle vulnerable dependencies
4. **Remediation Practice**: Learning how to identify and fix vulnerable dependencies

## Fixing Vulnerabilities

To fix the vulnerabilities in this sample:

1. Update package versions in `Directory.Packages.props` to latest stable versions
2. Remove or replace packages that don't have secure versions available
3. Use `dotnet list package --vulnerable` to identify vulnerable packages
4. Use `dotnet list package --outdated` to find packages that can be updated

## ⚠️ Warning

**This repository contains intentionally vulnerable dependencies and should not be used in production environments.** It is designed for educational and testing purposes only.

## Sample Code Features

- **VulnerableWebApi**: JWT authentication and JSON serialization using vulnerable packages
- **VulnerableLibrary**: Logging and JWT token processing with security issues
- **VulnerableConsole**: Database connectivity and logging with known vulnerabilities
