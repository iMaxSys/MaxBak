//ef command
dotnet tool install --global dotnet-ef

dotnet tool update --global dotnet-ef

dotnet ef

dotnet ef migrations add init

dotnet ef database update

dotnet ef migrations remove

dotnet ef migrations list

dotnet ef migrations script
