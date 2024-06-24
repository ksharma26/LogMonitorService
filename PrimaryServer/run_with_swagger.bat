@echo off
start dotnet run --urls "http://localhost:5000"
timeout /t 5  // Wait for 5 seconds to ensure the server starts
start http://localhost:5000/swagger
