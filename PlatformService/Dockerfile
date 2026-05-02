# Dockerfile for building and running a .NET application
# This Dockerfile uses a multi-stage build to first build the application using the .NET SDK image, 
# and then creates a smaller runtime image to run the application.

# The first stage is named 'build-env' and uses the .NET SDK image to build the application.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
# Set the working directory in the container to /app
# This is where the application code will be copied and built.
WORKDIR /app

# Copy the project file to the container and restore the dependencies.
# The 'COPY *.csproj ./' command copies all .csproj files from the current directory on the host machine to the current directory in the container.
# This is necessary because the 'dotnet restore' command needs the project file to know which dependencies to restore.
# By copying only the project file first, we can take advantage of Docker's layer caching.
# If the project file hasn't changed, Docker will use the cached layer for the restore step, which can speed up subsequent builds.
# If we copied the entire source code before restoring, any change in the source code would invalidate the cache for the restore step, causing it to run every time, even if the dependencies haven't changed.
# This approach optimizes the build process by minimizing unnecessary work and leveraging Docker's caching mechanism effectively.
COPY *.csproj ./
# Restore the project dependencies specified in the project file. 
# This step downloads and installs all the necessary packages required to build the application.
RUN dotnet restore

# Copy the rest of the source code to the container
COPY . ./

# Build and publish a release version of the application to the 'out' directory in the container
# The 'dotnet publish' command compiles the application and prepares it for deployment by copying the necessary files to the specified output directory (in this case, 'out').
# The '-c Release' flag specifies that we want to build the application in Release configuration, which typically includes optimizations and excludes debug information, making it suitable for production deployment.
# The '-o out' flag specifies the output directory for the published application.
# By publishing the application to a separate directory, we can easily copy only the necessary files to the runtime image in the next stage, keeping the final image smaller and more efficient.
RUN dotnet publish -c Release -o out

# Now we create the runtime image to run the application using the smaller ASP.NET runtime image
# The second stage of the build uses the ASP.NET runtime image, which is smaller than the SDK image and contains only the necessary components to run ASP.NET applications.
FROM mcr.microsoft.com/dotnet/aspnet:10.0
# Set the working directory in the container to /app
WORKDIR /app

# Copy the published output from the build stage to the runtime image 
# from the 'out' directory in the build stage to the current directory in the runtime image
COPY --from=build-env /app/out .
# Set the entry point for the container to run the application when the container starts
# The entry point specifies the command to run the application, which is 'dotnet PlatformService.dll'
ENTRYPOINT ["dotnet", "PlatformService.dll"]

# The resulting Docker image will contain the built .NET application and the necessary runtime to execute it.

# To build the Docker image, you can run the following command in the terminal:
    # Make sure you are in the directory containing the Dockerfile and the .NET project files when you run this command.
    # The '-t' flag is used to tag the image with a name (in this case, 'meenumee/platformservice:latest'), 
    # which you can use to reference the image later when running a container.
    # The '.' at the end of the command specifies the build context, which is the current directory.
# docker build -t meenumee/platformservice:latest .

# To run the Docker container from the built image, you can use the following command:
    # docker run -d -p 8080:80 --name platformservice meenumee/platformservice:latest  
# This command runs the container in detached mode, maps port 8080 of the host to port 80 of the container, 
# and names the container "platformservice". You can access the application by navigating to http://localhost:8080 in your web browser.

    # docker push meenumee/platformservice:latest
# Then you can push the image to a container registry (like Docker Hub) using the following command:
# You can pull it on any machine with Docker installed and run it using the same 'docker run' command as mentioned above.
    
    # docker ps -a
# This command lists all the Docker containers on your system, including those that are currently running and those that have stopped. 
# The '-a' flag stands for 'all', which ensures that you see

    # docker logs <container_id>
# This command retrieves the logs from a specific Docker container. 
# You can replace <container_id> with the actual ID or name of the container you want

    # docker stop <container_id>
# This command stops a running Docker container.
