#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyLife.Blazor/Server/MyLife.Blazor.Server.csproj", "MyLife.Blazor/Server/"]
COPY ["MyLife.Blazor/Shared/MyLife.Blazor.Shared.csproj", "MyLife.Blazor/Shared/"]
COPY ["MyLife.Blazor/Client/MyLife.Blazor.Client.csproj", "MyLife.Blazor/Client/"]
RUN dotnet restore "MyLife.Blazor/Server/MyLife.Blazor.Server.csproj"
COPY . .
WORKDIR "/src/MyLife.Blazor/Server"
RUN dotnet build "MyLife.Blazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyLife.Blazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLife.Blazor.Server.dll"]