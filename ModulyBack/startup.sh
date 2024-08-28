@echo off
cd .\ModulyBack\
dotnet run --urls "http://0.0.0.0:44353/"
start http://localhost:44353/swagger/index.html
