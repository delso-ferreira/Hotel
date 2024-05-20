FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /backend

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /backend
COPY --from=build /backend/out .

ENTRYPOINT [ "dotnet", "TrybeHotel.dll" ]