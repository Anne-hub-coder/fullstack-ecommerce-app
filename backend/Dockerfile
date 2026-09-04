
# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

COPY Ecommerce.Domain/Ecommerce.Domain.csproj Ecommerce.Domain/
COPY Ecommerce.Service/Ecommerce.Service.csproj Ecommerce.Service/
COPY Ecommerce.Presentation/Ecommerce.Presentation.csproj Ecommerce.Presentation/
COPY Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj Ecommerce.Infrastructure/

RUN dotnet restore Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj

COPY . .

RUN dotnet publish Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY --from=build /app/publish .

USER app

ENTRYPOINT ["dotnet", "Ecommerce.Infrastructure.dll"]

