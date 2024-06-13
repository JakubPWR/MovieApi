FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the source code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 5000
EXPOSE 5001

# Define the entry point for the application
ENTRYPOINT ["dotnet", "MovieApi.dll"]