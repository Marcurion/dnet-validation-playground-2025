# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln ./
COPY Src/ Src/
COPY Tests/ Tests/
COPY Directory.Build.props ./

# Restore dependencies
RUN for file in $(find Src -name '*.csproj'); do mkdir -p $(dirname $file) && mv $file $file.tmp && cp $file.tmp $file && rm $file.tmp; done
RUN dotnet restore

# Copy the rest of the application
COPY . .

# Build the WebAPI project
RUN dotnet publish Src/Presentation/Presentation.csproj -c Release -o /publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the built application from the previous stage
COPY --from=build /publish .

# Expose the port
EXPOSE 8080

# Define the entry point
ENTRYPOINT ["dotnet", "Presentation.dll"]
