# Dockerfile for building and running a .NET application
# This Dockerfile uses a multi-stage build to first build the application using the .NET SDK image, 
# and then creates a smaller runtime image to run the application.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
# Set the working directory in the container
WORKDIR /app

# Copy the project file to the container
COPY *.csproj ./
# Restore the project dependencies specified in the project file. 
# This step downloads and installs all the necessary packages required to build the application.
RUN dotnet restore

# Copy the rest of the source code to the container
COPY . ./

# Build and publish a release version of the application to the 'out' directory in the container
RUN dotnet publish -c Release -o out

# Now we create the runtime image to run the application using the smaller ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
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
