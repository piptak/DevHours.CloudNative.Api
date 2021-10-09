FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY src/DevHours.CloudNative.Api/DevHours.CloudNative.Api.csproj src/DevHours.CloudNative.Api/DevHours.CloudNative.Api.csproj
COPY src/DevHours.CloudNative.Application/DevHours.CloudNative.Application.csproj src/DevHours.CloudNative.Application/DevHours.CloudNative.Application.csproj
COPY src/DevHours.CloudNative.Core/DevHours.CloudNative.Core.csproj src/DevHours.CloudNative.Core/DevHours.CloudNative.Core.csproj
COPY src/DevHours.CloudNative.Infra/DevHours.CloudNative.Infra.csproj src/DevHours.CloudNative.Infra/DevHours.CloudNative.Infra.csproj
COPY src/DevHours.CloudNative.Shared.Abstraction/DevHours.CloudNative.Shared.Abstraction.csproj src/DevHours.CloudNative.Shared.Abstraction/DevHours.CloudNative.Shared.Abstraction.csproj
RUN dotnet restore src/DevHours.CloudNative.Api/DevHours.CloudNative.Api.csproj

COPY . .
RUN dotnet publish -c release -o /app --no-restore src/DevHours.CloudNative.Api/DevHours.CloudNative.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DevHours.CloudNative.Api.dll"]