# LogMonitoringService

## Description

The LogMonitoringService is a RESTful API built with ASP.NET Core that allows users to access logs from multiple servers without logging into each server. Users can retrieve logs from secondary servers via the GET Primary Log endpoint running on primary server.

Key Components includes: 
### Primary Server: Service that runs on the Primary Servers and calls the Secondary Server to fetch the logs.
- Program.cs: Sets up the web host and registers necessary services including controllers, Swagger, and configuration.
- Controllers/PrimaryLogController.cs: Handles requests to fetch logs from secondary servers.

### Secondary Server: Service that runs on the Secondary Servers
- Program.cs: Sets up the web host and registers necessary services including controllers and configuration.
- Controllers/LogController.cs: Provides an endpoint to serve log data from log files.

It supports operations such as:
- Specify filename within the var/log
- Ability to present the newest logs first
- Specify the n number of logs to be retrived
- Ability to filter the logs using the text
- Includes Swagger UI for testing and API documentation

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Github
## Installation

### Step 1: Install .NET Core SDK

1. **Download and Install .NET Core SDK**:
   - Go to the [.NET Core download page](https://dotnet.microsoft.com/download/dotnet/8.0).
   - Download the installer for your operating system.
   - Run the installer and follow the instructions to install .NET Core SDK.

2. **Verify Installation**:
   - Open a terminal or command prompt.
   - Run the following command to verify the installation:
     ```bash
     dotnet --version
     ```

### Step 2: Clone the Repository from GitHub 

1. Open a terminal or command prompt.
2. Navigate to the directory where you want to clone the repository.
3. Clone the repository using `git`:
   ```bash
   git clone <repository-url> 

Note - You can also Download the Zip file and unzip in a folder

### Step 3: Install Required NuGet Packages

#### Primary Server service 

1. Navigate to the PrimaryServer directory:
```bash 
    cd PrimaryServer
```
2. Restore NuGet packages:
```bash
dotnet restore
```

#### Secondary Server
1. Navigate to the SecondaryServer directory:

```bash
cd ../SecondaryServer
```

2. Restore NuGet packages:

```bash
dotnet restore
```

Note: Steps 4 and 5 run the services locally for testing on the same machine using batch/shell files

### Step 4: Run the Secondary Servers

Navigate to the SecondaryServer directory:

``` bash
cd <repository-directory>/SecondaryServer
```

Run the following command to start the service Secondary servers 

If using Windows platform:
``` bash 
run.bat
``` 

If using Unix platform:
```bash 
run.sh
```

This will start the secondary service on localhost:5003

### Step 5: Run the Primary Servers

Navigate to the PrimaryServer directory:

``` bash
cd <repository-directory>/PrimaryServer
```

Run the following command to start the service Secondary servers 

If using Windows platform:
``` bash 
run_with_swagger.bat
``` 

If using Unix platform:
```bash 
run_with_swagger.sh
```

This will run the PrimaryLog service on localhost:5000 (Primary SErver) and open the Swagger UI on the default browser. Enter the details such as secondary server ip  (localhost:5003), full path of the filename and keyword and execute the API to read the logs. 






