# This Dockerfile uses a multi-stage build to first build the application using the .NET SDK image, 
# and then creates a smaller runtime image to run the application.

# The first stage is named 'build-env' and uses the .NET SDK image to build the application.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
# This is where the application code will be copied and built.
WORKDIR /app

# The 'COPY *.csproj ./' command copies all .csproj files from the current directory on the host machine to the current directory in the container.
COPY *.csproj ./
# Restore the project dependencies specified in the project file. 
RUN dotnet restore

# Copy the rest of the source code to the container
COPY . ./

# Build and publish a release version of the application to the 'out' directory in the container
RUN dotnet publish -c Release -o out

# The second stage of the build uses the ASP.NET runtime image, which is smaller than the SDK image and contains only the necessary components to run ASP.NET applications.
FROM mcr.microsoft.com/dotnet/aspnet:10.0
# Set the working directory in the container to /app
WORKDIR /app

# Copy the published output from the build stage to the runtime image 
COPY --from=build-env /app/out .
# Set the entry point for the container to run the application when the container starts
ENTRYPOINT ["dotnet", "CommandService.dll"]