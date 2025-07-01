FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["HotelManagementSystem.sln", "."]

COPY ["HotelManagementSystem.WebAPI/HotelManagementSystem.WebAPI.csproj", "HotelManagementSystem.WebAPI/"]

COPY ["HotelManagementSystem.Core/HotelManagementSystem.Core.csproj", "HotelManagementSystem.Core/"]
COPY ["HotelManagementSystem.DL/HotelManagementSystem.DL.csproj", "HotelManagementSystem.DL/"]
COPY ["HotelManagementSystem.BL/HotelManagementSystem.BL.csproj", "HotelManagementSystem.BL/"]



RUN dotnet restore "HotelManagementSystem.sln"

COPY . .

WORKDIR "/src/HotelManagementSystem.WebAPI"
RUN dotnet publish "HotelManagementSystem.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelManagementSystem.WebAPI.dll"]