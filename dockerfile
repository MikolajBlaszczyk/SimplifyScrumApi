# Use the .NET SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file and the project files (excluding test projects)
COPY *.sln ./
COPY BacklogModule/*.csproj ./BacklogModule/
COPY BacklogModule.Tests/*.csproj ./BacklogModule.Tests/
COPY DashboardModule/*.csproj ./DashboardModule/
COPY DashboardModule.Tests/*.csproj ./DashboardModule.Tests/
COPY DataAccess/*.csproj ./DataAccess/
COPY DataAccess.Tests/*.csproj ./DataAccess.Tests/
COPY NotificationService/*.csproj ./NotificationService/
COPY OpenConnectionManagement/*.csproj ./OpenConnectionManagement/
COPY OpenConnectionManagement.Tests/*.csproj ./OpenConnectionManagement.Tests/
COPY SchedulingModule/*.csproj ./SchedulingModule/
COPY SchedulingModule.Tests/*.csproj ./SchedulingModule.Tests/
COPY SimplifyFramework/*.csproj ./SimplifyFramework/
COPY SimplifyScrumApi/*.csproj ./SimplifyScrumApi/
COPY SimplifyScrumApi.Tests/*.csproj ./SimplifyScrumApi.Tests/
COPY SimplfiyTestFramework/*.csproj ./SimplfiyTestFramework/
COPY UserModule/*.csproj ./UserModule/
COPY UserModule.Tests/*.csproj ./UserModule.Tests/

# Restore the dependencies
RUN dotnet restore

# Copy everything else and build (excluding test projects)
COPY . ./
RUN ls -R /app
RUN cat BacklogModule/BacklogModule.csproj
RUN cat DashboardModule/DashboardModule.csproj
RUN cat DataAccess/DataAccess.csproj
RUN cat NotificationService/NotificationService.csproj
RUN cat OpenConnectionManagement/OpenConnectionManagement.csproj
RUN cat SchedulingModule/SchedulingModule.csproj
RUN cat SimplifyFramework/SimplifyFramework.csproj
RUN cat SimplifyScrumApi/SimplifyScrumApi.csproj
RUN cat UserModule/UserModule.csproj
RUN dotnet publish ./SimplifyScrumApi/SimplifyScrumApi.csproj -c Release -o out  --verbosity diagnostic

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SimplifyScrumApi.dll"]