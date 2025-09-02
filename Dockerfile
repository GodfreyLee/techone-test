# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /src

# Copy project files
COPY NumberToWordsApp.csproj .
COPY NumberToWordsApp.sln .

# Restore dependencies
RUN dotnet restore NumberToWordsApp.csproj

# Copy source code
COPY . .

# Build the application
RUN dotnet build NumberToWordsApp.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish NumberToWordsApp.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8.0 runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set working directory
WORKDIR /app

# Copy published application from build stage
COPY --from=build /app/publish .

# Create non-root user for security
RUN addgroup --system --gid 1001 appgroup && \
    adduser --system --uid 1001 --gid 1001 --no-create-home appuser

# Change ownership of app directory
RUN chown -R appuser:appgroup /app

# Switch to non-root user
USER appuser

# Expose port 5000
EXPOSE 5000

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:5000/ || exit 1

# Start the application
ENTRYPOINT ["dotnet", "NumberToWordsApp.dll"]