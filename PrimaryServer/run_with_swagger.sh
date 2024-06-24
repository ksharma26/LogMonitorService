#!/bin/bash

dotnet run --urls "http://localhost:5000" &
sleep 5  # Wait for 5 seconds to ensure the server starts
xdg-open http://localhost:5000/swagger  # For Linux
open http://localhost:5000/swagger  # For MacOS
