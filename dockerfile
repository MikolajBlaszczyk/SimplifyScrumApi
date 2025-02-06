
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

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

RUN dotnet restore


COPY . ./
RUN dotnet publish ./SimplifyScrumApi/SimplifyScrumApi.csproj -c Release -o out --verbosity diagnostic

RUN apt-get update && apt-get install -y tzdata
ENV TZ=Europe/Warsaw
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone


FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SimplifyScrumApi.dll", "--server.urls", "https://+:8080"]